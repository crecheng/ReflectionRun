using System;
using System.Reflection;
using UnityEngine;

namespace ReflectionRun
{
    internal static partial class MethodParmaFieldPreciseTypeFactory
    {
        private static MethodParmaValueField StringMethodParmaValueField(String value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new StringMethodParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField BoolMethodParmaValueField(bool value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new BoolMethodParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField IntegerMethodParmaValueField(int value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new IntegerMethodParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField UnsignedMethodParmaValueField(uint value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new UnsignedMethodParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField LongMethodParmaValueField(long value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new LongMethodParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField UnsignedLongParmaValueField(ulong value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new UnsignedLongParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField FloatParmaValueField(float value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new FloatParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField DoubleParmaValueField(double value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new DoubleParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField Vector2ParmaValueField(Vector2 value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new Vector2ParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField Vector3ParmaValueField(Vector3 value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new Vector3ParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField Vector4ParmaValueField(Vector4 value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new Vector4ParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField RectParmaValueField(Rect value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new RectParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField Vector2IntParmaValueField(Vector2Int value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new Vector2IntParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField Vector3IntParmaValueField(Vector3Int value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new Vector3IntParmaValueField(parameterInfo, isEditor);
        }

        private static MethodParmaValueField RectIntParmaValueField(RectInt value, ParameterInfo parameterInfo, bool isEditor)
        {
            return new RectIntParmaValueField(parameterInfo, isEditor);
        }
        
    }
}