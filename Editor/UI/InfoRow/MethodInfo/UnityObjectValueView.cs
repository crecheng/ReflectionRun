using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace ReflectionRun.Editor
{
    #region UnityObject
    
    internal class UnityObjectValueView<T> : ValueView<ObjectField> where T : UnityEngine.Object
    {
        protected VisualElement layoutBox;
        protected Button _propertiesBtn;
        protected Button _typeBtn;
        protected ReflectionScriptableObject _scriptable;

        protected object _value;

        internal UnityObjectValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override void OnCreateVisualElement()
        {
            layoutBox = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            layoutBox.PaddingZero();

            _typeBtn = ReflectionRunUIUtil.GetButton("", ShowType,_isEditor);
            _typeBtn.style.color = ReflectionRunUIUtil.InfoGroupBoxTypeColor;
            layoutBox.Add(_typeBtn);
            
            _view = GetField();
            _view.visible = false;
            layoutBox.Add(_view);
            
            _propertiesBtn = ReflectionRunUIUtil.GetButton("Properties", ShowProperties,_isEditor);
            layoutBox.Add(_propertiesBtn);
            _propertiesBtn.visible = false;

            Add(layoutBox);
        }

        protected virtual ObjectField GetField()
        {
            var field = new ObjectField();
            field.style.width = 150;
            field.objectType = typeof(T);
            return field;
        }

        protected virtual void ShowType()
        {
            if (_value == null)
            {
                ReflectionRunCenter.CreateClassWindow(_isEditor, _defaultType);
            }
            else
            {
                ReflectionRunCenter.CreateClassWindow(_isEditor, _value);
            }
        }

        protected virtual void ShowProperties()
        {
            if (!_scriptable)
                _scriptable = GetReflectionScriptableObject.Invoke();
            _scriptable.Obj = _value;
            EditorUtility.OpenPropertyEditor(_scriptable);
        }

        public override void SetValue(object value)
        {
            _value = value;
            var isNull = value == null;
            _view.visible = true;
            _propertiesBtn.visible = !isNull;
            if (isNull)
            {
                _typeBtn.text = _defaultType.ToString();
                _view.value = null;
            }
            else
            {
                _typeBtn.text = value.GetType().ToString();
                _view.value = value as Object;
            }
        }

        public override void SetType(Type defaultType)
        {
            _defaultType = defaultType;
            _typeBtn.text = _defaultType.ToString();
        }
        
        public override object Value => _view.value;
    }
    internal class UnityObjectValueView : UnityObjectValueView<UnityEngine.Object>
    {
        internal UnityObjectValueView(bool isEditor) : base(isEditor)
        {
        }
    }

    internal class GameObjectValueView : UnityObjectValueView<GameObject>
    {
        internal GameObjectValueView(bool isEditor) : base(isEditor)
        {
        }
    }
    
    internal class TransformValueView : UnityObjectValueView<Transform>
    {
        internal TransformValueView(bool isEditor) : base(isEditor)
        {
        }
    }
    
    #endregion
}