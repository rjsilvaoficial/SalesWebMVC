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
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name= "Birth date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
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
