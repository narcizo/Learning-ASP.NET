using GenericProject.Model;

namespace GenericProject.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            //TODO: access DB, Create new entry for new 'person' and return the saved object
            return person;
        }

        public Person Update(Person person)
        {
            //TODO: access DB, updtade 'person' and return the saved object
            return person;
        }

        public Person Delete(long id)
        {
            //TODO: not sure if method must return void or the deleted Person
            throw new NotImplementedException();
        }

        public Person FindById(long id)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Zé",
                LastName = "Filipe",
                Address = "Mato Grosso",
                Gender = "Não Binário",
            };
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Person name" + i,
                LastName = "Person lastname" + i,
                Address = "Some Address" + i,
                Gender = i%2 == 0 ? "Male" : "Female",
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
