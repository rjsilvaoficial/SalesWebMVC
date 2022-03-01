using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} is required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage ="Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name= "Birth date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }


        [Range(1000,50000, ErrorMessage = "{0} must be from {1} and {2}")]
        [Required(ErrorMessage = "{0} is required")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        [Display(Name="Base salary")]
        public double BaseSalary { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller( string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        /*
        public double TotalSales(DateTime initial , DateTime finish)
        {
            double total = 0;
            foreach (SalesRecord sr in Sales)
            {
                if(sr.Date >= initial && sr.Date <= finish)
                {
                    total += sr.Amount;
                }
            }
            return total;

        }
        */
        public double TotalSales(DateTime initial, DateTime finish)
        {
            //Para escrevermos lambdas interdependentes, triamos o resultado no where
            //Posteriormente a isso usamos novamente lambda como atributo do método complementar para obter o resultado
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= finish).Sum(sr => sr.Amount);
        }

    }
}
