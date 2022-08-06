using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geneticInformationSystem.models.lexing {
    public enum TokenType {
        //literals
        LITERAL,    //variables

        //unary operators
        BANG,       //NOT

        //binary operators
        STAR,       //AND
        PLUS,       //OR

        //grouping
        LEFT_PAREN, 
        RIGHT_PAREN, 
        LEFT_BRACE, 
        RIGHT_BRACE,

        //end of line
        SEMICOLON,

        //comment
        SLASH,

        //end of file
        EOF
    }
}
