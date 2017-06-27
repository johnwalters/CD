using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLib.Domain
{
    public class OfferCode
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Code { get; set; }
        public string ClaimingUser { get; set; }
        public string ClaimedOn { get; set; }
    }

    public class OfferCodeExtended:OfferCode
        {
            public string BuyerEmail { get; set; }
        }

}
