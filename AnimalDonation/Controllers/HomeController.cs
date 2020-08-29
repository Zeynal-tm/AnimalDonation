using AnimalDonation.Core.Classes;
using AnimalDonation.Core.Interfaces;
using AnimalDonation.DataAccessLayer.Entities;
using AnimalDonation.Models;
using AnimalDonation.Validations;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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

        public IActionResult CreateDonation()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> CreateDonation(int amount, string description, OrderViewModel order)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return View("CreateDonation", order);
            }


            if (ModelState.IsValid)
            {
                var result = await _orderService.CreateOrder(amount, description);
                if (result.ErrorCode == 0)
                {
                    return Redirect(result.formUrl);
                }
                return View("Error");
            }
            return View();
        }



        public async Task<IActionResult> Status(string orderId)
        {

            const int successfulPayment = 2;

            var result = await _orderService.RequestOrderStatus(orderId);

            if (result.orderStatus == successfulPayment)
            {
                return RedirectToAction(nameof(CreateDonation));
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
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
