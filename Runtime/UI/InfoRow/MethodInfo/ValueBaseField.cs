using UnityEngine.UIElements;

namespace ReflectionRun
{
    public class ValueBaseField : VisualElement
    {
        protected Label _label;
        protected VisualElement layoutBox;
        protected bool _isEditor;
        protected ValueBaseField(bool isEditor)
        {
            _isEditor = isEditor;
        }

        protected void CreateVisualElement()
        {
            layoutBox = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            layoutBox.PaddingZero();
            Add(layoutBox);
            OnCreateVisualElement();
        }

        protected virtual void OnCreateVisualElement()
        {
            
        }
        protected virtual Label GetLabel()
        {
            return null;
        }
        public virtual object Value { get; }
    }
    
    public class ValueBaseField<T> : ValueBaseField where T : VisualElement
    {
        protected T _field;

        protected ValueBaseField(bool isEditor) : base(isEditor)
        {
            
        }
        protected override void OnCreateVisualElement()
        {
            _field = GetField();
            layoutBox.Add(_field);
        }
        
        protected virtual T GetField()
        {
            return null;
        }
    }
}