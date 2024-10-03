using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace sql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-DRM8A3J;Initial Catalog=Personel;Integrated Security=True;Encrypt=False");

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from calisanlar", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                // INSERT INTO komutu doğru yazıldı
                SqlCommand kayitEkle = new SqlCommand(
                    "INSERT INTO calisanlar (Ad, Soyad, Meslek, Yas, Bekaret, maas) VALUES (@p1, @p2, @p3, @p4, @p5, @p6)" +
                    " select * from calisanlar", baglanti);
                MessageBox.Show("Kayıt başarıyla Eklendi!");


                // Parametreler ayarlandı
                kayitEkle.Parameters.AddWithValue("@p1", textBox1.Text);
                kayitEkle.Parameters.AddWithValue("@p2", textBox2.Text);
                kayitEkle.Parameters.AddWithValue("@p3", textBox3.Text);
                kayitEkle.Parameters.AddWithValue("@p4", Convert.ToInt32(textBox4.Text));
                kayitEkle.Parameters.AddWithValue("@p5", checkBox1.Checked);
                kayitEkle.Parameters.AddWithValue("@p6", Convert.ToDecimal(textBox6.Text));



                // Komut çalıştırılıyor
                kayitEkle.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                // Delete 
                SqlCommand kayitEkle = new SqlCommand(
                    "Delete from calisanlar where Ad=@p1 AND Soyad=@p2 AND Meslek=@p3 AND yas=@p4 AND bekaret=@p5 And maas=@p6", baglanti);
                MessageBox.Show("Kayıt başarıyla Silindi!");

                // Parametreler ayarlandı
                kayitEkle.Parameters.AddWithValue("@p1", textBox1.Text);
                kayitEkle.Parameters.AddWithValue("@p2", textBox2.Text);
                kayitEkle.Parameters.AddWithValue("@p3", textBox3.Text);
                kayitEkle.Parameters.AddWithValue("@p4", Convert.ToInt32(textBox4.Text));
                kayitEkle.Parameters.AddWithValue("@p5", checkBox1.Checked);
                kayitEkle.Parameters.AddWithValue("@p6", Convert.ToDecimal(textBox6.Text));

                kayitEkle.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sacilen = dataGridView1.SelectedCells[0].RowIndex;
            string ad = dataGridView1.Rows[sacilen].Cells[1].Value.ToString();
            string Soyad = dataGridView1.Rows[sacilen].Cells[2].Value.ToString();
            string Meslek = dataGridView1.Rows[sacilen].Cells[3].Value.ToString();
            string yas = dataGridView1.Rows[sacilen].Cells[4].Value.ToString();
            int bekaret = Convert.ToInt32(dataGridView1.Rows[sacilen].Cells[5].Value);
            string maas = dataGridView1.Rows[sacilen].Cells[6].Value.ToString();

            textBox1.Text = ad;
            textBox2.Text = Soyad;
            textBox3.Text = Meslek;
            textBox4.Text = yas;
            if (bekaret == 1)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }

            textBox6.Text = maas;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                int sacilen = dataGridView1.SelectedCells[0].RowIndex;
                int cid = Convert.ToInt32(dataGridView1.Rows[sacilen].Cells[0].Value);
                // update
                SqlCommand kayitguncelle = new SqlCommand(
                    "update calisanlar set Ad=@p1 ,Soyad=@p2 , Meslek=@p3 ,yas=@p4 ,bekaret=@p5 ,maas=@p6 where Id=@cid", baglanti);
                MessageBox.Show("Kayıt başarıyla güncellendi!");

                // Parametreler ayarlandı
                kayitguncelle.Parameters.AddWithValue("@cid", cid);
                kayitguncelle.Parameters.AddWithValue("@p1", textBox1.Text);
                kayitguncelle.Parameters.AddWithValue("@p2", textBox2.Text);
                kayitguncelle.Parameters.AddWithValue("@p3", textBox3.Text);
                kayitguncelle.Parameters.AddWithValue("@p4", Convert.ToInt32(textBox4.Text));
                kayitguncelle.Parameters.AddWithValue("@p5", checkBox1.Checked);
                kayitguncelle.Parameters.AddWithValue("@p6", Convert.ToDecimal(textBox6.Text));

                kayitguncelle.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlDataAdapter arama = new SqlDataAdapter("select * from calisanlar where ad like @ara",baglanti);
            arama.SelectCommand.Parameters.AddWithValue("@ara","%"+textBox5.Text+"%");
            DataSet ds = new DataSet();
            arama.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
