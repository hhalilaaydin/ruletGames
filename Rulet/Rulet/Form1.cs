using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rulet
{
    public partial class Form1 : Form
    {
        long bakiye = 10000;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            TextBakiye.Text = "Kumar oynamak için "+ bakiye.ToString() +" bakiyeye sahipsin..";
        }   
        private void button1_Click(object sender, EventArgs e)   //btnOyna 
        {
            int kontrol;
            long toplam=topla();  //Toplam oyun değeri hesaplandı
            kontrol=bakiyeKontrol(toplam);      //bakiye kontrol edildi. return (0=="false - bir yerde sorun var" else 1=="true - sorun yok")
            long s1_18=0,s19_36=0,
                 tek=0,cift=0,
                 kirmizi=0,siyah=0,
                 s01_12=0,s13_24=0,s25_36=0,
                 s1kolon=0,s2kolon=0,s3kolon=0,kazanc=0;
            long[] sayilar = new long[37];
            degerCek(out s1_18, out s19_36,           //Tüm sayı değerlerini artık cektik.
                out cift, out tek,
                out kirmizi, out siyah,
                out s01_12, out s13_24, out s25_36,
                out s1kolon, out s2kolon, out s3kolon,
                sayilar);
            //MessageBox.Show(sayilar[5].ToString());
            if (kontrol == 1)
            {                
                Random rnd = new Random();
                int sonuc = rnd.Next(36);  //Sonuc sayısı - Kazanan sayıyı rastgele ürettik
                labelSayi.Text = sonuc.ToString();      //Sonucu Ekrana Bastık
                sonucuHesapla(s1_18, s19_36,           //Sonucu hesaplayacaz. Ne kazandı - kaybetti / bakiyeyi düşürmemiz - arttırmamız lazım.
                cift, tek,
                kirmizi, siyah,
                s01_12, s13_24, s25_36,
                s1kolon, s2kolon, s3kolon,
                sayilar,kazanc,toplam,sonuc);
            }
            temizle();
            
        }
       
        private void text1_18_KeyPress(object sender, KeyPressEventArgs e)  //Tüm tuşlarım kopyala-yapıstır özelliği kapatıldı.
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }
        private void button2_Click(object sender, EventArgs e) //Tüm textboxlar temizlendi.
        {
            temizle();
        }
        long topla()
        {
            long toplam = 0;  //Oyun için harcanacak parayı hesapla
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tbox = (TextBox)item;
                    if (tbox.Text != "")
                    {
                        toplam = toplam + Convert.ToInt32(tbox.Text);
                    }                    
                }
            }
            return toplam;
        }
        int bakiyeKontrol(long toplam)
        {
            if ((toplam <= bakiye) && (toplam > 0))     //herşey yolunda mayki
            {   
               // MessageBox.Show("1");
                return 1;
            }
            else if (toplam == 0)       //boş değerler için
            {
                MessageBox.Show("Lütfen Tutar Giriniz - 0");
                temizle();
                return 0;
            }
            else if(toplam>bakiye)
            {
                MessageBox.Show("Bakiyeniz Bu Oyun için Yeterli Değil - temizledim - 0");
                temizle();          //tüm textboxlar temizlenir.
                return 0;
            }
            else
            {
                MessageBox.Show("Beklenmeyen Hata WTF??? - 0");
                return 0;
            }
        }
        void temizle()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tbox = (TextBox)item;
                    tbox.Clear();
                }
            }
        }
        void degerCek(out long s1_18,out long s19_36,
                    out long cift,out long tek,
                    out long kirmizi,out long siyah,
                    out long s01_12,out long s13_24,out long s25_36,
                    out long s1kolon,out long s2kolon,out long s3kolon,
                    long []sayilar)                   
        {                        
                for (int i = 0; i <= 36; i++)  //Tüm sayıları diziye atıyorum 1-36 değerleri
                        {
                            string textboxname = "textBox" + i.ToString();
                            foreach (Control item in this.Controls)   // Tüm Controlleri geziyor.
                            {
                                if ((item is TextBox) && (item.Name == textboxname))   //Dogru kontrole ulaşınca
                                {
                                    TextBox tbox = (TextBox)item;
                                    if (tbox.Text != "")            //Eğer içinde null değer yoksa !!BOŞ DEĞER SAYIYA ÇEVRİLMEZ.
                                    {
                                        sayilar[i] = Convert.ToInt64(tbox.Text);
                                        //MessageBox.Show(tbox.Text);  //Kontrol içindi
                                    }
                                    else                    //Null değerse 0 ata
                                    {
                                        sayilar[i] = 0;
                                    }
                                }
                            }
                            /* Control ctl = this.Controls.Find(textboxname, true).FirstOrDefault();
                             if (ctl != null)  //eğer textbox bulunursa
                             {
                                 // use "ctl" directly:
                                 sayilar[i]=Convert.ToInt64(ctl.Text);
                             }  */
                        }
                if (text1_18.Text != "")   //1-18
                {
                    s1_18 = Convert.ToInt64(text1_18.Text);
                }
                else s1_18 = 0;
                if (text19_36.Text != "")   //19-36
                {
                    s19_36 = Convert.ToInt64(text19_36.Text);
                }
                else s19_36 = 0;
                if (textTek.Text != "")     //tek
                {
                    tek = Convert.ToInt64(textTek.Text);
                }
                else tek = 0;
                if (textCift.Text != "")    //çift
                {
                    cift = Convert.ToInt64(textCift.Text);
                }
                else cift = 0;
                if (textKırmızı.Text != "")     //red
                {
                    kirmizi = Convert.ToInt64(textKırmızı.Text);
                }
                else kirmizi = 0;
                if (textSiyah.Text != "")       //black
                {       
                    siyah = Convert.ToInt64(textSiyah.Text);
                }
                else siyah = 0;
                if (text1_12.Text != "")        //1-12
                {
                    s01_12 = Convert.ToInt64(text1_12.Text);
                }
                else s01_12 = 0;
                if (text13_24.Text != "")       //13-24
                {
                    s13_24 = Convert.ToInt64(text13_24.Text);
                }
                else s13_24 = 0;
                if (text25_36.Text != "")       //25-36
                {
                    s25_36 = Convert.ToInt64(text25_36.Text);
                }
                else s25_36 = 0;
                if (textBox1Kolon.Text != "")   //1.kolon
                {
                    s1kolon = Convert.ToInt64(textBox1Kolon.Text);
                }
                else s1kolon = 0;
                if (textBox2Kolon.Text != "")   //2.kolon
                {
                    s2kolon = Convert.ToInt64(textBox2Kolon.Text);
                }
                else s2kolon = 0;
                if (textBox3Kolon.Text != "")   //3.kolon
                {
                    s3kolon = Convert.ToInt64(textBox3Kolon.Text);
                }
                else s3kolon = 0;
        }
        
        void sonucuHesapla(long s1_18,long s19_36,
                    long tek,long cift,
                    long kirmizi,long siyah,
                    long s01_12,long s13_24,long s25_36,
                    long s1kolon,long s2kolon,long s3kolon,
                    long[] sayilar,long kazanc,long toplam,int sonuc)
        {
            
            kazanc = yarimAralik(s1_18, s19_36,sonuc) + tekcift(tek, cift, sonuc) + //nested method - içice method cagırımıyla tüm kazanma ihtimallerini hesaplıyorum.
                            renk(kirmizi,siyah,sonuc)       +     ucAralik(s01_12,s13_24,s25_36,sonuc)+
                            kolonlar(s1kolon,s2kolon,s3kolon,sonuc) + sayilariBul(sayilar,sonuc);   
            labelSonuc.Text = toplam.ToString()+ " yatırdığınız tutara karşılık " + kazanc.ToString() + " kazanç sağladınız.. ";
            bakiye = bakiye - toplam + kazanc;
            TextBakiye.Text = "Kumar oynamak için " + bakiye.ToString() + " bakiyeye sahipsin..";
            if (bakiye == 0)
            {
                MessageBox.Show("Kaybettiniz.. Tekrar Deneyiniz..");
                bakiye = 10000;
                TextBakiye.Text = "Kumar oynamak için " + bakiye.ToString() + " bakiyeye sahipsin..";
            }
        }
        long tekcift(long cift, long tek,int sonuc)
        {
            
            if ((sonuc!=0)&&(sonuc % 2 == 0))
            {
               return cift * 2;
            }
            else if (sonuc % 2 != 0)
            {
               return tek * 2;
            }
            else  return 0;
        }
        long yarimAralik(long s1_18, long s19_36,int sonuc)
        {
            if ((sonuc != 0) && (sonuc >= 1) && (sonuc <= 18))
            {
                return s1_18 * 2;
            }
            if ((sonuc != 0) && (sonuc >= 19) && (sonuc <= 36))
            {
                return s19_36 * 2;
            }
            else return 0;
        }
        long renk(long kirmizi, long siyah,int sonuc)
        {
            long renkKazanc = 0;
            if  ((sonuc==1)||(sonuc==3)||(sonuc==5)||(sonuc==7)||(sonuc==9)||(sonuc==12)||(sonuc==14)||(sonuc==16)||(sonuc==18)||
                (sonuc==19)||(sonuc==21)||(sonuc==23)||(sonuc==25)||(sonuc==27)||(sonuc==30)||(sonuc==32)||(sonuc==34)||(sonuc==36)){
                renkKazanc=renkKazanc+(kirmizi*2);
                labelSayi.BackColor = System.Drawing.Color.Red;
                }
            if ((sonuc == 2) || (sonuc == 4) || (sonuc == 6) || (sonuc == 8) || (sonuc == 10) || (sonuc == 11) || (sonuc == 13) || (sonuc == 15) || (sonuc == 17) ||
               (sonuc == 20) || (sonuc == 22) || (sonuc == 24) || (sonuc == 26) || (sonuc == 28) || (sonuc == 29) || (sonuc == 31) || (sonuc == 33) || (sonuc == 36))
            {
                renkKazanc = renkKazanc + (siyah * 2);
                labelSayi.BackColor = System.Drawing.Color.Black;
            }
         //   1,3,5,7,9,12,14,16,18,19,21,23,25,27,30,32,34,36 ---red
         //   2,4,6,8,10,11,13,15,17,20,22,24,26,28,29,31,33,35 ---black
            return renkKazanc;
        }
        long ucAralik(long s01_12, long s13_24, long s25_36,int sonuc)
        {
            if ((sonuc != 0) && (sonuc >= 1) && (sonuc <= 12))
            {
                return s01_12 * 3;
            }
            else if ((sonuc != 0) && (sonuc >= 13) && (sonuc <= 24))
            {
                return s13_24 * 3;
            }
            else if ((sonuc != 0) && (sonuc >= 25) && (sonuc <= 36))
            {
                return s25_36 * 3;
            }
            else return 0;
        }
        long kolonlar(long s1kolon,long s2kolon,long s3kolon,int sonuc){
            long kolKazanc=0;
            for (int i = 1; i <= 34; i=i+3)
            {
                if (i == sonuc)
                {
                    kolKazanc = kolKazanc+s1kolon * 3;
                }
            }
            for (int i = 2; i <= 35; i = i + 3)
            {
                if (i == sonuc)
                {
                    kolKazanc = kolKazanc+s2kolon * 3;
                }
            }
            for (int i = 3; i <= 36; i = i + 3)
            {
                if (i == sonuc)
                {
                    kolKazanc = kolKazanc+s3kolon * 3;
                }
            }
            return kolKazanc;
        }
        long sayilariBul(long[] sayilar,int sonuc)
        {
            long sayKazanc = 0;
            for (int i = 1; i <= 36; i++)
            {
                if ((sayilar[i] != 0) && (i == sonuc))
                {
                    sayKazanc = sayKazanc + (sayilar[i] * 36);
                }
            }
            return sayKazanc;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bakiye = 10000;
            TextBakiye.Text = "Kumar oynamak için " + bakiye.ToString() + " bakiyeye sahipsin..";
        }
      
    }
}
