using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Core.Models.Domains
{
    public class Task
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage ="Pole tytułjest wymagane.")]
        [Display(Name ="Tytuł")]
        public string Title { get; set; }
        [MaxLength(250)]
        [Required(ErrorMessage = "Pole opis jest wymagane.")]
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Pole kategoria jest wymagane.")]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Termin")]
        public DateTime? Term { get; set; }
        [Display(Name = "Zrealizowane")]
        public bool IsExecuted { get; set; }
        public string UserId { get; set; }

        public Category Category { get; set; }
        public ApplicationUser User { get; set; }
    }
} 
