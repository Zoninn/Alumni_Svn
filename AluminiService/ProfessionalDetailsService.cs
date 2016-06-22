using Alumini.Core;
using AluminiRepository;
using AluminiRepository.Interfaces;
using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService
{
    public class ProfessionalDetailsService : IProfessionalDetailsService
    {
        private readonly Alumini.Logger.ILogger _logger;
        private readonly IProfessionalDetailsRepository _professionaldetailreo;

        public ProfessionalDetailsService(Alumini.Logger.ILogger _logger, IProfessionalDetailsRepository _professionaldetailreo)
        {
            this._logger = _logger;
            this._professionaldetailreo = _professionaldetailreo;
        }
        public ProfessionalDetail Create(ProfessionalDetail obj)
        {
            return _professionaldetailreo.Create(obj);
        }
        public List<View_UserDetails> GetUserDetails(string Name,int id)
        {
            return _professionaldetailreo.GetUserDetails(Name,id);
        }
        public IEnumerable<Companys> GetCompanyDetailsforAutosearch(string Auto, string Designation)
        {

            return _professionaldetailreo.GetCompanyDetailsforAutosearch(Auto, Designation);
        }

        public ProfessionalDetail Get(int id)
        {
            return _professionaldetailreo.Get(id);
        }
        public List<Companys> GetAllCompanys(int id)
        {
            return _professionaldetailreo.GetAllCompanys(id);
        }

        public ProfessionalDetail Update(int id)
        {
            throw new NotImplementedException();
        }
        public List<View_UserDetails> GetUserDetailsonSearchbased(string Name, int id, string Designation)
        {
            return _professionaldetailreo.GetUserDetailsonSearchbased(Name, id, Designation);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public ProfessionalDetail UpdateUserProfDetails(int id, ProfessionalDetail Prof)
        {
            return _professionaldetailreo.UpdateUserProfDetails(id, Prof);
        }
    }
}
