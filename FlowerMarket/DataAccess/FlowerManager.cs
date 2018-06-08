using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowerMarket.Models;

namespace FlowerMarket.DataAccess
{
  public class FlowerManager
    {
        public IList<Flower> FlowerFilt(string name, string Country, string Sell_Price)
        {
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                IList<Flower> list = new List<Flower>();
                DbCommand command = db.CreateCommand();
                command.CommandText = "SELECT * from Flower where (Name like '%' + @Name + '%') or (Country like '%' + @Country + '%') or (Sell_Price like '%' + @Sell_Price + '%')";
                command.AddParameter("@Name", System.Data.DbType.String, name);
                command.AddParameter("@Country", System.Data.DbType.String, Country);
                command.AddParameter("@Sell_Price", System.Data.DbType.String, Sell_Price);
                db.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Flower model = new Flower();
                    model.ID = (int)reader["ID"];
                    model.Name = (string)reader["Name"];
                    model.Country = (string)reader["Country"];
                    model.Sell_Price = (double)reader["Sell_Price"];
                    list.Add(model);
                }
                return list;
            }
        }


        public IList<Flower> GetFlower(string sortField, string search = null)
        {
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                IList<Flower> list = new List<Flower>();
                DbCommand command = db.CreateCommand();
                string sort = "";
                if (!string.IsNullOrEmpty(sortField))
                {
                    sort = " Order by " + sortField.Replace("_desc", " ")
                                        + (sortField.EndsWith("_desc")
                                        ? " Desc "
                                        : " Asc "); 
                }
                else
                {
                    sort = "";
                }
                if (search != null && search.Trim().Length > 0)
                {
                    command.CommandText = "SELECT * FROM Flower where (Name like '%" + search.Trim() + "%') or (Country like '%" + search.Trim() + "%') or (Price like '%" + search.Trim() + "%')";
                }
                else if(sort == "")
                {
                    command.CommandText = "SELECT * FROM Flower";
                }
                else
                {
                    command.CommandText = "SELECT * FROM Flower" + sort;
                }
                
                command.CommandType = System.Data.CommandType.Text;
                db.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Flower model = new Flower();
                    model.ID = (int)reader["FlowerID"];
                    model.Name = (string)reader["Name"];
                    model.Country = (string)reader["Country"];
                    model.Sell_Price = (double)reader["Price"];
                    list.Add(model);
                }
                return list;
            }

        }
        public Flower GetFlowers(int ID)
        {
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = "SELECT * FROM Flower where ID=@ID";
                command.AddParameter("@ID", System.Data.DbType.Int32, ID);
                db.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Flower model = new Flower();
                    model.ID = (int)reader["FlowerID"];
                    model.Name = (string)reader["Name"];
                    model.Country = (string)reader["Country"];
                    model.Sell_Price = (double)reader["Price"];
                    return model;
                }
                return null;
            }
        }
    }
}
