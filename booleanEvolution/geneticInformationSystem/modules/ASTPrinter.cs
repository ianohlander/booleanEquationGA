using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using geneticInformationSystem.models.parsing;
namespace geneticInformationSystem.modules {
    public class ASTPrinter : Expr.Visitor<String> {
        public String print(Expr expr) {
            return expr.accept(this);
        }

        public  String visitBinaryExpr(Expr.Binary expr) {
            return parenthesize(expr.op.lexeme,
                                expr.left, expr.right);
        }

        public  String visitGroupingExpr(Expr.Grouping expr) {
            return parenthesize("group", expr.expression);
        }

        public  String visitLiteralExpr(Expr.Literal expr) {
            if (expr.value == null) return "nil";
            return expr.value.ToString();
        }

        public  String visitUnaryExpr(Expr.Unary expr) {
            return parenthesize(expr.op.lexeme, expr.right);
        }

        private String parenthesize(String name, params Expr[] exprs) {
            StringBuilder builder = new StringBuilder();

            builder.Append("(").Append(name);
            foreach (Expr expr in exprs) {
                builder.Append(" ");
                builder.Append(expr.accept(this));
            }
            builder.Append(")");

            return builder.ToString();
        }
    }
}
