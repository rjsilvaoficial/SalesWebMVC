using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }

        public Department( string name)
        {
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }
        
        public double TotalSales(DateTime initial, DateTime finish)
        {
            //Se temos uma coleção da qual precisaremos de um resultado que é o somatório de somatórios presentes nos membros desta coleção
            //Ou seja, se queremos o resultado de um somatório de itens de uma coleção, que são somatórios de outros itens
            //Chamamos a soma a partir do agregador mais alto e vamos descendo!
            //Sellers comporta todos os membros Seller de um Department
            //Cada membro Seller tem acesso ao seu próprio TotalSales()
            //Portanto Sellers, invoca o Sum()
            //Dentro deste Sum, requisitamos os objetos Seller de maneira que o mesmo tenha seu TotalSales compreendido entre initial e finish
            return Sellers.Sum(cadaSeller => cadaSeller.TotalSales(initial, finish));
        }
    }
}
