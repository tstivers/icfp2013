﻿using System;
using GameClient.SExpressionTree.GeneratedCode;
using OMetaSharp;

namespace GameClient.SExpressionTree
{
    public class SProgramParser
    {
        public static ProgramExpression Parse(string sexpr)
        {
            return Grammars.ParseWith<SExpressionParser>(sexpr, x => x.SProgram).As<ProgramExpression>();
        }

        public static void PrettyPrint(string sexpr)
        {
            var x = Parse(sexpr);

            RecursivePrettyPrint(x, 0);
        }

        private static void RecursivePrettyPrint(ProgramExpression x, int depth)
        {
            Console.Write(new String('\t', depth));

            Console.WriteLine("Program: {0}", x.Id);
            Console.WriteLine("Expression: {0}", x.Expression);
            //if ((x.Type & Types.Atom) == Types.Atom)
            //{
            //    if((x.Type & Types.Symbol) == Types.Symbol)
            //    {
            //        Console.WriteLine(String.Format("symbol: {0}", x.ToString()));
            //    } else if((x.Type & Types.Number) == Types.Number) {
            //        Console.WriteLine(String.Format("number: {0}", x.ToString()));
            //    }
            //}
            //else if((x.Type & Types.List) == Types.List)
            //{
            //    Console.WriteLine("list");
            //    SExprList list = x as SExprList;
            //    foreach (ISExpression y in list)
            //    {
            //        SExpression.RecursivePrettyPrint(y, depth + 1);
            //    }
            //}
        }
    }
}
