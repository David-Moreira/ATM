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

        public OperationsController(IOperationService operationsManager, IBankAccountService bankManager )
        {
            _bankManager = bankManager;
            _operationsManager = operationsManager;
        }
        // GET: Operations
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            
            ViewBag.accountNumber = _bankManager.GetByUserId(userID).AccountNumber;
            return View();
        }

        [HttpGet]
        public ActionResult Withdraw(int accountNumber)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Withdraw(TransactionViewModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                _operationsManager.Withdraw(transactionModel.Amount);
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
                _operationsManager.Deposit(transactionModel.Amount);
                return View("Index");
            }
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(TransactionViewModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                _operationsManager.Payment(transactionModel.accountNumber, transactionModel.Amount);
                return View("Index");
            }
            return View();
        }

        public ActionResult TransferFunds()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TransferFunds(TransactionViewModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                _operationsManager.TransferFunds(transactionModel.accountNumber, transactionModel.Amount);
                return View("Index");
            }
            return View();
        }

        public ActionResult QuickCash(string accountNumber)
        {
            if (accountNumber != "")
            {
                _operationsManager.QuickCash();
                return View();
            }
            return View("Index");
        }

        public ActionResult PrintStatement(string accountNumber)
        {
            if (accountNumber != "")
            {
                string statement = _operationsManager.PrintStatement();
                return View((object)statement);
            }
            return View("Index");
        }
    }
}