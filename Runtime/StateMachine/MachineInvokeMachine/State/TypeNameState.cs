using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ReflectionRun.StateMachine
{
    internal class TypeNameState : MachineInvokeState
    {
        public bool IsMethod;
        public bool isEndChar;
        internal TypeNameState(MachineInvokeState perNode) : base(perNode)
        {
            _charList = new List<char>();
        }

        public override MachineInvokeState Next(char data)
        {
            switch (data)
            {
                case '.':
                    return NextNode = new TypeNameState(this);
                case '(':
                    IsMethod = true;
                    return NextNode= new StartParmaState(this);
                case ')':
                    isEndChar=true;
                    return NextNode=new EndParmaState(this);
            }

            _charList.Add(data);
            return this;
        }
        
        public override object GetValue(Type type,object data)
        {
            
            if (!IsMethod)
            {
                if (data is string name)
                    return NextNode.GetValue(null, $"{name}.{GetString()}");
                else
                    return NextNode.GetValue(null, GetString());

            }

            Type findType = null;
            if (data is string lastName)
            {
                var typeName = lastName;
                var all = AppDomain.CurrentDomain.GetAssemblies();
            
                foreach (var assembly in all)
                {
                    findType= assembly.GetType(typeName,false,true);
                    if(findType!=null)
                        break;
                }

                if (findType == null)
                    throw new Exception($"not find type {typeName}, need full name");
            }
            else
            {
                findType = data.GetType();
            }
            
            var methodName = GetString();
            var methodInfos= findType.GetMethods(
                BindingFlags.Static 
                | BindingFlags.NonPublic 
                | BindingFlags.Public
                | BindingFlags.Instance);

            MethodInfo methodInfo = null;
            foreach (var info in methodInfos)
            {
                if (info.Name == methodName)
                {
                    methodInfo = info;
                    break;
                }
            }
                
            if(methodInfo==null)
                throw new Exception($"not find methodInfo {methodName}, check method name");

            return MethodInvoke(methodInfo, data);

        }

        protected object MethodInvoke(MethodInfo methodInfo,object data)
        {
            var nextNode = NextNode;
            if (nextNode is StartParmaState)
            {
                var next = NextNode;
                List<object> parma = new List<object>();
                var parmaType = methodInfo.GetParameters();
                int parmaIndex = 0;
                int start = 0;
                while (true)
                {
                    next = next.NextNode;
                    if (start == 0 && parmaType.Length>0)
                    {
                        if (next is ParmaTransferState transferState)
                        {
                            parma.Add(transferState.GetValue(parmaType[parmaIndex].ParameterType,null));
                            parmaIndex++;
                        }
                    }

                    if (next is StartParmaState)
                        start++;
                    else if (next is EndParmaState)
                    {
                        if(start==0)
                            break;
                        else
                            start--;
                    }

                }

                var obj = methodInfo.Invoke(data, parma.ToArray());
                    
                return next.GetValue(null, obj);
            }
            else
            {
                return null;
            }
        }
    }
    

    internal class StartParmaState: MachineInvokeState
    {
        public override bool CanNext => false;

        internal StartParmaState(MachineInvokeState perNode) : base(perNode)
        {
        }

        public override MachineInvokeState Next(char data)
        {
            return NextNode=new ParmaTransferState(this);
        }

        public override object GetValue(Type type, object data)
        {


            return null;
        }


        public override void StringBuild(StringBuilder stringBuilder)
        {
            stringBuilder.Append('(');
            NextNode?.StringBuild(stringBuilder);
        }
        
    }
    
    internal class EndParmaState: MachineInvokeState
    {
        internal EndParmaState(MachineInvokeState perNode) : base(perNode)
        {
        }

        public override MachineInvokeState Next(char data)
        {
            if (data == '.')
                return NextNode = new TypeNameState(this);
            return NextNode = new ParmaTransferState(this);
        }

        public override void StringBuild(StringBuilder stringBuilder)
        {
            stringBuilder.Append(')');
            NextNode?.StringBuild(stringBuilder);
        }

        public override object GetValue(Type type, object data)
        {
            if (NextNode is TypeNameState { IsMethod: true })
                return NextNode.GetValue(type, data);
            return data;
        }
    }
    
    
}