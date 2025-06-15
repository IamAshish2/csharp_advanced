using EntityFrameworkCore_Revision.Data;
using EntityFrameworkCore_Revision.Models;

using ApplicationDbContext context = new ApplicationDbContext();

/*
Product momo = new Product()
{
    Name = "MOMO",
    Price = 99.99M
};


Product chowemin = new Product()
{
    Name = "CHOWEMIN",
    Price = 50.75M
};

Product miniCake = new Product()
{
    Name = "MINI_CAKE",
    Price = 25M
};

context.Products.Add(momo);
context.Products.Add(chowemin);
context.Products.Add(miniCake);
context.SaveChanges();
*/

// query the Products table for items greater than 40
var queriedProducts = context.Products.Where(p => p.Price > 40).ToList();

Console.WriteLine("The products with price less than 40 are listed below: ");

foreach (var product in queriedProducts)
{
    Console.WriteLine($"{product.Name} : {product.Price:C2}");
    //Console.WriteLine(new string('-',20));
}


// updating a product 
var cake = context.Products.Where(p => p.Name == "MINI_CAKE").FirstOrDefault();
if (cake is not null)
{
    cake.Name = "PASTRY";
    context.SaveChanges();
} else
{
    Console.WriteLine("Sorry!, the searched product was not found.");
}

Console.WriteLine(new string('-', 20));
Console.WriteLine("Displaying all products with their price.");

var allProducts = context.Products.ToList();
foreach (var product in allProducts)
{
    Console.WriteLine($"{product.Name} : {product.Price:C2}");
    Console.WriteLine(new string('-', 20));
}

Console.WriteLine("Press any key to exit.");
Console.ReadKey();