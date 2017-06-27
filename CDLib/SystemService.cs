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
    public class SystemService
    {
        private SystemRepo _repo;
        private static readonly ILog _logger =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SystemService()
        {
            _repo = new SystemRepo();
        }

        public void SetUpDb(string setUpSqlDirectory)
        {
            try
            {
                _repo.SetUpDb(setUpSqlDirectory);
            }
            catch (Exception ex)
            {
                _logger.Error("Error during SystemService.SetUpDb()", ex);
                throw;
            }
        }


    }
}
