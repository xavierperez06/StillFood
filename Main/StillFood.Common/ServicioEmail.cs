using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace StillFood.Common
{
    public class ServicioEmail
    {
        public Enums.eResultadoEnvio Enviar(string pEmails, string pAsunto, string pMensaje,bool pEsHTML)
        {
            Enums.eResultadoEnvio wResultado;

            try
            {
                MailMessage wMail = new MailMessage();
                SmtpClient wSmtpServer = new SmtpClient("smtp.gmail.com");

                wMail.From = new MailAddress("uai.stilfood@gmail.com");


                foreach (string wEmail in pEmails.Split(Convert.ToChar(";")))
                {
                    if (string.IsNullOrWhiteSpace(wEmail))
                        continue;
                    wMail.To.Add(wEmail);
                }

                wMail.Subject = pAsunto;
                wMail.IsBodyHtml = pEsHTML;
                wMail.Body = pMensaje;
         
                 wSmtpServer.Port = 587;
                wSmtpServer.Credentials = new System.Net.NetworkCredential("uai.stilfood@gmail.com", "1549315455");
                wSmtpServer.EnableSsl = true;

                 wSmtpServer.Send(wMail);

                wResultado = Enums.eResultadoEnvio.Ok;
            }
            catch(Exception ex)
            {
                string a = ex.Message;
                wResultado = Enums.eResultadoEnvio.Error;
            }

            return wResultado;

        }

        public Enums.eResultadoEnvio Enviar(string pEmails, string pAsunto, string pMensaje, bool pEsHTML, LinkedResource pRecurso)
        {
            Enums.eResultadoEnvio wResultado;

            try
            {
                MailMessage wMail = new MailMessage();
                SmtpClient wSmtpServer = new SmtpClient("smtp.gmail.com");

                wMail.From = new MailAddress("uai.stilfood@gmail.com");


                foreach (string wEmail in pEmails.Split(Convert.ToChar(";")))
                {
                    wMail.To.Add(wEmail);
                }

                wMail.Subject = pAsunto;
                wMail.IsBodyHtml = pEsHTML;

                pRecurso.ContentId = Guid.NewGuid().ToString();
                string wBody = string.Format(pMensaje, pRecurso.ContentId);

                var wView = AlternateView.CreateAlternateViewFromString(wBody, null, "text/html");
                wView.LinkedResources.Add(pRecurso);
                wMail.AlternateViews.Add(wView);

                wMail.Body = pMensaje;

                wSmtpServer.Port = 587;
                wSmtpServer.Credentials = new System.Net.NetworkCredential("uai.stilfood@gmail.com", "1549315455");
                wSmtpServer.EnableSsl = true;

                wSmtpServer.Send(wMail);

                wResultado = Enums.eResultadoEnvio.Ok;
            }
            catch (Exception ex)
            {
                string a = ex.Message;
                wResultado = Enums.eResultadoEnvio.Error;
            }

            return wResultado;

        }

    }
}
