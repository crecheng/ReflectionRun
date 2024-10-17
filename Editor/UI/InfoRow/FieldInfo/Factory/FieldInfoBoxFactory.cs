using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun.Editor
{
    internal static partial class EditorFactory
    {
        private static class FieldInfoBoxEditorFactory
        {
            private static readonly Dictionary<Type, MethodInfo> _preciseTypeMethod =
                new Dictionary<Type, MethodInfo>();

            private static readonly Dictionary<Type, MethodInfo> _baseTypeMethod =
                new Dictionary<Type, MethodInfo>();

            private static Type _baseType = typeof(InfoBaseGroupBox);

            internal static void FieldGroupBoxInit()
            {
                _preciseTypeMethod.Clear();
                _baseTypeMethod.Clear();
                List<MethodInfo> all = new List<MethodInfo>();
                
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(typeof(FieldInfoBoxPreciseTypeEditorFactory)));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_FieldInfoBoxPreciseTypeEditorFactory));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_FieldInfoBoxPreciseTypeFactory));
                all.AddRange(ReflectionRunEditorUtil.GetFieldInfoBoxPreciseTypeMethod());
                
                
                foreach (var method in all)
                {
                    var type = IsCreateMethod(method);
                    if (type == null)
                        continue;
                    _preciseTypeMethod[type] = method;
                }

                all.Clear();
                
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(typeof(FieldInfoBoxBaseTypeEditorFactory)));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_FieldInfoBoxBaseTypeEditorFactory));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_FieldInfoBoxBaseTypeFactory));
                all.AddRange(ReflectionRunEditorUtil.GetFieldInfoBoxBaseTypeMethod());
                
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
                if (param.Length != 4)
                    return null;
                ;
                if (param[1].ParameterType != typeof(FieldInfo))
                    return null;
                return param[0].ParameterType;
            }

            private static InfoBaseGroupBox CreateFromMethodInfo(
                MethodInfo methodInfo, FieldInfo info, object obj)
            {
                try
                {
                    var groupObj = methodInfo.Invoke(null, new[] { null, info, obj ,true});
                    if (groupObj is InfoBaseGroupBox groupBox)
                    {
                        return groupBox;
                    }
                    else
                    {
                        Debug.LogError($"{info.FieldType} FieldInfoBoxPreciseTypeFactory method return error!");
                        return new FieldInfoGroupBox(info, obj,true);
                    }

                }
                catch (Exception e)
                {
                    Debug.LogError($"{info.FieldType} FieldInfoBoxPreciseTypeFactory method  error!\n{e.ToString()}");
                    return new FieldInfoGroupBox(info, obj,true);
                }
            }
            internal static InfoBaseGroupBox GetInfoRowFromFactory(FieldInfo info, object obj)
            {
                if (_preciseTypeMethod.TryGetValue(info.FieldType, out var methodInfo))
                {
                    return CreateFromMethodInfo(methodInfo, info, obj);
                }
                else
                {
                    foreach (var (type, value) in _baseTypeMethod)
                    {
                        if (type.IsAssignableFrom(info.FieldType))
                        {
                            return CreateFromMethodInfo(value, info, obj);
                        }
                    }
                }

                if (info.FieldType.IsEnum)
                    return ReflectionRunEditorUtil.GetEnumFieldInfoGroupBox(info, obj, true);
                return new FieldInfoGroupBox(info, obj,true);
            }
        }
    }
}