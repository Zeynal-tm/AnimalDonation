using AnimalDonation.Core.Classes;
using AnimalDonation.Core.Interfaces;
using AnimalDonation.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalDonation.Components
{
    public class DonatorsListViewComponent : ViewComponent
    {
        private IOrderService _orderService;


        public DonatorsListViewComponent(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IViewComponentResult Invoke()
        {
            return View(_orderService.GetPaidDonationers());
        }
    }
}
