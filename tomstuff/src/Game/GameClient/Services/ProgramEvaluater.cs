using System.Reflection;
using log4net;

namespace GameClient.Services
{
    public class ProgramEvaluater
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ProgramEvaluater(string program)
        {
        }

        public static ulong[] Eval(string program, ulong[] input)
        {
            var p = new ProgramEvaluater(program);
            return p.Eval(input);
        }

        public ulong[] Eval(ulong[] input)
        {
            var output = new ulong[input.Length];
            for (int i = 0; i < input.Length; i++)
                output[i] = Eval(input[i]);

            return output;
        }

        private ulong Eval(ulong input)
        {
            ulong output = 0;

            return output;
        }
    }
}
