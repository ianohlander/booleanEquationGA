using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geneticInformationSystem.modules {
    public class DNAGenerator {
        private string[] operatorBases = { "+", "*", "!" };
        private string[] inputBases;
        public double selectorBorderPercentage { get; set; }
        public  int length { get; set; }
        private int asciiA = 65;
        private int numInputs;
        public int NumInputs { 
            get { 
                return numInputs; 
            } 
            set {
                //cannot have more than a-z as Inputs
                if (value < 27) {
                    numInputs = value;
                    inputBases = new string[numInputs];
                    for (int i = 0; i < numInputs; i++) {
                        inputBases[i] = Convert.ToChar(asciiA + i).ToString();
                    }
                }
            } 
        }

        public DNAGenerator(int len, double selPer, int inputs) {
            length = len;
            selectorBorderPercentage = selPer;
            NumInputs = inputs;
            inputBases = new string[NumInputs];
            for (int i = 0; i < NumInputs; i++) {
                inputBases[i] = Convert.ToChar(asciiA + i).ToString();
            }
        }

        public DNAGenerator() {
        }


        public string generateDNA() {
            StringBuilder DNA = new StringBuilder();
            Random r = new Random();
            int rInt;
            string c;
            if (NumInputs > 0) {
                for (int i = 0; i <= length; i++) {
                    rInt = r.Next(101);
                    if (rInt > selectorBorderPercentage) {//select literal
                        rInt = r.Next(0, NumInputs);
                        c = inputBases[rInt];
                        DNA.Append(c);
                    }
                    else {//select operator
                        rInt = r.Next(0, 3);
                        c = operatorBases[rInt];
                        DNA.Append(c);
                    }
                }
            }
            else {
                DNA.Append("Must have between 1 and 26 inputs.");
            }
            return DNA.ToString();
        }
    }
}
