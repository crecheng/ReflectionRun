using System;
using System.Reflection;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    public class MethodParmaValueField : ValueBaseField
    {
        protected ParameterInfo _parameterInfo;

        protected MethodParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(isEditor)
        {
            _parameterInfo = parameterInfo;
            CreateVisualElement();
        }
    }

    public class MethodParmaValueField<T> : MethodParmaValueField where T : VisualElement, new()
    {
        protected T _field;

        protected MethodParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        protected override void OnCreateVisualElement()
        {
            _label = GetLabel();
            if (_label != null)
                layoutBox.Add(_label);
            _field = GetField();
            layoutBox.Add(_field);
            tooltip = _parameterInfo.ParameterType.ToString();
        }

        protected override Label GetLabel()
        {
            var label = new Label();
            label.text = _parameterInfo.Name;
            label.style.width = 100;
            return label;
        }

        protected virtual T GetField()
        {
            var field = new T();
            field.style.width = 100;
            return field;
        }
    }

    internal class EnumMethodParmaValueField : MethodParmaValueField<EnumField>
    {
        public EnumMethodParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {
        }

        protected override EnumField GetField()
        {
            var field = base.GetField();
            field.Init((Enum)Activator.CreateInstance(_parameterInfo.ParameterType));
            return field;
        }

        public override object Value => _field.value;
    }

    #region 基础类型

    internal class StringMethodParmaValueField : MethodParmaValueField<TextField>
    {
        public StringMethodParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        public override object Value => _field.value;
    }

    internal class BoolMethodParmaValueField : MethodParmaValueField<Toggle>
    {
        public BoolMethodParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        public override object Value => _field.value;
    }

    internal class IntegerMethodParmaValueField : MethodParmaValueField<IntegerField>
    {
        public IntegerMethodParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        public override object Value => _field.value;
    }

    internal class UnsignedMethodParmaValueField : MethodParmaValueField<UnsignedIntegerField>
    {
        public UnsignedMethodParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        public override object Value => _field.value;
    }

    internal class LongMethodParmaValueField : MethodParmaValueField<LongField>
    {
        public LongMethodParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        public override object Value => _field.value;
    }

    internal class UnsignedLongParmaValueField : MethodParmaValueField<UnsignedLongField>
    {
        public UnsignedLongParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        public override object Value => _field.value;
    }

    internal class FloatParmaValueField : MethodParmaValueField<FloatField>
    {
        public FloatParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        public override object Value => _field.value;
    }

    internal class DoubleParmaValueField : MethodParmaValueField<DoubleField>
    {
        public DoubleParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        public override object Value => _field.value;
    }

    #endregion

    #region Unity基础类型

    internal class Vector2ParmaValueField : MethodParmaValueField<Vector2Field>
    {
        public Vector2ParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        protected override Vector2Field GetField()
        {
            var field = new Vector2Field();
            field.style.width = 150;
            field[0].style.width = 0;
            return field;
        }

        public override object Value => _field.value;
    }

    internal class Vector3ParmaValueField : MethodParmaValueField<Vector3Field>
    {
        public Vector3ParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        protected override Vector3Field GetField()
        {
            var field = new Vector3Field();
            field.style.width = 200;
            field[0].style.width = 0;
            return field;
        }

        public override object Value => _field.value;
    }

    internal class Vector4ParmaValueField : MethodParmaValueField<Vector4Field>
    {
        public Vector4ParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        protected override Vector4Field GetField()
        {
            var field = new Vector4Field();
            field.style.width = 200;
            field[0].style.width = 0;
            return field;
        }

        public override object Value => _field.value;
    }

    internal class RectParmaValueField : MethodParmaValueField<RectField>
    {
        public RectParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        protected override RectField GetField()
        {
            var field = new RectField();
            field.style.width = 200;
            field[0].style.width = 0;
            return field;
        }

        public override object Value => _field.value;
    }

    internal class Vector2IntParmaValueField : MethodParmaValueField<Vector2IntField>
    {
        public Vector2IntParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        protected override Vector2IntField GetField()
        {
            var field = new Vector2IntField();
            field.style.width = 150;
            field[0].style.width = 0;
            return field;
        }

        public override object Value => _field.value;
    }

    internal class Vector3IntParmaValueField : MethodParmaValueField<Vector3IntField>
    {
        public Vector3IntParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        protected override Vector3IntField GetField()
        {
            var field = new Vector3IntField();
            field.style.width = 200;
            field[0].style.width = 0;
            return field;
        }

        public override object Value => _field.value;
    }

    internal class RectIntParmaValueField : MethodParmaValueField<RectIntField>
    {
        public RectIntParmaValueField(ParameterInfo parameterInfo, bool isEditor) : base(parameterInfo, isEditor)
        {

        }

        protected override RectIntField GetField()
        {
            var field = new RectIntField();
            field.style.width = 200;
            field[0].style.width = 0;
            return field;
        }

        public override object Value => _field.value;
    }

    #endregion
}