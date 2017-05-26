using CDLib.Domain;
using System.Collections.Generic;


namespace CDLib.DataLayer
{
    public class CompanyRepo 
    {

        public void Save(Company company)
        {
            if(company.Id == 0)
            {
               company.Id =  new Data().CreateCompany(company);
            } else
            {
                new Data().UpdateCompany(company);
            }
        }

        public void Delete(int id)
        {
            new Data().DeleteCompany(id);
        }


        public Company Get(int id)
        {
            return new Data().GetCompany(id);
        }

        public List<Company> GetAll()
        {
            return new Data().GetAllCompanies();
        }

        public Company GetByUserId(string userId)
        {
            return new Data().GetCompanyByUserId(userId);
        }

        











    }
}
