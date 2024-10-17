using System;
using System.Reflection;
using UnityEngine;

namespace ReflectionRun.Editor
{
    internal static partial class FieldInfoBoxPreciseTypeEditorFactory
    {
        private static InfoBaseGroupBox TransformFieldInfoGroupBox(Transform type, FieldInfo fieldInfo, object obj,bool isEditor)
        {
            return new TransformFieldInfoGroupBox(fieldInfo, obj);
        }
        
        private static InfoBaseGroupBox RectTransformFieldInfoGroupBox(RectTransform type, FieldInfo fieldInfo, object obj,bool isEditor)
        {
            return new RectTransformFieldInfoGroupBox(fieldInfo, obj);
        }
        
        private static InfoBaseGroupBox GameObjectFieldInfoGroupBox(GameObject type, FieldInfo fieldInfo, object obj,bool isEditor)
        {
            return new GameObjectFieldInfoGroupBox(fieldInfo, obj);
        }
    }
}