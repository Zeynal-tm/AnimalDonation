
using AnimalDonation.Core.Classes;
using AnimalDonation.Core.Helpers;
using AnimalDonation.Core.Interfaces;
using AnimalDonation.DataAccessLayer.Context;
using AnimalDonation.DataAccessLayer.Entities;
using AnimalDonation.DataAccessLayer.Interfaces;
using AnimalDonation.DataAccessLayer.Repository;
using AutoMapper;
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

        public OrderService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            Database = unitOfWork;
        }

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
            var returnUrl = _configuration["Urls:Return"];
            var server = _configuration["Urls:Server"];
            var userName = _configuration["Position:Name"];

            var client = new HttpClient();

            var request = new OrderRegistrationRequest
            {
                UserName = userName,
                Password = Convert.ToString(PasswordConvertor.Convert(userName)),
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

        //----------------------------------------------------------------------------------------------------------------------------------------------------


        public async Task<OrderStatusResponse> RequestOrderStatus()
        {
            var client = new HttpClient();

            var server = _configuration["Urls:Server"];
            var userName = _configuration["Position:Name"];


            var getDonater = GetDonationers();
            var lastDonater = getDonater.LastOrDefault();


            var request = new OrderResponse
            {
                UserName = _configuration["Position:Name"],
                Password = Convert.ToString(PasswordConvertor.Convert(userName)),
                orderId = lastDonater.PaymentSystemOrderId
            };

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = server + "/payment/rest/getOrderStatus.do";

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();

            return SimpleJson.DeserializeObject<OrderStatusResponse>(result);
        }


        //----------------------------------------------------------------------------------------------------------------------------------------------------


        public IEnumerable<OrderDTO> GetDonationers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Order>, List<OrderDTO>>(Database.Orders.GetAll());
        }

    }
}