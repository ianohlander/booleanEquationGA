using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using geneticInformationSystem.models.lexing;
using geneticInformationSystem.modules;

namespace geneticInformationSystem.models.parsing {
    public class BooleanPhenotype {
        public List<Expr> ExpressedASTs { get; set; }

        public string DNA { get; set; }

        public List<Token> DNATokens { get; set; }

        public int Inputs { get; set; }

        public double Threshhold { get; set; }

        public double Length { get; set; }

        public double ViabilityScore { get; set; }

        public double FitnessScore { get; set; }

        public BooleanPhenotype() {
        }
    

    }
}
