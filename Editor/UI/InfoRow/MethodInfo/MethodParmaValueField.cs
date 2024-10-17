using System;
using System.Reflection;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReflectionRun.Editor
{
    
    #region UnityObject
    
    internal class UnityObjectParmaValueField<T> : MethodParmaValueField<ObjectField> where T : UnityEngine.Object
    {
        protected UnityObjectParmaValueField(ParameterInfo parameterInfo,bool isEditor) : base(parameterInfo, isEditor)
        {
        }
        
        protected override Label GetLabel()
        {
            var label = new Label();
            label.text = _parameterInfo.Name;
            label.style.width = 70;
            return label;
        }
        
        protected override ObjectField GetField()
        {
            var field = base.GetField();
            field.objectType = typeof(T);
            field.style.width = 130;
            return field;
        }

        public override object Value => _field.value;
    }
    
    internal class UnityObjectParmaValueField : UnityObjectParmaValueField<UnityEngine.Object>
    {
        public UnityObjectParmaValueField(ParameterInfo parameterInfo,bool isEditor) : base(parameterInfo, isEditor)
        {
            
        }
    }
    
    internal class GameObjectParmaValueField : UnityObjectParmaValueField<GameObject>
    {
        public GameObjectParmaValueField(ParameterInfo parameterInfo,bool isEditor) : base(parameterInfo, isEditor)
        {
            
        }
    }
    
    internal class TransformParmaValueField : UnityObjectParmaValueField<Transform>
    {
        public TransformParmaValueField(ParameterInfo parameterInfo,bool isEditor) : base(parameterInfo, isEditor)
        {
            
        }
    }

    #endregion
}