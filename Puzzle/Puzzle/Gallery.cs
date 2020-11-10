using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class Gallery : Form
    {
        public Gallery()
        {
            InitializeComponent();
        }

        private void Gallery_Load(object sender, EventArgs e)
        {
     
            for (int i = 0; i < 15; i++)
            {
                imageList1.Images.Add(Image.FromFile(@"C:\\\Users\\Alsu\\Desktop\\7semester\Puzzle\Puzzle\Puzzle\gallery\gallery2.jpg"));
            }
            listView1.LargeImageList = imageList1;
            imageList1.Images.Add(Image.FromFile(@"C:\\\Users\\Alsu\\Desktop\\7semester\Puzzle\Puzzle\Puzzle\gallery\gallery1.jpg"));
            for (int j = 0; j < imageList1.Images.Count; j++)
            {
                ListViewItem item = new ListViewItem();
            //    item.Text = name[j];
                item.ImageIndex = j;
                listView1.Items.Add(item);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файлы изображений | *.jpg";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            Image image;
            try
            {
                image = Image.FromFile(dialog.FileName);
            }
            catch (OutOfMemoryException ex)
            {
                MessageBox.Show("Ошибка чтения картинки");
                return;
            }
            //добавить запрос в БД?
            //перенести в другой метод
            imageList1.Images.Add(image);
            ListViewItem item = new ListViewItem();
            //    item.Text = name[j];
            item.ImageIndex =imageList1.Images.Count-1;
            listView1.Items.Add(item);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
           // listView1.Items.RemoveAt(listView1.SelectedIndex);
        }

        private void buttonItem_click(object sender, EventArgs e)
        {
            listView1.SelectedItems.Clear();
        }
    }
}
