using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Drawing;

namespace CoupangImage {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            MinimumSize = new Size(400, 600);
            Form1_Resize(this, null);
            textBox_TextChanged(this, null);
        }

        private void button1_Click(object sender, EventArgs e) {
            var allowedExtensions = new[] { ".png", ".jpg", ".gif" };
            var allFiles = Directory
                .GetFiles(rootPath.Text, "*.*", SearchOption.AllDirectories)
                .Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
                .ToList();

            // Grab and listing
            List<Item> itemsF1 = new List<Item>();
            List<Item> itemsF2 = new List<Item>();
            List<Item> itemsOther = new List<Item>();
            foreach (string file in allFiles) {
                if (Path.GetFileName(file).Contains(f1Box.Text)) {
                    itemsF1.Add(new Item {
                        fileName = Path.GetFileName(file),
                        path = file.Replace(Path.GetFullPath(Path.Combine(rootPath.Text, @"..\")), "")
                    }); 
                } else if (Path.GetFileName(file).Contains(F2Box.Text)) {
                    itemsF2.Add(new Item {
                        fileName = Path.GetFileName(file),
                        path = file.Replace(Path.GetFullPath(Path.Combine(rootPath.Text, @"..\")), "")
                    }); 
                } else {
                    itemsOther.Add(new Item {
                        fileName = Path.GetFileName(file),
                        path = file.Replace(Path.GetFullPath(Path.Combine(rootPath.Text, @"..\")), "")
                    }); 
                }
            }

            // DataTable
            DataTable f1Table = new DataTable();
            DataTable f2Table = new DataTable();
            DataTable othersTable = new DataTable();
            f1Table.Columns.Add("File Name", typeof(string));
            f1Table.Columns.Add("Path", typeof(string));

            f2Table.Columns.Add("File Name", typeof(string));
            f2Table.Columns.Add("Path", typeof(string));

            othersTable.Columns.Add("File Name", typeof(string));
            othersTable.Columns.Add("Path", typeof(string));

            foreach (Item item in itemsF1)
                f1Table.Rows.Add(item.fileName, item.path);
            dataGridView1.DataSource = f1Table;

            foreach (Item item in itemsF2)
                f2Table.Rows.Add(item.fileName, item.path);
            dataGridView2.DataSource = f2Table;

            foreach (Item item in itemsOther)
                othersTable.Rows.Add(item.fileName, item.path);
            dataGridView3.DataSource = othersTable;


            // Output
            string f1Output = "", f2Output = "", othersOutput = "";
            foreach (Item item in itemsF1)
                f1Output += item.path + ", ";

            if (f1Output.Length > 2)
                f1RTB.Text = f1Output.Substring(0, f1Output.Length - 2);

            foreach (Item item in itemsF2)
                f2Output += item.path + ", ";

            if (f2Output.Length > 2)
                f2RTB.Text = f2Output.Substring(0, f2Output.Length - 2);

            foreach (Item item in itemsOther)
                othersOutput += item.path + ", ";

            if (othersOutput.Length > 2)
                othersRTB.Text = othersOutput.Substring(0, othersOutput.Length - 2);
        }

        private void Form1_Resize(object sender, EventArgs e) {
            rootPath.Width = (int)(Width * 0.8);
            tabControl1.Height = (int)(Height * 0.3);
            tableLayoutPanel2.Height = Height - panel1.Height - tabControl1.Height - tableLayoutPanel1.Height - 50;
        }

        private void textBox_TextChanged(object sender, EventArgs e) {
            f1Label.Text = f1Box.Text;
            f2Label.Text = F2Box.Text;
        }

        public class Item {
            public string fileName { get; set; }
            public string path { get; set; }
        }
    }
}
