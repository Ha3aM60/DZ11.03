

namespace ConsoleApp27
{
    public interface IPersonDataWriter
    {
        void write(Person person);
    }
    
    public class PersonConsoleWriter : IPersonDataWriter
    {
        public void write(Person p)
        {
            Console.WriteLine($"{p.FirstName} {p.LastName} {p.dateOfBirth} {p.OtherInfo}");
        }
    }

    public class PersonFileWriter : IPersonDataWriter
    {
        private readonly string _filePath;
        public PersonFileWriter(string filePath)
        {
            _filePath = filePath;
        }
        public void write(Person p)
        {
            File.Create("persons.txt");
            string[] lstTmp = { "*****", $"Name: {p.FirstName} ", $"Last: {p.LastName} ", $"Bt: {p.dateOfBirth} ", $"Des: {p.OtherInfo} ", "*****" };
            File.WriteAllLines("persons.txt", lstTmp);
        }
    }
    public interface IPersonDataReader
    {
        void write(Person person);
        List<Person> LoadPerson();
    }
    public class PersonService : IPersonDataReader
    {
        private readonly string _filepath;
        public PersonService(string filepath)
        {
            _filepath = filepath;
        }

        public List<Person> LoadPerson()
        { 
            var lstPersons = new List<Person>();
            var readText = File.ReadAllLines(_filepath);
            Person person = new Person();
            foreach (var s in readText)
            {
                if (s.Contains("*") || s.Contains("%"))
                {
                    if (person.FirstName != null && person.FirstName.Length > 0)
                    {
                        lstPersons.Add(person);
                        person = new Person();
                    }

                    else
                        person = new Person();
                }
                else if (s.Contains("Name"))
                {
                    person.FirstName = s;
                }
                else if (s.Contains("Last"))
                {
                    person.LastName = s;
                }
                else if (s.Contains("Bt"))
                {
                    person.dateOfBirth = s;
                }
                else if (s.Contains("Des"))
                {
                    person.OtherInfo = s;
                }
            }
            return lstPersons;
        }

        public void write(Person p)
        {
            if(!File.Exists("persons.txt"))
            File.Create("persons.txt");
            string[] lstTmp = { "*****", $"Name: {p.FirstName} ", $"Last: {p.LastName} ", $"Bt: {p.dateOfBirth} ", $"Des: {p.OtherInfo} ", "*****" };
            File.WriteAllLines("persons.txt", lstTmp);
        }
    }
    public class PersonController
    {
        private readonly IPersonDataReader personDataReader;
        public PersonController(IPersonDataReader personDataReader)
        {
            this.personDataReader = personDataReader;
        } 
        public void AddPerson()
        {
            Person person = new Person();
            Console.WriteLine("Enter name:");
            person.FirstName = Console.ReadLine();
            Console.WriteLine("Enter last name:");
            person.LastName = Console.ReadLine();
            Console.WriteLine("Enter date of birth:");
            person.dateOfBirth = Console.ReadLine();
            Console.WriteLine("Enter other info:");
            person.OtherInfo= Console.ReadLine();
            personDataReader.write(person);
        }
        public void ListPersons()
        {
            var pe = personDataReader.LoadPerson();
            foreach(var p in pe )
            {
                Console.WriteLine($"{p.FirstName} {p.LastName} {p.dateOfBirth} {p.OtherInfo}");
            }
        }
    }
    public class Person
    {
        public string FirstName { get; set; }    
        public string LastName { get; set; }
        public string dateOfBirth { get; set; }
        public string OtherInfo { get; set; }
    }
}
