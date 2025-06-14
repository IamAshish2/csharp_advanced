using System.Reflection.Metadata.Ecma335;

namespace interview_prep.Configuration;

public class ESewa
{
    public bool SandBoxMode { get; set; } = true;
    public required string ProductCode { get; set; }  // Merchant code provided by eSewa
    public required string ESewaKey { get; set; }
    public decimal Amount { get; set; }
    public decimal TaxAmount { get; set; } = 0;
    public decimal ProductServiceCharge { get; set; } = 0;
    public decimal ProductDeliveryCharge { get; set; } = 0;
    public decimal TotalAmount { get; set; }
    public required string TransactionUuid { get; set; }
    public required string SuccessUrl { get; set; }
    public required string FailureUrl { get; set; }
    public required string SignedFieldNames { get; set; } // Unique field names to be sent which is used for generating signature
    public required string Signature { get; set; } // hmac signature generated through above process.

    // Username: 9806800001/2/3/4/5 (Any of these numbers)
    // Password: Nepal@123
    // Token: 123456
}