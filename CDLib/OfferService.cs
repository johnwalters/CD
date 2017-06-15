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
        private OfferCodeRepo _codeRepo;
        private static readonly ILog _logger =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //Offer Methods

        public OfferService()
        {
            _repo = new OfferRepo();
            _codeRepo = new OfferCodeRepo();
            _logger.Debug("New OfferService");
        }

        public void SaveOffer(Offer Offer)
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

        public void DeleteOffer(int id)
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


        public Offer GetOffer(int id)
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

       

        public List<Offer> GetAllOffers(int companyId)
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

        //OfferCode methods

        public void SaveOfferCode(OfferCode OfferCode)
        {

            try
            {
                _codeRepo.Save(OfferCode);
            }
            catch (Exception ex)
            {
                _logger.Error("Error during OfferService.SaveOfferCode()", ex);
                throw;
            }
        }

        public void DeleteOfferCode(int id)
        {
            try
            {
                _codeRepo.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.Error(String.Format("Error during OfferService.DeleteOfferCode({0}", id), ex);
                throw;
            }
        }


        public OfferCode GetOfferCode(int id)
        {
            try
            {
                return _codeRepo.Get(id);
            }
            catch (Exception ex)
            {
                _logger.Error(String.Format("Error during OfferService.GetOfferCode({0})", id), ex);
                throw;
            }
        }



        public List<OfferCode> GetAllOfferCodes(int offerId)
        {
            try
            {
                return _codeRepo.GetAll(offerId);
            }
            catch (Exception ex)
            {
                _logger.Error("Error during OfferService.GetAllOfferCodes", ex);
                throw;
            }
        }

        //public Offer GetOfferByToken(string token)
        //{
        //    return;
        //}

        public string ClaimNextCode(int offerId, string userId)
        {
            try
            {
                return _repo.ClaimNextCode(offerId, userId);
            }
            catch (Exception ex)
            {
                _logger.Error("Error during OfferService.ClaimNextCode", ex);
                throw;
            }
        }
    }
}
