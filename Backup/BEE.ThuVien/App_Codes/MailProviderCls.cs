using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Collections.Generic;


public class MailProviderCls
{
    #region Bien Can Dung
    public string SmtpServer { get; set; }
    public bool EnableSsl { get; set; }
    public string MailFrom { get; set; }
    public MailAddress MailAddressFrom { get; set; }
    public string PassWord { get; set; }
    public string MailTo { get; set; }
    public MailAddress MailAddressTo { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public int Port { get; set; }
    public string Attachment { get; set; }
    public List<string> ListAtt { get; set; }
    #endregion

    #region Phuong thuc xu ly
    public void SendMail()
    {
        MailMessage objMail = new MailMessage(MailFrom, MailTo, Subject, Content);
        objMail.IsBodyHtml = true;
        objMail.BodyEncoding = System.Text.Encoding.UTF8;

        SmtpClient smtpMail = new SmtpClient(SmtpServer);
        smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpMail.EnableSsl = EnableSsl;
        smtpMail.Port = Port;
        smtpMail.Credentials = new NetworkCredential(MailFrom, PassWord);
        smtpMail.Send(objMail);
    }

    public void SendMailV3()
    {
        try
        {
            System.Web.Mail.MailMessage myMail = new System.Web.Mail.MailMessage();
            myMail.Fields.Add
                ("http://schemas.microsoft.com/cdo/configuration/smtpserver",
                              "smtp.gmail.com");
            myMail.Fields.Add
                ("http://schemas.microsoft.com/cdo/configuration/smtpserverport",
                              "465");
            myMail.Fields.Add
                ("http://schemas.microsoft.com/cdo/configuration/sendusing",
                              "2");
            //sendusing: cdoSendUsingPort, value 2, for sending the message using 
            //the network.

            //smtpauthenticate: Specifies the mechanism used when authenticating 
            //to an SMTP 
            //service over the network. Possible values are:
            //- cdoAnonymous, value 0. Do not authenticate.
            //- cdoBasic, value 1. Use basic clear-text authentication. 
            //When using this option you have to provide the user name and password 
            //through the sendusername and sendpassword fields.
            //- cdoNTLM, value 2. The current process security context is used to 
            // authenticate with the service.
            myMail.Fields.Add
            ("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            //Use 0 for anonymous
            myMail.Fields.Add
            ("http://schemas.microsoft.com/cdo/configuration/sendusername",
                MailAddressFrom.Address);
            myMail.Fields.Add
            ("http://schemas.microsoft.com/cdo/configuration/sendpassword",
                 PassWord);
            myMail.Fields.Add
            ("http://schemas.microsoft.com/cdo/configuration/smtpusessl",
                 "true");
            myMail.From = MailAddressFrom.Address;
            myMail.To = MailAddressTo.Address;
            myMail.Subject = Subject;
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            myMail.BodyFormat = System.Web.Mail.MailFormat.Html;
            myMail.Body = Content;
            //single attachment 
            if (Attachment != null)
            {
                System.Web.Mail.MailAttachment MyAttachment =
                        new System.Web.Mail.MailAttachment(Attachment);
                myMail.Attachments.Add(MyAttachment);
                myMail.Priority = System.Web.Mail.MailPriority.High;
            }

            //muilple attachment 
            if (ListAtt != null)
            {
                foreach (var item in ListAtt)
                {
                    System.Web.Mail.MailAttachment MyAttachment =
                       new System.Web.Mail.MailAttachment(item);
                    myMail.Attachments.Add(MyAttachment);
                    myMail.Priority = System.Web.Mail.MailPriority.High;
                }
            }
            
            System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com:465";
            System.Web.Mail.SmtpMail.Send(myMail);
            //return true;
        }
        catch (Exception ex)
        {
        }
    
    }

    public void SendMailV2()
    {
        MailMessage objMail = new MailMessage(MailAddressFrom, MailAddressTo);
        objMail.Subject = Subject;
        objMail.Body = Content;
        objMail.IsBodyHtml = true;
        objMail.BodyEncoding = System.Text.Encoding.UTF8;

        SmtpClient smtpMail = new SmtpClient(SmtpServer);
        smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpMail.EnableSsl = EnableSsl;
        smtpMail.Host = "smtp.gmail.com";
        smtpMail.Port = Port;
        smtpMail.Credentials = new NetworkCredential(MailAddressFrom.Address, PassWord);
        smtpMail.Send(objMail);
    }

    public void SendMailAttachFile(string fileName)
    {
        MailMessage objMail = new MailMessage(MailFrom, MailTo, Subject, Content);
        objMail.IsBodyHtml = true;
        objMail.BodyEncoding = System.Text.Encoding.UTF8;

        Attachment attachFile = new Attachment(fileName);
        objMail.Attachments.Add(attachFile);

        SmtpClient smtpMail = new SmtpClient(SmtpServer);
        smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpMail.EnableSsl = EnableSsl;
        smtpMail.Port = Port;
        smtpMail.Credentials = new NetworkCredential(MailFrom, PassWord);
        smtpMail.Send(objMail);
    }

    public void SendMailAttachFileV2(string fileName)
    {
        MailMessage objMail = new MailMessage(MailAddressFrom, MailAddressTo);
        objMail.Subject = Subject;
        objMail.Body = Content;
        objMail.IsBodyHtml = true;
        objMail.BodyEncoding = System.Text.Encoding.UTF8;

        Attachment attachFile = new Attachment(fileName);
        objMail.Attachments.Add(attachFile);

        SmtpClient smtpMail = new SmtpClient(SmtpServer);
        smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpMail.EnableSsl = EnableSsl;
        smtpMail.Port = Port;
        smtpMail.Credentials = new NetworkCredential(MailAddressFrom.Address, PassWord);
        smtpMail.Send(objMail);
    }

    public void SendMailAttachFile(System.IO.Stream stream, string name)
    {
        MailMessage objMail = new MailMessage(MailFrom, MailTo, Subject, Content);
        objMail.IsBodyHtml = true;
        objMail.BodyEncoding = System.Text.Encoding.UTF8;

        stream.Seek(0, System.IO.SeekOrigin.Begin);
        Attachment attachFile = new Attachment(stream, name, "application/pdf");
        objMail.Attachments.Add(attachFile);

        SmtpClient smtpMail = new SmtpClient(SmtpServer);
        smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpMail.EnableSsl = EnableSsl;
        smtpMail.Port = Port;
        smtpMail.Credentials = new NetworkCredential(MailFrom, PassWord);
        smtpMail.Send(objMail);
    }

    public void SendMailAttachFileV2(System.IO.Stream stream, string name)
    {
        MailMessage objMail = new MailMessage(MailAddressFrom, MailAddressTo);
        objMail.Subject = Subject;
        objMail.Body = Content;
        objMail.IsBodyHtml = true;
        objMail.BodyEncoding = System.Text.Encoding.UTF8;

        stream.Seek(0, System.IO.SeekOrigin.Begin);
        Attachment attachFile = new Attachment(stream, name, "application/pdf");
        objMail.Attachments.Add(attachFile);

        SmtpClient smtpMail = new SmtpClient(SmtpServer);
        smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpMail.EnableSsl = EnableSsl;
        smtpMail.Port = Port;
        smtpMail.Credentials = new NetworkCredential(MailAddressFrom.Address, PassWord);
        smtpMail.Send(objMail);
    }
    #endregion
}