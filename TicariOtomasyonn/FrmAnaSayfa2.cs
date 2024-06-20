using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicariOtomasyonn.DAL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TicariOtomasyonn
{
    public partial class FrmAnaSayfa2 : Form
    {
        VTIslemleri vti = new VTIslemleri();
        public FrmAnaSayfa2()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        //ÜRÜNLER SAYFASI
        void listele()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBL_URUNLER", baglan.baglanti());
            adapter.Fill(table);
            dataGridView1.DataSource=table;
        }

        void temizle()
        {
            adtxt.Text="";
            TxtAlis.Text="";
            txtID.Text="";
            TxtMarka.Text="";
            TxtModel.Text="";
            TxtSatis.Text="";
            maskedYil.Text="";
            numericAdet.Value=0;
            richTextBoxDetay.Text="";
        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_URUNLER (URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY,ID) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", baglan.baglanti());
            komut.Parameters.AddWithValue("p1", adtxt.Text);
            komut.Parameters.AddWithValue("p2", TxtMarka.Text);
            komut.Parameters.AddWithValue("p3", TxtModel.Text);
            komut.Parameters.AddWithValue("p4", maskedYil.Text);
            komut.Parameters.AddWithValue("p5", int.Parse((numericAdet.Value).ToString()));
            komut.Parameters.AddWithValue("p6", decimal.Parse(TxtAlis.Text).ToString());
            komut.Parameters.AddWithValue("p7", decimal.Parse(TxtSatis.Text).ToString());
            komut.Parameters.AddWithValue("p8", richTextBoxDetay.Text);
            komut.Parameters.AddWithValue("p9", txtID.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Ürün Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("DELETE FROM TBL_URUNLER WHERE ID=@p1", baglan.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtID.Text);
            komutsil.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_URUNLER SET URUNAD=@p1,MARKA=@p2,MODEL=@p3,YIL=@p4,ADET=@p5,ALISFIYAT=@p6,SATISFIYAT=@p7,DETAY=@p8 WHERE ID=@p9", baglan.baglanti());
            komut.Parameters.AddWithValue("p1", adtxt.Text);
            komut.Parameters.AddWithValue("p2", TxtMarka.Text);
            komut.Parameters.AddWithValue("p3", TxtModel.Text);
            komut.Parameters.AddWithValue("p4", maskedYil.Text);
            komut.Parameters.AddWithValue("p5", int.Parse((numericAdet.Value).ToString()));
            komut.Parameters.AddWithValue("p6", decimal.Parse(TxtAlis.Text));
            komut.Parameters.AddWithValue("p7", decimal.Parse(TxtSatis.Text));
            komut.Parameters.AddWithValue("p8", richTextBoxDetay.Text);
            komut.Parameters.AddWithValue("p9", txtID.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Ürün Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        //PERSONEL SAYFASI

        void personelliste()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBL_PERSONEL", baglan.baglanti());
            adapter.Fill(table);
            dataGridView3.DataSource=table;

        }

        //il getirme
        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("SELECT sehir FROM TBL_ILLER", baglan.baglanti());
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                cmbIL.Items.Add(reader[0]);
            }
            baglan.baglanti().Close();
        }

        void temizle2()
        {
            IDTEXT.Text="";
            ADTXTBOX.Text="";
            SOYADTXTBOX.Text="";
            MskTC.Text="";
            MskTel.Text="";
            TxtMail.Text="";
            cmbIL.Text="";
            CMBILCE.Text="";
            RCHADRES.Text="";
            TXTGOREV.Text="";
        }

        private void KAYDETbtn_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_PERSONEL (AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", baglan.baglanti());
            komut.Parameters.AddWithValue("@P1", ADTXTBOX.Text);
            komut.Parameters.AddWithValue("@P2", SOYADTXTBOX.Text);
            komut.Parameters.AddWithValue("@P3", MskTel.Text);
            komut.Parameters.AddWithValue("@P4", MskTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtMail.Text);
            komut.Parameters.AddWithValue("@P6", cmbIL.Text);
            komut.Parameters.AddWithValue("@P7", CMBILCE.Text);
            komut.Parameters.AddWithValue("@P8", RCHADRES.Text);
            komut.Parameters.AddWithValue("@P9", TXTGOREV.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Personel Bilgileri Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelliste();
        }

        private void SILbtn_Click(object sender, EventArgs e)
        {

            SqlCommand komutsil = new SqlCommand("DELETE FROM TBL_PERSONEL WHERE ID=@P1", baglan.baglanti());
            komutsil.Parameters.AddWithValue("@P1", IDTEXT.Text);
            komutsil.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Personel Listeden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.None);
            personelliste();
            temizle();
        }

        private void GUNCELLEbtn_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_PERSONEL SET AD=@P1,SOYAD=@P2,TELEFON=@P3,TC=@P4,MAIL=@P5,IL=@P6,ILCE=@P7,ADRES=@P8,GOREV=@P9 WHERE ID=@P10", baglan.baglanti());
            komut.Parameters.AddWithValue("@P1", ADTXTBOX.Text);
            komut.Parameters.AddWithValue("@P2", SOYADTXTBOX.Text);
            komut.Parameters.AddWithValue("@P3", MskTel.Text);
            komut.Parameters.AddWithValue("@P4", MskTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtMail.Text);
            komut.Parameters.AddWithValue("@P6", cmbIL.Text);
            komut.Parameters.AddWithValue("@P7", CMBILCE.Text);
            komut.Parameters.AddWithValue("@P8", RCHADRES.Text);
            komut.Parameters.AddWithValue("@P9", TXTGOREV.Text);
            komut.Parameters.AddWithValue("@P10", IDTEXT.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Personel Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            personelliste();
        }

        private void TEMIZLEbtn_Click(object sender, EventArgs e)
        {
            temizle2();
        }

        // ilce getirme
        private void cmbIL_SelectedIndexChanged(object sender, EventArgs e)
        {
            CMBILCE.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT ilce FROM TBL_ILCELER WHERE sehir=@p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbIL.SelectedIndex + 1);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                CMBILCE.Items.Add(reader[0]);
            }
            baglan.baglanti().Close();
        }

        private void FrmAnaSayfa2_Load(object sender, EventArgs e)
        {
            personelliste();
            sehirlistesi();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            try
            { // baglan.baglanti().Open();
                SqlDataAdapter veri = new SqlDataAdapter("SELECT * FROM TBL_URUNLER", baglan.baglanti());
                DataTable table = new DataTable();
                veri.Fill(table);
                dataGridView1.DataSource = table;
                baglan.baglanti().Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void BtnYardim_Click(object sender, EventArgs e)
        {
            Form FrmYardim = new FrmYardim();
            FrmYardim.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnKYDT_Click(object sender, EventArgs e)
        {
            //baglan.baglanti().Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_MUSTERI (AD,SOYAD,TELEFON,MAIL,ADRES) VALUES (@P1,@P2,@P3,@P4,@P5)", baglan.baglanti());
            komut.Parameters.AddWithValue("@P1", musteriAD.Text);
            komut.Parameters.AddWithValue("@P2", musteriSoyad.Text);
            komut.Parameters.AddWithValue("@P3", mskMusteriTel.Text);
            komut.Parameters.AddWithValue("@P4", musteriMail.Text);
            komut.Parameters.AddWithValue("@P5", rchMusteriAdres.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Müşteri Bilgileri Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            musteriAD.Clear();
            musteriSoyad.Clear();
            mskMusteriTel.Clear();
            musteriMail.Clear();
            rchMusteriAdres.Clear();
        }

        private void BtnGoruntule_Click(object sender, EventArgs e)
        {
            SqlDataAdapter veri = new SqlDataAdapter("SELECT * FROM TBL_MUSTERI", baglan.baglanti());
            DataTable table = new DataTable();
            veri.Fill(table);
            dataGridView5.DataSource = table;
            baglan.baglanti().Close();
        }
        //SATISLAR
        private void txtAD_TextChanged(object sender, EventArgs e)
        {
            // baglan.baglanti().Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_MUSTERI WHERE AD='" + txtAD.Text + "'", baglan.baglanti());
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                textSoyad.Text = oku["SOYAD"].ToString();
                mskTELEFON.Text = oku["TELEFON"].ToString();
                textMAIL.Text = oku["MAIL"].ToString();
                richTextADRES.Text = oku["ADRES"].ToString();
            }
            baglan.baglanti().Close();

        }

        private void textID_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("SELECT * FROM TBL_URUNLER WHERE ID='" + textID.Text + "'", baglan.baglanti());
            SqlDataReader oku2 = komut2.ExecuteReader();
            while (oku2.Read())
            {
                textURUNAD.Text = oku2["URUNAD"].ToString();
                textMARKA.Text = oku2["MARKA"].ToString();
                textMODEL.Text = oku2["MODEL"].ToString();
                maskedTextYIL.Text = oku2["YIL"].ToString();
            }
            baglan.baglanti().Close();
        }

        int a, b, c;

        // personeller datagrid uzerindeki verileri textboxa aktarma
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IDTEXT.Text=dataGridView3.CurrentRow.Cells[0].Value.ToString();
            ADTXTBOX.Text=dataGridView3.CurrentRow.Cells[1].Value.ToString();
            SOYADTXTBOX.Text=dataGridView3.CurrentRow.Cells[2].Value.ToString();
            MskTel.Text=dataGridView3.CurrentRow.Cells[3].Value.ToString();
            MskTC.Text=dataGridView3.CurrentRow.Cells[4].Value.ToString();
            TxtMail.Text=dataGridView3.CurrentRow.Cells[5].Value.ToString();
            cmbIL.Text=dataGridView3.CurrentRow.Cells[6].Value.ToString();
            CMBILCE.Text=dataGridView3.CurrentRow.Cells[7].Value.ToString();
            RCHADRES.Text=dataGridView3.CurrentRow.Cells[8].Value.ToString();
            TXTGOREV.Text=dataGridView3.CurrentRow.Cells[9].Value.ToString();
        }

        // musteriler datagrid uzerindeki verileri textboxa aktarma
        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            musteriAD.Text=dataGridView5.CurrentRow.Cells[0].Value.ToString();
            musteriSoyad.Text=dataGridView5.CurrentRow.Cells[1].Value.ToString();
            mskMusteriTel.Text=dataGridView5.CurrentRow.Cells[2].Value.ToString();
            musteriMail.Text=dataGridView5.CurrentRow.Cells[3].Value.ToString();
            rchMusteriAdres.Text=dataGridView5.CurrentRow.Cells[4].Value.ToString();
        }

        public string ad;
        private void StokGoruntule_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlDataAdapter veri = new SqlDataAdapter("SELECT * FROM TBL_STOKLAR", baglan.baglanti());
            veri.Fill(table);
            dataGridView2.DataSource = table;
            baglan.baglanti().Close();
        }

        // urunler datagrid uzerindeki verileri textboxa aktarma
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text=dataGridView1.CurrentRow.Cells[0].Value.ToString();
            adtxt.Text=dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TxtMarka.Text=dataGridView1.CurrentRow.Cells[2].Value.ToString();
            TxtModel.Text=dataGridView1.CurrentRow.Cells[3].Value.ToString();
            maskedYil.Text=dataGridView1.CurrentRow.Cells[4].Value.ToString();
            numericAdet.Text=dataGridView1.CurrentRow.Cells[5].Value.ToString();
            TxtAlis.Text=dataGridView1.CurrentRow.Cells[6].Value.ToString();
            TxtSatis.Text=dataGridView1.CurrentRow.Cells[7].Value.ToString();
            richTextBoxDetay.Text=dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }




        private void BtnSatisYap_Click(object sender, EventArgs e)
        {
            //baglan.baglanti().Open();
            int a = Convert.ToInt32(numericUpADET.Text);
            SqlCommand komut2 = new SqlCommand("SELECT ADET FROM TBL_URUNLER WHERE ID=@UrunID", baglan.baglanti());
            komut2.Parameters.AddWithValue("@UrunID", textID.Text);

            try
            {
               // baglan.baglanti().Open();
                SqlDataReader oku2 = komut2.ExecuteReader();
                if (oku2.Read())
                {
                    int b = Convert.ToInt32(oku2["ADET"]);
                    int c = b - a;
                    label36.Text ="Kalan Stok: " + c.ToString();
                }
                else
                {
                    MessageBox.Show("Ürün bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }




        }
    }
}

