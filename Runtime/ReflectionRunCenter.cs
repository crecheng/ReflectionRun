using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    /// <summary>
    /// 编辑器与运行时区分
    /// </summary>
    public static class ReflectionRunCenter
    {
        
        public static IReflectionRunOpenWindow RuntimeWindow;
        public static IReflectionRunOpenWindow EditorWindow;
        
        public static IReflectionRunFactory RuntimeFactory;
        public static IReflectionRunFactory EditorFactory;
        
        private static bool _init=false;
        public static void Init()
        {
            if(_init)
                return;
            _init = true;
            ReflectionRunExtensionsUtil.Collect();
            
            RuntimeFactory = new RuntimeFactoryInstance();
            RuntimeWindow = new ReflectionRunOpenRuntimeWindow();
            RuntimeFactory.Init();
#if !UNITY_EDITOR
            EditorWindow = new ReflectionRunOpenRuntimeWindow();
            EditorFactory = new RuntimeFactoryInstance();
#endif
        }
        public static HashSet<Type> BaseValueType = new HashSet<Type>()
        {
            typeof(byte),
            typeof(char),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(ulong),
            typeof(float),
            typeof(double)
        };

        public static bool IsBaseType(Type type)
        {
            if (!type.IsValueType)
                return false;

            return BaseValueType.Contains(type);
        }

        public static MethodInfo[] GetStaticNoPublicMethodInfo(Type type)
        {
            if (type == null)
                return Array.Empty<MethodInfo>();
            return type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
        }

        public static void CreateRuntimeMainWindow(VisualElement content)
        {
            var runtime = (ReflectionRunOpenRuntimeWindow)RuntimeWindow;
            var window = new RuntimeMainWindows();
            content.Add(window);
            runtime.SetMainWindow(window);
        }

        #region 公有方法
        
        public static InfoBaseGroupBox GetFieldInfoRow(bool isEditor, FieldInfo fieldInfo, object obj)
        {
            if (isEditor)
                return EditorFactory.GetFieldInfoRow(fieldInfo, obj);
            else
                return RuntimeFactory.GetFieldInfoRow(fieldInfo, obj);
        }
        
        public static InfoBaseGroupBox GetMethodInfoRow(bool isEditor, MethodInfo methodInfo, object obj)
        {
            if (isEditor)
                return EditorFactory.GetMethodInfoRow(methodInfo, obj);
            else
                return RuntimeFactory.GetMethodInfoRow(methodInfo, obj);
        }
        
        public static InfoBaseGroupBox GetPropertyInfoRow(bool isEditor, PropertyInfo propertyInfo, object obj)
        {
            if (isEditor)
                return EditorFactory.GetPropertyInfoRow(propertyInfo, obj);
            else
                return RuntimeFactory.GetPropertyInfoRow(propertyInfo, obj);
        }
        
        public static ValueView GetDeepValueView(bool isEditor, Type type)
        {
            if (isEditor)
                return EditorFactory.GetDeepValueView(type);
            else
                return RuntimeFactory.GetDeepValueView(type);
        }
        
        public static MethodParmaValueField GetMethodParmaValueField(bool isEditor, ParameterInfo info)
        {
            if (isEditor)
                return EditorFactory.GetMethodParmaValueField(info);
            else
                return RuntimeFactory.GetMethodParmaValueField(info);
        }
        
        public static bool CanCreateMethodParmaValueField(bool isEditor, Type type)
        {
            if (isEditor)
                return EditorFactory.CanCreateMethodParmaValueField(type);
            else
                return RuntimeFactory.CanCreateMethodParmaValueField(type);
        }
        
        public static ValueView GetMethodReturnValueView(bool isEditor, Type type,bool defaultBox=true)
        {
            if (isEditor)
                return EditorFactory.GetMethodReturnValueView(type,defaultBox);
            else
                return RuntimeFactory.GetMethodReturnValueView(type,defaultBox);
        }

        public static VisualElement CreateClassWindow(bool isEditor, Type type)
        {
            if (isEditor)
                return EditorWindow.CreateClassWindow(type);
            else
                return RuntimeWindow.CreateClassWindow(type);
        }

        public static VisualElement CreateClassWindow(bool isEditor, object obj)
        {
            if (isEditor)
                return EditorWindow.CreateClassWindow(obj);
            else
                return RuntimeWindow.CreateClassWindow(obj);
        }

        public static VisualElement CreateListViewWindow(bool isEditor, IList list)
        {
            if (isEditor)
                return EditorWindow.CreateListViewWindow(list);
            else
                return RuntimeWindow.CreateListViewWindow(list);
        }
        
        public static void OpenPropertyEditor(bool isEditor, UnityEngine.Object obj)
        {
            if (isEditor)
                EditorWindow.OpenPropertyEditor(obj);
            else
                RuntimeWindow.OpenPropertyEditor(obj);
        }

        #endregion


    }
}