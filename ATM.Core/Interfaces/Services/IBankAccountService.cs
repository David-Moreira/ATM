using ATM.Core.Entities;

namespace ATM.Core.Interfaces.Services
{
    public interface IBankAccountService : IServiceBase<BankAccount>
    {
        BankAccount GetByUserId(string id);
    }
}