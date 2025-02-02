namespace WebApp.ChainOfResponsibility.ChainOfRes
{
    public interface IProcessHandler
    {
        IProcessHandler SetNext(IProcessHandler handler);
        object Handle(object request);
    }
}
