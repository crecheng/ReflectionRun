using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace ReflectionRun.Editor
{
    internal class ReflectionRunOpenEditorWindow : IReflectionRunOpenWindow
    {
        public VisualElement CreateClassWindow(Type type)
        {
            ReflectionClassEditorWindow wnd = EditorWindow.CreateWindow<ReflectionClassEditorWindow>(typeof(ReflectionClassEditorWindow));
            wnd.titleContent = new GUIContent(type.Name);
            wnd.Window.SetType(type);
            return wnd.rootVisualElement;
        }
        public  VisualElement CreateClassWindow(object obj)
        {
            ReflectionClassEditorWindow wnd = EditorWindow.CreateWindow<ReflectionClassEditorWindow>(typeof(ReflectionClassEditorWindow));
            wnd.titleContent = new GUIContent(obj.GetType().Name);
            wnd.Window.SetObject(obj);
            return wnd.rootVisualElement;
        }
        public VisualElement CreateListViewWindow(IList list)
        {
            ReflectionListObjEditorWindow wnd = EditorWindow.CreateWindow<ReflectionListObjEditorWindow>(typeof(ReflectionListObjEditorWindow));
            wnd.titleContent = new GUIContent(list.GetType().Name);
            wnd.Window.SetList(list);
            return wnd.rootVisualElement;
        }

        public void OpenPropertyEditor(Object obj)
        {
            EditorUtility.OpenPropertyEditor(obj);
        }
    }
}