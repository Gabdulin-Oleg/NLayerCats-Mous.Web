using Microsoft.AspNetCore.Mvc;
using NLayerCats_Mous.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NLayerCats_Mous.BLL.DataTransferObject;
using NLayerCats_Mous.BLL.Interface;

namespace NLayerCats_Mous.Web.Controllers
{
    public class DonatController : Controller
    {
        IOrderService orderService;

        public DonatController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Index(ViewModelOrder viewModel)
        {
            var mapper = new MapperConfiguration(conf => conf.CreateMap<ViewModelOrder, OrderDTO>()).CreateMapper();
            
            return Redirect(await orderService.RegisteredOrder(mapper.Map<OrderDTO>(viewModel)));
        }
    }
}
