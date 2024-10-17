using System;
using System.Reflection;
using UnityEngine;

namespace ReflectionRun.Editor
{
    internal static partial class MethodReturnViewPreciseTypeEditorFactory
    {
        
        private static ValueView GameObjectValueView(GameObject value,bool isEditor)
        {
            return new GameObjectValueView(isEditor);
        }
        
        private static ValueView TransformValueView(Transform value,bool isEditor)
        {
            return new TransformValueView(isEditor);
        }
        
    }
}