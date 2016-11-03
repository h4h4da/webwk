using MyStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Concrete
{
    public class EmailOrderProcessor :IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public void ProcessOrder(Cart cart, ShippingAddress shippingAddress)
        {
            SmtpClient smtp = new SmtpClient();
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential(emailSettings.UserName, emailSettings.PassWord);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                StringBuilder body = new StringBuilder();

                foreach (var line in cart.Line)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendLine(string.Format("{0} x {1} (小计： {2:c})",line.Quantity,line.Product.Name,subtotal));

                 
                }
                body.AppendLine(string.Format("订单总金额：{0:c}",cart.ComputeTotalValue()))
                    .AppendLine("---")
                    .AppendLine("送货信息：")
                    .AppendLine(string.Format("姓名 {0} 联系电话 {1}",shippingAddress.Name,shippingAddress.Phone))
                    .AppendLine(string.Format("地址:{0}{1}{2}{3}",shippingAddress.Province,shippingAddress.City,shippingAddress.District,shippingAddress.DetailAddress))
                    .AppendLine(string.Format("邮政编码 {0}",shippingAddress.Zip))
                    .AppendFormat("需要礼盒包装：{0}",shippingAddress.GiftWrap?"是":"否");

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    string.Format("Mystore 订单 {0}", System.DateTime.Now.ToString()),
                    body.ToString());

                mailMessage.BodyEncoding = Encoding.UTF8;
                smtpClient.Send(mailMessage);
                    
                    
                   

            }
        }
    }
}
