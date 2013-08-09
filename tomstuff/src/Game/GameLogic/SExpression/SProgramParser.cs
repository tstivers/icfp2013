using System;
using OMetaSharp;
using SExpression.GeneratedCode;

namespace GameClient.SExpression
{
    public class SProgramParser
    {
        public static SProgram Parse(string sexpr)
        {
            return Grammars.ParseWith<SExpressionParser>(sexpr, x => x.SProgram).As<SProgram>();
        }

        public static void PrettyPrint(string sexpr)
        {
            SProgram x = Parse(sexpr);

            RecursivePrettyPrint(x, 0);
        }

        private static void RecursivePrettyPrint(SProgram x, int depth)
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
