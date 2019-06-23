using HIS.DAL.DAL;
using HIS.Domain.Models.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.BLL.BLL
{
    public class PatientBLL : IPatientBll
    {
        #region Intialization

        private IPatientDAL _Patient { get; set; }

        public PatientBLL()
        {
            _Patient = new PatientDAL();
        }

        #endregion

        #region Patient
        public int SavePatient(Patient patient)
        {
            if (patient != null && patient.Id < 1)
            {
                // generate a new MRN
                patient.Mrno = patient.GenderId.ToString().PadLeft(2, '0') + "-" + _Patient.NextPatientId.ToString().PadLeft(7,'0');
                patient.AccessCode = Guid.NewGuid().ToString();
            }
            return _Patient.SavePatient(patient);
        }
        #endregion
    }

    public interface IPatientBll
    {
        #region Title

        int SavePatient(Patient patient);

        #endregion
        
    }
}
