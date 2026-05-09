// 1. create repository and service

using TodoApp.Repositories;
using TodoApp.Services;

var repository = new TodoRepository();
var service = new TodoService(repository);

var inputKey = Console.ReadKey().Key;
Console.Clear(); // clear console

//actions menu
//1.show todos
//2. create todo
//3. complete todo
//4. delete todo
//5. show completed todos
//6. show pending todos
//7. show todos for user
//0. exit

while (inputKey != ConsoleKey.D0)
{
    //1. Show actions menu
    //Console.WriteLine($"Actions menu:" +
    //    $"\r\n" +
    //    $"\r\n(1).Show todos" +
    //    $"\r\n(2).Create todo" +
    //    $"\r\n(3).Complete todo" +
    //    $"\r\n(4).Delete todo" +
    //    $"\r\n(5).Show completed todos" +
    //    $"\r\n(6).Show pending todos" +
    //    $"\r\n(7).Show todos for user" +
    //    $"\r\n(0).Exit");

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

            while (inputKey != 0)
            {
                if (inputKey == ConsoleKey.D1)
                {
                    Console.Clear();
                    Console.WriteLine("Enter an order number or press 0 to EXIT:");
                    string orderInput = Console.ReadLine();
                    bool validInput = int.TryParse(input, out int numberInput);
                    var orderNumber = allTodo.Single(x => x.Order == numberInput);

                    while (inputKey != 0)
                    {
                        if (orderNumber.Order != numberInput)
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

    //if(inputKey == ConsoleKey.D3)
    //{

    //    var completeTodo = service.CompleteTodoAsync(id);
    //}

}
