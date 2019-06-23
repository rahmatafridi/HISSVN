using HIS.DAL.DAL;
using HIS.Domain.Models.Common;
using HIS.Domain.Models.Email;
using HIS.Domain.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.BLL
{
    public class EmailBLL: IEmailBll
    {
        #region Initialization

        private IEmailDAL EmailDal { get; set; }

        public EmailBLL()
        {
            EmailDal = new EmailDAL();
        }

        #endregion

        #region Email

        public List<EmailData> GetEmail(SearchCriteria criteria, out int TotalRecords)
        {
            return EmailDal.GetEmail(criteria, out TotalRecords);
        }

        public EmailData GetEmailById(int id)
        {

            return EmailDal.GetEmailById(id);
        }

        public bool SentEmail(EmailData email)
        {
            return EmailDal.SentEmail(email);
        }

        public int DeleteEmailById(int id)
        {
            return EmailDal.DeleteEmailById(id);
        }

        #endregion

        #region Sms

        public List<SmsData> GetSms(SearchCriteria criteria, out int TotalRecords)
        {
            return EmailDal.GetSms(criteria, out TotalRecords);
        }

        public SmsData GetSmsById(int id)
        {
            return EmailDal.GetSmsById(id);
        }

        public bool SentSms(SmsData sms)
        {
            return EmailDal.SentSms(sms);
        }

        public int DeleteSmsById(int id)
        {
            return EmailDal.DeleteSmsById(id);
        }

        #endregion


    }

    public interface IEmailBll
    {
        #region Email

        List<EmailData> GetEmail(SearchCriteria criteria, out int TotalRecords);
        EmailData GetEmailById(int id);
        int DeleteEmailById(int id);
        bool SentEmail(EmailData emailData);
        #endregion

        #region Sms

        List<SmsData> GetSms(SearchCriteria criteria, out int TotalRecords);
        SmsData GetSmsById(int id);
        int DeleteSmsById(int id);
        bool SentSms(SmsData SMSData);
        #endregion
    }

}
