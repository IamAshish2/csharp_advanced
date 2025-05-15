using ConsoleUI.delegates;
using DemoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static ShoppingCartModel cart = new ShoppingCartModel();

        static void Main(string[] args)
        {
            PopulateCartWithDemoData();

            // we can do it this way too
            //MentionDiscount discountDeleg = new MentionDiscount(PrintDiscount);
            //Console.WriteLine($"The total price of the items is {cart.GenerateTotal(discountDeleg):C2}");

            // we can basically pass in the method that matches the delegate signature to the GenerateTotal()
            Console.WriteLine($"The total price of the items is {cart.GenerateTotal(PrintDiscount, CalculateLeveledDiscount,AlertUser):C2}");
            Console.Write("Please press any key to exit the application...");
            Console.ReadKey();
        }

        private static void PopulateCartWithDemoData()
        {
            cart.Items.Add(new ProductModel { ItemName = "Cereal", Price = 100M });
            cart.Items.Add(new ProductModel { ItemName = "Milk", Price = 2.95M });
            cart.Items.Add(new ProductModel { ItemName = "Strawberries", Price = 7.51M });
            cart.Items.Add(new ProductModel { ItemName = "Blueberries", Price = 8.84M });
        }

        // the function being used as the MentionDiscount delegate
        private static void PrintDiscount(decimal discount)
        {
            Console.WriteLine($"Discount on the purchase is : {discount:C2}");
        }


        // the method being used as the Action delegate
        private static void AlertUser(string alertMessage)
        {
            Console.WriteLine($"Alert : {alertMessage}");
        }

        // the method that is used as the Func delegate type
        private static decimal CalculateLeveledDiscount(List<ProductModel> items, decimal subTotal)
        {
            Console.WriteLine($"The total items in the cart are {items.Count}.");

            if (subTotal >= 100)
            {
                // 20% discount
                return subTotal * 0.80M;
            }
            else if (subTotal >= 80)
            {
                // 15% discount
                return subTotal * 0.85M;
            }
            else if (subTotal < 40)
            {
                // 10% discount
                return subTotal * 0.90M;
            }


            return subTotal;
        }
    }
}
