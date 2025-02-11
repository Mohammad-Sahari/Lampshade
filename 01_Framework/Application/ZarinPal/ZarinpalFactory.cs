using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace _01_Framework.Application.ZarinPal
{
    public class ZarinPalFactory : IZarinPalFactory
    {
        private readonly IConfiguration _configuration;

        public string Prefix { get; set; }
        private string MerchantId { get; set; }

        public ZarinPalFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            Prefix = _configuration.GetSection("Payment")["method"];
            MerchantId= _configuration.GetSection("Payment")["merchant"];
        }

        public PaymentResponse CreatePaymentRequest(string amount, string mobile, string email, string description,
             long orderId)
        {
            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);
            var siteUrl = _configuration.GetSection("payment")["siteUrl"];
            var client = new RestClient("https://sandbox.zarinpal.com/pg/v4/payment/request.json");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");
            var body = new 
            {
                merchant_id = MerchantId,
                amount = finalAmount,
                callback_url = $"{siteUrl}/Checkout?handler=CallBack&oId={orderId}",
                description = "خرید لوازم دکوری",
                metadata = new
                {
                    mobile = "09125958832",
                    email = "mohammadsahari98@outlook.com"
                }
            };
            request.AddJsonBody(body);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<PaymentResponse>(response.Content);
        }

        public VerificationResponse CreateVerificationRequest(string authority, string amount)
        {
            var client = new RestClient("https://sandbox.zarinpal.com/pg/v4/payment/verify.json");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");

            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);

            request.AddJsonBody(new VerificationRequest
            {
                amount = finalAmount,
                merchant_id = MerchantId,
                authority = authority
            });
            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<VerificationResponse>(response.Content);
        }
    }
}