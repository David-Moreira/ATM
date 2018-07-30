using ATM.Core.Interfaces.Services;
using ATM.Core.Validation;
using ATM.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace ATM.Controllers
{
    [Authorize]
    public class OperationsController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!AccountNumber.HasValue)
            {
                string userID = User.Identity.GetUserId();
                AccountNumber = _bankManager.GetByUserId(userID).AccountNumber;
            }
        }

        private void AddErrors(OperationResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IBankAccountService _bankManager;
        private IOperationService _operationsManager;

        public int? AccountNumber
        {
            get { return (int?)Session["AccountNumber"]; }
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

            return View();
        }

        public ActionResult Accounts()
        {
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
                _operationsManager.Withdraw(AccountNumber.Value, transactionModel.Amount);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Deposit()
        {
            ViewBag.AccountBalance = _bankManager.GetById(AccountNumber.Value).Balance;
            return View();
        }

        [HttpGet]
        public ActionResult DepositCash()
        {
            ViewBag.AccountBalance = _bankManager.GetById(AccountNumber.Value).Balance;
            return View();
        }

        [HttpGet]
        public ActionResult DepositCheck()
        {
            ViewBag.AccountBalance = _bankManager.GetById(AccountNumber.Value).Balance;
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(int amount)
        {
            if (ModelState.IsValid)
            {
                _operationsManager.Deposit(AccountNumber.Value, amount);
                return RedirectToAction("Index");
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
                _operationsManager.Payment(AccountNumber.Value, transactionModel.recipientAccountNumber, transactionModel.Amount);
                return RedirectToAction("Index");
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
                _operationsManager.TransferFunds(AccountNumber.Value, transactionModel.recipientAccountNumber, transactionModel.Amount);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult QuickCash()
        {
            var result = _operationsManager.QuickCash(AccountNumber.Value);
            if (result.Succeeded)
                return View();
             AddErrors(result);
             return RedirectToAction("Index");
        }

        public ActionResult PrintStatement()
        {
            string statement = _operationsManager.PrintStatement(AccountNumber.Value);
            return View((object)statement);
        }
    }
}