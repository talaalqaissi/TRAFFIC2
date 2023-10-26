﻿using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using TRAFFIC2.Models;

namespace TRAFFIC2.Controllers
{
    public class OrderController : Controller
    {
        [BindProperty]
        public OrderEntity _OrderDetails { get; set; }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult InitiateOrder()
        {
            string key = "rzp_test_STDrikMsg9t9l7";
            string secret = "op4UyGBnhcGOWeWp70lU8aYG";

            Random _random = new Random();
            String TransactionId = _random.Next(0, 10000).ToString();

            Dictionary<string, object> input = new Dictionary<string, object>();
            //input.Add("amount", Convert.ToDecimal(_OrderDetails.TotalAmount) * 100); // this amount should be same as transaction amount
            input.Add("amount", 10000);
            input.Add("currency", "INR");
            input.Add("receipt", TransactionId);


            RazorpayClient client = new RazorpayClient(key, secret);
            Razorpay.Api.Order order = client.Order.Create(input);
            ViewBag.orderid = order["id"].ToString();
            return View("Payment", _OrderDetails);
        }
        public IActionResult Payment(String razorpay_payment_id, String razorpay_order_id, String razorpay_signature)
        {
            Dictionary<String, String> attributes = new Dictionary<String, String>();

            attributes.Add("razorpay_payment_id", razorpay_payment_id);
            attributes.Add("razorpay_order_id", razorpay_order_id);
            attributes.Add("razorpay_signature", razorpay_signature);
            Utils.verifyPaymentSignature(attributes);
            OrderEntity OrderDetails = new OrderEntity();
            OrderDetails.TransactionId = razorpay_payment_id;
            OrderDetails.OrderId = razorpay_order_id;
            return View("PaymentSuccess", OrderDetails);
        }

        public IActionResult PaymentSuccess()
        {
            return View();
        }
    }
}

