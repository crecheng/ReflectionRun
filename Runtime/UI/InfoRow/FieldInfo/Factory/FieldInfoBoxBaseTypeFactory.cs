using System.Collections;
using System.Reflection;
using UnityEngine;

namespace ReflectionRun
{
    internal static partial class FieldInfoBoxBaseTypeFactory
    {
        
        private static InfoBaseGroupBox ListFieldInfoGroupBox(IList type, FieldInfo fieldInfo, object obj,bool isEditor)
        {
            return new ListFieldInfoGroupBox(fieldInfo, obj,isEditor);
        }
    }
}