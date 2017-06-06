using CDLib;
using CDLib.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CDUnitTests
{
    public partial class CompanyTests
    {
        [TestClass]
        public class OfferTests
        {

            private Random _random;

            public OfferTests()
            {
                _random = new Random();
            }

            [TestMethod]
            public void OfferCRUD()
            {
                // create 
                var Offer = new Offer();
                var testTitle = "Knife Sale";
                var testDescription = "40% off these real sharp knives!";
                var testUrl = "fakeurl.amazon.com/";
                var testCategory = "Kitchen Supplies";
                var testCompanyId = RandomInteger();
                



                Offer.Title = testTitle;
                Offer.Description = testDescription;
                Offer.Url = testUrl;
                Offer.Category = testCategory;
                Offer.CompanyId = testCompanyId;


                var OfferService = new OfferService();
                OfferService.Save(Offer);
                Assert.IsTrue(Offer.Id != 0);

                // get it, verify it's there
                var OfferId = Offer.Id;
                var Offer2 = OfferService.Get(OfferId);
                Assert.IsNotNull(Offer2);

                // verify property values
                Assert.AreEqual(Offer2.Title, testTitle);
                Assert.AreEqual(Offer2.Description, testDescription);
                Assert.AreEqual(Offer2.Url, testUrl);
                Assert.AreEqual(Offer2.Category, testCategory);
                Assert.AreEqual(Offer2.CompanyId, testCompanyId);

                

                //// Verify that I can get by user id
                //var OfferUserId = OfferService.GetByUserId(testUserId);
                //Assert.IsNotNull(OfferUserId);

                // update it
                var testTitle2 = "Jills Offer " + RandomDigits();
                var testDescription2 = "Since no one's buying, 95% off these real sharp knives!";
                var testCategory2 = "Weapons";
                Offer2.Title = testTitle2;
                Offer2.Description = testDescription2;
                Offer2.Category = testCategory2;
                OfferService.Save(Offer2);


                // get it again, verify property values
                var Offer3 = OfferService.Get(OfferId);
                Assert.AreEqual(testTitle2, Offer3.Title);
                Assert.AreEqual(testDescription2, Offer3.Description);
                Assert.AreEqual(testCategory2, Offer3.Category);


                // delete it 
                OfferService.Delete(OfferId);
                Offer3 = OfferService.Get(OfferId);
                Assert.IsNull(Offer3);


            }
            private string RandomDigits()
            {
                return _random.Next(10000, 99999).ToString();
            }
            private int RandomInteger()
            {
                return _random.Next(10000, 99999);
            }

            [TestMethod]
            public void GetAllOffers()
            {
                CompanyService companyService = new CompanyService();
                OfferService offerService = new OfferService();

                //Get all companies
                List<Company> testCompanyList = new List<Company>();
                testCompanyList = companyService.GetAll();

                foreach (var item in testCompanyList)
                {
                    companyService.Delete(item.Id);
                }

                //Create companies and verify offer count
                Company testCompany1 = new Company();
                testCompany1.Name = "No. One inc.";
                testCompany1.UserId = "co1" + RandomDigits();

                Company testCompany2 = new Company();
                testCompany2.Name = "No. Two inc.";
                testCompany2.UserId = "co2" + RandomDigits();

                companyService.Save(testCompany1);//Id = 41
                companyService.Save(testCompany2);//Id = 42

                Offer testOffer11 = new Offer();
                testOffer11.Title = "Banana peelers 5% off!";
                testOffer11.Description = "Peel yer' nanners!";
                testOffer11.Url = "fakebananapeeler.bamazon.com/";
                testOffer11.Category = "Kitchen Supplies";
                testOffer11.CompanyId = testCompany1.Id;

                //Saving offers of first company(First digit = company number, second digit = offer number)
                offerService.Save(testOffer11);

                //Creating offers for second company
                Offer testOffer21 = new Offer();
                testOffer21.Title = "Fishing Rods 10% off!";
                testOffer21.Description = "Get yer' fishin' rods here!";
                testOffer21.Url = "fakefishingrod.bamazon.com/";
                testOffer21.Category = "Fishing";
                testOffer21.CompanyId = testCompany2.Id;

                Offer testOffer22 = new Offer();
                testOffer22.Title = "Fishing bait 20% off!";
                testOffer22.Description = "We got worms here real cheap!";
                testOffer22.Url = "fakefishingbait.bamazon.com/";
                testOffer22.Category = "Fishing";
                testOffer22.CompanyId = testCompany2.Id;

                //Saving offers of second company(First digit = company number, second digit = offer number)
                offerService.Save(testOffer21);
                offerService.Save(testOffer22);

                List<Offer> offerList1 = new List<Offer>();
                offerList1 = offerService.GetAll(testCompany1.Id);//41 because id of company1 == 41
                Assert.IsTrue(offerList1.Count == 1);
                
                List<Offer> offerList2 = new List<Offer>();
                offerList2 = offerService.GetAll(testCompany2.Id);
                Assert.IsTrue(offerList2.Count == 2);
                

                var companyList = companyService.GetAll();
                foreach (var item in companyList)
                {
                    var offerList = offerService.GetAll(item.Id);
                    foreach (var item2 in offerList1)
                    {
                        offerService.Delete(item2.Id);
                    }
                    companyService.Delete(item.Id);
                }







            }

        }
        
        
      


    }
}
