﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models;
using Capstone.Models.ViewModels;
using Capstone.Providers.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    public class OwnerController : HomeController
    {

        public OwnerController(IApplicationDAL applicationDAL, IPropertyDAL propertyDAL, IHttpContextAccessor contextAccessor, IUserDAL userDAL, IUnitDAL unitDAL, IAuthProvider authProvider, IServiceRequestDAL serviceRequestDAL,
            IPaymentDAL paymentDAL) : 
            base(applicationDAL, propertyDAL, contextAccessor, userDAL, unitDAL, authProvider, serviceRequestDAL, paymentDAL)
        {

        }

        public new IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Property()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Property(Property property)
        {
            propertyDAL.AddProperty(property);

            return RedirectToAction("Unit");
        }

        [HttpGet]
        public IActionResult Unit()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Unit(Unit unit)
        {
            unitDAL.AddUnit(unit);

            return RedirectToAction("Unit");
        }

        [HttpGet]
        public IActionResult Review()
        {
            List<Application> applications = applicationDAL.GetAllUnreviewedApplications();

            return View(applications);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Approve(int applicationID)
        {
                applicationDAL.ApproveApplication(applicationID);

                return RedirectToAction("Review");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Deny(int applicationID)
        {
            applicationDAL.DenyApplication(applicationID);

            return RedirectToAction("Review");
        }

        [HttpGet]
        public IActionResult Statistics()
        {
            //TODO: Implement get owner's ID
            int currentOwnerID = 2;

            OwnersPropertiesViewModel statisticsForOwnerProperties = new OwnersPropertiesViewModel();

            statisticsForOwnerProperties.CurrentOwnerProperties = propertyDAL.GetPropertiesForOwner(currentOwnerID);

            return View(statisticsForOwnerProperties);
        }
    }
}