﻿using System.Collections.Generic;

namespace GameClient.SExpressionTree
{
    public class EvalContext : Dictionary<SIdExpression, SExpression>
    {
        public EvalContext()
        {
        }

        public EvalContext(EvalContext parent) : base(parent)
        {
        }
    }
}
