using System;
using System.Collections.Generic;
using geneticInformationSystem.models.lexing;

namespace geneticInformationSystem.models.parsing {

    public abstract class Expr {
        public interface IVisitor<T> {
            T VisitBinaryExpr(Binary expr);
            T VisitGroupingExpr(Grouping expr);
            T VisitLiteralExpr(Literal expr);
            T VisitUnaryExpr(Unary expr);
        }
        public  class Binary : Expr {
            public Binary(Expr left, Token op, Expr right) {
                this.left = left;
                this.op = op;
                this.right = right;
                this.value = op.lexeme;
            }

            public override T Accept<T>(IVisitor<T> visitor) {
                return visitor.VisitBinaryExpr(this);
            }

            public override Object GetLeaf() {
                Object obj = null;
                if(this.right.GetLeaf()!=null && this.left.GetLeaf() != null) {
                    obj = new Object();
                }
                return obj;
            }

        }
        public  class Grouping : Expr {
            public Grouping(Expr expression) {
                this.expression = expression;
            }

            public override T Accept<T>(IVisitor<T> visitor) {
                return visitor.VisitGroupingExpr(this);
            }

            public Expr expression;
            public override Object GetLeaf() {
                return this.expression.GetLeaf();
            }
        }
        public class Literal : Expr {
            public Literal(Token token) {
                this.value = token.lexeme;
                this.op = token;
            }

            public override T Accept<T>(IVisitor<T> visitor) {
                return visitor.VisitLiteralExpr(this);
            }

            public override Object GetLeaf() {
                return this.value;
            }
        }
        public class Unary : Expr {
            public Unary(Token op, Expr right) {
                this.op = op;
                this.right = right;
                this.value = op.lexeme;
            }

            public override T Accept<T>(IVisitor<T> visitor) {
                return visitor.VisitUnaryExpr(this);
            }

            public override Object GetLeaf() {
                return this.right.GetLeaf();
            }
        }

        public abstract Object GetLeaf();
        public abstract T Accept<T>(IVisitor<T> visitor);
        public Expr parent;
        public Expr left;
        public Token op;
        public Expr right;
        public Object value;

        
        
    }
}
