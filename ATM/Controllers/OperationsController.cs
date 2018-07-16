using ATM.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace ATM.Controllers
{
    [Authorize]
    public class OperationsController : Controller
    {



        // GET: Operations
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            userBankAccountsViewModel userAccount = new userBankAccountsViewModel(); //Probably won't be used as viewmodel nvm
            userAccount.getuserBankAccount(userID);
            ViewBag.accountNumber = userAccount.userBankAccount;
            return View(userAccount);
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
                OperationsModel operation = new OperationsModel(transactionModel.accountNumber);
                operation.Withdraw(transactionModel.Amount);
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
                OperationsModel operation = new OperationsModel(transactionModel.accountNumber);
                operation.Deposit(transactionModel.Amount);
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
                OperationsModel operation = new OperationsModel(transactionModel.accountNumber);
                operation.Payment(transactionModel.accountNumber, transactionModel.Amount);
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
                OperationsModel operation = new OperationsModel(transactionModel.accountNumber);
                operation.TransferFunds(transactionModel.accountNumber, transactionModel.Amount);
                return View("Index");
            }
            return View();
        }

        public ActionResult QuickCash(string accountNumber)
        {
            if (accountNumber != "")
            {
                OperationsModel operation = new OperationsModel(accountNumber);
                operation.QuickCash();
                return View();
            }
            return View("Index");
        }

        public ActionResult PrintStatement(string accountNumber)
        {
            if (accountNumber != "")
            {
                OperationsModel operation = new OperationsModel(accountNumber);
                string statement = operation.PrintStatement();
                return View((object)statement);
            }
            return View("Index");
        }
    }
}