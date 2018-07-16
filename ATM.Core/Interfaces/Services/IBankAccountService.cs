using ATM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Core.Interfaces.Services
{
    public interface IBankAccountService : IServiceBase<BankAccount>
    {
        BankAccount GetByUserId(string id);
    }
}
