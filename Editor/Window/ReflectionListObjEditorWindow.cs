using System;
using UnityEditor;

namespace ReflectionRun.Editor
{
    public class ReflectionListObjEditorWindow : EditorWindow
    {
        public ReflectionListObjWindow Window { get; protected set; }

        private void CreateGUI()
        {
            EditorFactory.Init();
            Window = new ReflectionListObjWindow(true);
            rootVisualElement.Add(Window);
        }

        private void OnDestroy()
        {
            Window.OnDestroy();
        }
    }
}