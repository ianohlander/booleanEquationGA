using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

using geneticInformationSystem.models.lexing;
using geneticInformationSystem.models.parsing;
using geneticInformationSystem.modules;

namespace geneticInformationSystem {
    public class giSystem {
        private bool hadError = false;

        public static void Main(string[] args) {
            giSystem gs = new giSystem();
            if (args.Length > 2 || args.Length ==0){
                Console.WriteLine("giSystem: lexer [script]");
                Environment.Exit(0);
            }
            if (args.Length == 2) {
                if (args[0].Equals("lexer")) {
                    gs.lexFile(args[1]);
                }
            }
            if (args.Length == 1) {
                if (args[0].Equals("lexer")) {
                    gs.lexPrompt();
                }
            }
            
        }

        private void lexFile(String path){
            string data = null;
            try {
                data= System.IO.File.ReadAllText(path);
                lex(data);
                if (hadError) Environment.Exit(0);
            }
            catch(Exception ex) {
                Console.WriteLine("Error reading: " + path+" : "+ex.Message);
                Environment.Exit(0);
            }
        }

        private void lexPrompt(){
            int len;
            int numInputs;
            double border;
            DNAGenerator DNAGen;
            for (;;) { 
                Console.Write("> ");
                String line = Console.ReadLine();
                if (line == null) break;

                string[] t = line.Split(',');
                if (t.Length == 3) {
                    len =int.Parse(t[0]);
                    border = double.Parse(t[1]);
                    numInputs = int.Parse(t[2]);
                    DNAGen = new DNAGenerator(len, border, numInputs);
                    //convert from infix to postfix 
                    //string infix=infixToPostfix(line);
                    //Console.WriteLine("Infix to postfix: " + infix);

                    BooleanPhenotype phenotype = new BooleanPhenotype();
                    phenotype.inputs = numInputs;
                    phenotype.dna = DNAGen.generateDNA(); ;

                    //lex
                    phenotype.dnatokens= lex(phenotype.dna);

                    //parse
                    phenotype.expressedASTs = parse(phenotype.dnatokens);

                    //print
                    Console.WriteLine("Found Trees:");
                    int i = 1;
                    foreach (Expr exp in phenotype.expressedASTs) {
                        Console.WriteLine("Tree "+i++);
                        Console.WriteLine(new ASTPrinter().print(exp));
                        Console.WriteLine(ASTDisplay.print2D(exp, false));
                    }

                    //covert from postfix to infix
                    /*string postfix = postfixToInfix(infix);
                    Console.WriteLine("Postfix to Infix: " + postfix);*/
                    hadError = false;
                }
            }
        }

        public List<Token> lex(string source) {
            Console.WriteLine("In lexer with: "+source);
            Lexer lexer = new Lexer(source, this);
            List<Token> tokens = lexer.scanTokens();

            // For now, just print the tokens.
            foreach (Token token in tokens) {
                Console.WriteLine(token.toString());
            }
            return tokens;
        }

        public List<Expr> parse(List<Token> tokens) {
            Console.WriteLine("In parser");
            Parser parser = new Parser(tokens, this);
            List<Expr> exp = parser.parse();

            return exp;
        }

        public void error(int line, String message) {
            report(line, "", message);
        }

        private void report(int line, String where, String message) {
            Console.WriteLine(
                "[line " + line + "] Error" + where + ": " + message);
            hadError = true;
        }

        public void error(Token token, String message) {
            if (token.type == TokenType.EOF) {
                report(token.line, " at end", message);
            }
            else {
                report(token.line, " at '" + token.lexeme + "'", message);
            }
        }

        //boolean precedence calculation
        private int precedence(char op) {
            if (op == '!')
                return 3;
            else if (op == '*')
                return 2;
            else if (op == '+')
                return 1;
            else return -1;
        }

        public bool infixToPostfix(string expn, out string strResult, out string errMess, out int numinputs) {
            Stack<char> stk = new Stack<char>();
            string output = null;
            strResult = null;
            errMess = null;
            numinputs = 0;
            bool result = true;
            HashSet<char> inputs = new HashSet<char>();
            try {
                char _out;
                foreach (var ch in expn) {
                    bool isAlphaBet = Regex.IsMatch(ch.ToString(), "[A-Za-z]", RegexOptions.IgnoreCase);

                    if (isAlphaBet) {
                        output = output + ch;
                        inputs.Add(ch);
                    }
                    else {
                        switch (ch) {
                            case '+':
                            case '-':
                            case '*':
                            case '/':
                            case '%':
                            case '!':
                                while (stk.Count > 0 && precedence(ch) <= precedence(stk.Peek())) {
                                    _out = stk.Peek();
                                    stk.Pop();
                                    output = output + _out;
                                }
                                stk.Push(ch);
                                //output = output;
                                break;
                            case '(':
                                stk.Push(ch);
                                break;
                            case ')':
                                while (stk.Count > 0 && (_out = stk.Peek()) != '(') {
                                    stk.Pop();
                                    output = output + _out;
                                }
                                if (stk.Count > 0 && (_out = stk.Peek()) == '(')
                                    stk.Pop();
                                break;
                        }
                    }
                }
                while (stk.Count > 0) {
                    _out = stk.Peek();
                    stk.Pop();
                    output = output + _out;
                }
                strResult = output;
                numinputs = inputs.Count;
            }
            catch(Exception ex) {
                errMess = ex.Message;
                result = false;
            }
            return result;
        }

        private Boolean isOperand(char x) {
            return (x >= 'a' && x <= 'z') ||
                    (x >= 'A' && x <= 'Z');
        }

        public bool postfixToInfix(String exp, out string strResult, out string errMess) {
            Stack s = new Stack();
            strResult = null;
            errMess = null;
            bool result = true;
            try {
                for (int i = 0; i < exp.Length; i++) {
                    char c = exp[i];
                    // Push operands
                    if (isOperand(c)) {
                        s.Push(c.ToString());
                    }

                    // We assume that input is
                    // a valid postfix and expect
                    // an operator.
                    else {
                        if (c.Equals('*') || c.Equals('+')) {
                            String op1 = (String)s.Peek();
                            s.Pop();
                            String op2 = (String)s.Peek();
                            s.Pop();
                            s.Push("(" + op2 + c +
                                    op1 + ")");
                        }
                        else {
                            if (c.Equals('!')) {
                                String op1 = (String)s.Peek();
                                s.Pop();
                                s.Push(c +
                                        op1);

                            }
                        }
                    }
                }

                // There must be a single element
                // in stack now which is the required
                // infix.
                if (s.Count > 0) {
                    strResult = (String)s.Peek();
                }
            }
            catch (Exception ex) {
                errMess = ex.Message;
                result = false;
            }
            return result;
        }
    }
}
