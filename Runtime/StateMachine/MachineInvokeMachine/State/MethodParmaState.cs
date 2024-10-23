using System;
using System.Collections.Generic;

namespace ReflectionRun.StateMachine
{
    internal class MethodParmaState : MachineInvokeState
    {
        public MethodParmaState NextMethodParma;

        public bool IsName;

        internal MethodParmaState(MachineInvokeState perNode,char first) : base(perNode)
        {
            _charList = new List<char>();
            _charList.Add(first);
        }

        public override MachineInvokeState Next(char data)
        {
            switch (data)
            {
                case '.' : 
                    IsName = true;
                    return NextNode = new TypeNameState(this);
                case ',':
                    return NextNode = new ParmaTransferState(this);
                default:
                    _charList.Add(data);
                    return this;
            }
        }

        public override object GetValue(Type type,object data)
        {

            return NextNode.GetValue(type, GetString());
        }
    }
}