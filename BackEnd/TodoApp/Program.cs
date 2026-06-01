using TodoApp.Repositories;
using TodoApp.Services;

var repository = new TodoRepository();
var service = new TodoService(repository);
Console.WriteLine(@"
1. Show todos
    - Select todo by Order (number on keyboard) => show all details of the todo
        - Complete todo
        - Delete todo
    - Create todo
2. Show completed todos
3. Show pending todos
4. Show todos for user
5. EXIT
");

var inputKey = Console.ReadKey().Key;

while (inputKey != ConsoleKey.D0)
{
    if (inputKey == ConsoleKey.D1)
    {
        // show todos
        while (inputKey != ConsoleKey.D0)
        {
            Console.Clear();
            Console.WriteLine(@"1. Select todo by order number
2. Create todo
0. EXIT");
            string input = Console.ReadLine();

            var allTodo = service.GetTodoSummaries();

            while (inputKey != ConsoleKey.D0)
            {
                if (inputKey == ConsoleKey.D1)
                {
                    Console.Clear();
                    Console.WriteLine("Enter an order number or press 0 to EXIT:");
                    string orderInput = Console.ReadLine();
                    bool validInput = int.TryParse(input, out int numberInput);
                    var todoSummaryDto = allTodo.SingleOrDefault(x => x.Order == numberInput);


                    if (todoSummaryDto is null)
                    {
                        Console.WriteLine("Invalid order number. Please try again or press 0 to EXIT:");
                        orderInput = Console.ReadLine();
                        validInput = int.TryParse(input, out numberInput);
                        Console.WriteLine();
                        allTodo = service.GetTodoSummaries();

                    }
                    else
                    {

                    }


                }

            }
            if (inputKey == ConsoleKey.D2)
            {

                Console.Write("Choose a title:");
                var title = Console.ReadLine();

                Console.Write("Describe:");
                var description = Console.ReadLine();

                Console.Write("Assign to:");
                var assignedTo = Console.ReadLine();

                var createTodo = service.CreateTodoAsync(title, description, assignedTo);
            }

        }
    }

    if (inputKey == ConsoleKey.D2)
    {

        Console.Write("Choose a title:");
        var title = Console.ReadLine();

        Console.Write("Describe:");
        var description = Console.ReadLine();

        Console.Write("Assign to:");
        var assignedTo = Console.ReadLine();

        var createTodo = service.CreateTodoAsync(title, description, assignedTo);
    }

}
