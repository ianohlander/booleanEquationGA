using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using geneticInformationSystem;
using geneticInformationSystem.models.lexing;

namespace geneticInformationSystem.modules {
    public class Lexer {
        private String source;
        public List<Token> tokens = new List<Token>();
        private GISystem gis;
        private int start = 0;
        private int current = 0;
        private int line = 1;

        public Lexer(String source, GISystem myGIS) {
            this.source = source;
            this.gis = myGIS;
        }

        public List<Token> scanTokens() {
            while (!isAtEnd()) {
                // We are at the beginning of the next lexeme.
                start = current;
                scanToken();
            }

            tokens.Add(new Token(TokenType.EOF, "", null, line));
            return tokens;
        }

        private bool isAtEnd() {
            return current >= source.Length;
        }

        public void scanToken() {
            char c = advance();
            switch (c) {
                case '(': addToken(TokenType.LEFT_PAREN); break;
                case ')': addToken(TokenType.RIGHT_PAREN); break;
                case '{': addToken(TokenType.LEFT_BRACE); break;
                case '}': addToken(TokenType.RIGHT_BRACE); break;
                case '+': addToken(TokenType.PLUS); break;
                case ';': addToken(TokenType.SEMICOLON); break;
                case '*': addToken(TokenType.STAR); break;
                case '!': addToken(TokenType.BANG); break;
                case '/':
                    if (match('/')) {
                        // A comment goes until the end of the line.
                        while (peek() != '\n' && !isAtEnd()) advance();
                    }
                    else {
                        addToken(TokenType.SLASH);
                    }
                    break;
                case ' ':
                case '\r':
                case '\t':
                    // Ignore whitespace.
                    break;

                case '\n':
                    line++;
                    break;
                default:
                    if (Char.IsLetter(c)) {
                        addToken(TokenType.LITERAL, c);
                    }
                    else {
                        gis.Error(line, "Unexpected character.");
                    }
                    break;
            }
        }

        private char advance() {
            return source[current++];
        }

        private bool match(char expected) {
            if (isAtEnd()) return false;
            if (source[current] != expected) return false;

            current++;
            return true;
        }

        private char peek() {
            if (isAtEnd()) return '\0';
            return source[current];
        }

        private void addToken(TokenType type) {
            addToken(type, null);
        }

        private void addToken(TokenType type, Object literal) {
            int length = current - (start);
            String text = source.Substring(start, length);
            tokens.Add(new Token(type, text, null, line));
        }
    }
}
