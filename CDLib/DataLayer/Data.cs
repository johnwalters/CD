﻿using System.Linq;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

using System.Data;
using System;
using System.Collections.Generic;
using CDLib.Domain;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace CDLib.DataLayer
{
    public class Data
    {
        private SqlConnection _dapperSqlConnection;
        private string _connectionString;

        private SqlConnection SqlConnection
        {
            get
            {
                if (_dapperSqlConnection == null)
                {
                    _dapperSqlConnection = new SqlConnection(ConnectionString);
                }

                return _dapperSqlConnection;
            }
        }

        private string ConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    var connectionStringName = ConfigurationManager.AppSettings["ConnectionStringName"] ?? "";
                    if (string.IsNullOrEmpty(connectionStringName))
                    {
                        connectionStringName = "DefaultConnection";
                    }
                    _connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
                }
                return _connectionString;
            }
        }


        #region Company Methods
        public int CreateCompany(Company company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id",  dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Name", company.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@UserId", company.UserId, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Address1", company.Address1, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Address2", company.Address2, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@City", company.City, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@State", company.State, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@PostalCode", company.PostalCode, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@PhoneNumber", company.PhoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);

            SqlConnection.Execute("Company_Add", parameters, commandType: CommandType.StoredProcedure);
            int id = parameters.Get<int>("@Id");
            return id;
        }

        public void DeleteCompany(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            SqlConnection.Execute("Company_Delete", parameters, commandType: CommandType.StoredProcedure);
        }

        public void UpdateCompany(Company company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", company.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameters.Add("@Name", company.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@UserId", company.UserId, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Address1", company.Address1, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Address2", company.Address2, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@City", company.City, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@State", company.State, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@PostalCode", company.PostalCode, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@PhoneNumber", company.PhoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            
            SqlConnection.Execute("Company_Update", parameters, commandType: CommandType.StoredProcedure);

        }

        public Company GetCompany(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var company = SqlConnection.Query<Company>("Company_Get", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return company;
        }

        public List<Company> GetAllCompanies()
        {
            var parameters = new DynamicParameters();
            var companies = SqlConnection.Query<Company>("Company_GetAll", parameters, commandType: CommandType.StoredProcedure).ToList();
            return companies;
        }

       

        public Company GetCompanyByUserId(string userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId, dbType: DbType.String, direction: ParameterDirection.Input);

            var company = SqlConnection.Query<Company>("Company_GetByUserId", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return company;
        }
        #endregion

        #region Offer Methods
        public int CreateOffer(Offer offer)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Title", offer.Title, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@CompanyId", offer.CompanyId, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Description", offer.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Url", offer.Url, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Category", offer.Category, dbType: DbType.String, direction: ParameterDirection.Input);

            SqlConnection.Execute("Offer_Add", parameters, commandType: CommandType.StoredProcedure);
            int id = parameters.Get<int>("@Id");
            return id;
        }

        public void UpdateOffer(Offer offer)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", offer.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameters.Add("@Title", offer.Title, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Description", offer.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Url", offer.Url, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Category", offer.Category, dbType: DbType.String, direction: ParameterDirection.Input);

            SqlConnection.Execute("Offer_Update", parameters, commandType: CommandType.StoredProcedure);

        }

        public void DeleteOffer(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            SqlConnection.Execute("Offer_Delete", parameters, commandType: CommandType.StoredProcedure);
        }

        public Offer GetOffer(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var offer = SqlConnection.Query<Offer>("Offer_Get", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return offer;
        }

        public Offer GetOffer(string token)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Token", token, dbType: DbType.String, direction: ParameterDirection.Input);

            var offer = SqlConnection.Query<Offer>("Offer_GetByToken", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return offer;
        }

        public List<OfferExtended> GetAllOffers(int companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId, dbType: DbType.String, direction: ParameterDirection.Input);

            var offers = SqlConnection.Query<OfferExtended>("Offer_GetAll", parameters, commandType: CommandType.StoredProcedure).ToList();
            return offers;
        }
        #endregion
        #region OfferCode Methods
        public int CreateOfferCode(OfferCode offerCode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OfferId", offerCode.OfferId, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Code", offerCode.Code, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@ClaimingUser", offerCode.ClaimingUser, dbType: DbType.String, direction: ParameterDirection.Input);

            SqlConnection.Execute("OfferCode_Add", parameters, commandType: CommandType.StoredProcedure);
            int id = parameters.Get<int>("@Id");
            return id;
        }

        public void UpdateOfferCode(OfferCode offerCode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", offerCode.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameters.Add("@Code", offerCode.Code, dbType: DbType.String, direction: ParameterDirection.Input);

            SqlConnection.Execute("OfferCode_Update", parameters, commandType: CommandType.StoredProcedure);

        }
        public void DeleteOfferCode(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            SqlConnection.Execute("OfferCode_Delete", parameters, commandType: CommandType.StoredProcedure);
        }

        public OfferCodeExtended GetOfferCode(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OfferCodeId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var offerCode = SqlConnection.Query<OfferCodeExtended>("OfferCode_Get", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return offerCode;
        }
        
      

        public List<OfferCodeExtended> GetAllOfferCodes(int offerId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OfferId", offerId, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var offerCodes = SqlConnection.Query<OfferCodeExtended>("OfferCode_GetAll", parameters, commandType: CommandType.StoredProcedure).ToList();
            return offerCodes;
        }

        public string ClaimNextCode(int offerId, string userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OfferId", offerId, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@UserId", userId, dbType: DbType.String, direction: ParameterDirection.Input);
            var nextCode = SqlConnection.Query<string>("Offer_ClaimNextCode", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            //not sure if ToString is the right thing to show
            
            return nextCode;
        }
        #endregion

        #region SetUp Methods
        public void SetupDb(string setUpSqlDirectory)
        {
            var sql = @"
            IF OBJECT_ID('Company', 'U') IS NOT NULL 
            BEGIN
	            SELECT 1 as 'IsDbSetup'
            END
            ELSE 
            BEGIN
	            SELECT 0 as 'IsDbSetup'
            END
            ";
            var isDbSetup = SqlConnection.Query<bool>(sql).FirstOrDefault();
            if (!isDbSetup)
            {
                // execute setup db script
                var setupSqlPath = Path.Combine(setUpSqlDirectory, "SetupDb.sql");
                var setupSqlContent = System.IO.File.ReadAllText(setupSqlPath);
                var migrateSqlPath = Path.Combine(setUpSqlDirectory, "MigrateDb.sql");
                var migrateSqlContent = System.IO.File.ReadAllText(migrateSqlPath);

                ExecuteSqlCommandScript(setupSqlContent);
                ExecuteSqlCommandScript(migrateSqlContent);
            }
        }

        private void ExecuteSqlCommandScript(string sql)
        {
            string sqlBatch = string.Empty;

            //sql += "\nGO";   // make sure last batch is executed.
            foreach (string line in sql.Split(new string[2] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.ToUpperInvariant().Trim() == "GO")
                {
                    SqlConnection.Execute(sqlBatch);
                    sqlBatch = string.Empty;
                }
                else
                {
                    sqlBatch += line + "\n";
                }
            }

        }
        #endregion


    }


}
