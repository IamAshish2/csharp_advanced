namespace microsoft_csharp_foundational;

public class ManagePetVisits
{
    // The design specification for the Contoso Petting Zoo application is as follows:
    //
    // There are currently three visiting schools
    //
    // School A has six visiting groups (the default number)
    // School B has three visiting groups
    // School C has two visiting groups
    // For each visiting school, perform the following tasks
    //
    //     Randomize the animals
    // Assign the animals to the correct number of groups
    //     Print the school name
    //     Print the animal groups

    string[] pettingZoo =
    {
        "alpacas", "capybaras", "chickens", "ducks", "emus", "geese",
        "goats", "iguanas", "kangaroos", "lemurs", "llamas", "macaws",
        "ostriches", "pigs", "ponies", "rabbits", "sheep", "tortoises",
    };

    public void RandomizeAnimals()
    {
        Random random = new Random();

        for (int k = 0; k < pettingZoo.Length; k++)
        {
            int i = 0;
            int j = random.Next(i, pettingZoo.Length);

            // deconstruction
            // (pettingZoo[i], pettingZoo[j]) = (pettingZoo[j], pettingZoo[i]);

            string temp = pettingZoo[i];
            pettingZoo[i] = pettingZoo[j];
            pettingZoo[j] = temp;
        }
        
        foreach(string animal in pettingZoo) 
        {
            Console.WriteLine(animal);
        }
    }

    public string[,] AssignAnimalsToGroups(int groups = 6)
    {
        // now to assign the animals to the correct number of groups
        int animalsPerGroup = pettingZoo.Length / groups;
        // create an array to manage the groups and animals asigned to them
        string[,] groupsDivision = new string[groups, animalsPerGroup];

        int start = 0;

        for (int i = 0; i < groups; i++)
        {
            for (int j = 0; j < animalsPerGroup; j++)
            {
                groupsDivision[i, j] += pettingZoo[start++] + " ";
            }
        }

        return groupsDivision;
    }

    public void PrintSchoolName(string schoolName)
    {
        Console.WriteLine(schoolName);
    }

    public void PrintAnimalGroups(string [,] group)
    {
        for (int i = 0; i < group.GetLength(0); i++)
        {
            Console.WriteLine($"Group {i + 1} ");
            for (int j = 0; j < group.GetLength(1); j++)
            {
                Console.Write(group[i,j]);
            }   
            Console.WriteLine();
        }
    }
    
//     ManagePetVisits manage = new ManagePetVisits();
//     manage.RandomizeAnimals();
//     manage.PrintSchoolName("School A");
//     string[,] groups = manage.AssignAnimalsToGroups(groups:6);
//
//     manage.PrintAnimalGroups(groups);
// }


}