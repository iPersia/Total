namespace Nzl.Utils
{
    using System;
    using System.Net.Mail;

    public static class EmailUtil
    {
        /// <summary>
        /// 发送邮件,返回true表示发送成功.
        /// </summary>
        /// <param name="sender">发件人邮箱地址；发件人用户名</param>
        /// <param name="password">密码</param>
        /// <param name="receiver">接受者邮箱地址</param>
        /// <param name="host">SMTP服务器的主机名</param>
        /// <param name="subject">邮件主题行</param>
        /// <param name="body">邮件主体正文</param>
        public static bool Send(string sender, string password, string receiver, string host, string subject, string body)
        {            
            try
            {
                System.Net.Mail.SmtpClient client = new SmtpClient();
                client.Host = host;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(sender, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                System.Net.Mail.MailMessage message = new MailMessage(sender, receiver);
                message.Subject = subject;
                message.Body = body;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
