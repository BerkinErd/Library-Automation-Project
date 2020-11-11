using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kütüphane_Projesi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        OleDbConnection baglantı = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\dosyalar\\kutuphane.accdb");
        OleDbCommand komut = new OleDbCommand(); 

        private void Form2_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (baglantı.State == ConnectionState.Closed)
                baglantı.Open();
            ds.Clear();
            OleDbDataAdapter adpKitap = new OleDbDataAdapter("select YazarAdi,KitapAdi,BaskiNo,Adet,MevcutDurumu From kitaplar", baglantı);
            adpKitap.Fill(ds);
            dgwKitap.DataSource = ds.Tables[0];

            komut = new OleDbCommand("SELECT COUNT(*) FROM kitaplar", baglantı);
            lblKayitSayisi.Text = "Veritabanında " + (int)komut.ExecuteScalar() + " tane kayıt var.";

            baglantı.Close();
        }

        private void cmdanamenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 frmAnaForm = new Form2();
            frmAnaForm.Show();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtkitapadi.Text))
            {
                MessageBox.Show("Lütfen Kitap Adı Alanını Doldurunuz", "Hata");
                return;
            }
            else
            {
                try
                {
                    DataSet ds = new DataSet();
                    if (baglantı.State == ConnectionState.Closed)
                        baglantı.Open();
                    ds.Clear();

                    OleDbCommand komut = new OleDbCommand("INSERT INTO kitaplar (YazarAdi,KitapAdi,BaskiNo,Adet,MevcutDurumu) VALUES ('" + txtYazarAdi.Text + "','" + txtkitapadi.Text + "','" + txtBaskiNo.Text + "','" + txtAdet.Text + "','" + txtDurum.Text + "')", baglantı);
                    komut.ExecuteNonQuery();

                    komut = new OleDbCommand("SELECT COUNT(*) FROM kitaplar", baglantı);
                    lblKayitSayisi.Text = "Veritabanında " + (int)komut.ExecuteScalar() + " tane kayıt var.";

                    baglantı.Close();
                    MessageBox.Show("Kayıt Eklendi!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    baglantı.Close();
                }
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                if (baglantı.State == ConnectionState.Closed)
                    baglantı.Open();
                ds.Clear();
                OleDbCommand komut = new OleDbCommand("UPDATE kitaplar SET YazarAdi='" + txtYazarAdi.Text + "',KitapAdi='" + txtkitapadi.Text + "', BaskiNo='" + txtBaskiNo.Text + "',Adet='" + txtAdet.Text + "',MevcutDurumu='" + txtDurum.Text + "' WHERE KitapAdi='" + txtkitapadi.Text + "'", baglantı);
                komut.ExecuteNonQuery();
                baglantı.Close();
                MessageBox.Show("Kayıt Güncellendi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglantı.Close();
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                if (baglantı.State == ConnectionState.Closed)
                    baglantı.Open();
                ds.Clear();

                OleDbCommand komut = new OleDbCommand("DELETE FROM kitaplar WHERE KitapAdi='" + txtkitapadi.Text + "'", baglantı);
                komut.ExecuteNonQuery();

                komut = new OleDbCommand("SELECT COUNT(*) FROM kitaplar", baglantı);
                lblKayitSayisi.Text = "Veritabanında " + (int)komut.ExecuteScalar() + " tane kayıt var.";

                baglantı.Close();
                MessageBox.Show("Kayıt Silindi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglantı.Close();
            }

        }

        private void dgwKitap_CellClick(object sender,DataGridViewCellEventArgs e)
        {
            int satirno, sutunno;
            string veri;

            satirno = e.RowIndex;
            sutunno = e.ColumnIndex;

            veri = dgwKitap.Rows[satirno].Cells[sutunno].Value.ToString();

            txtYazarAdi.Text = dgwKitap.Rows[satirno].Cells[0].Value.ToString();
            txtkitapadi.Text = dgwKitap.Rows[satirno].Cells[1].Value.ToString();
            txtBaskiNo.Text = dgwKitap.Rows[satirno].Cells[2].Value.ToString();
            txtAdet.Text = dgwKitap.Rows[satirno].Cells[3].Value.ToString();
            txtDurum.Text = dgwKitap.Rows[satirno].Cells[4].Value.ToString();

         }

















    }
}
