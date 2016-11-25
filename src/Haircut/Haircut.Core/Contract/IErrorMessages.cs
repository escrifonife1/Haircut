namespace Haircut.Core.Contract
{
    public interface IErrorMessages
    {
        string ErrorMessage();
        bool HasMessageError();
        void SetErrorMessage(string message);
    }
}