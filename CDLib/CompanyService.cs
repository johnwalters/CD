using CDLib.DataLayer;
using CDLib.Domain;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Erik is testing whether he's able to push to github

namespace CDLib
{
    public class CompanyService
    {
        private CompanyRepo _repo;
        private static readonly ILog _logger =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CompanyService()
        {
            _repo = new CompanyRepo();
            _logger.Debug("New companyService");
        }

        public void Save(Company company)
        {
            try
            {
                _repo.Save(company);
            }
            catch (Exception ex)
            {
                _logger.Error("Error during CompanyService.Save()",ex);
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
                _logger.Error(String.Format( "Error during CompanyService.Delete({0}",id), ex);
                throw;
            }
        }


        public Company Get(int id)
        {
            try
            {
                return _repo.Get(id);
            }
            catch (Exception ex)
            {
                _logger.Error(String.Format("Error during CompanyService.Get({0})", id), ex);
                throw;
            }
        }

        public Company GetByUserId(string userId)
        {
            try
            {
                return _repo.GetByUserId(userId);
            }
            catch (Exception ex)
            {
                _logger.Error(String.Format("Error during CompanyService.GetByUserId({0})", userId), ex);
                throw;
            }
        }

        public List<Company> GetAll()
        {
            try
            {
                return _repo.GetAll();
            }
            catch (Exception ex)
            {
                _logger.Error("Error during CompanyService.GetAll", ex);
                throw;
            }
        }
    }
}
