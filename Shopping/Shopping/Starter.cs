using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping
{
    internal class Starter
    {
        public void Run()
        {
            User user = CreateUser();

            // Якщо користувач із правами адміна, то може створити новий товар Пароль:(123456)
            ListProducts listProducts = CreateProductForShop();

            listProducts = CheckedUserOnAdmin(user, listProducts);

            (int countProduct, double _) = ShowAllProduct(listProducts);

            Order order = AddProductToCart(user, listProducts, countProduct);

            OrderProducts(order);
        }

        /// <summary>
        /// Створення користувача.
        /// </summary>
        /// <returns>Повертаємо створеного користувача.</returns>
        private User CreateUser()
        {
            Console.WriteLine("If you want to get a discount, you need to register\n1. I want to register\n2. No, I don`t want to regiatration");

            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Console.WriteLine("Please, input your data");

                Console.WriteLine("Input your Firstname");
                string firstName = Console.ReadLine();

                Console.WriteLine("Input your Lastname");
                string lastName = Console.ReadLine();

                Console.WriteLine("Input yout Email");
                string email = Console.ReadLine();

                Console.WriteLine("Input your phone numbers");
                string phoneNumbers = Console.ReadLine();

                Console.WriteLine("Input password");
                string password = Console.ReadLine();

                User user = new User(firstName, lastName, email, phoneNumbers, password);

                return user;
            }

            return new User();
        }

        /// <summary>
        /// Перевірка користувача на права для створення нового товару для магазину.
        /// </summary>
        /// <param name="user">Користува для перевірки.</param>
        /// <param name="listProducts">Список товарі який буде наповнюватися.</param>
        /// <returns>Новий список товарів.</returns>
        private ListProducts CheckedUserOnAdmin(User user, ListProducts listProducts)
        {
            if (user.Rols.ToLower() == "admin")
            {
                Console.WriteLine("You can create new product\nDo you want create new product for shop?\n1. Yes\n2. No");
                int choiceForCreateProduct = int.Parse(Console.ReadLine());

                if (choiceForCreateProduct == 1)
                {
                    Console.WriteLine("How you want to create products?");
                    int counteCreateProduct = int.Parse(Console.ReadLine());

                    for (int i = 0; i < counteCreateProduct; i++)
                    {
                        listProducts = AddProduct(listProducts);
                    }
                }
            }

            return listProducts;
        }

        /// <summary>
        /// Створення списку товарів.
        /// </summary>
        /// <returns>Готовий двухсвязаний список товарів.</returns>
        private ListProducts CreateProductForShop()
        {
            Product product = new Product();

            product.AddProductToList(new Product("T-shirt", 14.99, 3));
            product.AddProductToList(new Product("Pants", 18.99, 8));
            product.AddProductToList(new Product("Shoes", 15.99, 5));
            product.AddProductToList(new Product("T-shirt", 16.49, 9));
            product.AddProductToList(new Product("Skirt", 8.99, 15));
            product.AddProductToList(new Product("Hat", 12.49, 12));

            return product.GetListProducts();
        }

        /// <summary>
        /// Надає можливіть Adminу додавати нові товари до каталогу.
        /// </summary>
        /// <param name="listProducts">Приймає, готовий список товарів, для додавання нових товарів.</param>
        /// <returns>Повертає список із доданими товарами.</returns>
        private ListProducts AddProduct(ListProducts listProducts)
        {
            Product product = new Product();

            // Додаємо товари до нового списку, в який додамо новий товар.
            while (listProducts != null)
            {
                product.AddProductToList(listProducts.Value);

                listProducts = listProducts.Next;
            }

            // Створення нового товару.
            Console.WriteLine("Input name for product:");
            string nameProduct = Console.ReadLine();

            Console.WriteLine($"Input price for product '{nameProduct}':");

            string price = Console.ReadLine().Replace('.', ',');
            double priceProduct = Convert.ToDouble(price);

            Console.WriteLine($"Input quantity for product '{nameProduct}':");
            int quantityProduct = int.Parse(Console.ReadLine());

            product.AddProductToList(new Product(nameProduct, priceProduct, quantityProduct));

            Console.WriteLine($"\nProduct '{nameProduct}' is create\n");

            return product.GetListProducts();
        }

        /// <summary>
        /// Виведення інформації про наявні товари в магазині.
        /// </summary>
        /// <param name="listProducts">Двухсвязаний список товарів.</param>
        /// <returns>Повертаю (загальна кількість товарів, повертає ціну на товар).</returns>
        private (int, double) ShowAllProduct(ListProducts listProducts)
        {
            int productIndex = 1;

            double productPrice = 0;

            // Виводимо інфу про товари.
            while (listProducts != null)
            {
                Console.WriteLine($"{productIndex++} - Product name: {listProducts.Value.ProductName} - Product price: {listProducts.Value.ProductPrice}");

                productPrice += listProducts.Value.ProductPrice;

                listProducts = listProducts.Next;
            }

            return (productIndex, productPrice);
        }

        /// <summary>
        /// Наповнення корзини товарами які вибираємо.
        /// </summary>
        /// <param name="user">Користувач для відправки товару.</param>
        /// <param name="listProducts">Список всіх товарів.</param>
        /// <param name="countProducts">Загальна кількість товарів.</param>
        /// <returns>Об'єкт Order із сформованими товарами для додавання їх до відправки.</returns>
        private Order AddProductToCart(User user, ListProducts listProducts, int countProducts)
        {
            Product product = new Product();

            Cart cart = new Cart();

            int productIndexForAddedToCart = 1;

            // Створення масиву товарів для додавання в корзину (та сортування для обробки).
            Console.WriteLine("\nSelect products, what you want to buy:");

            string[] indexForAddedProductToCart = Console.ReadLine().Split(',');

            for (int i = 0; i < indexForAddedProductToCart.Length; i++)
            {
                indexForAddedProductToCart[i] = indexForAddedProductToCart[i].Trim();
            }

            Array.Sort(indexForAddedProductToCart);

            // Наповнення корзини вибраними товарами.
            for (int i = 0; i < indexForAddedProductToCart.Length; i++)
            {
                for (int y = 0; y < countProducts; y++)
                {
                    if (productIndexForAddedToCart == int.Parse(indexForAddedProductToCart[i].Trim()))
                    {
                        product = listProducts.Value;

                        cart.AddProductToCart(product);

                        listProducts = listProducts.Next;

                        productIndexForAddedToCart++;

                        break;
                    }

                    listProducts = listProducts.Next;

                    productIndexForAddedToCart++;
                }
            }

            // Перевірна користувача на реєстрацію, та надання данних.
            user = CheckedUser(user);

            Order order = new Order(user, cart.GetListOrders());

            return order;
        }

        /// <summary>
        /// Фомруємо ордер та загальну ціну із бонусом.
        /// </summary>
        /// <param name="order">Об'єкт сформованого ордеру для відправки товару користувачу</param>
        private void OrderProducts(Order order)
        {
            Console.WriteLine($"\nDear {order.User.FirstName}\nYou create order product\n");

            double priceCoupon = CreatePriceOfCoupon(order);

            // Дізнаємося загальну ціну на товари.
            (int _, double productPrice) = ShowAllProduct(order.Products);

            Console.WriteLine($"Your order price = {Math.Round(productPrice, 4)} \nPrice in coupon:{priceCoupon} - {productPrice - (productPrice * (priceCoupon / 100))}");

            // Записуємо інформацію в файл.
            WriteReadFile writeReadFile = new WriteReadFile(order.Products, order.User);
        }

        /// <summary>
        /// Перевіряємо на створення корисувача.
        /// </summary>
        /// <param name="user">Об'єкт користувача User.</param>
        /// <returns>Повернення створеного користувача.</returns>
        private User CheckedUser(User user)
        {
            // Перевіляємо, чи є інформація про людину, кому саме потрібно відправляти товар.
            bool ischecked = true;

            while (ischecked)
            {
                if (user.FirstName == null)
                {
                    Console.WriteLine("You need to enter your details so that we know where and to whom to send the goods");

                    user = CreateUser();

                    ischecked = true;
                }
                else
                {
                    ischecked = false;
                }
            }

            return user;
        }

        /// <summary>
        /// Створення купона для користувача.
        /// </summary>
        /// <param name="order">Створений користувач якому буде надаватися купон.</param>
        /// <returns>Ціна купона.</returns>
        private double CreatePriceOfCoupon(Order order)
        {
            Random random = new Random();

            double priceCoupons = 0;

            if (order.Id > 0)
            {
                priceCoupons = random.Next(1, 10);
            }

            return priceCoupons;
        }
    }
}
