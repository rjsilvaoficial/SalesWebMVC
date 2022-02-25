using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        /*
         * Every post needs a get first! Get is love!
         * The get will be return a view to you, and with a view giving to you access for a form for example, you can do your post!
         * How can you make a post, if you don't have a address in your browser to make him?
         */
        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}

                 
