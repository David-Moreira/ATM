using ATM.Core.Interfaces.Services;
using ATM.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace ATM.Controllers
{
    [Authorize]
    public class OperationsController : Controller
    {
        private IBankAccountService _bankManager;
        private IOperationService _operationsManager;

        public int AccountNumber
        {
            get { return (int)Session["AccountNumber"]; }
            private set { Session["AccountNumber"] = value; }
        }

        public OperationsController(IOperationService operationsManager, IBankAccountService bankManager)
        {
            _bankManager = bankManager;
            _operationsManager = operationsManager;
        }

        //[OutputCache(Duration = 86400, VaryByCustom = "UserSession")]
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            AccountNumber = _bankManager.GetByUserId(userID).AccountNumber;
            return View();
        }

        [HttpGet]
        public ActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Withdraw(TransactionViewModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                _operationsManager.Withdraw(AccountNumber, transactionModel.Amount);
                return View("Index");
            }
            return View();
        }

        public ActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(TransactionViewModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                _operationsManager.Deposit(AccountNumber, transactionModel.Amount);
                return View("Index");
            }
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(TransferFundsViewModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                _operationsManager.Payment(AccountNumber, transactionModel.recipientAccountNumber, transactionModel.Amount);
                return View("Index");
            }
            return View();
        }

        public ActionResult TransferFunds()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TransferFunds(TransferFundsViewModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                _operationsManager.TransferFunds(AccountNumber, transactionModel.recipientAccountNumber, transactionModel.Amount);
                return View("Index");
            }
            return View();
        }

        public ActionResult QuickCash()
        {
            _operationsManager.QuickCash(AccountNumber);
            return View();
        }

        public ActionResult PrintStatement()
        {
            string statement = _operationsManager.PrintStatement(AccountNumber);
            return View((object)statement);
        }
    }
}