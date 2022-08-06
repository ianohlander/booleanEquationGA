using System;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using booleanGA.elements;

namespace booleanGA {
    public partial class Form1 : Form {

        String[] operatorArray = { "*", "+", "~", "-", "/" };
        string expressionToAnalyze = "A B * ~";



        public Form1() {
            var y = !false;
            InitializeComponent();
            Debug.WriteLine("hello");
            //Debug.WriteLine(reversePolishEvaluate(expressionToAnalyze));
            string prepedExpression=preProcessExpression(expressionToAnalyze);
            int i = 0;
        }

        public string preProcessExpression(string expression) {

            string[] tokens = expression.Split(' ');

            for(int i=0; i< tokens.Length;i++) {
                if(Regex.IsMatch(tokens[i], @"^[a-zA-Z]+$")) {
                    tokens[i] = "{" + i + "}";
                }
            }
            StringBuilder newExpressionTemp = new StringBuilder();
            foreach(string s in tokens) {
                newExpressionTemp.Append(s + " ");
            }
            string newExpression = newExpressionTemp.ToString().Trim();
            return newExpression;

        }

        public string reversePolishEvaluate(string expression) {
            string result = "";
            var expr = expression.ToUpper().Split(" ");
            Stack evalStack = new Stack();

            if (expression.Length < 0) {
                return result;
            }
            for (int i = 0; i < expr.Length; i++) {
                if (!operatorArray.Contains(expr[i])) {
                    evalStack.Push(expr[i]);               
                }
                else {
                    var a = evalStack.Pop().ToString().ToUpper() == "TRUE" ? true : false;
                    var b = false;
                    if (expr[i] != "~") {
                        b = evalStack.Pop().ToString().ToUpper() == "TRUE" ? true : false;
                    }
                    if (expr[i].Equals("*")) {
                        evalStack.Push(a && b);
                    }
                    if (expr[i].Equals("~")) {
                        var  x = !a;
                        evalStack.Push(x);
                    }
                }
            }
            result = evalStack.Pop().ToString();
            return result;
        }
    }
}
