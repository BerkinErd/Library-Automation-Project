using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace Kütüphane_Projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglantı = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\dosyalar\\kutuphane.accdb");
        OleDbCommand komut = new OleDbCommand();

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                DataSet ds = new DataSet();
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();

                OleDbDataAdapter adp = new OleDbDataAdapter("Select * from kitaplar where KitapAdi LIKE '" + txtaranan.Text + "%'", baglantı);
                adp.Fill(ds);
                lstsonuc.DataSource = ds.Tables[0];
                lstsonuc.DisplayMember = "KitapAdi";
                txtAdet.DataBindings.Clear();
                txtAdet.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "Adet", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
                txtBaskiNo.DataBindings.Clear();
                txtBaskiNo.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "BaskiNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
                txtDurum.DataBindings.Clear();
                txtDurum.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "MevcutDurumu", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
                txtKitapAdi.DataBindings.Clear();
                txtKitapAdi.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "KitapAdi", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
                txtYazarAdi.DataBindings.Clear();
                txtYazarAdi.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "YazarAdi", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
                baglantı.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                baglantı.Close();
            }
        }

        private void txtaranan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();

                OleDbDataAdapter adp = new OleDbDataAdapter("Select * from kitaplar where KitapAdi LIKE '" + txtaranan.Text + "%'", baglantı);
                adp.Fill(ds);
                lstsonuc.DataSource = ds.Tables[0];
                lstsonuc.DisplayMember = "KitapAdi";

                txtAdet.DataBindings.Clear();
                txtAdet.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "Adet", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
                txtBaskiNo.DataBindings.Clear();
                txtBaskiNo.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "BaskiNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
                txtDurum.DataBindings.Clear();
                txtDurum.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "MevcutDurumu", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
                txtKitapAdi.DataBindings.Clear();
                txtKitapAdi.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "KitapAdi", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
                txtYazarAdi.DataBindings.Clear();
                txtYazarAdi.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "YazarAdi", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
                baglantı.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                baglantı.Close();
            }
        }

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 frmAyarlar = new Form2();
            frmAyarlar.Show();
        }

        private void cmdcikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnYazdır_Click(object sender, EventArgs e)
        {
            try
            {
                Word.Application WordApp = new Word.Application();
                Word.Document WordDoc = new Word.Document();

                DataSet ds = new DataSet();
                if (baglantı.State == ConnectionState.Closed) baglantı.Open();

                OleDbCommand komut = new OleDbCommand("Select YazarAdi,KitapAdi from kitaplar", baglantı);
                OleDbDataReader reader = komut.ExecuteReader();

                WordDoc = WordApp.Documents.Open(Application.StartupPath + "\\bos_kitaplar.docx");
                WordApp.Visible = true;
                int i = 2;
                while (reader.Read())
                {
                    WordDoc.Range().Tables[1].Cell(i, 1).Range.InsertAfter(reader["YazarAdi"].ToString());
                    WordDoc.Range().Tables[1].Cell(i, 2).Range.InsertAfter(reader["KitapAdi"].ToString());
                    WordDoc.Range().Tables[1].Rows.Add();
                    i++;
                }
                WordDoc.SaveAs(Application.StartupPath + "\\tum_kitaplar" + ".doc");
                WordDoc.Close();
                WordApp.Quit();

                WordDoc = null;
                WordApp = null;

                MessageBox.Show("Sorular WORD'a aktarıldı!");
                baglantı.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                baglantı.Close();
            }
        }






        

    }
}