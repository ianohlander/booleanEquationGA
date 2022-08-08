using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;

using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

using geneticInformationSystem.models.lexing;
using geneticInformationSystem.models.parsing;
using geneticInformationSystem.modules;

using giSystemUtilities;

using treeDrawingLibrary;

namespace booleanTestInterface {
    public partial class gaTestInterface : Form {
        private BooleanPhenotype phenotype;
        generateTree mgt;
        

        public gaTestInterface() {
            InitializeComponent();
            mgt = new generateTree();
        }

        private void pfConvert_Click(object sender, EventArgs e) {
            if (infixTB.Text.Length > 0) {
                string errMessage = null;
                string pf = null;
                int numinputs;
                if (gisystem.infixToPostfix(infixTB.Text,out pf,out errMessage, out numinputs)) {
                    this.statusBar.Text = "";
                    postfixTB.Text = pf;
                    phenotype = new BooleanPhenotype();
                    phenotype.dna = pf;
                    phenotype.inputs = numinputs;
                }
                else {
                    this.statusBar.Text = "Error converting to postfix.";
                }
            }
        }

        private void lengthTB_Validating(object sender, CancelEventArgs e) {
            if (!Regex.IsMatch(lengthTB.Text, @"^\d+$") || string.IsNullOrEmpty(lengthTB.Text)) {
                e.Cancel = true;
                lengthTB.Focus();
                interfaceErrorProvider.SetError(lengthTB, "Must enter a number.");
            }
            else {
                e.Cancel = false;
                interfaceErrorProvider.SetError(lengthTB, "");
            }
        }

        private void thresholdTB_Validating(object sender, CancelEventArgs e) {
            if (!Regex.IsMatch(thresholdTB.Text, @"^\d+$") || string.IsNullOrEmpty(thresholdTB.Text)) {
                e.Cancel = true;
                thresholdTB.Focus();
                interfaceErrorProvider.SetError(thresholdTB, "Must enter a number.");
            }
            else {
                e.Cancel = false;
                interfaceErrorProvider.SetError(thresholdTB, "");
            }
        }

        private void numInputsTB_Validating(object sender, CancelEventArgs e) {
            if (!Regex.IsMatch(numInputsTB.Text, @"^\d+$") || string.IsNullOrEmpty(numInputsTB.Text)) {
                e.Cancel = true;
                numInputsTB.Focus();
                interfaceErrorProvider.SetError(numInputsTB, "Must enter a number.");
            }
            else {
                e.Cancel = false;
                interfaceErrorProvider.SetError(numInputsTB, "");
            }
        }

        private void generate_Click(object sender, EventArgs e) {
            if (ValidateChildren(ValidationConstraints.Enabled)) {
                //MessageBox.Show("all good!", "Demo App - Message!");
                int len = int.Parse(lengthTB.Text);
                double border = double.Parse(thresholdTB.Text);
                int numInputs = int.Parse(numInputsTB.Text);
                DNAGenerator DNAGen = new DNAGenerator(len, border, numInputs);

                phenotype = new BooleanPhenotype();
                phenotype.inputs = numInputs;
                phenotype.dna = DNAGen.generateDNA();

                generatedDNATB.Text = phenotype.dna;
            }
        }

        private void clearButton_Click(object sender, EventArgs e) {
            clear();
        }

        private void clear() {
            generatedDNATB.Text = "";
            postfixTB.Text = "";
            infixTB.Text = "";
            phenotype = null;
            lexTB.Text = null;
        }

        private void lexButton_Click(object sender, EventArgs e) {
            if (phenotype != null && !string.IsNullOrEmpty(phenotype.dna)) {
                Lexer lexer = new Lexer(phenotype.dna, gisystem);
                phenotype.dnatokens = lexer.scanTokens();
                StringBuilder tokenString = new StringBuilder();
                // For now, just print the tokens.
                foreach (Token token in phenotype.dnatokens) {
                    tokenString.AppendLine(token.toString());
                }
                lexTB.Text = tokenString.ToString();

            }
            else {
                statusBar.Text = "No DNA to Lex.";
            }
        }

        private void parseButton_Click(object sender, EventArgs e) {
            if (phenotype != null && phenotype.dnatokens!=null) {
                Parser parser = new Parser(phenotype.dnatokens, gisystem);
                phenotype.expressedASTs= parser.parse();
                //StringBuilder astString = new StringBuilder();

                //astString.AppendLine("Found Trees:");
                mgt=new generateTree();
                //int i = 0;
                string astname;
                mgt = new generateTree();
                astLB.Items.Clear();
                for (int i=0;i< phenotype.expressedASTs.Count;i++){
                    Expr exp = phenotype.expressedASTs[i];
                    astname="Tree " + i;
                    string astString = new ASTPrinter().print(exp);
                    astLB.Items.Add(astname + ": "+astString);
                    //astString.AppendLine(new ASTPrinter().print(exp));
                    //astString.AppendLine(ASTDisplay.print2D(exp, true));;
                    

                    //Bitmap treePic = mgt.generateTreeFromExpr(exp, "Tree " + i, treePB.Width, treePB.Height);
                    //treePB.Image = treePic;

                    //astTB.Text=ASTDisplay.print2D(exp, true);
                    //Debug.WriteLine(ASTDisplay.print2D(exp, true));
                }
            }
            else {
                statusBar.Text = "No DNA Tokens to Parse.";
            }
        }

        private void astLB_SelectedIndexChanged(object sender, EventArgs e) {
            int index = astLB.SelectedIndex;
            if (index != -1) {
                //Bitmap treePic = mgt.generateSampleTreeImage(treePB.Width,treePB.Height);
                Bitmap treePic = mgt.generateTreeFromExpr(phenotype.expressedASTs[index], "Tree " + index, treePB.Width, treePB.Height);
                treePB.Image = treePic;
            }
        }

        private void generateTreeButton_Click(object sender, EventArgs e) {
            int index = astLB.SelectedIndex;
            if (index != -1) {
                if (phenotype != null && phenotype.expressedASTs.Count > 0) {
                    Expr root = phenotype.expressedASTs[index];
                    treeNode<string> treeRoot = convertExprToTreeNode.convert(root, null);

                    TreeHelpers.
                }
            }
        }
    }
}
