using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun
{
    /// <summary>
    /// list的显示窗口
    /// </summary>
    public class ReflectionListObjWindow : VisualElement
    {
        private VisualElement _view;
        private IList _list;
        private List<ScriptableObject> _scriptables;

        public Action<VisualElement> OnClose;
        
        public bool IsEditor { get; protected set; }
        public VisualElement rootVisualElement => this;
        public ReflectionListObjWindow(bool isEditor)
        {
            IsEditor = isEditor;
            CreateGUI();
        }
        
        private void CreateGUI()
        {
            if (!IsEditor)
            {
                var btn = ReflectionRunUIUtil.GetButton("关闭", OnDestroy,IsEditor);
                btn.style.minHeight = 50;
                btn.style.height = 50;
                rootVisualElement.Add(btn);
            }
            _scriptables = new List<ScriptableObject>();
            var hBtn = ReflectionRunUIUtil.GetHorizontalLayoutGroup();
            _view = new ScrollView();
            rootVisualElement.Add(_view);
        }

        public void SetList(IList list)
        {
            _list = list;
            Refresh();
        }

        private void Refresh()
        {
            _view.Clear();
            for (var i = 0; i < _list.Count; i++)
            {
                var obj = _list[i];
                var view = new ListObjValue(IsEditor);
                view.GetReflectionScriptableObject = CreateViewScriptableObject<ReflectionScriptableObject>;
                view.SetValue(i, obj);
                view.OnApply = ApplyValue;
                _view.Add(view);
                
            }
        }

        private void ApplyValue(int index, object value)
        {
            _list[index] = value;
        }
        
        public T CreateViewScriptableObject<T>() where T : ScriptableObject
        {
            var obj = ScriptableObject.CreateInstance<T>();
            _scriptables.Add(obj);
            return obj as T;
        }
        
        public void OnDestroy()
        {
            _list = null;

            foreach (var scriptable in _scriptables)
            {
                UnityEngine.Object.DestroyImmediate(scriptable);
            }
            _scriptables?.Clear();
            OnClose?.Invoke(this);
        }
        
    }
}