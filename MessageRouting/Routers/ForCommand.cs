using System.Diagnostics.CodeAnalysis;

namespace MessageRouting.Routers
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public interface ForCommand<in TCommand>
    {
        void Handle(TCommand c);
    }
}