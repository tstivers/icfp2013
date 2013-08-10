using OMetaSharp;

namespace GameClient.SExpressionTree.GeneratedCode
{
    public class SExpressionParser : Parser
    {
        public virtual bool SProgram(OMetaStream<char> inputStream, out OMetaList<HostExpression> result,
            out OMetaStream<char> modifiedStream)
        {
            OMetaList<HostExpression> id = null;
            OMetaList<HostExpression> e = null;
            modifiedStream = inputStream;
            if (
                !MetaRules.Apply(
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (
                            !MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2,
                                ("(").AsHostExpressionList()))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        if (
                            !MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2,
                                ("lambda").AsHostExpressionList()))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        if (
                            !MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2,
                                ("(").AsHostExpressionList()))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        if (!MetaRules.Apply(Id, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        id = result2;
                        if (
                            !MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2,
                                (")").AsHostExpressionList()))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        if (!MetaRules.Apply(Expression, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        e = result2;
                        if (
                            !MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2,
                                (")").AsHostExpressionList()))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        result2 =
                            (new ProgramExpression(id.As<IdExpression>(), e.As<IExpression>())).AsHostExpressionList();
                        return MetaRules.Success();
                    }, modifiedStream, out result, out modifiedStream))
                return MetaRules.Fail(out result, out modifiedStream);
            return MetaRules.Success();
        }

        public virtual bool Expression(OMetaStream<char> inputStream, out OMetaList<HostExpression> result,
            out OMetaStream<char> modifiedStream)
        {
            OMetaList<HostExpression> e1 = null;
            OMetaList<HostExpression> e2 = null;
            OMetaList<HostExpression> i0 = null;
            OMetaList<HostExpression> fe = null;
            modifiedStream = inputStream;
            if (
                !MetaRules.Or(modifiedStream, out result, out modifiedStream,
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (!MetaRules.Apply(NumberExpression, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        return MetaRules.Success();
                    },
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (!MetaRules.Apply(Id, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        return MetaRules.Success();
                    },
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (
                            !MetaRules.Apply(
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("(").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    if (
                                        !MetaRules.Apply(Op1Expression, modifiedStream3, out result3,
                                            out modifiedStream3))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    e1 = result3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, (")").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    result3 = (e1).AsHostExpressionList();
                                    return MetaRules.Success();
                                }, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        return MetaRules.Success();
                    },
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (
                            !MetaRules.Apply(
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("(").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    if (
                                        !MetaRules.Apply(Op2Expression, modifiedStream3, out result3,
                                            out modifiedStream3))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    e2 = result3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, (")").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    result3 = (e2).AsHostExpressionList();
                                    return MetaRules.Success();
                                }, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        return MetaRules.Success();
                    },
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (
                            !MetaRules.Apply(
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("(").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    if (
                                        !MetaRules.Apply(If0Expression, modifiedStream3, out result3,
                                            out modifiedStream3))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    i0 = result3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, (")").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    result3 = (i0).AsHostExpressionList();
                                    return MetaRules.Success();
                                }, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        return MetaRules.Success();
                    },
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (
                            !MetaRules.Apply(
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("(").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    if (
                                        !MetaRules.Apply(FoldExpression, modifiedStream3, out result3,
                                            out modifiedStream3))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    fe = result3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, (")").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    result3 = (fe).AsHostExpressionList();
                                    return MetaRules.Success();
                                }, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        return MetaRules.Success();
                    }))
                return MetaRules.Fail(out result, out modifiedStream);
            return MetaRules.Success();
        }

        public virtual bool Id(OMetaStream<char> inputStream, out OMetaList<HostExpression> result,
            out OMetaStream<char> modifiedStream)
        {
            OMetaList<HostExpression> l = null;
            OMetaList<HostExpression> v = null;
            modifiedStream = inputStream;
            if (
                !MetaRules.Apply(
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (!MetaRules.Apply(Spaces, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        if (!MetaRules.Apply(Letter, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        l = result2;
                        if (
                            !MetaRules.Many(
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.Or(modifiedStream3, out result3, out modifiedStream3,
                                            delegate(OMetaStream<char> inputStream4,
                                                out OMetaList<HostExpression> result4,
                                                out OMetaStream<char> modifiedStream4)
                                            {
                                                modifiedStream4 = inputStream4;
                                                if (
                                                    !MetaRules.Apply(LetterOrDigit, modifiedStream4, out result4,
                                                        out modifiedStream4))
                                                    return MetaRules.Fail(out result4, out modifiedStream4);
                                                return MetaRules.Success();
                                            },
                                            delegate(OMetaStream<char> inputStream4,
                                                out OMetaList<HostExpression> result4,
                                                out OMetaStream<char> modifiedStream4)
                                            {
                                                modifiedStream4 = inputStream4;
                                                if (
                                                    !MetaRules.ApplyWithArgs(Exactly, modifiedStream4, out result4,
                                                        out modifiedStream4, ("_").AsHostExpressionList()))
                                                    return MetaRules.Fail(out result4, out modifiedStream4);
                                                return MetaRules.Success();
                                            }))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    return MetaRules.Success();
                                }, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        v = result2;
                        result2 = (new IdExpression(l + v.ToLiteralString())).AsHostExpressionList();
                        return MetaRules.Success();
                    }, modifiedStream, out result, out modifiedStream))
                return MetaRules.Fail(out result, out modifiedStream);
            return MetaRules.Success();
        }

        public virtual bool NumberExpression(OMetaStream<char> inputStream, out OMetaList<HostExpression> result,
            out OMetaStream<char> modifiedStream)
        {
            OMetaList<HostExpression> n = null;
            modifiedStream = inputStream;
            if (
                !MetaRules.Apply(
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (
                            !MetaRules.Or(modifiedStream2, out result2, out modifiedStream2,
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("0").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    return MetaRules.Success();
                                },
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("1").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    return MetaRules.Success();
                                }))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        n = result2;
                        result2 = (new NumberExpression(n.ToString())).AsHostExpressionList();
                        return MetaRules.Success();
                    }, modifiedStream, out result, out modifiedStream))
                return MetaRules.Fail(out result, out modifiedStream);
            return MetaRules.Success();
        }

        public virtual bool Op1Expression(OMetaStream<char> inputStream, out OMetaList<HostExpression> result,
            out OMetaStream<char> modifiedStream)
        {
            OMetaList<HostExpression> op = null;
            OMetaList<HostExpression> e = null;
            modifiedStream = inputStream;
            if (
                !MetaRules.Apply(
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (!MetaRules.Apply(Op1, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        op = result2;
                        if (!MetaRules.Apply(Expression, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        e = result2;
                        result2 = (new Op1Expression(op.ToString(), e.As<IExpression>())).AsHostExpressionList();
                        return MetaRules.Success();
                    }, modifiedStream, out result, out modifiedStream))
                return MetaRules.Fail(out result, out modifiedStream);
            return MetaRules.Success();
        }

        public virtual bool Op2Expression(OMetaStream<char> inputStream, out OMetaList<HostExpression> result,
            out OMetaStream<char> modifiedStream)
        {
            OMetaList<HostExpression> op = null;
            OMetaList<HostExpression> e1 = null;
            OMetaList<HostExpression> e2 = null;
            modifiedStream = inputStream;
            if (
                !MetaRules.Apply(
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (!MetaRules.Apply(Op2, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        op = result2;
                        if (!MetaRules.Apply(Expression, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        e1 = result2;
                        if (!MetaRules.Apply(Expression, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        e2 = result2;
                        result2 =
                            (new Op2Expression(op.ToString(), e1.As<IExpression>(), e2.As<IExpression>()))
                                .AsHostExpressionList();
                        return MetaRules.Success();
                    }, modifiedStream, out result, out modifiedStream))
                return MetaRules.Fail(out result, out modifiedStream);
            return MetaRules.Success();
        }

        public virtual bool If0Expression(OMetaStream<char> inputStream, out OMetaList<HostExpression> result,
            out OMetaStream<char> modifiedStream)
        {
            OMetaList<HostExpression> e1 = null;
            OMetaList<HostExpression> e2 = null;
            OMetaList<HostExpression> e3 = null;
            modifiedStream = inputStream;
            if (
                !MetaRules.Apply(
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (
                            !MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2,
                                ("if0").AsHostExpressionList()))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        if (!MetaRules.Apply(Expression, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        e1 = result2;
                        if (!MetaRules.Apply(Expression, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        e2 = result2;
                        if (!MetaRules.Apply(Expression, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        e3 = result2;
                        result2 =
                            (new If0Expression(e1.As<IExpression>(), e2.As<IExpression>(), e3.As<IExpression>()))
                                .AsHostExpressionList();
                        return MetaRules.Success();
                    }, modifiedStream, out result, out modifiedStream))
                return MetaRules.Fail(out result, out modifiedStream);
            return MetaRules.Success();
        }

        public virtual bool FoldExpression(OMetaStream<char> inputStream, out OMetaList<HostExpression> result,
            out OMetaStream<char> modifiedStream)
        {
            OMetaList<HostExpression> e1 = null;
            OMetaList<HostExpression> e2 = null;
            OMetaList<HostExpression> id1 = null;
            OMetaList<HostExpression> id2 = null;
            OMetaList<HostExpression> e3 = null;
            modifiedStream = inputStream;
            if (
                !MetaRules.Apply(
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (
                            !MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2,
                                ("fold").AsHostExpressionList()))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        if (!MetaRules.Apply(Expression, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        e1 = result2;
                        if (!MetaRules.Apply(Expression, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        e2 = result2;
                        if (
                            !MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2,
                                ("(").AsHostExpressionList()))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        if (
                            !MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2,
                                ("lambda").AsHostExpressionList()))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        if (
                            !MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2,
                                ("(").AsHostExpressionList()))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        if (!MetaRules.Apply(Id, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        id1 = result2;
                        if (!MetaRules.Apply(Id, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        id2 = result2;
                        if (
                            !MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2,
                                (")").AsHostExpressionList()))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        if (!MetaRules.Apply(Expression, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        e3 = result2;
                        if (
                            !MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2,
                                (")").AsHostExpressionList()))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        result2 =
                            (new FoldExpression(e1.As<IExpression>(), e2.As<IExpression>(), id1.As<IdExpression>(),
                                id2.As<IdExpression>(), e3.As<IExpression>())).AsHostExpressionList();
                        return MetaRules.Success();
                    }, modifiedStream, out result, out modifiedStream))
                return MetaRules.Fail(out result, out modifiedStream);
            return MetaRules.Success();
        }

        public virtual bool Op1(OMetaStream<char> inputStream, out OMetaList<HostExpression> result,
            out OMetaStream<char> modifiedStream)
        {
            OMetaList<HostExpression> op = null;
            modifiedStream = inputStream;
            if (
                !MetaRules.Apply(
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (
                            !MetaRules.Or(modifiedStream2, out result2, out modifiedStream2,
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("not").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    return MetaRules.Success();
                                },
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("shl1").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    return MetaRules.Success();
                                },
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("shr16").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    return MetaRules.Success();
                                },
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("shr1").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    return MetaRules.Success();
                                },
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("shr4").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    return MetaRules.Success();
                                }))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        op = result2;
                        if (!MetaRules.Apply(Spaces, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        result2 = (op).AsHostExpressionList();
                        return MetaRules.Success();
                    }, modifiedStream, out result, out modifiedStream))
                return MetaRules.Fail(out result, out modifiedStream);
            return MetaRules.Success();
        }

        public virtual bool Op2(OMetaStream<char> inputStream, out OMetaList<HostExpression> result,
            out OMetaStream<char> modifiedStream)
        {
            OMetaList<HostExpression> op = null;
            modifiedStream = inputStream;
            if (
                !MetaRules.Apply(
                    delegate(OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2,
                        out OMetaStream<char> modifiedStream2)
                    {
                        modifiedStream2 = inputStream2;
                        if (
                            !MetaRules.Or(modifiedStream2, out result2, out modifiedStream2,
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("and").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    return MetaRules.Success();
                                },
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("or").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    return MetaRules.Success();
                                },
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("xor").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    return MetaRules.Success();
                                },
                                delegate(OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3,
                                    out OMetaStream<char> modifiedStream3)
                                {
                                    modifiedStream3 = inputStream3;
                                    if (
                                        !MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3,
                                            out modifiedStream3, ("plus").AsHostExpressionList()))
                                        return MetaRules.Fail(out result3, out modifiedStream3);
                                    return MetaRules.Success();
                                }))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        op = result2;
                        if (!MetaRules.Apply(Spaces, modifiedStream2, out result2, out modifiedStream2))
                            return MetaRules.Fail(out result2, out modifiedStream2);
                        result2 = (op).AsHostExpressionList();
                        return MetaRules.Success();
                    }, modifiedStream, out result, out modifiedStream))
                return MetaRules.Fail(out result, out modifiedStream);
            return MetaRules.Success();
        }
    }
}
