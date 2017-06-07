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

        public List<Offer> GetAll(int companyId)
        {
            return new Data().GetAllOffers(companyId);
        }

       
        public void Save(OfferCode OfferCode)
        {
            if (OfferCode.Id == 0)
            {
                OfferCode.Id = new Data().CreateOfferCode(OfferCode);
            }
            else
            {
                new Data().UpdateOfferCode(OfferCode);
            }
        }
    }
}
