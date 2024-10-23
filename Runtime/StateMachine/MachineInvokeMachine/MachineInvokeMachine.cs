using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ReflectionRun.StateMachine
{
    internal class MachineInvokeMachine
    {
        public string Input;
        private MachineInvokeState _head;

        public MachineInvokeMachine(string input)
        {
            Input = input;
        }

        public void Parse()
        {
            _head = new TypeNameState(null);
            MachineInvokeState next = _head;
            int index = 0;
            char[] data = Input.ToCharArray();
            while (next!=null && index<data.Length)
            {
                var c = data[index];
                next = next.Next(data[index]);
                if (next!=null && next.CanNext)
                {
                    index++;
                }
                
            }

            StringBuilder stringBuilder = new StringBuilder();
            _head.StringBuild(stringBuilder);
            Debug.Log(stringBuilder.ToString());
            
        }

        public object Invoke()
        {
            return _head.GetValue(null,null);
        }
    }
}