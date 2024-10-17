using System.Reflection;

namespace ReflectionRun.Editor
{
    internal static partial class MethodParmaFieldBaseTypeEditorFactory
    {
        private static MethodParmaValueField UnityObjectParmaValueField(UnityEngine.Object value, ParameterInfo parameterInfo,bool isEditor)
        {
            return new UnityObjectParmaValueField(parameterInfo,isEditor);
        }
    }
}