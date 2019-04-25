using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Создайте приложение по шаблону ConsoleApplication.
В этом приложении сделайте экземпляр класса DataSet с именем
ShopDB.Создайте объекты DataTable с именами Orders, Customers,
Employees, OrderDetails, Products со схемами, идентичными схемам
таблицам базы данных ShopDB(c ограничениями для столбцов).
Добавьте созданные таблицы в коллекцию таблиц ShopDB.
Для всех таблиц в ShopDB создайте ограничения PrimaryKey и
ForeignKey.*/

namespace AdoNet_HW_4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Создание БД
            DataSet shopDB = new DataSet("ShopDB");
            //Создание таблицы
            DataTable customers = new DataTable("Customers");//Покупатель
            DataTable employes = new DataTable("Employees");//Сотрудники
            DataTable products = new DataTable("Products");
            DataTable orders = new DataTable("Orders");//заказы
            DataTable orderDetails = new DataTable("OrderDetails");//Информация для заказа
            

            //Создаю столбцы и задаю свойства
            Init_Man(ref customers);
            Init_Man(ref employes);
            Init_Product(ref products);
            Init_Order(ref orders);
            Init_orderDetails(ref orderDetails);
            /*-----------------------------------------------*/
            

            ForeignKeyConstraint FK_orders_customer = new ForeignKeyConstraint(orders.Columns["customer"], customers.Columns["ID"]);
            customers.Constraints.Add(FK_orders_customer);

            ForeignKeyConstraint FK_orders_employees = new ForeignKeyConstraint(orders.Columns["seller"], employes.Columns["ID"]);
            employes.Constraints.Add(FK_orders_employees);

            ForeignKeyConstraint FK_orders_products = new ForeignKeyConstraint(orders.Columns["product"], products.Columns["Id"]);
            products.Constraints.Add(FK_orders_products);

            ForeignKeyConstraint FK_orderDetails = new ForeignKeyConstraint(orderDetails.Columns["Id"], orders.Columns["DetailId"]);
            orders.Constraints.Add(FK_orderDetails);


            shopDB.Tables.AddRange(new DataTable[]
            {
                customers,
                employes,
                products,
                orders,
                orderDetails
            });

            Human man = Init_man();
            Add_Man(customers, man);

            Human man2 = Init_man();
            Add_Man(employes, man2);

            Console.WriteLine("Введите колличество наименований продуктов на складе");
            int.TryParse(Console.ReadLine(), out int cnt);
            for(int i = 0; i<= cnt; i++)
            {
                Product product = Init_Product();
                Add_Product(products, product);
            }
            
        }

        private static void Init_orderDetails(ref DataTable orderDetails)
        {
            DataColumn id = new DataColumn("Id", typeof(int));
            id.AllowDBNull = false;
            id.AutoIncrement = true;
            id.AutoIncrementSeed = 1;
            id.AutoIncrementStep = 1;
            id.Caption = "Идентификатор";
            id.Unique = true;
            id.ReadOnly = true;

            DataColumn detail = new DataColumn("detail", typeof(string))
            {
                AllowDBNull = false,
                Caption = "Наличие продукта",
                MaxLength = 10
            };

            orderDetails.Columns.AddRange(new DataColumn[]
            {
                id,
                detail
            });

            orderDetails.PrimaryKey = new DataColumn[] { orderDetails.Columns["Id"] };
        }
        private static void Init_Order(ref DataTable orders)
        {
            DataColumn id = new DataColumn("Id", typeof(int));
            id.AllowDBNull = false;
            id.AutoIncrement = true;
            id.AutoIncrementSeed = 1;
            id.AutoIncrementStep = 1;
            id.Caption = "ID";
            id.Unique = true;
            id.ReadOnly = true;

            DataColumn customer = new DataColumn("customer", typeof(int));
            DataColumn seller = new DataColumn("seller", typeof(int));            
            DataColumn time = new DataColumn("time", typeof(DateTime));
            DataColumn product = new DataColumn("product", typeof(int));
            DataColumn product_count = new DataColumn("product_count", typeof(int));
            DataColumn sum = new DataColumn("sum", typeof(int));
            DataColumn DetailId = new DataColumn("DetailId", typeof(int))
            {
                Caption = "Наличие продукта",
                AllowDBNull = false
            };

            orders.Columns.AddRange(new DataColumn[]
             {
                id,
                customer,
                time,
                product,
                product_count,
                sum
             });

            orders.PrimaryKey = new DataColumn[] { orders.Columns["Id"] };           
        }

        /*-----------------------------------------------*/
        private static void Init_Product(ref DataTable products)
        {
            DataColumn Id = new DataColumn("Id", typeof(int));
            Id.AllowDBNull = false;
            Id.AutoIncrement = true;
            Id.AutoIncrementSeed = 1;
            Id.AutoIncrementStep = 1;
            Id.Caption = "ID";
            Id.Unique = true;
            Id.ReadOnly = true;

            DataColumn ProductName = new DataColumn("Product_name", typeof(string));
            ProductName.Caption = "Название продукта";
            ProductName.MaxLength = 60;

            DataColumn FirmName = new DataColumn("Firm", typeof(string));
            FirmName.Caption = "Производитель";
            FirmName.MaxLength = 20;

            DataColumn Price = new DataColumn("Price", typeof(string));
            Price.Caption = "Цена";
            Price.AllowDBNull = false;

            products.Columns.AddRange(new DataColumn[]
            {
                Id,
                ProductName,
                FirmName,
                Price
            });

            products.PrimaryKey = new DataColumn[] { products.Columns["ID"] };
        }
        private static void Add_Product(DataTable table, Product product)
        {
            DataRow newRow = table.NewRow();
            newRow["ProductName"] = product.Product_name;
            newRow["Firm"] = product.Firm_name;
            newRow["Price"] = product.Price;

            table.Rows.Add(newRow);
        }
        private static Product Init_Product()
        {
            Console.WriteLine("Введите название продукта");
            string prod_name = Console.ReadLine();
            Console.WriteLine("Введите наименование производителя");
            string firm = Console.ReadLine();
            Console.WriteLine("Введите цену продукта");
            double.TryParse(Console.ReadLine(), out double price);
            Product product = new Product(prod_name, firm, price);
            return product;
        }

        /*-----------------------------------------------*/
        private static void Init_Man(ref DataTable customers)
        {
            DataColumn ID = new DataColumn("ID", typeof(int));
            ID.AllowDBNull = false;
            ID.AutoIncrement = true;
            ID.AutoIncrementSeed = 1;
            ID.AutoIncrementStep = 1;
            ID.Caption = "ID";
            ID.Unique = true;
            ID.ReadOnly = true;
            
            DataColumn Name = new DataColumn("Name", typeof(string));
            Name.Caption = "Имя";
            Name.MaxLength = 60;

            DataColumn Telephon_number = new DataColumn("Number", typeof(string));
            Telephon_number.Caption = "Телефон";
            Telephon_number.MaxLength = 20;

            customers.Columns.AddRange(new DataColumn[]
            {
                ID,
                Name,
                Telephon_number
            });

            customers.PrimaryKey = new DataColumn[] { customers.Columns["ID"] };
        }
        private static void Add_Man(DataTable table, Human man)
        {
            DataRow newRow = table.NewRow();
            newRow["Name"] = man.Name;
            newRow["Number"] = man.Telephon_number;

            table.Rows.Add(newRow);
        }        
        private static Human Init_man()
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();
            Console.WriteLine("Введите телефон");
            string number = Console.ReadLine();            
            Human man = new Human(name, number);
            return man;
        }
        /*-----------------------------------------------*/



    }
}
