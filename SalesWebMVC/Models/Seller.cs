﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
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
