using CDLib;
using CDLib.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CDUnitTests
{
    [TestClass]
    public class OfferCodeTests
    {
        private Random _random;

        public OfferCodeTests()
        {
            _random = new Random();
        }
        [TestMethod]
        public void OfferCodeCRUD()
        {
            //create (offer)
            var Offer = new Offer();
            var testTitle = "Sword Sale";
            var testDescription = "90% off these real dull swords!";
            var testUrl = "fakeurl.bamazon.swords/";
            var testCategory = "Weapons";
            var testCompanyId = RandomInteger();

            

            Offer.Title = testTitle;
            Offer.Description = testDescription;
            Offer.Url = testUrl;
            Offer.Category = testCategory;
            Offer.CompanyId = testCompanyId;

            //create (offercode)
            var OfferCode = new OfferCode();
            var testCode = "ABCDEF" + RandomDigits();

            OfferCode.OfferId = Offer.Id;
            OfferCode.Code = testCode;

            var OfferService = new OfferService();

            OfferService.SaveOffer(Offer);
            Assert.IsTrue(Offer.Id != 0);

            OfferService.SaveOfferCode(OfferCode);
            Assert.IsTrue(OfferCode.Id != 0);

            // get it, verify it's there (offer)
            var OfferId = Offer.Id;
            var Offer2 = OfferService.GetOffer(OfferId);
            Assert.IsNotNull(Offer2);

            // get it, verify it's there (offercode)
            var OfferCodeId = OfferCode.Id;
            var OfferCode2 = OfferService.GetOfferCode(OfferCodeId);
            Assert.IsNotNull(OfferCode2);

            // verify property values (offer)
            Assert.AreEqual(Offer2.Title, testTitle);
            Assert.AreEqual(Offer2.Description, testDescription);
            Assert.AreEqual(Offer2.Url, testUrl);
            Assert.AreEqual(Offer2.Category, testCategory);
            Assert.AreEqual(Offer2.CompanyId, testCompanyId);

            //verify property values (offercode)
            Assert.AreEqual(OfferCode2.OfferId, OfferCode.OfferId);
            Assert.AreEqual(OfferCode2.Code, testCode);

            //update offercode
            var testCode2 = "XYZ123" + RandomDigits();
            OfferCode2.Code = testCode2;
            OfferService.SaveOfferCode(OfferCode2);

            // get it again, verify property values
            var OfferCode3 = OfferService.GetOfferCode(OfferCode2.Id);
            Assert.AreEqual(testCode2, OfferCode3.Code);

            //delete it
            OfferService.DeleteOfferCode(OfferCodeId);
            OfferCode3 = OfferService.GetOfferCode(OfferCodeId);
            Assert.IsNull(OfferCode3);

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
