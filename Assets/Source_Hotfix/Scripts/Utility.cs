using HT.Framework;

namespace HotfixDemo
{
    public static class Utility
    {
        static Utility()
        {
            ReflectionToolkit.AddRunTimeAssembly("HotfixDemo");
        }
    }
}