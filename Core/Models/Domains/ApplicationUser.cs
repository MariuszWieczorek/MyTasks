using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Core.Models.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Tasks = new Collection<Task>();
            Users = new Collection<ApplicationUser>();
        }
        
        public ICollection<Task> Tasks;
        public ICollection<ApplicationUser> Users;
    }
}
