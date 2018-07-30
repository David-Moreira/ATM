using ATM.Core.Validation;

namespace ATM.Core.Interfaces.Services
{
    public interface IOperationService
    {
        OperationResult Withdraw(int accountNumber, int amount);

        OperationResult Deposit(int accountNumber, int amount);

        OperationResult Payment(int accountNumber, int recipientAccountNumber, int amount);

        OperationResult TransferFunds(int accountNumber, int recipientAccountNumber, int amount);

        OperationResult QuickCash(int accountNumber);

        string PrintStatement(int accountNumber);
    }
}