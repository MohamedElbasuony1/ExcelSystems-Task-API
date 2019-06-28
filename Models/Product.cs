using System.Collections.Generic;

namespace Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public int SupplierId { get; set; }
        public User User { get; set; }
    }
}
