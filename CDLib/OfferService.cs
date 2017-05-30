using CDLib.DataLayer;
using CDLib.Domain;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLib
{
    public class OfferService
    {
        private OfferRepo _repo;
        private static readonly ILog _logger =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public OfferService()
        {
            _repo = new OfferRepo();
            _logger.Debug("New OfferService");
        }

        public void Save(Offer Offer)
        {
            try
            {
                _repo.Save(Offer);
            }
            catch (Exception ex)
            {
                _logger.Error("Error during OfferService.Save()", ex);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _repo.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.Error(String.Format("Error during OfferService.Delete({0}", id), ex);
                throw;
            }
        }


        public Offer Get(int id)
        {
            try
            {
                return _repo.Get(id);
            }
            catch (Exception ex)
            {
                _logger.Error(String.Format("Error during OfferService.Get({0})", id), ex);
                throw;
            }
        }

       

        public List<Offer> GetAll(int companyId)
        {
            try
            {
                return _repo.GetAll(companyId);
            }
            catch (Exception ex)
            {
                _logger.Error("Error during OfferService.GetAll", ex);
                throw;
            }
        }
    }
}
