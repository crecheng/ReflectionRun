using System;
using System.Reflection;

namespace ReflectionRun
{
    /// <summary>
    /// 运行时工厂
    /// </summary>
    internal static partial class RuntimeFactory
    {
        private static bool _isInit = false;
        public static void Init()
        {
            if(_isInit)
                return;
            _isInit = true;
            FieldInfoBoxFactory.FieldGroupBoxInit();
            MethodParmaValueFieldFactory.MethodParmaValueFieldInit();
            MethodReturnViewFactory.MethodParmaValueFieldInit();
        }
        
        public static InfoBaseGroupBox GetFieldInfoRow(FieldInfo info, object obj)
        {
            return FieldInfoBoxFactory.GetInfoRowFromFactory(info, obj);
        }
        
        public static MethodParmaValueField GetMethodParmaValueField(ParameterInfo info)
        {
            return MethodParmaValueFieldFactory.GetValueFieldFromFactory(info);
        }

        public static bool CanCreateMethodParmaValueField(Type type)
        {
            return MethodParmaValueFieldFactory.ContainMethodParmaValue(type);
        }

        public static InfoBaseGroupBox GetMethodInfoRow(MethodInfo info, object obj)
        {
            return new MethodInfoGroupBox(info, obj,false);
        }

        public static InfoBaseGroupBox GetPropertyInfoRow(PropertyInfo info, object obj)
        {
            return new PropertyInfoGroupBox(info, obj,false);
        }

        public static ValueView GetMethodReturnValueView(Type type, bool defaultView=true)
        {
            return MethodReturnViewFactory.GetValueViewFromFactory(type, defaultView);
        }

        public static ValueView GetDeepValueView(Type type)
        {
            return MethodReturnViewFactory.GetDeepFieldValueView(type);
        }
    }
}