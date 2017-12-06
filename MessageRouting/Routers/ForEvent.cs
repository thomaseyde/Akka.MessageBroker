using System.Diagnostics.CodeAnalysis;

namespace MessageRouting.Routers
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public interface ForEvent<in TEvent>
    {
        void Apply(TEvent e);
    }
}