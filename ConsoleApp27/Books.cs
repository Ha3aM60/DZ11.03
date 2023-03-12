using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp27
{
   public interface IBookDataWriter
    {
        void write(Books book);
    }

      public class BookConsoleWriter : IBookDataWriter
    {
        public void write(Books book)
        {
            Console.WriteLine($"{book.Name} {book.Author} {book.style} {book.year}");
        }
    }

      public class BookFileWriter : IBookDataWriter
    {
        private readonly string _filePath;
        public BookFileWriter(string filePath)
        {
            _filePath = filePath;
        }
        public void write(Books b)
        {
            File.Create("books.txt");
            string[] lstTmp = { "*****", $"Name: {b.Name} ", $"Author: {b.Author} ", $"style: {b.style} ", $"year: {b.year} ", "*****" };
            File.WriteAllLines("books.txt", lstTmp);
        }
    }
    public interface IBookDataReader
    {
        void write(Books book);
        List<Books> LoadBooks();
    }
    public class BookService : IBookDataReader
    {
        private readonly string _filepath;
        public BookService(string filepath)
        {
            _filepath = filepath;
        }

        public List<Books> LoadBooks()
        {
            var lstBooks = new List<Books>();
            var readText = File.ReadAllLines(_filepath);
            Books book = new Books();
            foreach (var s in readText)
            {
                if (s.Contains("*") || s.Contains("%"))
                {
                    if (book.Name != null && book.Name.Length > 0)
                    {
                        lstBooks.Add(book);
                        book = new Books();
                    }

                    else
                        book = new Books();
                }
                else if (s.Contains("Name"))
                {
                    book.Name = s;
                }
                else if (s.Contains("Author"))
                {
                    book.Author = s;
                }
                else if (s.Contains("style"))
                {
                    book.style = s;
                }
                else if (s.Contains("year"))
                {
                    book.year = s;
                }
            }
            return lstBooks;
        }

        public void write(Books b)
        {
            if (!File.Exists("books.txt"))
                File.Create("books.txt"); 
            string[] lstTmp = { "*****", $"Name: {b.Name} ", $"Author: {b.Author} ", $"style: {b.style} ", $"year: {b.year} ", "*****" };
            File.WriteAllLines("books.txt", lstTmp);
        }
    }
    public class BooksController
    {
        private readonly IBookDataReader bookDataReader;
        public BooksController(IBookDataReader bookDataReader)
        {
            this.bookDataReader = bookDataReader;
        }
        public void AddBook()
        {
            Books b = new Books();
            Console.WriteLine("Enter name:");
            b.Name = Console.ReadLine();
            Console.WriteLine("Enter author of book:");
            b.Author = Console.ReadLine();
            Console.WriteLine("Enter style of book:");
            b.style = Console.ReadLine();
            Console.WriteLine("Enter  year of book:");
            b.year = Console.ReadLine();
            bookDataReader.write(b);
        }
        public void ListBooks()
        {
            var pe = bookDataReader.LoadBooks();
            foreach (var book in pe)
            {
                Console.WriteLine($"{book.Name} {book.Author} {book.style} {book.year}");
            }
        }
    }
    public class Books
    {
        public string Name { get; set; }    
        public string Author { get; set; }   
        public string style { get; set; }
        public string year { get; set; } 
    }
}
