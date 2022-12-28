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
        GenerateTree mgt;
        TreeGenerator treegen;


        public gaTestInterface() {
            InitializeComponent();
            mgt = new GenerateTree();
            treegen = new TreeGenerator();
        }

        private void pfConvert_Click(object sender, EventArgs e) {
            if (infixTB.Text.Length > 0) {
                string errMessage = null;
                string pf = null;
                int numinputs;
                if (gisystem.InfixToPostfix(infixTB.Text,out pf,out errMessage, out numinputs)) {
                    this.statusBar.Text = "";
                    postfixTB.Text = pf;
                    phenotype = new BooleanPhenotype();
                    phenotype.DNA = pf;
                    phenotype.Inputs = numinputs;
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
                phenotype.Inputs = numInputs;
                phenotype.DNA = DNAGen.generateDNA();

                generatedDNATB.Text = phenotype.DNA;
            }
        }

        private void clearButton_Click(object sender, EventArgs e) {
            clear();
        }

        private void clear() {
            generatedDNATB.Text = "";
            postfixTB.Text = "";
            infixTB.Text = "(!x * y) + (x * !y)";
            phenotype = null;
            lexTB.Text = null;
            astLB.Items.Clear();
            treePB.Image = null;
        }

        private void lexButton_Click(object sender, EventArgs e) {
            if (phenotype != null && !string.IsNullOrEmpty(phenotype.DNA)) {
                Lexer lexer = new Lexer(phenotype.DNA, gisystem);
                phenotype.DNATokens = lexer.scanTokens();
                StringBuilder tokenString = new StringBuilder();
                // Just Print the tokens.
                foreach (Token token in phenotype.DNATokens) {
                    tokenString.AppendLine(token.ToString());
                }
                lexTB.Text = tokenString.ToString();

            }
            else {
                statusBar.Text = "No DNA to Lex.";
            }
        }

        private void parseButton_Click(object sender, EventArgs e) {
            if (phenotype != null && phenotype.DNATokens!=null) {
                Parser parser = new Parser(phenotype.DNATokens, gisystem);
                phenotype.ExpressedASTs= parser.parse();
                mgt=new GenerateTree();
                string astname;
                mgt = new GenerateTree();
                astLB.Items.Clear();
                for (int i=0;i< phenotype.ExpressedASTs.Count;i++){
                    Expr exp = phenotype.ExpressedASTs[i];
                    astname="Tree " + i;
                    string astString = new ASTPrinter().Print(exp);
                    astLB.Items.Add(astname + ": "+astString);              
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
                Bitmap treePic = mgt.GenerateMsaglTreeFromExpr(phenotype.ExpressedASTs[index], "Tree " + index, treePB.Width, treePB.Height);
                treePB.Image = treePic;
            }
        }

        private void generateTreeButton_Click(object sender, EventArgs e) {
            int index = astLB.SelectedIndex;
            if (index != -1) {
                if (phenotype != null && phenotype.ExpressedASTs.Count > 0) {
                    Expr root = phenotype.ExpressedASTs[index];
                    TreeNode<string> treeRoot = convertExprToTreeNode.convert(root, null);

                    treegen.InitializeNodes(treeRoot);
                Q23WA4E5R345Y6U7KIJM
                        }
            }
        }
    }
}
