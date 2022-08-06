using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geneticInformationSystem.modules {
    public class DNAGenerator {
        private string[] operatorBases = { "+", "*", "!" };
        private string[] inputBases;
        private double literalPercentage;
        private double operatorPercentage;
        public double selectorBorderPercentage { get; set; }
        public  int length { get; set; }
        private int asciiA = 65;
        private int numInputs;
        public int NumInputs { 
            get { 
                return numInputs; 
            } 
            set { 
                numInputs = value;
                inputBases = new string[numInputs];
                for (int i = 0; i < numInputs; i++) {
                    inputBases[i] = Convert.ToChar(asciiA + i).ToString();
                }
            } 
        }

        public DNAGenerator(int len, double selPer, int inputs) {
            length = len;
            selectorBorderPercentage = selPer;
            numInputs = inputs;
            inputBases = new string[numInputs];
            for (int i = 0; i < numInputs; i++) {
                inputBases[i] = Convert.ToChar(asciiA + i).ToString();
            }
        }

        public DNAGenerator() {
        }


        public string generateDNA() {
            StringBuilder DNA = new StringBuilder();
            double typeSelect;
            Random r= new Random();
            int rInt;
            string c;
            for (int i= 0; i<=length; i++) {
                //r = new Random();
                rInt = r.Next(101);
                if (rInt > selectorBorderPercentage) {//select literal
                    //r = new Random();
                    rInt = r.Next(0, numInputs);
                    c = inputBases[rInt];
                    DNA.Append(c);
                }
                else {//select operator
                    //r = new Random();
                    rInt = r.Next(0, 3);
                    c = operatorBases[rInt];
                    DNA.Append(c);
                }
            }
            return DNA.ToString();
        }
    }
}
