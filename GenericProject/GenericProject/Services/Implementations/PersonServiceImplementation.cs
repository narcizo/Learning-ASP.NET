using GenericProject.Model;

namespace GenericProject.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private MySqlContext _context;

        public PersonServiceImplementation(MySqlContext context)
        {
            _context = context;
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }

        public Person Update(Person person)
        {
            var queryResult = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            if (queryResult != null)
            {
                try
                {
                    _context.Entry(queryResult).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return person;
            }

            return new Person();
        }

        public void Delete(long id)
        {
            var queryResult = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (queryResult != null)
            {
                try
                {
                    _context.Persons.Remove(queryResult);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return;
            }
        }

    }
}
