using AnimalDonation.Core.Classes;
using AnimalDonation.Core.Interfaces;
using AnimalDonation.DataAccessLayer.Entities;
using AnimalDonation.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AnimalDonation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderService _orderService;

        public HomeController(IOrderService order)
        {
            _orderService = order;
        }

        [HttpGet]
        public IActionResult CreateDonation()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateDonation([Range(1, 1000)] int amount, string description)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.CreateOrder(amount, description);
                if (result.ErrorCode == 0)
                {
                    return Redirect(result.formUrl);
                }
                return View("Error");
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }

        public  IActionResult Status()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetStatus()                
        {
            int successfulPayment = 2;
            int successfulPaymentWithADelay = 8;

            if (ModelState.IsValid)
            {

                var result = await _orderService.RequestOrderStatus();             

                if (result.orderStatus == successfulPayment || result.orderStatus == successfulPaymentWithADelay)
                {
                    return RedirectToAction(nameof(Donationers));
                }
                else
                {
                    return RedirectToAction(nameof(Error));
                }
            }
            return null;
        }


        public IActionResult Donationers()
        {
            IEnumerable<OrderDTO> orderDTOs = _orderService.GetDonationers();
            var mappar = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>()).CreateMapper();
            var orders = mappar.Map<IEnumerable<OrderDTO>, List<OrderViewModel>>(orderDTOs);
            return View(orders);
        }

        
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
