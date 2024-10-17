using System;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    public class StringFindModule : IReflectionModule
    {
        protected TextField _cmdInput;
        protected ReflectionRunMainWindow _window;
        private bool _isEditor;
        public void CreateModelVisualElement(VisualElement rootVisualElement,bool isEditor)
        {
            _isEditor = isEditor;
            var stringFind = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            rootVisualElement.Add(stringFind);
            
            stringFind.Add(new Label("字符串查找Type："));
            _cmdInput = new TextField();
            _cmdInput.style.width = 300;
            stringFind.Add(_cmdInput);

            var findBtn = ReflectionRunUIUtil.GetButton("查找", OnStringFind, _isEditor);
            stringFind.Add(findBtn);
        }

        private void OnStringFind()
        {
            _window.OnStringFind(_cmdInput.value,this);
        }

        public virtual void SwitchType(Type type)
        {
            if (_isEditor)
                ReflectionRunCenter.EditorWindow.CreateClassWindow(type);
            else
                ReflectionRunCenter.RuntimeWindow.CreateClassWindow(type);
            
        }

        public void Init(ReflectionRunMainWindow window)
        {
            _window = window;
        }
    }
}