using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace EBSSendMailBySMTP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OpenFileDialog Op; string Dosyayolu = ""; Attachment attachment;
        string[] KullanıcıBilgileri = { "admin@admin.com", "passwd", "ebubekirbastama.com.tr", "587", "info@admin.com", "info@admin.com" };
        private void button1_Click(object sender, EventArgs e)
        {
            EBSSmtpSender(txtsub.Text, bodeytxt.Text);
        }

        //Mesaj Yollama Tetodu
        public void EBSSmtpSender(string subject, string body)
        {

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = KullanıcıBilgileri[2].ToString();
            client.Port = int.Parse(KullanıcıBilgileri[3]);

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(KullanıcıBilgileri[0].ToString(), KullanıcıBilgileri[1].ToString());
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(KullanıcıBilgileri[4].ToString());
            msg.To.Add(new MailAddress(KullanıcıBilgileri[5].ToString()));
            if (Dosyayolu != "")// Eğer Dosya Seçildiyse çalışacak kod bloğu
            {
                attachment = new Attachment(Dosyayolu.ToString());
                msg.Attachments.Add(attachment);
                msg.Subject = subject;
                msg.IsBodyHtml = true;
                msg.Body = string.Format(body);
                client.Send(msg);
            }
            else// Eğer Dosya Seçilmediyse çalışacak kod bloğu...
            {
                msg.Subject = subject;
                msg.IsBodyHtml = true;
                msg.Body = string.Format(body);
                client.Send(msg);
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Op = new OpenFileDialog();
                if (Op.ShowDialog()==DialogResult.OK)
                {
                    Dosyayolu = Op.FileName.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"EBSTime");
            }
        }
    }
}
