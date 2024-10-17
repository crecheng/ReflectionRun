using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    public class ToggleButton : Button
    {
        public bool Value
        {
            set
            {
                _value = value;
                style.backgroundColor = _value ? _highColor : _grayColor;
            }
            get => _value;
        }

        private bool _value;
        private Action<bool> _onChange;
        private static readonly StyleColor _grayColor = new Color(0.2f, 0.2f, 0.2f, 1);
        private static readonly StyleColor _highColor  = Color.gray;
        public ToggleButton(string text, Action<bool> onChange=null)
        {
            var textLabel = new Label(text);
            Add(textLabel);
            style.backgroundColor = _grayColor;
            _onChange = onChange;
            clicked += Click;
            
        }

        private void Click()
        {
            _value = !_value;
            style.backgroundColor = _value ? _highColor : _grayColor;
            _onChange?.Invoke(_value);
        }
        
    }
}