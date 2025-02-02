namespace WebApp.ChainOfResponsibility.ChainOfRes
{
    public abstract class ProcessHandler : IProcessHandler
    {
        private IProcessHandler? _nextHandler;
        public virtual object Handle(object request)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(request);
            }
            return null!;
        }

        public IProcessHandler SetNext(IProcessHandler handler)
        {
            _nextHandler = handler;
            return _nextHandler;
        }
    }

}
