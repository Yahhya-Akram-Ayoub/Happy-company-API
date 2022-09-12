using ModelsRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.Test.MocData
{
    public class UserMocData
    {
        private static List<User> users = new List<User>()
        {
            new User(){ Id = Guid.NewGuid() ,  Email = "Yahhya@test.com" , Active = true ,Role = "Admin" , FullName = "Yahhya Ayoub" , Password = "123456"},
            new User(){ Id = Guid.NewGuid() ,  Email = "Zead@test.com" , Active = true ,Role = "Auditor" , FullName = "Zead Ali" , Password = "123456"},
            new User(){ Id = Guid.NewGuid() ,  Email = "Ahmad@test.com" , Active = true ,Role = "Management" , FullName = "Ahmad Hmad" , Password = "123456"},
            new User(){ Id = Guid.NewGuid() ,  Email = "Maria@test.com" , Active = true ,Role = "Auditor" , FullName = "Maria" , Password = "123456"}
        };

        public static IQueryable<User> GetUsers()
        {
            return users.AsQueryable();
        }
    }
}
