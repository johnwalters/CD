using CDLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLib.DataLayer
{
    public class SystemRepo
    {
        public void SetUpDb(string setUpSqlDirectory)
        {
            new Data().SetupDb(setUpSqlDirectory);
        }

        
    }
}
