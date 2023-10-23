using Business.Concrete;
using Dto;

public class Program
{
    private static void Main(string[] args)
    {
        Service<Entity> service = new();
        Console.WriteLine("Please make a choice.");
        Console.WriteLine("1.Request => 'challenge.photier.com/todos'");
        Console.WriteLine("2.Request => 'challenge.photier.com/todos/search?query='");
        Console.WriteLine("3.Request => 'challenge.photier.com/todos?id='");
        Console.WriteLine("4.Request => 'challenge.photier.com/start'");
        Console.WriteLine("5.Request => 'challenge.photier.com/completed'");
        Console.Write("Enter a value : ");
        var select = Console.ReadLine();

        switch (select)
        {
            case "1":
                var case1Process = service.GetListAsync().Result;
                foreach (var item in case1Process)
                {
                    Console.WriteLine($"Id = {item.Id} , Desc =  {item.Desc} , Completed = {item.Completed}");
                }
                Console.ReadLine();
                break;
            case "2":
                Console.Write("Please enter the query value :");
                var query = Console.ReadLine();

                var case2Process = service.GetAsync(query).Result;
                foreach (var item in case2Process)
                {
                    Console.WriteLine($"Id = {item.Id} , Desc =  {item.Desc} , Completed = {item.Completed}");
                }
                Console.ReadLine();
                break;
            case "3":
                Console.Write("Please enter the id value :");
                var id = Console.ReadLine();

                var case3Process = service.DeleteAsync(Convert.ToInt32(id)).Result;
                foreach (var item in case3Process)
                {
                    Console.WriteLine($"Id = {item.Id} , Desc =  {item.Desc} , Completed = {item.Completed}");
                }
                Console.ReadLine();
                break;
            case "4":
                Console.Write("Please enter the email :");
                var mail = Console.ReadLine();

                var case4Process = service.Start(mail).Result;
                Console.WriteLine(case4Process);
                Console.ReadLine();
                break;
            case "5":
                Console.Write("Please enter the file path :");
                var path = Console.ReadLine();
                Console.Write("Please enter the last code :");
                var last_code = Console.ReadLine();

                var case5Process = service.Complete(path, last_code).Result;
                Console.WriteLine(case5Process);
                Console.ReadLine();
                break;
            default:
                Console.WriteLine("Invalid or wrong selection");
                break;
        }
    }
}