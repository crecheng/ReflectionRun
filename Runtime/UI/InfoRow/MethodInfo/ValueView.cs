using System;
using UnityEngine;
using UnityEngine.UIElements;


namespace ReflectionRun
{
    public class ValueView : VisualElement
    {
        /// <summary>
        /// 默认Type
        /// </summary>
        protected Type _defaultType;
        public Func<ReflectionScriptableObject> GetReflectionScriptableObject;
        protected bool _isEditor;
        protected ValueView(bool isEditor)
        {
            _isEditor = isEditor;
        }

        /// <summary>
        /// 设置Type时
        /// </summary>
        /// <param name="defaultType"></param>
        public virtual void SetType(Type defaultType)
        {
            _defaultType = defaultType;
        }

        /// <summary>
        /// 设置Value
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetValue(object value)
        {

        }

        public virtual object Value => null;

    }

    public class ValueView<T> : ValueView where T : VisualElement
    {
        protected T _view;

        protected ValueView(bool isEditor): base(isEditor)
        {
            CreateVisualElement();
        }

        private void CreateVisualElement()
        {
            OnCreateVisualElement();
        }

        protected virtual void OnCreateVisualElement()
        {

        }

        public override void SetValue(object value)
        {

        }
    }
    
    internal class ObjectValueView : ValueView<Label>
    {
        protected VisualElement layoutBox;
        protected Button _propertiesBtn;
        protected Button _typeBtn;
        protected ReflectionScriptableObject _scriptable;

        protected object _value;

        internal ObjectValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override void OnCreateVisualElement()
        {
            layoutBox = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            layoutBox.PaddingZero();

            _typeBtn = ReflectionRunUIUtil.GetButton("", ShowType,_isEditor);
            _typeBtn.style.color = ReflectionRunUIUtil.InfoGroupBoxTypeColor;
            layoutBox.Add(_typeBtn);

            if(_isEditor)
            {
                _propertiesBtn = ReflectionRunUIUtil.GetButton("Properties", ShowProperties, _isEditor);
                layoutBox.Add(_propertiesBtn);
                _propertiesBtn.DisplayStyle(false);
            }

            _view = new Label("");
            layoutBox.Add(_view);

            Add(layoutBox);
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
            ReflectionRunCenter.OpenPropertyEditor( _isEditor,_scriptable);
        }

        public override void SetValue(object value)
        {
            _value = value;
            var isNull = value == null;
            if(_isEditor)
                _propertiesBtn.DisplayStyle(!isNull);
            if (isNull)
            {
                _typeBtn.text = _defaultType.ToString();
                _view.text = "Null";
            }
            else
            {
                _typeBtn.text = value.GetType().ToString();
                _view.text = value.ToString();
            }
        }

        public override void SetType(Type defaultType)
        {
            _defaultType = defaultType;
            _typeBtn.text = _defaultType.ToString();
        }

        public override object Value => _value;
    }

    internal class BaseValueTypeValueView<T> : ValueView<T> where T : VisualElement, new()
    {
        protected VisualElement layoutBox;
        protected Button _typeBtn;

        protected BaseValueTypeValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override void OnCreateVisualElement()
        {
            layoutBox = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            layoutBox.PaddingZero();

            _typeBtn = ReflectionRunUIUtil.GetButton("", ShowType,_isEditor);
            _typeBtn.style.color = ReflectionRunUIUtil.InfoGroupBoxTypeColor;
            layoutBox.Add(_typeBtn);

            _view = GetView();
            _view.DisplayStyle(false);
            layoutBox.Add(_view);

            Add(layoutBox);
        }

        protected virtual T GetView()
        {
            return new T();
        }

        protected virtual void ShowType()
        {
            ReflectionRunCenter.CreateClassWindow(_isEditor, _defaultType);
        }

        public override void SetType(Type defaultType)
        {
            _defaultType = defaultType;
            _typeBtn.text = _defaultType.ToString();
        }
    }
    
    internal class EnumValueView : BaseValueTypeValueView<EnumField>
    {
        internal EnumValueView(bool isEditor) : base(isEditor)
        {
        }
        protected override EnumField GetView()
        {
            var view = base.GetView();
            view.style.width = 100;
            return view;
        }

