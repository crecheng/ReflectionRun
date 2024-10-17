using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun.Editor
{
    internal static partial class EditorFactory
    {
        private static class MethodParmaValueFieldEditorFactory
        {
            private static readonly Dictionary<Type, MethodInfo> _preciseTypeMethod =
                new Dictionary<Type, MethodInfo>();
            
            private static readonly Dictionary<Type, MethodInfo> _baseTypeMethod =
                new Dictionary<Type, MethodInfo>();

            private static Type _baseType = typeof(MethodParmaValueField);

            internal static void MethodParmaValueFieldInit()
            {
                _preciseTypeMethod.Clear();
                List<MethodInfo> all = new List<MethodInfo>();

                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(typeof(MethodParmaFieldPreciseTypeEditorFactory)));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_MethodParmaFieldPreciseTypeEditorFactory));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_MethodParmaFieldPreciseTypeFactory));
                all.AddRange(ReflectionRunEditorUtil.GetMethodParmaFieldPreciseTypeMethod());
                
                foreach (var method in all)
                {
                    var type = IsCreateMethod(method);
                    if (type == null)
                        continue;
                    _preciseTypeMethod[type] = method;
                }
                
                _baseTypeMethod.Clear();
                all.Clear();
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(typeof(MethodParmaFieldBaseTypeEditorFactory)));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_MethodParmaFieldBaseTypeEditorFactory));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_MethodParmaFieldBaseTypeFactory));
                all.AddRange(ReflectionRunEditorUtil.GetMethodParmaFieldBaseTypeMethod());
                
                foreach (var method in all)
                {
                    var type = IsCreateMethod(method);
                    if (type == null)
                        continue;
                    _baseTypeMethod[type] = method;
                }
            }

            private static Type IsCreateMethod(MethodInfo methodInfo)
            {
                if (methodInfo.ReturnParameter == null)
                    return null;
                var reType = methodInfo.ReturnParameter.ParameterType;
                if (!_baseType.IsAssignableFrom(reType))
                    return null;
                ;
                var param = methodInfo.GetParameters();
                if (param.Length != 3)
                    return null;
                ;
                if (param[1].ParameterType != typeof(ParameterInfo))
                    return null;
                return param[0].ParameterType;
            }

            internal static bool ContainMethodParmaValue(Type type)
            {
                var precise = _preciseTypeMethod.ContainsKey(type);
                if (precise)
                    return true;
                foreach (var (key, methodInfo) in _baseTypeMethod)
                {
                    if (key.IsAssignableFrom(type))
                    {
                        return true;
                    }
                }

                if (type.IsEnum)
                    return true;
                return false;
            }

            private static MethodParmaValueField CreateFromMethodInfo(
                MethodInfo methodInfo, ParameterInfo info)
            {
                try
                {
                    var groupObj = methodInfo.Invoke(null, new[] { null, info ,(object)true});
                    if (groupObj is MethodParmaValueField valueField)
                    {
                        return valueField;
                    }
                    else
                    {
                        Debug.LogError($"{info.ParameterType} MethodParmaFieldPreciseTypeFactory method return error!");
                        return GetErrorDefault(info);
                    }

                }
                catch (Exception e)
                {
                    Debug.LogError(
                        $"{info.ParameterType} MethodParmaFieldPreciseTypeFactory method  error!\n{e.ToString()}");
                    return GetErrorDefault(info);
                }
            }

            public static MethodParmaValueField GetValueFieldFromFactory(ParameterInfo info)
            {
                if (_preciseTypeMethod.TryGetValue(info.ParameterType, out var methodInfo))
                {
                    var view=CreateFromMethodInfo(methodInfo, info);
                    SetBorder(view);
                    return view;
                }
                else
                {
                    foreach (var (key, methodInfoB) in _baseTypeMethod)
                    {
                        if (key.IsAssignableFrom(info.ParameterType))
                        {
                            var view=CreateFromMethodInfo(methodInfoB, info);
                            SetBorder(view);
                            return view;
                        }
                    }
                }

                if (info.ParameterType.IsEnum)
                {
                    var view=ReflectionRunEditorUtil.GetEnumMethodParmaValueField(info);
                    SetBorder(view);
                    return view;
                }

                return GetErrorDefault(info);
            }

            private static void SetBorder(MethodParmaValueField view)
            {
                view.style.borderTopWidth = 1;
                view.style.borderBottomWidth = 1;
                view.style.borderLeftWidth = 1;
                view.style.borderRightWidth = 1;
                
                view.style.borderTopColor = Color.black;
                view.style.borderBottomColor = Color.black;
                view.style.borderLeftColor = Color.black;
                view.style.borderRightColor = Color.black;
                
                view.style.borderBottomLeftRadius=5;
                view.style.borderBottomRightRadius=5;
                view.style.borderTopLeftRadius=5;
                view.style.borderTopRightRadius=5;
            }

            private static MethodParmaValueField GetErrorDefault(ParameterInfo info)
            {
                var field = ReflectionRunEditorUtil.GetDefaultMethodParmaValueField(info);
                SetBorder(field);
                return field;
            }
        }
    }
}