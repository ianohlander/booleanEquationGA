using System;
using System.Collections.Generic;
using geneticInformationSystem.models.lexing;

namespace geneticInformationSystem.models.parsing{

    public abstract class Expr {
    public interface Visitor<T> {
        T visitBinaryExpr(Binary expr);
        T visitGroupingExpr(Grouping expr);
        T visitLiteralExpr(Literal expr);
        T visitUnaryExpr(Unary expr);
      }
    public static class Binary : Expr {
        public Binary(Expr left,Token op,Expr right) {
        this.left = left;
        this.op = op;
        this.right = right;
        }

        public override T accept<T>(Visitor<T> visitor) {
          return visitor.visitBinaryExpr(this);
        }

        private Expr left;
        private Token op;
        private Expr right;
      }
    public static class Grouping : Expr {
        public Grouping(Expr expression) {
        this.expression = expression;
        }

        public override T accept<T>(Visitor<T> visitor) {
          return visitor.visitGroupingExpr(this);
        }

        private Expr expression;
      }
    public static class Literal : Expr {
        public Literal(Object value) {
        this.value = value;
        }

        public override T accept<T>(Visitor<T> visitor) {
          return visitor.visitLiteralExpr(this);
        }

        private Object value;
      }
    public static class Unary : Expr {
        public Unary(Token op,Expr right) {
        this.op = op;
        this.right = right;
        }

        public override T accept<T>(Visitor<T> visitor) {
          return visitor.visitUnaryExpr(this);
        }

        private Token op;
        private Expr right;
      }

        public abstract T accept<T>(Visitor<T> visitor);
        }
    }
