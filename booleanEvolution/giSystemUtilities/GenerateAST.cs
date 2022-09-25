using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using geneticInformationSystem.models.parsing;
using geneticInformationSystem.models.lexing;
using geneticInformationSystem.modules;

namespace giSystemUtilities {
    public class GenerateAST {
        public static void Main(String[] args) {
            if (args.Length != 1) {
                Console.WriteLine("Usage: generate_ast <output directory>");
                Environment.Exit(0);
            }

            /*List<string> grammerDefition = new List<string>{
                "Binary   : Expr left,Token op,Expr right",
                "Grouping : Expr expression",
                "Literal  : Object value",
                "Unary    : Token op,Expr right"};*/


            Expr expression = new Expr.Binary(
                                    new Expr.Unary(
                                        new Token(TokenType.BANG, "!", null, 1),
                                        new Expr.Literal(new Token(TokenType.LITERAL, "A","A",0))
                                    ),
                                    new Token(TokenType.STAR, "*", null, 1),
                                    new Expr.Grouping(
                                        new Expr.Literal(new Token(TokenType.LITERAL, "B", "B", 0))
                                    )
                                );

            Console.WriteLine(new ASTPrinter().Print(expression));
        }
        private static void DefineAst(String outputDir, String baseName, List<String> types) {
            String path = outputDir + "/" + baseName + ".cs";
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8)) {

                writer.WriteLine("using System;");
                writer.WriteLine("using System.Collections.Generic;");
                
                writer.WriteLine("using geneticInformationSystem.models.lexing;");
                writer.WriteLine();
                writer.WriteLine("namespace geneticInformationSystem.models.parsing{");
                writer.WriteLine();
                
                writer.WriteLine("    public abstract class " + baseName + " {");

                DefineVisitor(writer, baseName, types);

                foreach (String type in types) {
                    String className = type.Split(':')[0].Trim();
                    String fields = type.Split(':')[1].Trim();
                    DefineType(writer, baseName, className, fields);
                }

                // The base Accept() method.
                writer.WriteLine();
                writer.WriteLine("        public abstract T accept<T>(Visitor<T> visitor);");

                writer.WriteLine("        }");
                writer.WriteLine("    }");
            }
        }    

        private static void DefineType(StreamWriter writer, String baseName,
            String className, String fieldList) {
            writer.WriteLine("    public class " + className + " : " +
                baseName + " {");

            // Constructor.
            writer.WriteLine("        public " + className + "(" + fieldList + ") {");

            // Store parameters in fields.
            String[] fields = fieldList.Split(',');
            foreach (String field in fields) {
                String name = field.Split(' ')[1];
                writer.WriteLine("        this." + name + " = " + name + ";");
            }

            writer.WriteLine("        }");

            // IVisitor pattern.
            writer.WriteLine();
            writer.WriteLine("        public override T accept<T>(Visitor<T> visitor) {");
            writer.WriteLine("          return visitor.visit" + className + baseName + "(this);");
            writer.WriteLine("        }");

            // Fields.
            writer.WriteLine();
            foreach (String field in fields) {
                writer.WriteLine("        public " + field + ";");
            }

            writer.WriteLine("      }");
        }

        private static void DefineVisitor(StreamWriter writer, String baseName, List<String> types) {
            writer.WriteLine("    public interface Visitor<T> {");

            foreach (String type in types) {
                String typeName = type.Split(':')[0].Trim();
                writer.WriteLine("        T visit" + typeName + baseName + "(" +
                    typeName + " " + baseName.ToLower() + ");");
            }

            writer.WriteLine("      }");
        }
    }
}

