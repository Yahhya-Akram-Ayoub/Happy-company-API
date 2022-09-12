using ModelsRepository;
using ModelsRepository.Models;

namespace Kaizen_Task.Services
{
    public class DataSeeder
    {
        private readonly IManagerUnit _manager;
        public DataSeeder(IManagerUnit managerunit)
        {
            _manager = managerunit;
        }

        public void Seed()
        {
            if (_manager.Roles.Count() == 0)
            {
                List<Role> roles = new List<Role>()
                {
                    new Role(){ Name = "Admin" },
                    new Role(){ Name = "Management"},
                    new Role(){ Name = "Auditor"}
                };
                _manager.Roles.SaveRange(roles);
                _manager.Complete();
            }

            if (_manager.Users.Count() == 0)
            {
                User admin = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@happywarehouse.com",
                    Password = "P@ssw0rd",
                    FullName = "Admin",
                    Role = "Admin",
                    Active = true,
                };
                _manager.Users.Add(admin);
                _manager.Complete();
            }
        }
    }
}
