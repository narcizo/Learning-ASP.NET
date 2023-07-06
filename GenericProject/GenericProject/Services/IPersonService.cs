﻿using GenericProject.Model;

namespace GenericProject.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person Update(Person person);
        Person FindById(long id);
        List<Person> FindAll();
        Person Delete(long id);
    }
}
