﻿using CDLib;
using CDLib.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CDUnitTests
{
    [TestClass]
    public class CompanyTests
    {
        private Random _random;

        public CompanyTests()
        {
            _random = new Random();
        }

        [TestMethod]
        public void CompanyCRUD()
        {
            // create 
            var company = new Company();
            var testName = "fredsCo " + RandomDigits();
            company.Name = testName;

            var companyService = new CompanyService();
            companyService.Save(company);
            Assert.IsTrue(company.Id != 0);

            // get it, verify it's there
            var companyId = company.Id;
            var company2 = companyService.Get(companyId);
            Assert.IsNotNull(company2);

            // verify property values
            Assert.AreEqual(company2.Name, testName);

            // update it
            var testName2 = "JillsCo " + RandomDigits();
            company2.Name = testName2;
            companyService.Save(company2);

            // get it again, verify property values
            var company3 = companyService.Get(companyId);
            Assert.AreEqual(company3.Name, testName2);

            // delete it 
            companyService.Delete(companyId);
            company3 = companyService.Get(companyId);
            Assert.IsNull(company3);


        }

        [TestMethod]
        public void CompanyList()
        {
            // create 
            var company = new Company();
            var testName = "fredsCo " + RandomDigits();
            company.Name = testName;

            var company2 = new Company();
            var testName2 = "JillsCo " + RandomDigits();
            company2.Name = testName2;

            var companyService = new CompanyService();
            companyService.Save(company);
            companyService.Save(company2);

            var list = companyService.GetAll();
            Assert.IsTrue(list.Count >= 2);
            Assert.IsTrue(list.Any(li => li.Name == testName));
            Assert.IsTrue(list.Any(li => li.Name == testName2));


            // delete them 
            companyService.Delete(company.Id);
            companyService.Delete(company2.Id);



        }

        private string RandomDigits()
        {
            return _random.Next(10000, 99999).ToString();
        }
    }
}