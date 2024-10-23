namespace ReflectionRun.StateMachine
{
    internal interface IMachineState<T,TV>
    {
        public T Next(TV data);
    }
}