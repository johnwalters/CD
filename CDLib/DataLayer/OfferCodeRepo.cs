using CDLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLib.DataLayer
{
    class OfferCodeRepo
    {
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

        public void Delete(int id)
        {
            new Data().DeleteOfferCode(id);
        }


        public OfferCode Get(int id)
        {
            return new Data().GetOfferCode(id);
        }

        public List<OfferCodeExtended> GetAll(int offerId)
        {
            return new Data().GetAllOfferCodes(offerId);
        }
    }
}
