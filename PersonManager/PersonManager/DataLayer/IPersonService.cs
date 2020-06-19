using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonManager.DataLayer
{
    public interface IPersonService
    {
        Task<IList<Person>> GetPeople();
        Task<bool> AddPerson(Person person);
        Task<bool> DeletePerson(int id);
        Task<Person> GetOldestPerson();
    }
}
