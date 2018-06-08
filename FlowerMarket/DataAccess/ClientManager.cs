using FlowerMarket.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FlowerMarket.DataAccess
{
    public class ClientManager
    {
        public Client GetCLientByID(int ID)
        {
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = "select top 1 * from customer where ID=@ID";
                command.AddParameter("@ID", DbType.Int32, ID);
                db.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return GetClient(reader);
                }
                return null;
            }
        }
        public Client AuthirizedClient()
        {
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = "select top 1 * from client where Username=@Username";
                command.AddParameter("@username", DbType.String, HttpContext.Current.User.Identity.Name);
                db.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return GetClient(reader);
                }
                return null;
            }
        }

        public Client GetClient(DbDataReader reader)
        {
            Client model = new Client();
            model.ID = (int)reader["ClientID"];
            model.Username = (string)reader["Username"];
            model.Password = (string)reader["Password"];
            model.Name = (string)reader["Name"];
            model.Surname = (string)reader["Surname"];
            model.Email = (string)reader["Email"];
            model.Age = (int)reader["Age"];
            model.WorkExperience = (int)reader["W_ex"];
            model.Phone = (string)reader["Phone"];

            return model;
        }
        public bool UserExist(Client model)
        {
            bool auth = false;
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = "select [dbo].User_created(@Username, @Password)";
                command.AddParameter("@Username", DbType.String, model.Username);
                command.AddParameter("@Password", DbType.String, model.Password);
                db.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    auth = reader.GetBoolean(0);
                }
            }
            return auth;
        }
        public void New_Client(Client model)
        {
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = @"New_user";
                command.CommandType = CommandType.StoredProcedure;
                command.AddParameter("@Username", DbType.String, model.Username);
                command.AddParameter("@Password", DbType.String, model.Password);
                command.AddParameter("@Name", DbType.String, model.Name);
                command.AddParameter("@Surname", DbType.String, model.Surname);
                command.AddParameter("@Email", DbType.String, model.Email);
                command.AddParameter("@Age", DbType.Int32, model.Age);
                command.AddParameter("@W_ex", DbType.Int32, model.WorkExperience);
                command.AddParameter("@Phone", DbType.String, model.Phone);
                db.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Update_Client(Client model)
        {
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = @"Update client set
                                           [Surname]    = @Surname,
                                           [Email]      = @Email,
                                           [Age]        = @Age,
                                           [W_ex]       = @W_ex,
                                           [Phone]      = @Phone,
                                           [Password]   = @Password
                                        From [dbo].[Client] 
                                        Where [ClientID] = @ClientID
                                       ";
                
                command.AddParameter("@Password", DbType.String, model.Password);
                command.AddParameter("@Surname", DbType.String, model.Surname);
                command.AddParameter("@Email", DbType.String, model.Email);
                command.AddParameter("@Age", DbType.Int32, model.Age);
                command.AddParameter("@W_ex", DbType.Int32, model.WorkExperience);
                command.AddParameter("@Phone", DbType.String, model.Phone);
                command.AddParameter("@ClientID", DbType.Int32, model.ID);
                db.Open();
                command.ExecuteNonQuery();
            }
        }
        public IList<Client> GetAllClients()
        {
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                IList<Client> list = new List<Client>();
                DbCommand command = db.CreateCommand();
                command.CommandText = @"Select [ClientID],
                                                [Name],
                                                [Surname],
                                                [Email],
                                                [Age],
                                                [W_ex],
                                                [Phone],
                                                [Password]
                                        From [dbo].[Client]
                                        where [Name] like '%' + @Name + '%'";
                command.AddParameter("@Name", DbType.String, HttpContext.Current.User.Identity.Name);
                db.Open();
                DbDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    Client cl = new Client()
                    {
                        ID = (int)rdr["ClientID"],
                        Password = (string)rdr["Password"],
                        Name = (string)rdr["Name"],
                        Surname = (string)rdr["Surname"],
                        Email = (string)rdr["Email"],
                        Age = (int)rdr["Age"],
                        WorkExperience = (int)rdr["W_ex"],
                        Phone = (string)rdr["Phone"]
                    };
                    list.Add(cl);
                }
                return list;
            }
        }
        public Client GetClientByIdE (int id)
        {
            Client cl = null;
            using (DbConnection db = new SqlConnection(FL_Manager.connectionString))
            {
                DbCommand command = db.CreateCommand();
                command.CommandTimeout = 120;
                command.CommandText = @"Select [ClientID],
                                                [Name],
                                                [Surname],
                                                [Email],
                                                [Age],
                                                [W_ex],
                                                [Phone],
                                                [Password]
                                        From [dbo].[Client]   
                                        Where [ClientID] = @ClientID";
                command.AddParameter("@ClientID", DbType.Int32, id);
                db.Open();
                using(DbDataReader rdr = command.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        cl = new Client()
                        {
                            ID = (int)rdr["ClientID"],
                            Password = (string)rdr["Password"],
                            Name = (string)rdr["Name"],
                            Surname = (string)rdr["Surname"],
                            Email = (string)rdr["Email"],
                            Age = (int)rdr["Age"],
                            WorkExperience = (int)rdr["W_ex"],
                            Phone = (string)rdr["Phone"]
                        };
                    }
                }

            }
            return cl;
        }

    }
}
