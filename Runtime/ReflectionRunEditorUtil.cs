using System;
using System.Reflection;

namespace ReflectionRun
{
    /// <summary>
    /// 给Editor提供的方法
    /// </summary>
    public static class ReflectionRunEditorUtil
    {
        public static InfoBaseGroupBox GetEnumFieldInfoGroupBox(FieldInfo fieldInfo, object obj, bool isEditor)
        {
            return new EnumFieldInfoGroupBox(fieldInfo, obj, isEditor);
        }

        public static MethodInfo[] GetFieldInfoBoxPreciseTypeMethod()
        {
            return typeof(FieldInfoBoxPreciseTypeFactory).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
        }
        public static MethodInfo[] GetFieldInfoBoxBaseTypeMethod()
        {
            return typeof(FieldInfoBoxBaseTypeFactory).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
        }
        
        public static MethodInfo[] GetMethodReturnViewPreciseTypeMethod()
        {
            return typeof(MethodReturnViewPreciseTypeFactory).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
        }
        
        public static MethodInfo[] GetMethodReturnViewBaseTypeMethod()
        {
            return typeof(MethodReturnViewBaseTypeFactory).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
        }
        
        public static MethodInfo[] GetMethodParmaFieldBaseTypeMethod()
        {
            return typeof(MethodParmaFieldBaseTypeFactory).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
        }
        
        public static MethodInfo[] GetMethodParmaFieldPreciseTypeMethod()
        {
            return typeof(MethodParmaFieldPreciseTypeFactory).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
        }
        
        public static ValueView GetValueViewDefault()
        {
            var field = new ObjectValueView(true);
            return field;
        }
        
        public static ValueView GetDeepFieldValueView(Type type)
        {
            var view = new DeepFieldValueView(true);
            view.SetType(type);
            return view;
        }
        
        public static ValueView GetEnumValueView(Type type)
        {
            var view = new EnumValueView(true);
            view.SetType(type);
            return view;
        }

        public static MethodParmaValueField GetEnumMethodParmaValueField(ParameterInfo info)
        {
            var view=new EnumMethodParmaValueField(info,true);
            return view;
        }

        public static MethodParmaValueField GetDefaultMethodParmaValueField(ParameterInfo info)
        {
            var field = new StringMethodParmaValueField(info,true);
            field.SetEnabled(false);
            return field;
        }
    }
}