using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using geneticInformationSystem.models.lexing;
using geneticInformationSystem.models.parsing;

namespace geneticInformationSystem.modules {
    public class Parser {
        private class ParseError : Exception { }
        private  List<Token> tokens;
        private int current = 0;
        private giSystem gis;
        Stack<Expr> stack;
        List<Expr> resultTrees;
        private int expressionErrors;
        private int countLiteral = 0;

        public Parser(List<Token> tokens, giSystem myGIS) {
            this.tokens = tokens;
            this.gis = myGIS;
            stack = new Stack<Expr>();
            resultTrees = new List<Expr>();
            expressionErrors = 0;
        }
        public List<Expr> parse() {
            Expr right;
            Expr left;
            try {
                for(int i=0; i<tokens.Count;i++) {
                    switch (tokens[i].type) {
                        case TokenType.LITERAL:
                            countLiteral++;
                            Expr.Literal lit = new Expr.Literal(tokens[i]);
                            stack.Push(lit);
                            break;
                        case TokenType.BANG:
                            if (validUnary()) {
                                Expr child = stack.Pop();
                                Expr.Unary not = new Expr.Unary(tokens[i], child);
                                child.parent = not;
                                stack.Push(not);
                            }
                            else {
                                if (stack.Count > 0) {
                                    Expr discreteExpr = stack.Pop();
                                    resultTrees.Add(discreteExpr);
                                    stack.Clear();
                                }
                                expressionErrors++;
                            }
                            break;
                        case TokenType.PLUS:
                            if (validBinary()) { 
                                right = stack.Pop();
                                left = stack.Pop();
                                Expr.Binary or_op = new Expr.Binary(left, tokens[i], right);
                                right.parent = or_op;
                                left.parent = or_op;
                                stack.Push(or_op);
                            }
                            else {
                                if (stack.Count > 0) {
                                    Expr discreteExpr = stack.Pop();
                                    resultTrees.Add(discreteExpr);
                                    stack.Clear();
                                }
                                expressionErrors++;
                            }
                            break;
                        case TokenType.STAR:
                            if (validBinary()) {
                                right = stack.Pop();
                                left = stack.Pop();
                                Expr.Binary and_op = new Expr.Binary(left, tokens[i], right);
                                right.parent = and_op;
                                left.parent = and_op;
                                stack.Push(and_op);
                            }
                            else {
                                if (stack.Count > 0) {
                                    Expr discreteExpr = stack.Pop();
                                    resultTrees.Add(discreteExpr);
                                    stack.Clear();
                                }
                                expressionErrors++;
                            }
                            break;
                        case TokenType.EOF:
                            if (stack.Count > 0) {
                                resultTrees.Add(stack.Pop());
                            }
                            break;
                        default:
                            expressionErrors++;
                            break;
                    }
                }
                return resultTrees;
            }
            catch (ParseError error) {
                return null;
            }
        }

        private bool validBinary() {
            bool result = false;
            if (stack.Count > 1) {
                Expr first = stack.Pop();
                Expr second = stack.Peek();
                stack.Push(first);
                if (first.getLeaf()!=null && second.getLeaf() != null) {
                    result = true;
                }
            }
            return result;
        }

        private bool validUnary() {
            bool result = false;
            if (stack.Count > 0) {
                Expr first = stack.Peek();
                if (first.getLeaf() != null) {
                    result = true;
                }
            }
            return result;
        }

        private bool validLiteral() {
            bool result = false;
            if (stack.Count > 0) {
                Expr first = stack.Peek();
                if (first.getLeaf() != null) {
                    result = true;
                }
            }
            return result;
        }

        private Token advance() {
            if (!isAtEnd()) current++;
            return previous();
        }

        private bool check(TokenType type) {
            if (isAtEnd()) return false;
            return peek().type == type;
        }

        private bool match(params TokenType[] types) {
            foreach (TokenType type in types) {
                if (check(type)) {
                    advance();
                    return true;
                }
            }

            return false;
        }

        private bool isAtEnd() {
            return peek().type == TokenType.EOF;
        }

        private Token peek() {
            return tokens[current];
        }

        private Token previous() {
            return tokens[current - 1];
        }

        private ParseError error(Token token, String message) {
            gis.error(token, message);
            return new ParseError();
        }
    }
}
