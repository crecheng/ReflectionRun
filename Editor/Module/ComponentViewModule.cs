using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun.Editor
{
    public class ComponentViewModule: IReflectionModule
    {
        public bool IsOnlyEditor => true;
        private ReflectionRunMainWindow _window;
        private ObjectField _objectField;
        private bool _isEditor;
        public void CreateModelVisualElement(VisualElement rootVisualElement,bool isEditor)
        {
            _isEditor = isEditor;
            var objFindBox = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            rootVisualElement.Add(objFindBox);
            objFindBox.Add(new Label("查看组件Type："));
            _objectField = new ObjectField();
            _objectField.objectType = typeof(Component);
            _objectField.allowSceneObjects = true;
            _objectField.style.width = 300;
            objFindBox.Add(_objectField);
            
            objFindBox.Add(ReflectionRunUIUtil.GetButton("查看",OnObjView,_isEditor));
        }

        public void SwitchType(Type type)
        {
            
        }

        private void OnObjView()
        {
            ReflectionRunCenter.EditorWindow.CreateClassWindow(_objectField.value);
        }

        public void Init(ReflectionRunMainWindow window)
        {
            _window = window;
        }
    }
}