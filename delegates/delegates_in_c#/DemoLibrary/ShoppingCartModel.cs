using ConsoleUI.delegates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoLibrary
{
    public class ShoppingCartModel
    {



        public List<ProductModel> Items { get; set; } = new List<ProductModel>();
        /*
        public decimal GenerateTotal(MentionDiscount mentionDiscount)
        {
            decimal subTotal = Items.Sum(x => x.Price);
            // we are mentioning the initial subTotal before discount applied
            mentionDiscount(subTotal);

            if (subTotal >= 100)
            {
                // 20% discount
                return subTotal * 0.80M;
            } else if (subTotal >=80)
            {
                // 15% discount
                return  subTotal * 0.85M;
            } else if (subTotal < 40)
            {
                // 10% discount
                return subTotal * 0.90M;
            }


            return subTotal;
        }
        */

        // The actual power of delegates comes in handy when there are different applications 
        // consuming a service with different implementaions, we can just define the pattern in a base class library
        // and have it work from there.
        // for example -> in this example, the Console and the Winform application both use the GenerateTotal() method
        // which takes in delegates as parameters,
        // and we can pass different implementation methods to the GenerateTotal() method via delegates and have them do different 
        // task for different applications
        // this way the core does not change
        // and if some changes are required for a specific application, we just change it there


        // delegates are used in places where the logic changes a lot 
        // a place where delegates are used a lot are EVENTS
        public decimal GenerateTotal(MentionSubTotal mentionSubTotal,
            Func<List<ProductModel>,decimal,decimal> calculateDiscountedTotal,
            Action<string> mentionDiscount)
        {
            decimal subTotal = Items.Sum(x => x.Price);
            // we are mentioning the initial subTotal before discount applied
            mentionSubTotal(subTotal);


            // the Action delegate here
            mentionDiscount("We are applying your discount now....");

            // the decimal is the return type of this Func method
            // Func is a special kind of delegate
            return calculateDiscountedTotal(Items,subTotal);
        }
    }
}
