using ATM.Core.Interfaces.Services;
using ATM.Core.Validation;
using ATM.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ATM.Controllers
{
    [Authorize]
    public class OperationsController : Controller
    {
        private IBankAccountService _bankManager;

        private IOperationService _operationsManager;

        public OperationsController(IOperationService operationsManager, IBankAccountService bankManager)
        {
            _bankManager = bankManager;
            _operationsManager = operationsManager;
        }

        public int? GetAccountNumber()
        { return (int?)Session["AccountNumber"]; }

        private void SetAccountNumber(int? value)
        { Session["AccountNumber"] = value; }

        [HttpGet]
        public ActionResult SelectAccount(int accountNumber)
        {
            //Don't forget to validate if account is related to user
            SetAccountNumber(accountNumber);
            return View("Index");
        }

        [HttpGet]
        public ActionResult AccountOperations()
        {
            ViewBag.AccountBalance = _bankManager.GetById(GetAccountNumber().Value).Balance;
            return View();
        }

        [HttpGet]
        public ActionResult Accounts()
        {
            List<AvailableAccountsViewModel> availableAccounts = new List<AvailableAccountsViewModel>()
            {
                new AvailableAccountsViewModel()
                {
                    AccountID = 1,
                    AccountName = "DummyAccount"
                },
                new AvailableAccountsViewModel()
                {
                    AccountID   = 2,
                    AccountName = "AnotherDummyAccount"
                }
            };
            return View(availableAccounts);
        }

        public ActionResult BackButton()
        {
            return PartialView("_BackPartial");
        }

        [HttpGet]
        public ActionResult Deposit()
        {
            ViewBag.AccountBalance = _bankManager.GetById(GetAccountNumber().Value).Balance;
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(int amount)
        {
            if (ModelState.IsValid)
            {
                _operationsManager.Deposit(GetAccountNumber().Value, amount);
                return View("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult DepositCash()
        {
            ViewBag.AccountBalance = _bankManager.GetById(GetAccountNumber().Value).Balance;
            return View();
        }

        [HttpGet]
        public ActionResult DepositCheck()
        {
            ViewBag.AccountBalance = _bankManager.GetById(GetAccountNumber().Value).Balance;
            return View();
        }

        //[OutputCache(Duration = 86400, VaryByCustom = "UserSession")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(TransferFundsViewModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                _operationsManager.Payment(GetAccountNumber().Value, transactionModel.recipientAccountNumber, transactionModel.Amount);
                return View("Index");
            }
            return View();
        }

        public ActionResult PrintStatement()
        {
            string statement = _operationsManager.PrintStatement(GetAccountNumber().Value);
            return View((object)statement);
        }

        public ActionResult QuickCash()
        {
            var result = _operationsManager.QuickCash(GetAccountNumber().Value);
            if (result.Succeeded)
                return View();
            AddErrors(result);
            return View("Index");
        }

        [HttpGet]
        public ActionResult TransferFunds()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TransferFunds(TransferFundsViewModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                _operationsManager.TransferFunds(GetAccountNumber().Value, transactionModel.recipientAccountNumber, transactionModel.Amount);
                return View("Index");
            }
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
                _operationsManager.Withdraw(GetAccountNumber().Value, transactionModel.Amount);
                return View("Index");
            }
            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!GetAccountNumber().HasValue)
            {
                string userID = User.Identity.GetUserId();
                SetAccountNumber(_bankManager.GetByUserId(userID).AccountNumber);
            }
        }

        private void AddErrors(OperationResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}