using System;
using System.Collections.Generic;
using ReflectionRun.StateMachine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    public class StringToMethodInvokeModule : IReflectionModule
    {
        private List<MachineInvokeMachine> _dataList;
        protected TextField _cmdInput;
        protected ReflectionRunMainWindow _window;
        protected GroupBox _resultVauleView;
        private bool _isEditor;
        public void CreateModelVisualElement(VisualElement rootVisualElement,bool isEditor)
        {
            _isEditor = isEditor;
            var stringFind = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            rootVisualElement.Add(stringFind);
            
            stringFind.Add(new Label("解析运行："));
            _cmdInput = new TextField();
            _cmdInput.style.width = 300;
            stringFind.Add(_cmdInput);

            var findBtn = ReflectionRunUIUtil.GetButton("Invoke", Invoke, _isEditor);
            stringFind.Add(findBtn);

            _resultVauleView = ReflectionRunUIUtil.GetNoPaddingGroupBox();
            _resultVauleView.DisplayStyle(false);
            rootVisualElement.Add(_resultVauleView);
        }

        public void SwitchType(Type type)
        {
            
        }

        private void Invoke()
        {
            var input = _cmdInput.value.Trim();
            if(string.IsNullOrEmpty(input))
                return;
            var machine = new MachineInvokeMachine(input);
            machine.Parse();
            var obj= machine.Invoke();
            _resultVauleView.DisplayStyle(true);
            _resultVauleView.Clear();
            if (obj != null)
            {
                var valueView = ReflectionRunCenter.GetMethodReturnValueView(_isEditor, obj.GetType(), false);
                if (valueView == null)
                    valueView = ReflectionRunCenter.GetDeepValueView(_isEditor, obj.GetType());
                valueView.SetValue(obj);
                _resultVauleView.Add(valueView);
            }
            else
            {
                _resultVauleView.Add(new Label("Null"));
            }
        }
        

        public void Init(ReflectionRunMainWindow window)
        {
            _window = window;
        }
    }
}