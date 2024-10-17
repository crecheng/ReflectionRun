using System;
using System.Collections.Generic;
using System.Reflection;
using ReflectionRun.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun.Editor
{
    public class ReflectionRunMainEditorWindow : EditorWindow
    {
        [MenuItem("Tools/ReflectionRun/MainWindow", false, 101)]
        public static void ShowExample()
        {
            ReflectionRunMainEditorWindow wnd = GetWindow<ReflectionRunMainEditorWindow>();
            wnd.titleContent = new GUIContent("ReflectionRunMainWindow");
        }

        private void CreateGUI()
        {
            EditorFactory.Init();
            var main = new ReflectionRunMainEditorWindowInternal(true);
            rootVisualElement.Add(main);
        }
        
        /// <summary>
        /// 编辑器上的，除了运行时支持的，还有编辑器相关的
        /// </summary>
        private class ReflectionRunMainEditorWindowInternal : ReflectionRunMainWindow
        {
            public ReflectionRunMainEditorWindowInternal(bool isEditor) : base(isEditor)
            {
            }

            protected override List<MethodInfo> GetCreateMethods()
            {
                var all = new List<MethodInfo>();
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(typeof(ModuleFactory)));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_ModuleFactory));
                
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(typeof(EditorModuleFactory)));
                all.AddRange(ReflectionRunCenter.GetStaticNoPublicMethodInfo(ReflectionRunExtensionsUtil.RR_EditorModuleFactory));
                
                return all;
            }
        }
    }
}