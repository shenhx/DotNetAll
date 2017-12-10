using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationOne.Models;

namespace WebApplicationOne.Controllers
{
    public class StoreController : Controller
    {
        public List<OrderModel> orderModelList = new List<OrderModel>();

        public StoreController()
        {
            orderModelList.Add(new OrderModel { Id = 1, Name = "商品1", Description = "", Amount = 1 });
            orderModelList.Add(new OrderModel { Id = 2, Name = "商品2", Description = "", Amount = 5 });
            orderModelList.Add(new OrderModel { Id = 3, Name = "商品3", Description = "", Amount = 2 });
            orderModelList.Add(new OrderModel { Id = 4, Name = "商品4", Description = "", Amount = 3 });
            orderModelList.Add(new OrderModel { Id = 5, Name = "商品5", Description = "", Amount = 27 });
            orderModelList.Add(new OrderModel { Id = 6, Name = "商品6", Description = "", Amount = 1 });
            orderModelList.Add(new OrderModel { Id = 7, Name = "商品7", Description = "", Amount = 8 });
        }

        // GET: Store
        public ActionResult Index()
        {
            return View(orderModelList);
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            OrderModel orderModel = orderModelList.FirstOrDefault(p => p.Id == id);
            return View(orderModel);
        }
    }
}
