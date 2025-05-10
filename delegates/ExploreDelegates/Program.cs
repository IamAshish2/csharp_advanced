using Microsoft.Win32.SafeHandles;

namespace ExploreDeletages;

class Program
{
    public static void Main(string[] args)
    {
        var nb1 = 3;

        // basic method calling by passing variables to it
        // PrintNumber(nb1);
        // MultiplyNumber(nb1);

        // initialize and use the delegate
        // var firstDelegate = new FirstDelegate(PrintNumber);
        // firstDelegate(8);

        // creating a second delegate 
        // here the MuliplyNumber method matches the return type and parameter of the FristDelegate
        // var secondDelegate = new FirstDelegate(MultiplyNumber);
        // DelegateHandler(secondDelegate);
        // DelegateHandler(firstDelegate);



        /*
        DELEGATE CHANING 
        it just means that we can chain a bunch of similar methods that match the criteria 
        into the same delegate variable. Instead of creating multiple delegate variables, we can just
        chain the same delegate variable to do the task
        */

        //  ->  adding methods to the chain


        // here i just defined the AddNumber method to the firstDelegate delegate variable
        var firstDelegate = new FirstDelegate(AddNumber);
        // now what i can do is, i can chain another method to this delegate just like adding 
        // into a int variable
        firstDelegate += PrintNumber;
        firstDelegate += MultiplyNumber;

        // if we remove from here, it will be like as if we did not add it


        // anywho, we can just pass anonymous lambda functions to the delegate, but it still has to follow the delegate
        // signature
        firstDelegate += (x) => Console.WriteLine(x * 90 + " is the output of the lambda function.");

        var stringDel = new SaySomethingDelegate(SaySomething);



        // calls all the methods in a linear fashion
        // firstDelegate(nb1);

        // utilizing the .Invoke() method for calling delegates
        firstDelegate.Invoke(nb1);

        // to remove i can say in my DelegateHandler(FirstDelegate delg) method


        // say call the Delegate handler here and pass the firstDelegate delegate to it
        // DelegateHandler(firstDelegate);
        // what if i just call the firstDelegate variable by passing a value
        // RIGHT NOW, IT DOES THE SAME THING
        // firstDelegate(2);



        // Dynamic Delegate handler
        GenericDelegateHandler(firstDelegate);
        
        
        // GenericDelegateHandler(stringDel);
        // throws error since, the delegate below is passing deleg?.DynamicInvoke(23)
        // string cannot be converted to int type

    }


    public static void SaySomething(string name)
    {
        Console.WriteLine($"Hey i am saying something. {name}");
    }

    public static void PrintNumber(int nb)
    {
        Console.WriteLine($"The number is : {nb}");
    }

    public static void MultiplyNumber(int nb)
    {
        nb *= 2;
        Console.WriteLine($"The product of the number is : {nb}");
    }

    public static void AddNumber(int nb)
    {
        nb += 200;
        Console.WriteLine($"The sum of the number is : {nb}");
    }

    // this method accepts a delegate method and handles it
    // for now, it is just initializing the delegate
    public static void DelegateHandler(FirstDelegate deleg)
    {
        Console.WriteLine();
        // -> removing methods from the delegate chain
        // lets say i want to use only a specific method 
        // deleg -= MultiplyNumber;

        // deleg(23);


        // now instead of just intializing the delegate we can check for null before initializing the delegate
        deleg?.Invoke(23);
        // this provides a way for checking null before invoking the functions chained to the delegate 
        // well, one less exception lol
    }


    // Dynamic Invoke


    // handling invocation of unknown delegates dynamically
    // Delegate is the base class of all delegates
    public static void GenericDelegateHandler(Delegate deleg)
    {
        // since we are using the base class Delegate
        // the Invoke method won't be available to us
        // why? because the base class doesn't know what kind of delegate 
        // we'll pass, if the parameter will have anything
        // it does not understand what the delegate passed to it will do
        // deleg?.Invoke(23);


        // to resolve it, we can say dynamic invoke with DynamicInvoke() method
        deleg?.DynamicInvoke(23);
        // this is like a safe space for the delegate we are passing
        // how? you ask.

        // well, DynamicInvoke(23) , the delegate being passed to it will 
        // have to match the parameter and it's type here


        // if i passed the printName("Ashish") method to this GenericDelegateHandler()
        // it will throw error, since the DynamicInvoke(23 i.e. a int)  can't match with the
        // printName(Ashish i.e. a string data type)


        //==================================================//
        // DynamicInvoke() is extremly slow  compared to normal Invoke()
        // almost 5-6 times slow
        // only use DynamicInvoke() when Delegate is unknown or Generic
    }

}