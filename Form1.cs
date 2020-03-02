using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Ses dosyalarını çalıştırabilmemize yarayan kütüphanemizin eklenmesi.
using System.Media;
//Veritabanı kütüphanesinin eklenmesi
using System.Data.OleDb;

namespace HIT_PTS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Veritabanı dosya yolu ve provider nesnesinin belirlenmesi.
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");


        //Formlar arasında veri aktarımında kullanılacak değişkenler.
        public static string tcno, adi, soyadi, yetki;

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close(); //butona tıklandığında programı sonlandırır.
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (hak != 0)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("Select * from Kullanicilar", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while(kayitokuma.Read())
                {
                    if(rdbYonetici.Checked==true)
                    {
                        if(kayitokuma["kullaniciadi"].ToString()==txtKullaniciAdi.Text&& kayitokuma["parola"].ToString()==txtParola.Text&&kayitokuma["yetki"].ToString()=="Yönetici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            SoundPlayer baglantisesi = new SoundPlayer(@"C:\Users\RYD - Bilgi İşlem\Desktop\dial.wav");
                            baglantisesi.Play();
                            this.Hide();
                            yoneticipaneli frm2 = new yoneticipaneli();
                            frm2.Show();
                            break;
                        }
                    }

                    if (rdbKullanici.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == txtKullaniciAdi.Text && kayitokuma["parola"].ToString() == txtParola.Text && kayitokuma["yetki"].ToString() == "Kullanıcı")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            SoundPlayer baglantisesi = new SoundPlayer(@"C:\Users\RYD - Bilgi İşlem\Desktop\dial.wav");
                            baglantisesi.Play();
                            this.Hide();
                            kullanicipaneli frm3 = new kullanicipaneli();
                            frm3.Show();
                            break;
                        }
                    }
                    
                }
            }
            if (durum == false)
                hak--;
            baglantim.Close();
            lblUyari.Text = Convert.ToString(hak);

            if(hak==0)
            {
                btnGiris.Enabled = false;
                MessageBox.Show("Giriş Hakkı Kalmadı!", "HİT Personel Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        //Yerel yani yalnızca bu formda geçerli olacak değişkenler.
        int hak = 3;bool durum = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Girişi";
            this.AcceptButton = btnGiris;this.CancelButton = btnCikis;
            lblUyari.Text =  Convert.ToString(hak);
            rdbYonetici.Checked = true;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
