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
    public class GISystem {
        private bool hadError = false;

        public static void Main(string[] args) {
            GISystem gs = new GISystem();
            if (args.Length > 2 || args.Length ==0){
                Console.WriteLine("giSystem: lexer [script]");
                Environment.Exit(0);
            }
            if (args.Length == 2) {
                if (args[0].Equals("lexer")) {
                    gs.LexFile(args[1]);
                }
            }
            if (args.Length == 1) {
                if (args[0].Equals("lexer")) {
                    gs.LexPrompt();
                }
            }
            
        }

        private void LexFile(String path){
            string data;
            try {
                data= System.IO.File.ReadAllText(path);
                Lex(data);
                if (hadError) Environment.Exit(0);
            }
            catch(Exception ex) {
                Console.WriteLine("Error reading: " + path+" : "+ex.Message);
                Environment.Exit(0);
            }
        }

        private void LexPrompt(){
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
                    //string infix=InfixToPostfix(line);
                    //Console.WriteLine("Infix to postfix: " + infix);

                    // dotnet_style_object_initializer = false
                    BooleanPhenotype phenotype = new BooleanPhenotype {
                        Inputs = numInputs,
                        DNA = DNAGen.generateDNA()
                    };
                    //Lex
                    phenotype.DNATokens= Lex(phenotype.DNA);
                    //Parse
                    phenotype.ExpressedASTs = Parse(phenotype.DNATokens);

                    //Print
                    Console.WriteLine("Found Trees:");
                    int i = 1;
                    foreach (Expr exp in phenotype.ExpressedASTs) {
                        Console.WriteLine("Tree "+i++);
                        Console.WriteLine(new ASTPrinter().Print(exp));
                        Console.WriteLine(ASTDisplay.Print2D(exp, false));
                    }

                    //covert from postfix to infix
                    /*string postfix = PostfixToInfix(infix);
                    Console.WriteLine("Postfix to Infix: " + postfix);*/
                    hadError = false;
                }
            }
        }

        public List<Token> Lex(string source) {
            Console.WriteLine("In lexer with: "+source);
            Lexer lexer = new Lexer(source, this);
            List<Token> tokens = lexer.scanTokens();

            // For now, just Print the tokens.
            foreach (Token token in tokens) {
                Console.WriteLine(token.ToString());
            }
            return tokens;
        }

        public List<Expr> Parse(List<Token> tokens) {
            Console.WriteLine("In parser");
            Parser parser = new Parser(tokens, this);
            List<Expr> exp = parser.parse();

            return exp;
        }

        public void Error(int line, String message) {
            Report(line, "", message);
        }

        private void Report(int line, String where, String message) {
            Console.WriteLine(
                "[line " + line + "] Error" + where + ": " + message);
            hadError = true;
        }

        public void Error(Token token, String message) {
            if (token.type == TokenType.EOF) {
                Report(token.line, " at end", message);
            }
            else {
                Report(token.line, " at '" + token.lexeme + "'", message);
            }
        }

        //boolean Precedence calculation
        private int Precedence(char op) {
            if (op == '!')
                return 3;
            else if (op == '*')
                return 2;
            else if (op == '+')
                return 1;
            else return -1;
        }

        public bool InfixToPostfix(string expn, out string strResult, out string errMess, out int numinputs) {
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
                        output +=  ch;
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
                                while (stk.Count > 0 && Precedence(ch) <= Precedence(stk.Peek())) {
                                    _out = stk.Peek();
                                    stk.Pop();
                                    output += _out;
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
                                    output += _out;
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
                    output += _out;
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

        private Boolean IsOperand(char x) {
            return (x >= 'a' && x <= 'z') ||
                    (x >= 'A' && x <= 'Z');
        }

        public bool PostfixToInfix(String exp, out string strResult, out string errMess) {
            Stack s = new Stack();
            strResult = null;
            errMess = null;
            bool result = true;
            try {
                for (int i = 0; i < exp.Length; i++) {
                    char c = exp[i];
                    // Push operands
                    if (IsOperand(c)) {
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
