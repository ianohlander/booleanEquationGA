using System;
using System.Collections.Generic;
using geneticInformationSystem.models.lexing;

namespace geneticInformationSystem.models.parsing {

    public abstract class Expr {
        public interface Visitor<T> {
            T visitBinaryExpr(Binary expr);
            T visitGroupingExpr(Grouping expr);
            T visitLiteralExpr(Literal expr);
            T visitUnaryExpr(Unary expr);
        }
        public  class Binary : Expr {
            public Binary(Expr left, Token op, Expr right) {
                this.left = left;
                this.op = op;
                this.right = right;
                this.value = op.lexeme;
            }

            public override T accept<T>(Visitor<T> visitor) {
                return visitor.visitBinaryExpr(this);
            }

            public override Object getLeaf() {
                Object obj = null;
                if(this.right.getLeaf()!=null && this.left.getLeaf() != null) {
                    obj = new Object();
                }
                return obj;
            }

        }
        public  class Grouping : Expr {
            public Grouping(Expr expression) {
                this.expression = expression;
            }

            public override T accept<T>(Visitor<T> visitor) {
                return visitor.visitGroupingExpr(this);
            }

            public Expr expression;
            public override Object getLeaf() {
                return this.expression.getLeaf();
            }
        }
        public class Literal : Expr {
            public Literal(Token token) {
                this.value = token.lexeme;
                this.op = token;
            }

            public override T accept<T>(Visitor<T> visitor) {
                return visitor.visitLiteralExpr(this);
            }

            public override Object getLeaf() {
                return this.value;
            }
        }
        public class Unary : Expr {
            public Unary(Token op, Expr right) {
                this.op = op;
                this.right = right;
                this.value = op.lexeme;
            }

            public override T accept<T>(Visitor<T> visitor) {
                return visitor.visitUnaryExpr(this);
            }

            public override Object getLeaf() {
                return this.right.getLeaf();
            }
        }

        public abstract Object getLeaf();
        public abstract T accept<T>(Visitor<T> visitor);
        public Expr parent;
        public Expr left;
        public Token op;
        public Expr right;
        public Object value;

        
        
    }
}
