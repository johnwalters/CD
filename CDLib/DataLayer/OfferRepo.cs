using CDLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLib.DataLayer
{
    class OfferRepo
    {

        public void Save(Offer Offer)
        {
            if (Offer.Id == 0)
            {
                Offer.Id = new Data().CreateOffer(Offer);
            }
            else
            {
                new Data().UpdateOffer(Offer);
            }
        }

        public void Delete(int id)
        {
            new Data().DeleteOffer(id);
        }


        public Offer Get(int id)
        {
            return new Data().GetOffer(id);
        }

        public Offer Get(string token)
        {
            return new Data().GetOffer(token);
        }

        public List<Offer> GetAll(int companyId)
        {
            return new Data().GetAllOffers(companyId);
        }

       
        public void Save(OfferCode offerCode)
        {
            if (offerCode.Id == 0)
            {
                offerCode.Id = new Data().CreateOfferCode(offerCode);
            }
            else
            {
                new Data().UpdateOfferCode(offerCode);
            }
        }

        public string ClaimNextCode(int offerId, string userId)
        {
            return new Data().ClaimNextCode(offerId, userId);
        }
      
    }
}
