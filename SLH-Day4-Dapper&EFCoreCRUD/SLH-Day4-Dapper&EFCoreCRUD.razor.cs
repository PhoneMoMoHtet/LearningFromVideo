using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Runtime.CompilerServices;
using VideoLearning;
using System.Data;
using System.Diagnostics.Contracts;
using System.Net;
using System;
using VideoLearning.SLH_Day4_Dapper_EFCoreCRUD.Model;
using System.Transactions;


namespace VideoLearning.SLH_Day4_Dapper_EFCoreCRUD
{
    public partial class SLH_Day4_Dapper_EFCoreCRUD
    {
        [Inject]
        ProgramParameter ProgramParameter { get; set; }

        private string message;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            try
            {
                using (IDbConnection connection = new SqlConnection(ProgramParameter.DefaultConnectionString))
                {
                    connection.Open();
                    this.message += "connection success SicuraChiaveTest !<br>";
                    this.ShowEmployees(connection);
                }

                #region Author Insert/Slect test
                ////using (IDbConnection connection = new SqlConnection(ProgramParameter.PubsConnectionString))
                ////{
                ////    connection.Open();
                ////    this.message += "connection success Pubs !<br>";
                ////    var au_id = "172-32-1177";

                ////    using (var transaction = connection.BeginTransaction())
                ////    {
                ////        try
                ////        {
                ////            this.InsertAuthor(connection, transaction, new AuthorDataModel()
                ////                {
                ////                    Au_Id = au_id,
                ////                    Au_lname = "Htet",
                ////                    Au_fname = "Phone Mo Mo",
                ////                    Phone = "0823073709",
                ////                    Address = "Sukhumvit 64, Phrakhnong Tai",
                ////                    City = "Bangkok",
                ////                    State = "BK",
                ////                    Zip = "10260",
                ////                    Contract = true
                ////                });
                ////            this.message += "Insert Author Complete !<br>";

                ////            this.ShowAuthor(connection, transaction, au_id);
                ////            transaction.Commit();
                ////        }
                ////        catch(Exception ex)
                ////        {
                ////            this.message += ex.Message;
                ////            transaction.Rollback(); // if there is error, will not commit inserting testing data
                ////        }
                ////    }
                ////}
                #endregion


            }
            catch (Exception ex)
            {
                this.message += ex.Message;
            }
        }

        private void ShowEmployees(IDbConnection connection)
        {
            var query = "Select * from Employees";
            var employees = connection.Query(query);
            foreach (var employee in employees)
            {
                this.message += $"{employee.Id} {employee.Name}<br>";
            }
        }

        private void ShowAuthor(IDbConnection connection, IDbTransaction transaction, string au_id)
        {
            var author = this.GetAuthorById(connection, transaction, au_id);
            this.message += $"{author.Au_Id} {author.Au_fname} {author.Au_lname} {author.Phone} {author.Address} {author.City} {author.Zip}<br>";
        }

        private bool InsertAuthor(IDbConnection connection, IDbTransaction transaction, AuthorDataModel authorDataModel)
        {
            var query = @"INSERT INTO [dbo].[authors]
                               ( [au_id]
                               , [au_lname]
                               , [au_fname]
                               , [phone]
                               , [address]
                               , [city]
                               , [state]
                               , [zip]
                               , [contract])
                             VALUES
                               (@au_id,
                                @au_lname,
                                @au_fname,
                                @phone,
                                @address,
                                @city,
                                @state,
                                @zip,
                                @contract)";
            return connection.Execute(query, authorDataModel, transaction) == 1;
        }

        private AuthorDataModel GetAuthorById(IDbConnection connection,IDbTransaction transaction, string au_id)
        {
            var query = @"SELECT * FROM [dbo].[authors]
                          WHERE au_id = @au_id";
            return connection.QueryFirstOrDefault<AuthorDataModel>(query, new { au_id = au_id }, transaction);
        }
    }
}
