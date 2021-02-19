using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppFruitshop.Models;
using WebAppFruitshop.ViewModel;


namespace WebAppFruitshop.Repositories
{
    public class OrderRepository
    {
        private FruitShopDBEntities objfruitShopDBEntities;
        public OrderRepository()
        {
            objfruitShopDBEntities = new FruitShopDBEntities();
        }

        public bool AddOrder(OrderViewModel objOrderViewModel)
        {
            Order objOrder = new Order();

            objOrder.CustomerId = objOrderViewModel.CustomerId;
            objOrder.FinalTotal = objOrderViewModel.FinalTotal;
            objOrder.OrderDate = DateTime.Now;
            objOrder.OrderNumber = String.Format("{0:ddmmmyyyyhhmmss}", DateTime.Now);
            objOrder.PaymentTypeId = objOrderViewModel.PaymentTypeId;
            objfruitShopDBEntities.Orders.Add(objOrder);
            objfruitShopDBEntities.SaveChanges();

            int OrderId = objOrder.OrderId;

            foreach (var item in objOrderViewModel.ListOfOrderDetailViewModel)
            {
                OrderDetail objOrderDetail = new OrderDetail();
                objOrderDetail.OrderId = OrderId;
                objOrderDetail.Discount = item.Discount;
                objOrderDetail.ItemId = item.ItemId;
                objOrderDetail.Total = item.Total;
                objOrderDetail.UnitPrice = item.UnitPrice;
                objOrderDetail.Quantity = item.Quantity;
                objfruitShopDBEntities.OrderDetails.Add(objOrderDetail);
                objfruitShopDBEntities.SaveChanges();

                Transaction objTransaction = new Transaction();
                objTransaction.ItemId = item.ItemId;
                objTransaction.Quantity = (-1)*item.Quantity;
                objTransaction.TransactionDate = DateTime.Now;
                objTransaction.TypeId = 2;
                objfruitShopDBEntities.Transactions.Add(objTransaction);
                objfruitShopDBEntities.SaveChanges();

            }

            return true; 
        }

        public List<Order> getAllOrders()
        {
            List<Order> orders = new List<Order>();
            orders=objfruitShopDBEntities.Orders.ToList();
            return orders;
        }
    }
}