using System.Collections;
using System.Reflection;
using UnityEngine;

namespace ReflectionRun.Editor
{
    internal static partial class FieldInfoBoxBaseTypeEditorFactory
    {
        private static InfoBaseGroupBox UnityObjectFieldInfoGroupBox(Object type, FieldInfo fieldInfo, object obj,bool isEditor)
        {
            return new UnityObjectFieldInfoGroupBox(fieldInfo, obj);
        }
        
    }
}