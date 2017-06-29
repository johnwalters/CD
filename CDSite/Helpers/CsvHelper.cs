using CDLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDLib.Helpers
{
    public class CsvHelper
    {
        public string GetOfferCodeString(int offerId)
        {
            var offerService = new OfferService();
            var offer = offerService.GetOffer(offerId);
            var offerCodeList = offerService.GetAllOfferCodes(offerId);
            //offerCodeList = offerCodeList.Where(oc => oc.ClaimedOn != null).ToList();
            string offerCodeString = "Offer Title,Category,Code,Buyer,Date\n";
            foreach (var item in offerCodeList)
            {
                //from https://stackoverflow.com/questions/29893631/how-to-add-double-quotes-to-a-string-which-contains-comma
                //text = text.Contains(",") ? String.Format("\"{0}\"", text) : text;

               
               
                    offerCodeString += 
                        (offer.Title.Contains(",") ? String.Format("\"{0}\"", offer.Title) : offer.Title)
                        + ",";
                    offerCodeString += 
                        (offer.Category.Contains(",") ? String.Format("\"{0}\"", offer.Category) : offer.Category)
                        + ",";
                    offerCodeString += 
                        (item.Code.Contains(",") ? String.Format("\"{0}\"", item.Code) : item.Code)
                        + ",";
                    offerCodeString += 
                        (item.BuyerEmail.Contains(",") ? String.Format("\"{0}\"", item.BuyerEmail) : item.BuyerEmail)
                        + ",";
                    offerCodeString += 
                        (item.ClaimedOn.Contains(",") ? String.Format("\"{0}\"", item.ClaimedOn) : item.ClaimedOn)
                        + ",";
                    offerCodeString += "\n";//new row
                
                
            }
            return offerCodeString;
        }
    }
}