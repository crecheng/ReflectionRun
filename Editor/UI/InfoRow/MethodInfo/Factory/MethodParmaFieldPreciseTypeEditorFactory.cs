using System;
using System.Reflection;
using UnityEngine;

namespace ReflectionRun.Editor
{
    internal static partial class MethodParmaFieldPreciseTypeEditorFactory
    {

        private static MethodParmaValueField GameObjectParmaValueField(GameObject value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new GameObjectParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField TransformParmaValueField(Transform value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new TransformParmaValueField(parameterInfo, isEditor);
        }
    }
}