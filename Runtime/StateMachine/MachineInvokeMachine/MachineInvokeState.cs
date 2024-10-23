using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionRun.StateMachine
{
    internal class MachineInvokeState: IMachineState<MachineInvokeState, char>
    {
        protected List<char> _charList;
        public MachineInvokeState LastNode;
        public MachineInvokeState NextNode;

        public virtual bool CanNext => true;

        internal MachineInvokeState(MachineInvokeState perNode)
        {
            LastNode = perNode;
        }
        public virtual MachineInvokeState Next(char data)
        {
            return null;
        }

        public virtual object GetValue(Type type,object data)
        {
            return null;
        }

        public override string ToString()
        {
            return _charList==null? "" : string.Join(string.Empty,_charList);
        }

        public virtual void StringBuild(StringBuilder stringBuilder)
        {
            if (_charList != null)
            {
                foreach (var c in _charList)
                {
                    stringBuilder.Append(c);
                }
                
            }
            NextNode?.StringBuild(stringBuilder);
            
        }

        public virtual string GetString()
        {
            return new string(_charList.ToArray());
        }
    }
}