        public override void SetType(Type defaultType)
        {
            base.SetType(defaultType);
            _view.Init((Enum)Activator.CreateInstance(defaultType));
        }

        public override void SetValue(object value)
        {
            _view.value = (Enum)value;
            _view.DisplayStyle(true);
        }

        public override object Value => _view.value;
    }

    #region 基础类型
    
    internal class StringValueView : BaseValueTypeValueView<TextField>
    {
        internal StringValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override TextField GetView()
        {
            var view = base.GetView();
            view.style.width = 100;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (string)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }

    internal class BoolValueView : BaseValueTypeValueView<Toggle>
    {
        internal BoolValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override Toggle GetView()
        {
            var view = base.GetView();
            view.style.width = 100;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (bool)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }

    internal class IntegerValueView : BaseValueTypeValueView<IntegerField>
    {
        internal IntegerValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override IntegerField GetView()
        {
            var view = base.GetView();
            view.style.width = 100;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (int)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }

    internal class UnsignedIntegerValueView : BaseValueTypeValueView<UnsignedIntegerField>
    {
        internal UnsignedIntegerValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override UnsignedIntegerField GetView()
        {
            var view = base.GetView();
            view.style.width = 100;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (uint)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }

    internal class LongValueView : BaseValueTypeValueView<LongField>
    {
        internal LongValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override LongField GetView()
        {
            var view = base.GetView();
            view.style.width = 100;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (long)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }

    internal class UnsignedLongValueView : BaseValueTypeValueView<UnsignedLongField>
    {
        internal UnsignedLongValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override UnsignedLongField GetView()
        {
            var view = base.GetView();
            view.style.width = 100;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (ulong)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }

    internal class FloatValueView : BaseValueTypeValueView<FloatField>
    {
        internal FloatValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override FloatField GetView()
        {
            var view = base.GetView();
            view.style.width = 100;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (float)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }

    internal class DoubleValueView : BaseValueTypeValueView<DoubleField>
    {
        internal DoubleValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override DoubleField GetView()
        {
            var view = base.GetView();
            view.style.width = 100;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (double)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }

    #endregion

    #region Unity基础类型
    
    internal class Vector2FieldValueView : BaseValueTypeValueView<Vector2Field>
    {
        internal Vector2FieldValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override Vector2Field GetView()
        {
            var view = base.GetView();
            view.style.width = 150;
            view[0].style.width = 0;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (Vector2)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }

    internal class Vector3FieldValueView : BaseValueTypeValueView<Vector3Field>
    {
        internal Vector3FieldValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override Vector3Field GetView()
        {
            var view = base.GetView();
            view.style.width = 200;
            view[0].style.width = 0;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (Vector3)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }
    
    internal class Vector4FieldValueView : BaseValueTypeValueView<Vector4Field>
    {
        internal Vector4FieldValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override Vector4Field GetView()
        {
            var view = base.GetView();
            view.style.width = 250;
            view[0].style.width = 0;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (Vector4)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }
    
    internal class RectFieldValueView : BaseValueTypeValueView<RectField>
    {
        internal RectFieldValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override RectField GetView()
        {
            var view = base.GetView();
            view.style.width = 250;
            view[0].style.width = 0;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (Rect)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }
    
    internal class Vector2IntFieldValueView : BaseValueTypeValueView<Vector2IntField>
    {
        internal Vector2IntFieldValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override Vector2IntField GetView()
        {
            var view = base.GetView();
            view.style.width = 150;
            view[0].style.width = 0;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (Vector2Int)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }

    internal class Vector3IntFieldValueView : BaseValueTypeValueView<Vector3IntField>
    {
        internal Vector3IntFieldValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override Vector3IntField GetView()
        {
            var view = base.GetView();
            view.style.width = 200;
            view[0].style.width = 0;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (Vector3Int)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }
    
    internal class RectIntFieldValueView : BaseValueTypeValueView<RectIntField>
    {
        internal RectIntFieldValueView(bool isEditor) : base(isEditor)
        {
        }

        protected override RectIntField GetView()
        {
            var view = base.GetView();
            view.style.width = 250;
            view[0].style.width = 0;
            return view;
        }

        public override void SetValue(object value)
        {
            _view.value = (RectInt)value;
            _view.DisplayStyle(true);
        }
        public override object Value => _view.value;
    }
    
    #endregion
}