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

        public ActionResult Invoice()
        {
            FruitShopDBEntities db = new FruitShopDBEntities();
            List<Order> orderList = db.Orders.ToList();
            List<Customer> customerList = db.Customers.ToList();
            List<OrderDetail> orderdetaillist = db.OrderDetails.ToList();

            ViewData["jointables"] = from order in orderList
                                     join customer in customerList on order.CustomerId equals customer.CustomerId into table1
                                     from customer in table1.DefaultIfEmpty()
                                     select new MultipleTableJoin { orderlist = order, customerlist = customer };
          
            return View(ViewData["jointables"]);
        }
        
        [HttpGet]
        public JsonResult getItemUnitPrice(int itemId)
        {
            decimal UnitPrice = objfruitShopDBEntities.Items.Single(model => model.ItemId == itemId).ItemPrice;
            return Json(UnitPrice, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(OrderViewModel objOrderViewModel)
        {
            OrderRepository orderRepository = new OrderRepository();
            orderRepository.AddOrder(objOrderViewModel);
            return Json("Successfully Ordered", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult myModal(int Id)
        {
            Order order = objfruitShopDBEntities.Orders.Find(Id);
            List<OrderDetail> orderdetaillist = objfruitShopDBEntities.OrderDetails.ToList();
            var filterd = new List<OrderDetail>();
            foreach (var item in orderdetaillist)
            {
                if (item.OrderId==Id)
                {
                    filterd.Add(item);
                }
            }

            if (order == null)
            {
                return HttpNotFound();
            }
            else
            {
                return PartialView("_myModal",order);
            }
            

        }
    }
}