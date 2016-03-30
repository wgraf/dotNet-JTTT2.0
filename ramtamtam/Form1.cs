using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net.Mail;
using System.Web;


namespace WindowsFormsApplication1
{


    public partial class Form1 : Form
    {

        String adres;
        String tresc;
        String email;
        String task;


        //cos tutuaj dopisuje
        static string result1;
        static string result2;
        static string result3;
        static string result4;

        public Form1()
        {
            InitializeComponent();
        }

        public static String madres
        {
            get { return result1; }
            set { result1 = value; }
        }

        public static String mtresc
        {
            get { return result2; }
            set { result2 = value; }
        }

        public static String memail
        {
            get { return result3; }
            set { result3 = value; }
        }

        public static String mtask
        {
            get { return result4; }
            set { result4 = value; }
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void label2_Click(object sender, EventArgs e) { }

        private void label5_Click(object sender, EventArgs e) { }

        private void label6_Click(object sender, EventArgs e) { }

        private void label7_Click(object sender, EventArgs e) { }

        private void label9_Click(object sender, EventArgs e) { }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            tresc = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            email = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            task = textBox4.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            adres = textBox1.Text;
        }

        public class elementum
        {
            public string el_email { get; set; }
            public string el_adres { get; set; }
            public string el_tekst { get; set; }
            public string el_task { get; set; }
            public string show { get; set; }
            public int el_nr { get; set; }

            public elementum()
            {
                el_email = "";
                el_adres = "";
                el_tekst = "";
                el_task = "";
                el_nr = 0;
            }
        }

        BindingList<elementum> lista_elementow = new BindingList<elementum>();

        int nr;

        private void dodaj_do_listy(object sender, EventArgs e)
        {
            elementum el1 = new elementum();

            result1 = textBox1.Text;  //wpisuje otrzymany tekst do madres
            result2 = textBox2.Text;  //wpisuje otrzymany tekst do mtresc
            result3 = textBox3.Text;  //wpisuje otrzymany tekst do memail
            result4 = textBox4.Text;  //wpisuje otrzymany tekst do mtask

            el1.el_email = memail;
            el1.el_adres = madres;
            el1.el_task = mtask;
            el1.el_tekst = mtresc;
            el1.el_nr = nr;
            el1.show = el1.el_task + " szukaj frazy " + el1.el_tekst + " ze strony " + el1.el_adres;
            lista_elementow.Add(el1);

            //Bindowanie listy do listBoxa
            listBox1.DataSource = lista_elementow;
            listBox1.DisplayMember = "show";

            nr++;
        }

        int rozmiar;
        private void wykonaj(object sender, EventArgs e)
        {
            rozmiar = lista_elementow.Count;
            for (int i = 0; i < rozmiar; i++)
            {
                var hs = new HtmlSample(lista_elementow[i].el_adres, lista_elementow[i].el_email, lista_elementow[i].el_tekst, i);
                hs.PrintPageNodes();

            }
        }

        private void czysc(object sender, EventArgs e)
        {
            lista_elementow.Clear();
            nr = 0;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\dd\Desktop\dotnet\info.txt"))
            {
                sw.WriteLine(lista_elementow.Count.ToString());
            }
            //System.IO.File.WriteAllText(@"C:\Users\dd\Desktop\dotnet\info.txt", lista_elementow.Count.ToString());
            for (int i = 0; i < lista_elementow.Count; i++)
            {
                using (StreamWriter writer = new StreamWriter(@"C:\Users\dd\Desktop\dotnet\info.txt", true))
                {
                    writer.Write(lista_elementow[i].el_task + "\r\n");
                    writer.Write(lista_elementow[i].el_tekst + "\r\n");
                    writer.Write(lista_elementow[i].el_adres + "\r\n");
                    writer.Write(lista_elementow[i].el_email + "\r\n");

                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            lista_elementow.Clear();
            nr = 0;
            System.IO.StreamReader file =
                    new System.IO.StreamReader(@"C:\Users\dd\Desktop\dotnet\info.txt");
            int size;
            int.TryParse(file.ReadLine(), out size);
            for (int i = 0; i < size; i++)
            {
                elementum el1 = new elementum();
                el1.el_task = file.ReadLine();
                el1.el_tekst = file.ReadLine();
                el1.el_adres = file.ReadLine();
                el1.el_email = file.ReadLine();

                el1.el_nr = i + 1;
                el1.show = el1.el_task + " szukaj frazy " + el1.el_tekst + " ze strony " + el1.el_adres;
                lista_elementow.Add(el1);

                //Bindowanie listy do listBoxa
                listBox1.DataSource = lista_elementow;
                listBox1.DisplayMember = "show";

                nr++;
            }



        }
    }


}
