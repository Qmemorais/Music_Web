using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Interface
{
    public interface IUserService
    {
        bool Create(int id, string email, string name = "", string surname = "");
        bool Delete(int id);
        bool Rename(int id, string newName);
        bool Resurname(int id, string newSurname);
        bool Reemail(int id, string newEmail);
    }
}
