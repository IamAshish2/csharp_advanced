using DemoLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUI
{
    public partial class Dashboard : Form
    {
        ShoppingCartModel cart = new ShoppingCartModel();

        public Dashboard()
        {
            InitializeComponent();
            PopulateCartWithDemoData();
        }

        private void PopulateCartWithDemoData()
        {
            cart.Items.Add(new ProductModel { ItemName = "Cereal", Price = 3.63M });
            cart.Items.Add(new ProductModel { ItemName = "Milk", Price = 2.95M });
            cart.Items.Add(new ProductModel { ItemName = "Strawberries", Price = 7.51M });
            cart.Items.Add(new ProductModel { ItemName = "Blueberries", Price = 8.84M });
        }

        private void messageBoxDemoButton_Click(object sender, EventArgs e)
        {
            var subTotal = cart.GenerateTotal(MentionSubTotal,CalculateLabelledDiscount, PrintOutDiscountAlert);
            MessageBox.Show($"The total after applying discount is {subTotal}");
        }

        private void PrintOutDiscountAlert(string discountMessage)
        {
            MessageBox.Show($"We are applying your discount now....");
        }

        private void textBoxDemoButton_Click(object sender, EventArgs e)
        {
            decimal total = cart.GenerateTotal
                (
                  (subTotal) => subTotalTextBox.Text = $"{subTotal:C2}",
                  (List<ProductModel> items, decimal subTotal) => subTotal - items.Count,
                  (string alertText) => { }
                );
            totalTextBox.Text = $"{total:C2}";
        }

        private void MentionSubTotal(decimal subTotal)
        {
            MessageBox.Show($"The subtotal is {subTotal:C2}");
        }

        private decimal CalculateLabelledDiscount(List<ProductModel> items, decimal subTotal)
        {
            return subTotal - items.Count;
        }

    }
}
