namespace ReflectionRun.Editor
{
    internal static partial class MethodReturnViewBaseTypeEditorFactory
    {
        private static ValueView UnityObjectValueView(UnityEngine.Object value,bool isEditor)
        {
            return new UnityObjectValueView(isEditor);
        }
    }
}