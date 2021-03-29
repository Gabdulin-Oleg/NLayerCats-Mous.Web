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
    public class OrderService : IOrderService
    //: IOrderService
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

        private Order MappingOrder(RegistrationForm registrationForm)
        {
            var mapper = new MapperConfiguration(conf => conf.CreateMap<RegistrationForm, Order>()).CreateMapper();
            return mapper.Map<Order>(registrationForm);
        }

        public List<SuccessfulOrderDTO> GetSuccessfulOrders()
        {
            var mapper = new MapperConfiguration(conf => conf.CreateMap<Order, SuccessfulOrderDTO>()).CreateMapper();
            return mapper.Map<List<SuccessfulOrderDTO>>(db.GetAllSuccessfulOrder());
        }

        public RegistrationForm CreatRegistrationForm(OrderDTO orderDto)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            var httpRequest = httpContext.HttpContext.Request;

            registrationForm.Amount = orderDto.Amount;
            registrationForm.Description = orderDto.Description;
            registrationForm.OrderNumber = Helper.GenerationOrderNumber();
            registrationForm.UserName = "client8";
            registrationForm.Password = Helper.GetPassword(registrationForm.UserName);
            registrationForm.ReturnUrl = httpRequest.Scheme + "://" + httpRequest.Host.Value + $"/HomePage/CheckStarus?orderNumber={registrationForm.OrderNumber}";
            registrationForm.FaiUrl = httpRequest.Scheme + "://" + httpRequest.Host.Value + "/Home/Error";

            return registrationForm;

        }

        public GetStatusForm CreatStatusForm(string numberOrder)
        {
            GetStatusForm statusForm = new GetStatusForm();

            statusForm.UserName = "client8";
            statusForm.Password = Helper.GetPassword(statusForm.UserName);
            statusForm.orderId = db.Find(numberOrder).OrderId;

            return statusForm;
        }

        public async Task<string> CreatOrder(OrderDTO orderDto)
        {
            RegistrationForm registrationForm = CreatRegistrationForm(orderDto);
            Order order = MappingOrder(registrationForm);
            db.Creat(order);
            OrderID orderID = await RegisteredOrder(registrationForm);
            order.OrderId = orderID.orderId;

            db.UpDate(order);
            return orderID.formUrl;
        }

        public async Task<OrderID> RegisteredOrder(RegistrationForm registrationForm)
        {
            string urlRegistered = "payment/rest/register.do";

            OrderID orderId = JsonSerializer.Deserialize<OrderID>(await GetInformationFromSever(registrationForm, urlRegistered));

            return orderId;

        }
        public async Task ChecStatus(string numberOrder)
        {
            string urlChecStatus = "payment/rest/getOrderStatus.do";

            GetStatusForm statusForm = CreatStatusForm(numberOrder);

            OrderStatus orderStatus = JsonSerializer.Deserialize<OrderStatus>(await GetInformationFromSever(statusForm, urlChecStatus));

            Order order = db.Find(numberOrder);

            order.OrderStatus = orderStatus.orderStatus;

            ActionOptions(order);

        }
        //Варианты действия в зависимости от статуса заказа
        public void ActionOptions(Order order)
        {
            switch (order.OrderStatus)
            {
                case (int)EnumOrderStatus.RegisteredButNotPaid:
                    {
                        Thread.Sleep(10000);
                        Task.Run(() => ChecStatus(order.OrderNumber));
                        break;
                    }
                case (int)EnumOrderStatus.SuccessfullyPaid:
                    {
                        db.UpDate(order);
                        break;
                    }
                case (int)EnumOrderStatus.AuthorizationCanceled:
                    {
                        db.Delete(order);
                        break;
                    }
                case (int)EnumOrderStatus.OrderIsBeingProcessed:
                    {
                        Thread.Sleep(10000);
                        Task.Run(() => ChecStatus(order.OrderNumber));
                        break;
                    }
            }
        }
    }
}
