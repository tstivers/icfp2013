﻿using System;

namespace SExpression
{
    public class If0Expression : SExpression
    {        
        public SExpression Expression1 { get; set; }
        public SExpression Expression2 { get; set; }
        public SExpression Expression3 { get; set; }

        public If0Expression(SExpression expression1, SExpression expression2, SExpression expression3)
        {            
            Expression1 = expression1;
            Expression2 = expression2;
            Expression3 = expression3;
        }

        public override string ToString()
        {
            return String.Format("(if0 {0} {1} {2})", Expression1, Expression2, Expression3);
        }
    }
}