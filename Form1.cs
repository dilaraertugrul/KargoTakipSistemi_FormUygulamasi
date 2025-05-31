using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KargoTakipForm
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbTur.Items.Add("Yurtiçi");
            cmbTur.Items.Add("Yurtdışı");

            cmbDurum.Items.Add("Yolda");
            cmbDurum.Items.Add("TeslimEdildi");

            lvGonderiler.Columns.Add("Takip No", 100);
            lvGonderiler.Columns.Add("Gönderen", 120);
            lvGonderiler.Columns.Add("Alıcı", 120);
            lvGonderiler.Columns.Add("Durum", 100);
            lvGonderiler.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize); // tüm sütunlar içeriğe göre otomatik genişler


        }
        List<Gonderi> gonderiler = new List<Gonderi>();

        private void button1_Click(object sender, EventArgs e)
        {
            string takipNo = txtTakipNo.Text;
            string gonderen = txtGonderen.Text;
            string alici = txtAlici.Text;
            string tur = cmbTur.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(takipNo) || string.IsNullOrEmpty(gonderen) ||
                string.IsNullOrEmpty(alici) || string.IsNullOrEmpty(tur))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            Gonderi gonderi;

            if (tur == "Yurtiçi")
                gonderi = new YurticiGonderi(takipNo, gonderen, alici);
            else
                gonderi = new YurtdisiGonderi(takipNo, gonderen, alici);

            gonderiler.Add(gonderi);
            ListViewItem item = new ListViewItem(gonderi.TakipNo);
            item.SubItems.Add(gonderi.Gonderen);
            item.SubItems.Add(gonderi.Alici);
            item.SubItems.Add(gonderi.Durum.ToString());

            lvGonderiler.Items.Add(item);

            // Temizle
            txtTakipNo.Clear();
            txtGonderen.Clear();
            txtAlici.Clear();
            cmbTur.SelectedIndex = -1;
        }

        private void btnDurumGuncelle_Click(object sender, EventArgs e)
        {

            if (lvGonderiler.SelectedItems.Count == 0)
            {
                MessageBox.Show("Lütfen bir gönderi seçin.");
                return;
            }

            if (cmbDurum.SelectedItem == null)
            {
                MessageBox.Show("Lütfen yeni bir durum seçin.");
                return;
            }

            // Seçilen gönderi
            int index = lvGonderiler.SelectedItems[0].Index;
            Gonderi gonderi = gonderiler[index];

            // Durumu güncelle
            string secilenDurum = cmbDurum.SelectedItem.ToString();
            if (secilenDurum == "Yolda")
                gonderi.DurumGuncelle(Durum.Yolda);
            else if (secilenDurum == "TeslimEdildi")
                gonderi.DurumGuncelle(Durum.TeslimEdildi);

            // ListView satırını güncelle
            lvGonderiler.Items[index].SubItems[3].Text = gonderi.Durum.ToString();
        }
    }
}


