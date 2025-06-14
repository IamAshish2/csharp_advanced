using interview_prep.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using payment_gateway_nepal;

namespace interview_prep.Controller.PaymentController;

[ApiController]
[Route("api/[controller]")]
public class ESewaController : ControllerBase
{
    private readonly ESewa _settings;

    public ESewaController(IOptions<ESewa> settings)
    {
        _settings = settings.Value;
    }

    [HttpPost("pay-with-esewa", Name = "PayWithEsewa")]
    public async Task<IActionResult> PayWithESewa()
    {
        PaymentManager paymentManager = new PaymentManager(
            PaymentMethod.eSewa,
            PaymentVersion.v2,
            PaymentMode.Sandbox,
             _settings.ESewaKey
        );
        
        string currentUrl = new Uri($"{Request.Scheme}://{Request.Host}").AbsoluteUri;

        // Replace these values with your actual order data
        dynamic request = new
        {
            Amount = 100, // Your actual product amount
            TaxAmount = 10, // Your actual tax amount
            TotalAmount = 110, // Total including tax
            TransactionUuid = $"tx-{Guid.NewGuid().ToString("N").Substring(0, 8)}",
            ProductCode = _settings.ProductCode,
            ProductServiceCharge = 0, // Your service charge if any
            ProductDeliveryCharge = 0, // Your delivery charge if any
            SuccessUrl = _settings.SuccessUrl, // Your actual success URL
            FailureUrl = _settings.FailureUrl, // Your actual failure URL
            SignedFieldNames = "total_amount,transaction_uuid,product_code"
        };

        var response = await paymentManager.InitiatePaymentAsync<ApiResponse>(request);
        // the esewa api will return either the successUrl or the failure url
        return Redirect(response.data);
    }

    [HttpGet("Payment/Success")]
    public async Task<IActionResult> VerifyEsewaPayment(string data)
    {
        PaymentManager paymentManager = new PaymentManager(
            PaymentMethod.eSewa,
            PaymentVersion.v2,
            PaymentMode.Production,
            _settings.ESewaKey
        );
        
        eSewaResponse response = await paymentManager.VerifyPaymentAsync<eSewaResponse>(data);
        if (!string.IsNullOrEmpty(nameof(response)) &&
            string.Equals(response.status, "complete", StringComparison.OrdinalIgnoreCase))
        {
            // Handle successful payment
            // Update your order status, database, etc.
            return Ok($"Payment successful: {response.transaction_code}, Amount: {response.total_amount}");
        }

        // Handle failed payment
        return BadRequest("Payment verification failed");
    }
}