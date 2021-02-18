using MyTasks.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Core.ViewModels
{
    public class CategoryViewModel
    {
        public string Heading { get; set; }
        public Category Category { get; set; }
    }
}
