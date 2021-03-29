using NLayerCats_Mous.BLL.DataTransferObject;
using NLayerCats_Mous.BLL.Interface;
using NLayer_Cats_Mous.DAL.Intefaces;
using NLayer_Cats_Mous.DAL.Entitis;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using AutoMapper;
using NLayerCats_Mous.BLL.Hellper;
using System.Threading;
using Microsoft.AspNetCore.Http;
using NLayerCats_Mous.BLL.Models;

namespace NLayerCats_Mous.BLL.Service
{
    public class OrderService //: IOrderService
    {

        IRepository db;
        IHttpContextAccessor httpContext;

        public OrderService(IRepository repository, IHttpContextAccessor httpContext)
        {
            db = repository;
            this.httpContext = httpContext;
            
        }

        public async Task<string> GetInformationFromSever(RegistrationForm refistrationForm, string urlAddress)
        {
            using (HttpClient client = new HttpClient()
            { BaseAddress = new Uri("http://attest.turkmen-tranzit.com") })
            {
                var answer = await client.PostAsJsonAsync($"{client.BaseAddress}{urlAddress}", refistrationForm);

                string result = await answer.Content.ReadAsStringAsync();
                return result;
            }

        }

        public async Task<string> GetInformationFromSever(GetStatusForm getStatusForm, string urlAddress)
        {
            using (HttpClient client = new HttpClient()
            { BaseAddress = new Uri("http://attest.turkmen-tranzit.com") })
            {
                var answer = await client.PostAsJsonAsync($"{client.BaseAddress}{urlAddress}", getStatusForm);

                string result = await answer.Content.ReadAsStringAsync();
                return result;
            }

        }

        private Order MappingOrder(OrderDTO orderDTO)
        {
            var mapper = new MapperConfiguration(conf => conf.CreateMap<OrderDTO, Order>()).CreateMapper();
            return mapper.Map<Order>(orderDTO);
        }

        public List<SuccessfulOrderDTO> GetSuccessfulOrders()
        {
            var mapper = new MapperConfiguration(conf => conf.CreateMap<Order, SuccessfulOrderDTO>()).CreateMapper();
            return mapper.Map<List<SuccessfulOrderDTO>>(db.GetAllSuccessfulOrder());
        }

        private string GenerationOrderNumber()
        {
            Random rand = new Random();
            return rand.Next(1000000, 1000000000).ToString() + "-" + rand.Next(1000000, 1000000000).ToString() + "-" + rand.Next(1000000, 1000000000).ToString();
        }        

        public void CreatOrder(OrderDTO orderDto)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            Order order = MappingOrder(orderDto);
            db.Creat(order);
            registrationForm.OrderNumber = GenerationOrderNumber();
        }

        public async Task<string> RegisteredOrder(OrderDTO orderDto)
        {
            string urlRegistered = "payment/rest/register.do";

            Order order = MappingOrder(orderDto);
            db.Creat(order);
            orderDto.OrderNumber = GenerationOrderNumber();
            order.OrderNumber = orderDto.OrderNumber;

            var httpRequest = httpContext.HttpContext.Request;
            orderDto.ReturnUrl = httpRequest.Scheme + "://" + httpRequest.Host.Value + $"/HomePage/CheckStarus?orderNumber={order.OrderNumber}";
            orderDto.FaiUrl = httpRequest.Scheme + "://" + httpRequest.Host.Value + "/Home/Error";

           
            OrderID idOrder = JsonSerializer.Deserialize<OrderID>(await GetInformationFromSever(orderDto, urlRegistered));
            
            order.OrderId = idOrder.orderId;
            db.UpData(order);

            return idOrder.formUrl;
        }
        public async Task ChecStatus(string numberOrder)
        {
            string urlChecStatus = "payment/rest/getOrderStatus.do";

            var mapper = new MapperConfiguration(conf => conf.CreateMap<Order, OrderDTO>()).CreateMapper();
            OrderDTO orderDTO = mapper.Map<OrderDTO>(db.Find(numberOrder));
            
            OrderStatus statusOrder = JsonSerializer.Deserialize<OrderStatus>(await GetInformationFromSever(orderDTO, urlChecStatus));

            orderDTO.OrderStatus = statusOrder.orderStatus;

            switch (orderDTO.OrderStatus)
            {
                case (int)EnumOrderStatus.RegisteredButNotPaid:
                    {
                        Thread.Sleep(10000);
                        Task.Run(()=> ChecStatus(orderDTO.OrderNumber));
                        break;
                    }
                case (int)EnumOrderStatus.SuccessfullyPaid:
                    {
                        db.UpData(MappingOrder(orderDTO));
                        break;
                    }
                case (int)EnumOrderStatus.AuthorizationCanceled:
                    {
                        db.Delete(MappingOrder(orderDTO));
                        break;
                    }
                case (int)EnumOrderStatus.OrderIsBeingProcessed:
                    {
                        Thread.Sleep(10000);
                        Task.Run(() => ChecStatus(orderDTO.OrderNumber));
                        break;
                    }
            }
        }
    }
}
