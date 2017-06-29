using CDLib;
using CDLib.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
      
        [TestMethod]
        public void ClaimOfferCodeCRUD()
        {
            var Offer = new Offer();
            var testTitle = "Necklaces";
            var testDescription = "80% off these shiny necklaces!";
            var testUrl = "fakeurl.bamazon.necklaces/";
            var testCategory = "Jewelry";

            

            var testCompanyId = RandomInteger();

            Offer.Title = testTitle;
            Offer.Description = testDescription;
            Offer.Url = testUrl;
            Offer.Category = testCategory;
            Offer.CompanyId = testCompanyId;

            //create (offercode)
            var offerCode1 = new OfferCode();
            offerCode1.Code = "ABCDEF" + RandomDigits();
            var offerCode2 = new OfferCode();
            offerCode2.Code = "GHIJKL" + RandomDigits();
            var offerCode3 = new OfferCode();
            offerCode3.Code = "MNOPQR" + RandomDigits();

            offerCode1.OfferId = Offer.Id;
            offerCode2.OfferId = Offer.Id;
            offerCode3.OfferId = Offer.Id;

            OfferService offerService = new OfferService();

            offerService.SaveOffer(Offer);
            Assert.IsTrue(Offer.Id != 0);

            offerCode1.OfferId = Offer.Id;
            offerCode2.OfferId = Offer.Id;
            offerCode3.OfferId = Offer.Id;

            offerService.SaveOfferCode(offerCode1);
            offerService.SaveOfferCode(offerCode2);
            offerService.SaveOfferCode(offerCode3);
            Assert.IsTrue(offerCode1.Id != 0);
            Assert.IsTrue(offerCode2.Id != 0);
            Assert.IsTrue(offerCode3.Id != 0);

            var userId1 = "Jackie Bolton";

            var code1 = offerService.ClaimNextCode(Offer.Id, userId1);
            Assert.IsTrue(!String.IsNullOrEmpty(code1));
            //Verify that the first code is claimed by Jackie
            offerCode1 = offerService.GetOfferCode(offerCode1.Id);
            Assert.IsTrue(offerCode1.ClaimingUser.Equals(userId1));

            var userId2 = "Sandra Lollygagger";

            var code2 = offerService.ClaimNextCode(Offer.Id, userId2);
            Assert.IsTrue(!String.IsNullOrEmpty(code2));
            //Verify that the second code is claimed by Sandra
            offerCode2 = offerService.GetOfferCode(offerCode2.Id);
            Assert.IsTrue(offerCode2.ClaimingUser.Equals(userId2));
            Assert.IsTrue(!offerCode1.ClaimingUser.Equals(offerCode2.ClaimingUser));

            var userId3 = "Kevin Spaceman";

            var code3 = offerService.ClaimNextCode(Offer.Id, userId3);
            Assert.IsTrue(!String.IsNullOrEmpty(code3));
            //Verify that the third code is claimed by Kevin
            offerCode3 = offerService.GetOfferCode(offerCode3.Id);
            Assert.IsTrue(offerCode3.ClaimingUser.Equals(userId3));
            Assert.IsTrue(!offerCode1.ClaimingUser.Equals(offerCode3.ClaimingUser));

            var userId4 = "Richmond Nocode";

            var code4 = offerService.ClaimNextCode(Offer.Id, userId4);
            //This time verifying that code4 IS empty because we have no more codes in the offer.
            Assert.IsTrue(String.IsNullOrEmpty(code4));

            offerService.DeleteOfferCode(offerCode1.Id);
            offerService.DeleteOfferCode(offerCode2.Id);
            offerService.DeleteOfferCode(offerCode3.Id);

            offerService.DeleteOffer(Offer.Id);
            

        }

        [TestMethod]
        public void OfferCodeCRUDUsingToken()
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

            var offer1 = OfferService.GetOffer(Offer.Id);
            
            // get it, verify it's there (offer)
            var OfferToken = offer1.Token;
            var Offer2 = OfferService.GetOfferByToken(OfferToken);
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
            //var Offer = new Offer();
            //var testTitle = "Bracelets";
            //var testDescription = "70% off this gold jewelry shining so bright! " +
            //    "Goes great with strawberry champagne!";
            //var testUrl = "fakeurl.bamazon.whatilike/";
            //var testCategory = "Jewelry";

            //var testCompanyId = RandomInteger();

            //Offer.Title = testTitle;
            //Offer.Description = testDescription;
            //Offer.Url = testUrl;
            //Offer.Category = testCategory;
            //Offer.CompanyId = testCompanyId;

            ////create (offercode)
            //var offerCode1 = new OfferCode();
            //offerCode1.Code = "A" + RandomDigits();
            //var offerCode2 = new OfferCode();
            //offerCode2.Code = "B" + RandomDigits();
            //var offerCode3 = new OfferCode();
            //offerCode3.Code = "C" + RandomDigits();
            //offerCode1.OfferId = Offer.Id;
            //offerCode2.OfferId = Offer.Id;
            //offerCode3.OfferId = Offer.Id;

            //OfferService offerService = new OfferService();

            //offerService.SaveOffer(Offer);
            //Assert.IsTrue(Offer.Id != 0);

            //offerCode1.OfferId = Offer.Id;
            //offerCode2.OfferId = Offer.Id;
            //offerCode3.OfferId = Offer.Id;

            //offerService.SaveOfferCode(offerCode1);
            //offerService.SaveOfferCode(offerCode2);
            //offerService.SaveOfferCode(offerCode3);
            //Assert.IsTrue(offerCode1.Id != 0);
            //Assert.IsTrue(offerCode2.Id != 0);
            //Assert.IsTrue(offerCode3.Id != 0);

            //var userId1 = "Jackie Bolton";

            //var code1 = offerService.ClaimNextCode(Offer.Id, userId1);
            //Assert.IsTrue(!String.IsNullOrEmpty(code1));
            ////Verify that the first code is claimed by Jackie
            //offerCode1 = offerService.GetOfferCode(offerCode1.Id);
            //Assert.IsTrue(offerCode1.ClaimingUser.Equals(userId1));

            //var userId2 = "Sandra Lollygagger";

            //var code2 = offerService.ClaimNextCode(Offer.Id, userId2);
            //Assert.IsTrue(!String.IsNullOrEmpty(code2));
            ////Verify that the second code is claimed by Sandra
            //offerCode2 = offerService.GetOfferCode(offerCode2.Id);
            //Assert.IsTrue(offerCode2.ClaimingUser.Equals(userId2));
            //Assert.IsTrue(!offerCode1.ClaimingUser.Equals(offerCode2.ClaimingUser));

            //var userId3 = "Kevin Spaceman";

            //var code3 = offerService.ClaimNextCode(Offer.Id, userId3);
            //Assert.IsTrue(!String.IsNullOrEmpty(code3));
            ////Verify that the third code is claimed by Kevin
            //offerCode3 = offerService.GetOfferCode(offerCode3.Id);
            //Assert.IsTrue(offerCode3.ClaimingUser.Equals(userId3));
            //Assert.IsTrue(!offerCode1.ClaimingUser.Equals(offerCode3.ClaimingUser));

            //var userId4 = "Richmond Nocode";

            //var code4 = offerService.ClaimNextCode(Offer.Id, userId4);
            ////This time verifying that code4 IS empty because we have no more codes in the offer.
            //Assert.IsTrue(String.IsNullOrEmpty(code4));

            //offerService.DeleteOfferCode(offerCode1.Id);
            //offerService.DeleteOfferCode(offerCode2.Id);
            //offerService.DeleteOfferCode(offerCode3.Id);

            //offerService.DeleteOffer(Offer.Id);
        }

        [TestMethod]
        public void GetCsv()
        {
            var Offer = new Offer();
            var testTitle = "Necklaces";
            var testDescription = "80% off these shiny necklaces!";
            var testUrl = "fakeurl.bamazon.necklaces/";
            var testCategory = "Jewelry";



            var testCompanyId = RandomInteger();

            Offer.Title = testTitle;
            Offer.Description = testDescription;
            Offer.Url = testUrl;
            Offer.Category = testCategory;
            Offer.CompanyId = testCompanyId;

            //create (offercode)
            //codes that will be claimed
            var offerCode1 = new OfferCodeExtended();
            offerCode1.Code = "ABCDEF" + RandomDigits();
            var offerCode2 = new OfferCodeExtended();
            offerCode2.Code = "GHIJKL" + RandomDigits();
            var offerCode3 = new OfferCodeExtended();
            offerCode3.Code = "MNOPQR" + RandomDigits();
            //codes that will be unclaimed
            var offerCode4 = new OfferCodeExtended();
            offerCode4.Code = "STUVW" + RandomDigits();
            var offerCode5 = new OfferCodeExtended();
            offerCode5.Code = "XYZ" + RandomDigits();

            offerCode1.OfferId = Offer.Id;
            offerCode2.OfferId = Offer.Id;
            offerCode3.OfferId = Offer.Id;

            OfferService offerService = new OfferService();

            offerService.SaveOffer(Offer);
            Assert.IsTrue(Offer.Id != 0);

            offerCode1.OfferId = Offer.Id;
            
            offerCode2.OfferId = Offer.Id;
            offerCode3.OfferId = Offer.Id;

            offerService.SaveOfferCode(offerCode1);
            offerService.SaveOfferCode(offerCode2);
            offerService.SaveOfferCode(offerCode3);
            Assert.IsTrue(offerCode1.Id != 0);
            Assert.IsTrue(offerCode2.Id != 0);
            Assert.IsTrue(offerCode3.Id != 0);

            var userId1 = "Jackie Bolton";
            var email1 = userId1 + "@gmail.com";
            var code1 = offerService.ClaimNextCode(Offer.Id, userId1);
            Assert.IsTrue(!String.IsNullOrEmpty(code1));
            //Verify that the first code is claimed by Jackie
            offerCode1 = offerService.GetOfferCode(offerCode1.Id);
            Assert.IsTrue(offerCode1.ClaimingUser.Equals(userId1));

            var userId2 = "Sandra Lollygagger";
            var email2 = userId2 + "@gmail.com";
            var code2 = offerService.ClaimNextCode(Offer.Id, userId2);
            
            Assert.IsTrue(!String.IsNullOrEmpty(code2));
            //Verify that the second code is claimed by Sandra
            offerCode2 = offerService.GetOfferCode(offerCode2.Id);
            Assert.IsTrue(offerCode2.ClaimingUser.Equals(userId2));
            Assert.IsTrue(!offerCode1.ClaimingUser.Equals(offerCode2.ClaimingUser));

            var userId3 = "Kevin Spaceman";
            var email3 = userId3 + "@gmail.com";


            var code3 = offerService.ClaimNextCode(Offer.Id, userId3);
            Assert.IsTrue(!String.IsNullOrEmpty(code3));
            //Verify that the third code is claimed by Kevin
            offerCode3 = offerService.GetOfferCode(offerCode3.Id);
            Assert.IsTrue(offerCode3.ClaimingUser.Equals(userId3));
            Assert.IsTrue(!offerCode1.ClaimingUser.Equals(offerCode3.ClaimingUser));

            var userId4 = "Richmond Nocode";
            var email4 = userId4 + "@gmail.com";

            var code4 = offerService.ClaimNextCode(Offer.Id, userId4);

            string codesCsv = offerService.GetAllOfferCodesCsv(Offer.Id);

            //This time verifying that code4 IS empty because we have no more codes in the offer.
            Assert.IsTrue(String.IsNullOrEmpty(code4));

            offerService.DeleteOfferCode(offerCode1.Id);
            offerService.DeleteOfferCode(offerCode2.Id);
            offerService.DeleteOfferCode(offerCode3.Id);

            offerService.DeleteOffer(Offer.Id);
            Console.Out.WriteLine(codesCsv);

        }

        //Random Number methods
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
