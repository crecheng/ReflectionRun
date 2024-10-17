using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    /// <summary>
    /// MethodInfo显示
    /// </summary>
    public class MethodInfoGroupBox : InfoBaseGroupBox
    {
        protected MethodInfo _methodInfo;
        protected ParameterInfo[] _parameterInfos;
        protected MethodParmaValueField[] _valueInput;

        protected object _resultObj;

        protected ReflectionMethodScriptableObject _methodScriptable;

        protected bool _createParamInput;

        protected bool _isVoid;

        protected VisualElement _parmaRow;
        protected VisualElement _resultRow;
        private ValueView _resultValueView;

        public MethodInfoGroupBox(MethodInfo methodInfo, object ins, bool isEditor) : base(ins, isEditor)
        {
            _methodInfo = methodInfo;
            infoName = _methodInfo.Name.ToLower();
            _parameterInfos = _methodInfo.GetParameters();
            CreateGroupBox();
        }


        private void CreateGroupBox()
        {

            CreateInfoNameView();

            CreateMethodView();

            CreateMethodResultView();

            CreateParamInput();

        }

        private void CreateMethodView()
        {
            var groupBox = new GroupBox();
            groupBox.PaddingZero();
            groupBox.MarginZero();

            _parmaRow = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            _parmaRow.PaddingZero();
            groupBox.Add(_parmaRow);

            _resultRow = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            _resultRow.PaddingZero();
            groupBox.Add(_resultRow);

            layoutBox.Add(groupBox);
        }

        private void CreateInfoNameView()
        {
            var hLayout = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            hLayout.style.width = 300;
            hLayout.style.minWidth = 300;
            var declaringType = _methodInfo.DeclaringType;

            CreateDeclaringTypeView(hLayout, declaringType);

            var n = new Label(_methodInfo.Name);
            n.style.color = ReflectionRunUIUtil.MethodColor;
            n.style.unityFontStyleAndWeight = FontStyle.Bold;
            hLayout.Add(n);
            layoutBox.Add(hLayout);
        }

        protected virtual void CreateParamInput()
        {
            if (_parameterInfos.Length > 0)
            {
                bool canInput = true;
                for (var i = 0; i < _parameterInfos.Length; i++)
                {
                    canInput &= ReflectionRunCenter.CanCreateMethodParmaValueField(_isEditor,
                        _parameterInfos[i].ParameterType);
                    if (!canInput)
                        return;
                }

                _createParamInput = true;

                _valueInput = new MethodParmaValueField[_parameterInfos.Length];
                for (var i = 0; i < _parameterInfos.Length; i++)
                {
                    var input = ReflectionRunCenter.GetMethodParmaValueField(_isEditor, _parameterInfos[i]);
                    _valueInput[i] = input;
                    _parmaRow.Add(input);
                }

            }
        }

        private void CreateMethodResultView()
        {
            _parmaRow.Add(ReflectionRunUIUtil.GetButton($"Invoke({_parameterInfos.Length})", InvokeMethod,_isEditor));
            _isVoid = _methodInfo.ReturnType == typeof(void);
            if (_isVoid)
            {
                var typeLabel = new Label(_methodInfo.ReturnType.ToString());
                typeLabel.style.color = ReflectionRunUIUtil.InfoGroupBoxTypeColor;
                if (_parameterInfos.Length > 0)
                    _resultRow.Add(typeLabel);
                else
                    _parmaRow.Add(typeLabel);

            }
            else
            {
                _resultValueView = ReflectionRunCenter.GetMethodReturnValueView(_isEditor, _methodInfo.ReturnType);
                _resultValueView.GetReflectionScriptableObject =
                    () => _window.CreateViewScriptableObject<ReflectionScriptableObject>();
                _resultRow.Add(_resultValueView);
            }

        }

        protected virtual void RefreshResult()
        {
            if (_isVoid)
                return;
            _resultValueView.SetValue(_resultObj);
        }

        protected void InvokeMethod()
        {
            InvokeMethodInline();
        }

        private void InvokeMethodFromProperty()
        {
            if (_isVoid)
                _methodInfo.Invoke(_ins, _methodScriptable.param);
            else
            {
                _resultObj = _methodInfo.Invoke(_ins, _methodScriptable.param);
                RefreshResult();
            }
        }

        private object[] GetParmaFromInput()
        {
            object[] parma = new object[_parameterInfos.Length];
            for (var i = 0; i < _valueInput.Length; i++)
            {
                parma[i] = _valueInput[i].Value;
            }

            return parma;
        }

        protected virtual void InvokeMethodInline()
        {
            if (_parameterInfos.Length > 0)
            {
                //object Func( object[] parma )
                if (_createParamInput)
                    _resultObj = _methodInfo.Invoke(_ins, GetParmaFromInput());
                else
                    InvokeMethodOther();
            }
            else
            {
                //object Func()
                _resultObj = _methodInfo.Invoke(_ins, null);
            }

            if (!_isVoid)
                RefreshResult();
        }

        protected virtual void InvokeMethodOther()
        {
            if (!_methodScriptable)
            {
                _methodScriptable = _window.CreateViewScriptableObject<ReflectionMethodScriptableObject>();
                _methodScriptable.param = new object[_parameterInfos.Length];
                _methodScriptable.types = new Type[_parameterInfos.Length];
                for (var i = 0; i < _methodScriptable.param.Length; i++)
                {
                    var type = _parameterInfos[i].ParameterType;
                    _methodScriptable.types[i] = type;
                    if (type.IsValueType)
                        _methodScriptable.param[i] = Activator.CreateInstance(type);

                }

                _methodScriptable.OnInvoke = InvokeMethodFromProperty;
            }

            ReflectionRunCenter.OpenPropertyEditor(_isEditor, _methodScriptable);
        }

        public void ShowResultType()
        {
            if (_resultObj == null)
            {
                ReflectionRunCenter.CreateClassWindow(_isEditor, _methodInfo.ReturnType);
            }
            else
            {
                ReflectionRunCenter.CreateClassWindow(_isEditor, _resultObj);
            }
        }

    }
}