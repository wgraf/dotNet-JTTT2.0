using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using WindowsFormsApplication1;
using System.Drawing;
using System.Net.Mail;
using System.Web;


namespace WindowsFormsApplication1
{

    public class transfer
    {
        public string opis;
        public string link;
        public Image obrazek;
        public string ad_obrazu;
    }



    public class HtmlSample
    {
        private readonly string _url;
        private readonly string adress;
        private readonly string fraza;
        private readonly string filename;
        public HtmlSample(string url, string adress, string fraza, int filename)
        {
            this.filename = filename.ToString();
            this.fraza = fraza;
            this._url = url;
            this.adress = adress;
        }

        public string GetPageHtml()
        {
            using (var wc = new WebClient())
            {
                // Ustawiamy prawidłowe kodowanie dla tej strony
                wc.Encoding = Encoding.UTF8;
                // Dekodujemy HTML do czytelnych dla wszystkich znaków 
                var html = System.Net.WebUtility.HtmlDecode(wc.DownloadString(_url));

                return html;
            }
        }

        // string opis, link;
        // Image obrazek;

        public transfer obiekt1 = new transfer();
        public void PrintPageNodes()
        {
            //string opis, link;
            //Image obrazek;

            // Tworzymy obiekt klasy HtmlDocument zdefiniowanej w namespace HtmlAgilityPack
            // Uwaga - w referencjach projektu musi się znajdować ta biblioteka
            // Przy użyciu nuget-a pojawi się tam automatycznie
            var doc = new HtmlAgilityPack.HtmlDocument();

            // Używamy naszej metody do pobrania zawartości strony
            var pageHtml = GetPageHtml();

            // Ładujemy zawartość strony html do struktury documentu (obiektu klasy HtmlDocument)
            doc.LoadHtml(pageHtml);

            // Metoda Descendants pozwala wybrać zestaw node'ów o określonej nazwie
            var nodes = doc.DocumentNode.Descendants("img");
            //
            // Iterujemy po wszystkich znalezionych node'ach
            foreach (var node in nodes)
            {
                Console.WriteLine("---------");

                // Wyświetlamy nazwę node'a (powinno byc img")
                Console.WriteLine("Node name: " + node.Name);

                // Każdy node ma zestaw atrybutów - nas interesują atrybuty src oraz alt

                // Wyświetlamy wartość atrybuty src dla aktualnego węzła
                Console.WriteLine("Src value: " + node.GetAttributeValue("src", ""));
                obiekt1.link = node.GetAttributeValue("src", "").ToString();
                //obiekt1.link = Form1.madres + obiekt1.link;
                //MessageBox.Show(link);

                // Wyświetlamy wartość atrybuty alt dla aktualnego węzła
                Console.WriteLine("Alt value: " + node.GetAttributeValue("alt", ""));

                obiekt1.opis = node.GetAttributeValue("alt", "");

                // MessageBox.Show(Form1.mtresc);

                bool contains = Regex.IsMatch(obiekt1.opis, fraza);
                if (contains)
                {
                    MessageBox.Show("Zawiera!");
                    // WebClient webClient = new WebClient();
                    // WebClient.DownloadFile(link, "obrazek.jpg");

                    //POBIERANIE OBRAZKA
                    byte[] data;
                    // WebClient webClient = new WebClient();
                    MessageBox.Show(obiekt1.link);
                    //webClient.DownloadFile(link, @".");

                    string localFilename = @"C:\Users\dd\Desktop\dotnet\" + filename + ".jpg";
                    obiekt1.ad_obrazu = localFilename;
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(obiekt1.link, localFilename);
                    }



                    try
                    {
                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.poczta.onet.pl");
                        mail.From = new MailAddress("wojtasg3@autograf.pl");
                        mail.To.Add(adress);
                        mail.Subject = "Test Mail - 1";
                        mail.Body = "mail with attachment";

                        System.Net.Mail.Attachment attachment;
                        attachment = new System.Net.Mail.Attachment(@"C:\Users\dd\Desktop\dotnet\" + filename + ".jpg");
                        mail.Attachments.Add(attachment);

                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("wojtasg3@autograf.pl", "Tinittunga1");
                        SmtpServer.EnableSsl = true;
                        MessageBox.Show("wyslano");
                        SmtpServer.Send(mail);
                        MessageBox.Show("mail Send");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Nie wyslalem");
                        Console.WriteLine(ex.ToString());
                    }

                }



                // Oczywiscie w aplikacji JTTT nie będziemy tego wyświetlać tylko będziemy analizować 
                // wartość atrybutów node'a jako string

                // Wszystkie powyższe operacje można napisać zdecydowanie prościej i składniej na przyklad za pomoca wyrazenia LINQ
                // Ten zapis jest tylko do celów ćwiczebnych 
            }

        }

    }




    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());



            // Console.Read();
        }
    }
}
