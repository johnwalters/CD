using CDLib;
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
            var testUserId = "userId" + RandomDigits();
            company.Name = testName;
            company.UserId = testUserId;

            var companyService = new CompanyService();
            companyService.Save(company);
            Assert.IsTrue(company.Id != 0);

            // get it, verify it's there
            var companyId = company.Id;
            var company2 = companyService.Get(companyId);
            Assert.IsNotNull(company2);

            // verify property values
            Assert.AreEqual(company2.Name, testName);
            Assert.AreEqual(company2.UserId, testUserId);

            // Verify that I can get by user id
            var companyUserId = companyService.GetByUserId(testUserId);
            Assert.IsNotNull(companyUserId);

            // update it
            var testName2 = "JillsCo " + RandomDigits();
            var testUserId2 = "userId" + RandomDigits();
            company2.Name = testName2;
            company2.UserId = testUserId2;
            companyService.Save(company2);


            // get it again, verify property values
            var company3 = companyService.Get(companyId);
            Assert.AreEqual(company3.Name, testName2);
            Assert.AreEqual(company3.UserId, testUserId2);



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
            var userId = "ABCD" + RandomDigits();
            company.Name = testName;
            company.UserId = userId;

            var company2 = new Company();
            var testName2 = "JillsCo " + RandomDigits();
            var userId2 = "ABC" + RandomDigits();
            company2.Name = testName2;
            company2.UserId = userId2;

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
