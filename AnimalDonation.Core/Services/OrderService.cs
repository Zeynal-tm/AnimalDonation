
using AnimalDonation.Core.Classes;
using AnimalDonation.Core.Helpers;
using AnimalDonation.Core.Interfaces;
using AnimalDonation.DataAccessLayer.Context;
using AnimalDonation.DataAccessLayer.Entities;
using AnimalDonation.DataAccessLayer.Interfaces;
using AnimalDonation.DataAccessLayer.Repository;
using AnimalDonation.DataAccessLayer.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnimalDonation.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IConfiguration _configuration;

        IUnitOfWork Database { get; set; }

        private readonly IHttpContextAccessor httpContext;

        public OrderService(IConfiguration configuration, IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _configuration = configuration;
            Database = unitOfWork;
            this.httpContext = httpContext;
        }



        //------------------------------------------------------------------------------CreateOrder----------------------------------------------------------------------

        public async Task<OrderRegistrationResponse> CreateOrder(int amount, string description)
        {
            var order = new Order
            {
                Amount = amount,
                Description = description,
                Paid = false
            };

            Database.Orders.Create(order);

            await Database.SaveAsync();

            var result = await RegisterOrderInPaymentSystem(order);

            if (result.ErrorCode == 0)
            {
                order.PaymentSystemOrderId = result.orderId;
                await Database.SaveAsync();
            }

            return result;
        }


        private async Task<OrderRegistrationResponse> RegisterOrderInPaymentSystem(Order order)
        {
            var httpRequest = httpContext.HttpContext.Request;
            var returnUrl = httpRequest.Scheme + "://" + httpRequest.Host.Value + "/Home/Status";
            var server = _configuration["Urls:Server"];
            var userName = _configuration["Position:Name"];

            var client = new HttpClient();

            var request = new OrderRegistrationRequest
            {
                UserName = userName,
                Password = Convert.ToString(PasswordASCIIDecoder.ASCIIDecoder(userName)),
                OrderNumber = Convert.ToString(Guid.NewGuid()),
                ReturnUrl = returnUrl,
                Amount = order.Amount,
                Description = order.Description
            };


            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = server + "/payment/rest/register.do";

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();

            return SimpleJson.DeserializeObject<OrderRegistrationResponse>(result);

        }

        //------------------------------------------------------------------------------RequestOrderStatus----------------------------------------------------------------------


        public async Task<OrderStatusResponse> RequestOrderStatus(string orderId)
        {
            var client = new HttpClient();

            var server = _configuration["Urls:Server"];
            var userName = _configuration["Position:Name"];


            var getIDForOrderRequest = GetDonationer(orderId);

            var request = new OrderRequest
            {
                UserName = _configuration["Position:Name"],
                Password = Convert.ToString(PasswordASCIIDecoder.ASCIIDecoder(userName)),
                orderId = getIDForOrderRequest.PaymentSystemOrderId
            };

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = server + "/payment/rest/getOrderStatus.do";

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();

            var status = SimpleJson.DeserializeObject<OrderStatusResponse>(result);

            var getIDForStatusPosition = Database.Orders.Get(request.orderId);

            if (status.orderStatus == 2)
            {
                getIDForStatusPosition.Paid = true;

                Database.Orders.Update(getIDForStatusPosition);
                await Database.SaveAsync();
            }

            return status;
        }

        //------------------------------------------------------------------------GetDonatorByID----------------------------------------------------------------------------


        public OrderDTO GetDonationer(string orderId)
        {
            var order = Database.Orders.GetAll().FirstOrDefault(item => item.PaymentSystemOrderId == orderId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()).CreateMapper();
            return mapper.Map<OrderDTO>(order);
        }


        //------------------------------------------------------------------------GetPaidDonationers----------------------------------------------------------------------------

        public IEnumerable<OrderDTO> GetPaidDonationers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()).CreateMapper();

            return mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(Database.Orders.GetAllPaidOrders());
        }
    }
}