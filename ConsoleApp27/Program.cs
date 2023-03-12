using ConsoleApp27;


var personFilePath = "persons.txt";
var personService = new PersonService(personFilePath);
var person = new PersonController(personService);

while(true)
{
    Console.WriteLine("Enter:");
    Console.WriteLine("1.Add person");
    Console.WriteLine("2.List person");
    Console.WriteLine("3.Exit");
    var choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            person.AddPerson();
            break;
        case "2":
            person.ListPersons();
            break;
        case "3":
            break;
    }
    break;

}


var bookFilePath = "books.txt";
var bookService = new BookService(bookFilePath);
var booksController = new BooksController(bookService);
while (true)
{
    Console.WriteLine("Enter:");
    Console.WriteLine("1.Add book");
    Console.WriteLine("2.List books");
    Console.WriteLine("3.Exit");
    var choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            booksController.AddBook();
            break;
        case "2":
            booksController.ListBooks();
            break;
        case "3":
            break;
    }
    break;
}