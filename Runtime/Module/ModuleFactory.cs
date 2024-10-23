namespace ReflectionRun
{
    public static partial class ModuleFactory
    {
        private static StringFindModule StringFindModule()
        {
            return new StringFindModule();
        }
        
        
        private static StringToMethodInvokeModule StringToMethodInvokeModule()
        {
            return new StringToMethodInvokeModule();
        }
    }
}