using FlowerMarket;
using FlowerMarket.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerMarket.DataAccess
{
    public class OrderManager
    {
        public void New_Order(Order model)
        {
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = "insert into [Orders] (FlowerID, ClientID, Sell_Date, Price, Quantity) values(@Flower, @Client, getdate(), @Price, @Quantity)";
                command.AddParameter("@Flower", System.Data.DbType.Int32, model.Flower);
                ClientManager manager = new ClientManager();
                command.AddParameter("@Client", System.Data.DbType.Int32, manager.AuthirizedClient().ID);
                command.AddParameter("@Quantity", System.Data.DbType.Int32, model.Quantity);
                command.AddParameter("@Price", System.Data.DbType.Int32, model.Price);
                db.Open();
                command.ExecuteNonQuery();
            }
        }
        public IList<Order> GetOrders()
        {
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                IList<Order> list = new List<Order>();
                DbCommand command = db.CreateCommand();
                command.CommandText = "SELECT * FROM Orders";
                db.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order model = new Order();
                    model.ID = (int)reader["OrdersID"];

                    model.Client = (int)reader["ClientID"];
                    model.Flower = (int)reader["FlowerID"];
                    model.Price = (double)reader["Price"];
                    model.Quantity = (int)reader["Quantity"];
                    model.Sell_Date = (DateTime)reader["Sell_Date"];
                    list.Add(model);
                }
                return list;
            }
        }



        public void Remove_Order(int ID)
        {
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = "delete from Orders where OrdersID=@ID";
                command.AddParameter("@ID", System.Data.DbType.Int32, ID);
                db.Open();
                command.ExecuteNonQuery();
            }
        }

        public Order GetOrder(int ID)
        {
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = "SELECT * FROM Orders where OrdersID=@ID";
                command.AddParameter("@ID", System.Data.DbType.Int32, ID);
                db.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Order model = new Order();
                    model.ID = (int)reader["OrdersID"];
                    model.Price = (double)reader["Price"];
                    model.Quantity = (int)reader["Quantity"];
                    model.Sell_Date = (DateTime)reader["Sell_Date"];
                    model.Client = (int)reader["ClientID"];
                    model.Flower = (int)reader["FlowerID"];

                    return model;
                }
                return null;
            }
        }

        public void updateOrder(Order model)
        {
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = "update [Orders] set FlowerID=@FlowerID, ClientID=@Client, Sell_Date=@Sell_Date, Price=@Price, Quantity=@Quantity where OrdersID=@OrdersID";
                command.AddParameter("@OrdersID", System.Data.DbType.Int32, model.ID);
                command.AddParameter("@FlowerID", System.Data.DbType.Int32, model.Flower);
                ClientManager manager = new ClientManager();
                command.AddParameter("@Client", System.Data.DbType.Int32, manager.AuthirizedClient().ID);
                command.AddParameter("@Quantity", System.Data.DbType.Int32, model.Quantity);
                command.AddParameter("@Sell_Date", System.Data.DbType.DateTime, model.Sell_Date);
                command.AddParameter("@Price", System.Data.DbType.Double, model.Price);
                db.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
