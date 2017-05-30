using CDLib;
using CDLib.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
                var testCompanyId = RandomInteger();



                Offer.Title = testTitle;
                Offer.Description = testDescription;
                Offer.Url = testUrl;
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
                Assert.AreEqual(Offer2.CompanyId, testCompanyId);

                

                //// Verify that I can get by user id
                //var OfferUserId = OfferService.GetByUserId(testUserId);
                //Assert.IsNotNull(OfferUserId);

                // update it
                var testTitle2 = "Jills Offer " + RandomDigits();
                var testDescription2 = "Since no one's buying, 95% off these real sharp knives!";
                Offer2.Title = testTitle2;
                Offer2.Description = testDescription2;
                OfferService.Save(Offer2);


                // get it again, verify property values
                var Offer3 = OfferService.Get(OfferId);
                Assert.AreEqual(testTitle2, Offer3.Title);
                Assert.AreEqual(testDescription2, Offer3.Description);



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

        }
    }
}
