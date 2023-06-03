using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping
{
    internal class ListProducts
    {
        public ListProducts Next { get; }

        public Product Value { get; }

        public ListProducts(Product product, ListProducts next)
        {
            Next = next;

            Value = product;
        }
    }
}
