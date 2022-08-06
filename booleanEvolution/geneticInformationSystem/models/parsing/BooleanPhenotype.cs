using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using geneticInformationSystem.models.lexing;
using geneticInformationSystem.modules;

namespace geneticInformationSystem.models.parsing {
    public class BooleanPhenotype {
        public List<Expr> expressedASTs { get; set; }

        public string dna { get; set; }

        public List<Token> dnatokens { get; set; }

        public int inputs { get; set; }

        public double threshhold { get; set; }

        public double length { get; set; }

        public double viabilityScore { get; set; }

        public double fitnessScore { get; set; }

        public BooleanPhenotype() {
        }
    

    }
}
