using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping
{
    internal class WriteReadFile
    {
        public WriteReadFile(ListProducts listProducts, User user)
        {
            StringBuilder stringBuilder = new StringBuilder();

            string fileNameForSave = "orders.txt";

            File.WriteAllText(fileNameForSave, $"Your Fullname: {user.FirstName} {user.LastName}, Phone number: {user.PhoneNumber}\n");

            while (listProducts != null)
            {
                stringBuilder.AppendLine($"Name product: {listProducts.Value.ProductName}, Price product: {listProducts.Value.ProductPrice}");
                listProducts = listProducts.Next;
            }

            File.AppendAllText(fileNameForSave, stringBuilder.ToString());
        }
    }
}
