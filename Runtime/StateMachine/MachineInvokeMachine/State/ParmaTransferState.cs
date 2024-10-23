using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionRun.StateMachine
{
    internal class ParmaTransferState : MachineInvokeState
    {
        internal ParmaTransferState(MachineInvokeState perNode) : base(perNode)
        {
        }

        public override MachineInvokeState Next(char data)
        {
            switch (data)
            {
                case '.':
                    return NextNode = new TypeNameState(this);
                case '"': 
                    NextNode = new StringParmaStart(this,data);
                    return NextNode;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '-':
                    NextNode = new NumberParmaStart(this,data);
                    return NextNode;
                case ')':
                    return NextNode= new EndParmaState(this);
                default:
                    NextNode = new MethodParmaState(this,data);
                    return NextNode;
            }
        }

        public override object GetValue(Type type, object data)
        {
            return NextNode.GetValue(type, data);
        }

        public override void StringBuild(StringBuilder stringBuilder)
        {
            if(LastNode is not StartParmaState)
                stringBuilder.Append(',');
            NextNode?.StringBuild(stringBuilder);
        }
    }
    
    internal class StringParmaStart : MethodParmaState
    {
        private bool lastIsTransferChar;
        private bool endChar;
        internal StringParmaStart(MachineInvokeState perNode,char first) : base(perNode,first)
        {
            _charList.Clear();
        }

        public override MachineInvokeState Next(char data)
        {
            if (lastIsTransferChar)
            {
                lastIsTransferChar = false;
                switch (data)
                {
                    case 'n': 
                        _charList.Add('\n');
                        return this;
                    case '\\':
                        _charList.Add('\\');
                        return this;
                    case '"':
                        _charList.Add('"');
                        return this;
                }
            }
            switch (data)
            {
                case '\\':
                    lastIsTransferChar = true;
                    return this;
                case '"':
                    endChar = true;
                    return this;
                case ')':
                    return NextNode = new EndParmaState(this);
                case ',':
                    if (endChar)
                        return new ParmaTransferState(this);
                    
                    _charList.Add(data);
                    return this;
                default:
                    _charList.Add(data);
                    return this;
            }
        }

        public override object GetValue(Type type,object data)
        {
            return GetString();
        }
    }
    
    internal class NumberParmaStart : MethodParmaState
    {
        internal NumberParmaStart(MachineInvokeState perNode,char first) : base(perNode,first)
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