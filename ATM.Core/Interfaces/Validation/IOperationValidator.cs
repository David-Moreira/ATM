using ATM.Core.Validation;

namespace ATM.Core.Interfaces.Validation
{
    public interface IOperationValidator
    {
        OperationResult ValidateUserAccount(string userID, int accountNumber);
        OperationResult ValidateAmount(int amount);
        OperationResult ValidateAmountForPayment(int balance, int amountToSubtract);
    }
}
