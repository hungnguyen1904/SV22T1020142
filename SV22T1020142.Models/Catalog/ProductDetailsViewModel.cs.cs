using System.Collections.Generic;

namespace SV22T1020142.Models.Catalog
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }

        public List<ProductPhoto> Photos { get; set; }

        public List<ProductAttribute> Attributes { get; set; }
    }
}