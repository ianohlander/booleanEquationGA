using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using geneticInformationSystem.models.parsing;
namespace geneticInformationSystem.modules {
    public class ASTPrinter : Expr.IVisitor<String> {
        public String Print(Expr expr) {
            return expr.Accept(this);
        }

        public  String VisitBinaryExpr(Expr.Binary expr) {
            return Parenthesize(expr.op.lexeme,
                                expr.left, expr.right);
        }

        public  String VisitGroupingExpr(Expr.Grouping expr) {
            return Parenthesize("group", expr.expression);
        }

        public  String VisitLiteralExpr(Expr.Literal expr) {
            if (expr.value == null) return "nil";
            return expr.value.ToString();
        }

        public  String VisitUnaryExpr(Expr.Unary expr) {
            return Parenthesize(expr.op.lexeme, expr.right);
        }

        private String Parenthesize(String name, params Expr[] exprs) {
            StringBuilder builder = new StringBuilder();

            builder.Append("(").Append(name);
            foreach (Expr expr in exprs) {
                builder.Append(" ");
                builder.Append(expr.Accept(this));
            }
            builder.Append(")");

            return builder.ToString();
        }
    }
}
