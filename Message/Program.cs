using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace messageSystem
{
    public class sendMessage
    {
        static void Main(string[] args)
        {

        }

        public static string getHtml()
        {
            String mailContent = null;
            try
            {
                string messageBody = "<font>The following are the records: </font><br><br>";
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Student Name" + htmlTdEnd;
                messageBody += htmlTdStart + "DOB" + htmlTdEnd;
                messageBody += htmlTdStart + "Email" + htmlTdEnd;
                messageBody += htmlTdStart + "Mobile" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                messageBody = messageBody + htmlTableEnd;
                return messageBody; // return HTML Table as string from this function  
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void SendEmail()
        {
            try
            {
                //MailMessage message = new MailMessage();
                //SmtpClient smtp = new SmtpClient();
                //message.From = new MailAddress("shivabalan03@gmail.com");
                //message.To.Add(new MailAddress("shivabalan03@gmail.com"));
                //message.Subject = "India Events Your Event Posted Successfully..!";
                //message.IsBodyHtml = true; //to make message body as html  
                //message.Body = getHtml();
                //smtp.Port = 587;
                //smtp.Host = "smtp.gmail.com"; //for gmail host  
                //smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new NetworkCredential("shivabalan03@gmail.com", "Shiva@12345");
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.Send(message);


                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("shivabalan03@gmail.com");
                    mail.To.Add("shivabalan03@gmail.com");
                    //mail.CC.Add("CC Mail-ID");//if required

                    mail.Subject = "Marriage Quote ID";
                    mail.Body = getHtml();
                    mail.IsBodyHtml = true;
                    //mail.AlternateViews.Add(htBody);

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("shivabalan03@gmail.com", "Shiva@12345");
                        smtp.Send(mail);
                        //message = "Mail Send Successfully..!";
                    }
                }

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
