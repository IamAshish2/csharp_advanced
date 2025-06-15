using EntityFrameworkCore_Revision.Data;
using EntityFrameworkCore_Revision.Models;
using Microsoft.EntityFrameworkCore;

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


// DATABASE FIRST 
//   dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chinook" Microsoft.EntityFrameworkCore.SqlServer
//https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/?tabs=vs


// Scaffold-DbContext "Connection string" Microsoft.EntitityFrameworkCore.{your db type} -ContextDir {folder to store the context file i.e. Data} -OutputDir {Models} -DataAnnotation





// ADDING PERFORMANCE TO THE EF CORE QUERIES


// EF core creates a snapshot and saves it in memory i.e. ram and then  when a query is ran against the database
// EF CORE utilizes it to track changes 
// The changes are later written to the database. This works great for write scenarios.

// Well, for read only scenarios, we can  skip the snapshot, as we are not making any changes to the data
// We are just reading it. To skip adding the snapshot UTILIZE

// var customers = context.Customers.Include(c => c.Orders).AsNoTracking().ToListAsync()

// The AsNoTracking() helps us skip the snapshot 





// When using navigation properties to load data from related entities,EF core allows us to specify when the data
// from those related entities are read from the database
// There are two patterns to load the data i.e.

// EAGER loading and LAZY loading

// EAGER loading
// var customers = context.Customers.Include(c => c.Orders).AsNoTracking().ToListAsync()
// By using the Include method, we are already specifying that the realated Orders should be read
// on the same query where the customers data is read from the database.

// Sometimes, however, it might benefit our applicaton to wait to load the data until the data is needed.
// This can be achieved through what we call LAZY loading.
// Install the MICROSOFT.ENTITYFRAMEWORKCORE.PROXIES package

// EAGER loading does not work with LAZY loading and vice versa
// Include and AsNoTracking do not work with LAZY loading too

// When adding the dbcontext dependency to the web api project and adding the database provider
// builder.Services.AddDbContext<ApplicationDbContext>(
//  options => options.
//                  UseLazyLoadingProxies()
//                  AddSqlServer(@"Connection String");
// );
// Mark the NAVIGATION PROPERTIES AS virtual







// When EAGER loading wants many datasets, EF CORE defaults to using LEFT JOINS to retrive the datasets from the db in one query
// This can lead to very largedatasets when the data from the left side is repeated in turn in the right side
// This forms a Cartesian explosion and can be mitigated by using a feature called Split Queries
// Split Queries use multiple queries to get the same result making it faster than LEFT JOINS for huge data retrieval from many
// tables
// for example: 

//var customers = context.Customers
//            .Include(c => c.Orders)
//            .ThenInclude(o => o.OrderDetails)
//            .ThenInclude(od => od.Product)
//            .FirstOrDefault(m => m.CustomerId == 1);

// To retrive the data from all these tables, EF CORE will default to using LEFT joins from Customers -> Orders -> OrderDetails -> Product 
// This will generate one big query

// To use the Split Query to achieve the same result
//var customers = context.Customers
//            .Include(c => c.Orders)
//            .ThenInclude(o => o.OrderDetails)
//            .ThenInclude(od => od.Product)
//             .AsSplitQuery() -------------------------> Add this 
//            .FirstOrDefault(m => m.CustomerId == 1);






// Sometimes we want to use raw sql, for that purpose we can use
// FromSqlInterpolation($"the query goes here") 
// EF CORE processes the sql query as parameterized query to prevent the sql injection attacks




// Since the snapshots are saved in memory, we can utilize methods to first check the snapshots in memeory
// before going in to the database. This can save time and memory

// Utilize the Find() and FindAsync() methods for this operation

// The above query checks the database for matching customer id and returns teh first it finds
// Utilizing the FindAsync() method, we can save the roundtrip to the database if we have the Customer table snapshot in memory
//eg -> var customer = context.Customers.FindAsync(Id);





// There is a certain overhead when creating and destroying the DbContext object when we use it.
// We can bypass that utilizing the database context pooling to reuse the DbContext object again and again

// add the AddDbContextPool instead of AddDbContext

// builder.Services.AddDbContextPool<ApplicationDbContext>(
//  options => options.
//                  UseLazyLoadingProxies()
//                  AddSqlServer(@"Connection String");