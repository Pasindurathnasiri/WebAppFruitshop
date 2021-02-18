using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppFruitshop.Models;
using WebAppFruitshop.Repositories;
using WebAppFruitshop.ViewModel;

namespace WebAppFruitshop.Controllers
{
    public class BaseController : Controller
    {
        private FruitShopDBEntities objfruitShopDBEntities;
        public BaseController()
        {
            objfruitShopDBEntities = new FruitShopDBEntities();
        }
        // GET: Base
        public ActionResult Index()
        {

            CustomerRepository objCustomerRepository = new CustomerRepository();
            ItemRepository objitemRepository = new ItemRepository();
            PaymentTypeRepository objpaymentTypeRepository = new PaymentTypeRepository();
            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
                (objCustomerRepository.GetAllCustomers(),objitemRepository.GetAllItems(),objpaymentTypeRepository.GetAllPaymentTypes());
            return View(objMultipleModels);
        }
        
        [HttpGet]
        public JsonResult getItemUnitPrice(int itemId)
        {
            decimal UnitPrice = objfruitShopDBEntities.Items.Single(model => model.ItemId == itemId).ItemPrice;
            return Json(UnitPrice, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Index(OrderViewModel objOrderViewModel)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}