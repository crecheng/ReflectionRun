using System;
using System.Reflection;

namespace ReflectionRun.Editor
{
    internal static partial class EditorFactory
    {
        private static bool _isInit = false;
        public static void Init()
        {
            if(_isInit)
                return;
            _isInit = true;
            ReflectionRunCenter.EditorWindow = new ReflectionRunOpenEditorWindow();
            ReflectionRunCenter.EditorFactory = new EditorFactoryInstance();
            FieldInfoBoxEditorFactory.FieldGroupBoxInit();
            MethodParmaValueFieldEditorFactory.MethodParmaValueFieldInit();
            MethodReturnViewEditorFactory.MethodParmaValueFieldInit();
            
        }
        
        public static InfoBaseGroupBox GetFieldInfoRow(FieldInfo info, object obj)
        {
            return FieldInfoBoxEditorFactory.GetInfoRowFromFactory(info, obj);
        }
        
        public static MethodParmaValueField GetMethodParmaValueField(ParameterInfo info)
        {
            return MethodParmaValueFieldEditorFactory.GetValueFieldFromFactory(info);
        }

        public static bool CanCreateMethodParmaValueField(Type type)
        {
            return MethodParmaValueFieldEditorFactory.ContainMethodParmaValue(type);
        }

        public static InfoBaseGroupBox GetMethodInfoRow(MethodInfo info, object obj)
        {
            return new MethodInfoGroupBox(info, obj,true);
        }

        public static InfoBaseGroupBox GetPropertyInfoRow(PropertyInfo info, object obj)
        {
            return new PropertyInfoGroupBox(info, obj,true);
        }

        public static ValueView GetMethodReturnValueView(Type type, bool defaultView=true)
        {
            return MethodReturnViewEditorFactory.GetValueViewFromFactory(type, defaultView);
        }

        public static ValueView GetDeepValueView(Type type)
        {
            return MethodReturnViewEditorFactory.GetDeepFieldValueView(type);
        }

        
    }


}