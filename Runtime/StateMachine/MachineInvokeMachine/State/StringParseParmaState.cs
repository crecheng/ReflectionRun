using System;

namespace ReflectionRun.StateMachine
{
    internal class StringParesPaemaState : MethodParmaState
    {
        internal StringParesPaemaState(MachineInvokeState perNode,char first) : base(perNode,first)
        {
        }
        
        public override MachineInvokeState Next(char data)
        {
            switch (data)
            {
                case ',':
                    return NextNode = new ParmaTransferState(this);
                case ')':
                    return NextNode = new EndParmaState(this);
                default:
                    _charList.Add(data);
                    return this;
            }
        }

        public override object GetValue(Type type,object data)
        {
            var str = GetString();
            return Convert.ChangeType(str, type);
        }
    }
}