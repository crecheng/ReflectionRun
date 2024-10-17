using System;
using System.Reflection;
using UnityEngine;

namespace ReflectionRun
{
    internal static partial class MethodReturnViewPreciseTypeFactory
    {
        private static ValueView StringValueView(String value,bool isEditor)
        {
            return new StringValueView( isEditor );
        }
        
        private static ValueView BoolValueView(bool value,bool isEditor)
        {
            return new BoolValueView( isEditor );
        }
        
        private static ValueView IntValueView(int value,bool isEditor)
        {
            return new IntegerValueView( isEditor );
        }
        
        private static ValueView UnsignedIntegerValueView(uint value,bool isEditor)
        {
            return new UnsignedIntegerValueView( isEditor );
        }
        
        private static ValueView LongValueView(long value,bool isEditor)
        {
            return new LongValueView( isEditor );
        }
        
        private static ValueView UnsignedLongValueView(ulong value,bool isEditor)
        {
            return new UnsignedLongValueView( isEditor );
        }
        
        private static ValueView FloatValueView(float value,bool isEditor)
        {
            return new FloatValueView( isEditor );
        }
        
        private static ValueView DoubleValueView(double value,bool isEditor)
        {
            return new DoubleValueView( isEditor );
        }
        
        private static ValueView Vector2FieldValueView(Vector2 value,bool isEditor)
        {
            return new Vector2FieldValueView( isEditor );
        }
        
        private static ValueView Vector3FieldValueView(Vector3 value,bool isEditor)
        {
            return new Vector3FieldValueView( isEditor );
        }
        
        private static ValueView Vector4FieldValueView(Vector4 value,bool isEditor)
        {
            return new Vector4FieldValueView( isEditor );
        }
        
        private static ValueView RectFieldValueView(Rect value,bool isEditor)
        {
            return new RectFieldValueView( isEditor );
        }
        
        private static ValueView Vector2IntFieldValueView(Vector2Int value,bool isEditor)
        {
            return new Vector2IntFieldValueView( isEditor );
        }
        
        private static ValueView Vector3IntFieldValueView(Vector3Int value,bool isEditor)
        {
            return new Vector3IntFieldValueView( isEditor );
        }
        
        private static ValueView RectIntFieldValueView(RectInt value,bool isEditor)
        {
            return new RectIntFieldValueView( isEditor );
        }
        
        
    }
}