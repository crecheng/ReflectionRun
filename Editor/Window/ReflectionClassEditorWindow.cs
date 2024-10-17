using System;
using UnityEditor;

namespace ReflectionRun.Editor
{
    public class ReflectionClassEditorWindow : EditorWindow
    {
        public ReflectionClassWindow Window { get; protected set; }

        private void CreateGUI()
        {
            EditorFactory.Init();
            Window = new ReflectionClassWindow(true);
            rootVisualElement.Add(Window);
        }

        private void OnInspectorUpdate()
        {
            Window.Updata();
        }

        private void OnDestroy()
        {
            Window.OnDestroy();
        }
    }
}