using ATM.Core.Entities;
using ATM.Core.Interfaces.Services;
using ATM.Core.Validation;
using ATM.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System;

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

        public string GetAccountNumber()
        { return (string)Session["AccountNumber"]; }

        private void SetAccountNumber(string value)
        { Session["AccountNumber"] = value; }

        [HttpGet]
        public ActionResult SelectAccount(string accountNumber)
        {
            //Don't forget to validate if account is related to user
            SetAccountNumber(accountNumber);
            return View("Index");
        }

        [HttpGet]
        public ActionResult AccountOperations()
        {
            ViewBag.AccountBalance = _bankManager.GetByAccountNumber(GetAccountNumber()).Balance;
            return View();
        }

        [HttpGet]
        public ActionResult Accounts()
        {
            List<AvailableAccountsViewModel> availableAccounts = new List<AvailableAccountsViewModel>();
            IEnumerable<BankAccount> bankAccounts = _bankManager.GetMultipleByUserId(User.Identity.GetUserId());
            foreach (var account in bankAccounts)
            {
                availableAccounts.Add(
                    new AvailableAccountsViewModel()
                    {
                        AccountNumber = account.AccountNumber,
                        AccountName = account.AccountName,
                        AccountBalance = account.Balance,
                        AccountHolder = account.AccountHolder
                    }
                );
            }
            return View(availableAccounts);
        }

        public ActionResult BackButton()
        {
            return PartialView("_BackPartial");
        }

        [HttpGet]
        public ActionResult Deposit()
        {
            ViewBag.AccountBalance = _bankManager.GetByAccountNumber(GetAccountNumber()).Balance;
            return View();
        }

        [HttpPost]
        public ActionResult DepositCash(TransactionViewModel transaction)
        {
            if (ModelState.IsValid)
            {
                _operationsManager.Deposit(GetAccountNumber(), transaction.Amount);
                return View("Success");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DepositCheck(TransactionViewModel transaction)
        {
            if (ModelState.IsValid)
            {
                _operationsManager.Deposit(GetAccountNumber(), transaction.Amount);
                return View("Success");
            }
            return View();
        }

        [HttpGet]
        public ActionResult DepositCash()
        {
            ViewBag.AccountBalance = _bankManager.GetByAccountNumber(GetAccountNumber()).Balance;
            return View();
        }

        [HttpGet]
        public ActionResult DepositCheck()
        {
            ViewBag.AccountBalance = _bankManager.GetByAccountNumber(GetAccountNumber()).Balance;
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
                _operationsManager.Payment(GetAccountNumber(), transactionModel.recipientAccountNumber, transactionModel.Amount);
                return View("Index");
            }
            return View();
        }

        public ActionResult PrintStatement()
        {
            string statement = _operationsManager.PrintStatement(GetAccountNumber());
            return View((object)statement);
        }

        [HttpGet]
        public ActionResult QuickCash()
        {
            var result = _operationsManager.QuickCash(GetAccountNumber());
            if (result.Succeeded)
                return View("Success");
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
                _operationsManager.TransferFunds(GetAccountNumber(), transactionModel.recipientAccountNumber, transactionModel.Amount);
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
                _operationsManager.Withdraw(GetAccountNumber(), transactionModel.Amount);
                return View("Index");
            }
            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (String.IsNullOrEmpty(GetAccountNumber()))
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