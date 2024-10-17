using System;
using System.Reflection;
using UnityEngine;

namespace ReflectionRun
{
    internal static partial class FieldInfoBoxPreciseTypeFactory
    {
        private static InfoBaseGroupBox StringFieldInfoGroupBox(String type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new StringFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        private static InfoBaseGroupBox IntegerFieldInfoGroupBox(int type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new IntegerFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox BooleanFieldInfoGroupBox(bool type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new BooleanFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox UnsignedIntegerFieldInfoGroupBox(uint type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new UnsignedIntegerFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox UnsignedIntegerFieldInfoGroupBox(byte type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new UnsignedIntegerFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox IntegerFieldInfoGroupBox(short type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new IntegerFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox UnsignedIntegerFieldInfoGroupBox(ushort type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new UnsignedIntegerFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox LongFieldInfoGroupBox(long type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new LongFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox UnsignedLongFieldInfoGroupBox(ulong type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new UnsignedLongFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox FloatFieldInfoGroupBox(float type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new FloatFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox DoubleFieldInfoGroupBox(double type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new DoubleFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox Vector2FieldInfoGroupBox(Vector2 type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new Vector2FieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox Vector3FieldInfoGroupBox(Vector3 type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new Vector3FieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox Vector4FieldInfoGroupBox(Vector4 type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new Vector4FieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox RectFieldInfoGroupBox(Rect type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new RectFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox Vector2IntFieldInfoGroupBox(Vector2Int type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new Vector2IntFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
        
        private static InfoBaseGroupBox Vector3IntFieldInfoGroupBox(Vector3Int type, FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new Vector3IntFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }
    }
}