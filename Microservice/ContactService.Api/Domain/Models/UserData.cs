using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.Domain.Models
{
    public class UserData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static List<UserData> UserList = new List<UserData>();

        static UserData()
        {
            UserList.Add(new UserData { Id = new Guid("9bc377e7-688a-4c3a-9fbf-b06362a50292"), Name = "Alper" });
            UserList.Add(new UserData { Id = new Guid("aa04231d-fd6a-4076-ab96-f9cdd6cc02e3"), Name = "Test" });
        }
    }

}
