using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping
{
    internal class Product
    {
        private ListProducts _products;

        public string ProductName { get; init; }

        public double ProductPrice { get; init; }

        public int ProductQuantity { get; init; }

        public Product()
        {
        }

        public Product(string productName, double productPrice, int productQuantity)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            ProductQuantity = productQuantity;
        }

        /// <summary>
        /// Додавання товару до списку.
        /// </summary>
        /// <param name="product">Об'єкт товара для додавання.</param>
        public void AddProductToList(Product product)
        {
            if (_products == null)
            {
                _products = new ListProducts(product, null);
            }
            else
            {
                ListProducts listOrders = new ListProducts(product, _products);

                _products = listOrders;
            }
        }

        /// <summary>
        /// Повернення списку товарів.
        /// </summary>
        /// <returns>Список товарів.</returns>
        public ListProducts GetListProducts()
        {
            return _products;
        }
    }
}
