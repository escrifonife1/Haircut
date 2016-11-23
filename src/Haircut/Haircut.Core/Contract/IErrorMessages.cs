namespace Haircut.Core.Contract
{
    public interface IErrorMessages
    {
        string ErrorMessage();
        void SetErrorMessage(string message);
    }
}