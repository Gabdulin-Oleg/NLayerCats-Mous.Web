using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLayerCats_Mous.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NLayerCats_Mous.BLL.DataTransferObject;
using NLayerCats_Mous.BLL.Interface;

namespace NLayerCats_Mous.Web.Controllers
{
    public class HomePageController : Controller
    {
        private readonly ILogger<HomePageController> _logger;
        IOrderService orderService;
        public HomePageController(ILogger<HomePageController> logger, IOrderService orderService)
        {
            _logger = logger;
            this.orderService = orderService;
        }
        public IActionResult Index()
        {
            var mapper = new MapperConfiguration(conf => conf.CreateMap<OrderDTO, ViewModelOrder>()).CreateMapper();            
            ViewBag.AllOredrs =  mapper.Map<List<ViewModelOrder>>(orderService.GetSuccessfulOrders());
            return View();
        }

        public IActionResult CheckStarus(string orderNumber)
        {
            Task.Run(() => orderService.ChecStatus(orderNumber));
            return Redirect("/HomePage/Index");
        }   
        public IActionResult Error()
        {
            return View();
        }
        
    }
}
