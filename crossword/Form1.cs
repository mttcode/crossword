using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace _8smerovka
{
    public partial class Form1 : Form
    {
        int BOX_SIRKA = 32;
        int SIRKA = 32 + 5;
        string[,] pole = new string[100, 100];
        bool[,] bpole = new bool[100, 100];
        int[,] riesenia = new int[100, 4];
        int iriesenia;
        int pocx;
        int pocy;
        int aktx;
        int akty;
        int posx;
        int posy;
        bool oznac;
        bool edit = true;
        string pripustne_znaky;


        public Form1()
        {
            ProgDefault();

            InitializeComponent();
            openFileDialog1.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            saveFileDialog1.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            exampleToolStripMenuItem_Click(this, null);
        }


        private void ProgDefault()
        {
            int x, y;

            for (x = 0; x < 100; x++)
                for (y = 0; y < 100; y++)
                    bpole[x, y] = true;

            for (x = 0; x < 100; x++)
            {
                riesenia[x, 0] = 0;
                riesenia[x, 1] = 0;
                riesenia[x, 2] = 0;
                riesenia[x, 3] = 0;
            }

            pocx = 1; pocy = 1;
            aktx = 1; akty = 1;
            posx = 1; posy = 1;

            oznac = false;
            iriesenia = 0;

            try
            {
                using (StreamReader sr = new StreamReader("pripustne_znaky.txt"))
                {
                    if (sr != null)
                        pripustne_znaky = sr.ReadLine();
                }
            } 
            catch (Exception pom) 
            {
                pripustne_znaky = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNMéěÉĚŕřŔŘýÝúÚíÍóÓáÁšŠďĎĺĹžŽčČňŇ";
            }
        }


        private string DajSlovo(int x, int y, int smerx, int smery, int len)
        {
            string slovo="";

            if ((smerx ==  1) && ((x + len) > pocx)) return "";
            if ((smerx == -1) && ((x - len) < -1  )) return "";
            if ((smery ==  1) && ((y + len) > pocy)) return "";
            if ((smery == -1) && ((y - len) < -1  )) return "";

              //
              while((len>0) 
                  && (x < pocx) && (x >= 0)
                  && (y < pocy) && (y >= 0))
              {
                  slovo += pole[x,y];
                  len -= pole[x,y].Length;
                  x += smerx;
                  y += smery;
              }
              return slovo;
        }


        private bool NajdiOznacSlovo(string slovo)
        {
            int x, y, i;

            for(x=0; x < pocx; x++)
              for (y = 0; y < pocy; y++)
              {
                  if (slovo == DajSlovo(x, y, 1, 0, slovo.Length ))
                  {
                      for (i = 0; i < slovo.Length; i++)
                          bpole[x + i, y] = false;

                      riesenia[iriesenia, 0] = x;
                      riesenia[iriesenia, 1] = y;
                      riesenia[iriesenia, 2] = x + slovo.Length - 1;
                      riesenia[iriesenia, 3] = y;
                      iriesenia++;
                      return true;
                  }

                  if (slovo == DajSlovo(x, y, -1, 0, slovo.Length))
                  {
                      for (i = 0; i < slovo.Length; i++)
                          bpole[x - i, y] = false;

                      riesenia[iriesenia, 0] = x;
                      riesenia[iriesenia, 1] = y;
                      riesenia[iriesenia, 2] = x - slovo.Length+1;
                      riesenia[iriesenia, 3] = y;
                      iriesenia++;
                      return true;
                  }

                  if (slovo == DajSlovo(x, y, 0, 1, slovo.Length))
                  {
                      for (i = 0; i < slovo.Length; i++)
                          bpole[x, y + i] = false;

                      riesenia[iriesenia, 0] = x;
                      riesenia[iriesenia, 1] = y;
                      riesenia[iriesenia, 2] = x;
                      riesenia[iriesenia, 3] = y + slovo.Length-1;
                      iriesenia++;
                      return true;
                  }

                  if (slovo == DajSlovo(x, y, 0, -1, slovo.Length))
                  {
                      for (i = 0; i < slovo.Length; i++)
                          bpole[x, y - i] = false;

                      riesenia[iriesenia, 0] = x;
                      riesenia[iriesenia, 1] = y;
                      riesenia[iriesenia, 2] = x;
                      riesenia[iriesenia, 3] = y - slovo.Length+1;
                      iriesenia++;
                      return true;
                  }

                  if (slovo == DajSlovo(x, y, 1, 1, slovo.Length))
                  {
                      for (i = 0; i < slovo.Length; i++)
                          bpole[x + i, y + i] = false;

                      riesenia[iriesenia, 0] = x;
                      riesenia[iriesenia, 1] = y;
                      riesenia[iriesenia, 2] = x + slovo.Length-1;
                      riesenia[iriesenia, 3] = y + slovo.Length-1;
                      iriesenia++;
                      return true;
                  }

                  if (slovo == DajSlovo(x, y, -1, 1, slovo.Length))
                  {
                      for (i = 0; i < slovo.Length; i++)
                          bpole[x - i, y + i] = false;

                      riesenia[iriesenia, 0] = x;
                      riesenia[iriesenia, 1] = y;
                      riesenia[iriesenia, 2] = x - slovo.Length+1;
                      riesenia[iriesenia, 3] = y + slovo.Length-1;
                      iriesenia++;
                      return true;
                  }

                  if (slovo == DajSlovo(x, y, -1, -1, slovo.Length))
                  {
                      for (i = 0; i < slovo.Length; i++)
                          bpole[x - i, y - i] = false;

                      riesenia[iriesenia, 0] = x;
                      riesenia[iriesenia, 1] = y;
                      riesenia[iriesenia, 2] = x - slovo.Length+1;
                      riesenia[iriesenia, 3] = y - slovo.Length+1;
                      iriesenia++;
                      return true;
                  }

                  if (slovo == DajSlovo(x, y, 1, -1, slovo.Length))
                  {
                      for (i = 0; i < slovo.Length; i++)
                          bpole[x + i, y - i] = false;

                      riesenia[iriesenia, 0] = x;
                      riesenia[iriesenia, 1] = y;
                      riesenia[iriesenia, 2] = x + slovo.Length-1;
                      riesenia[iriesenia, 3] = y - slovo.Length+1;
                      iriesenia++;
                      return true;
                  }
              }
            return false;
        }


        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if(listBoxNevyriesene.SelectedItem == null)
            {
                if( listBoxNevyriesene.Items.Count>0)
                    listBoxNevyriesene.SelectedIndex = 0;
                if (listBoxNevyriesene.SelectedItem == null)
                    return;
            }

            string line = listBoxNevyriesene.SelectedItem.ToString();

            if (NajdiOznacSlovo(line))
            {
                listBoxNevyriesene.Items.Remove(listBoxNevyriesene.SelectedItem);
                listBoxVyriesene.Items.Add(line);
                ZatialRiesenie();
                pictureBox1.Refresh();
            }
        }


        private void listBoxNevyriesene_KeyPress(object sender, KeyPressEventArgs e)
        {
            if( e.KeyChar == 13)
                listBox1_DoubleClick(sender,null);
        }


        private void aktuálneSlovoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1_DoubleClick(sender, null);
            if (listBoxNevyriesene.Items.Count > 0)
                listBoxNevyriesene.SelectedIndex = 0;
        }


        private void vyriešVšetkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string line;
            int i;

            i = 0;
            while (i < listBoxNevyriesene.Items.Count)
            {
                line = listBoxNevyriesene.Items[i].ToString();

                if (NajdiOznacSlovo(line))
                {
                    listBoxNevyriesene.Items.Remove(line);
                    listBoxVyriesene.Items.Add(line);

                    ZatialRiesenie();
                    pictureBox1.Refresh();
                }
                else
                {
                    i++;
                }
            }
        }


        private void ZatialRiesenie()
        {
            string slovo="";

            for (int y = 0; y < pocy; y++)
                for (int x = 0; x < pocx; x++)
                    if( bpole[x,y])
                        slovo += pole[x, y];

            toolStripRiesenie.Text = slovo;
        }


        private void exampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] slova = {"KLADKOSTROJ", "KALANETIKA", "KARIKATURA", "KOLORATURA", "KATARAKTY", 
                "KORIANDER", "KARAVANA", "KASTIELE", "KINETIKA", "KLADIVKA", "KRALOVNA", "KURACINA", 
                "KVINTETO", "KLANICA", "KOMNATA", "KOSATKA", "KOVACNE", "KUCHARKY", "KABATY", 
                "KIMONO", "KLASIK", "KLOKAN", "KLUCIK", "KOMPAS", "KONINA", "KRASKA", "KVADRE", 
                "KVAPKY", "KACKA", "KARAS", "KLADA", "KOBKA", "KOROK", "KOTUL", "KROJE", "KUTAC", 
                "KVETY", "KYTKA"};

            string[,] pismena = {
                    {"A","J","E","A","Y","T","K","A","R","A","T","A","K","C","A","K"},
                    {"K","O","R","O","K","Z","A","O","R","Y","T","E","V","K","L","I"},
                    {"T","R","D","L","P","I","S","U","L","K","A","A","I","A","A","S"},
                    {"Y","T","A","B","A","K","T","A","S","O","K","O","N","I","N","A"},
                    {"K","S","V","S","V","A","I","E","T","R","R","I","T","M","V","L"},
                    {"R","O","K","E","K","R","E","M","N","I","C","A","E","D","O","K"},
                    {"A","K","V","I","D","A","L","K","O","A","K","O","T","U","L","K"},
                    {"CH","D","R","A","K","V","E","U","R","N","L","K","O","U","A","A"},
                    {"U","A","A","B","C","A","T","U","K","D","O","A","C","T","R","R"},
                    {"K","L","O","K","A","N","K","I","N","E","T","I","K","A","K","A"},
                    {"Y","K","L","A","D","A","E","J","O","R","K","O","M","P","A","S"}};

            ProgDefault();

            listBoxVyriesene.Items.Clear();

            for (int i = 0; i < slova.Count(); i++)
                listBoxNevyriesene.Items.Add(slova[i]);

            int x, y;
            pocx = 16;
            pocy = 11;
            for (x = 0; x < pocx; x++)
                for (y = 0; y < pocy; y++)
                    pole[x, y] = pismena[y, x];

            ZatialRiesenie();
            pictureBox1.Refresh();
        }


        private void KresliCiaru(PaintEventArgs e, int x1, int y1, int x2, int y2, int px, int py)
        {
            e.Graphics.DrawLine(new Pen(Color.Yellow),
                x1 * SIRKA + SIRKA / 2 - px, y1 * SIRKA + SIRKA / 2 - py,
                x2 * SIRKA + SIRKA / 2 - px, y2 * SIRKA + SIRKA / 2 - py);
        }


        private void KresliBox(PaintEventArgs e, int x, int y, int px, int py)
        {
            Color farba;
            e.Graphics.DrawRectangle(new Pen(Color.Black),
                x * SIRKA - px, y * SIRKA - py,
                BOX_SIRKA, BOX_SIRKA);


            if (bpole[x, y]) farba = Color.Black;
            else farba = Color.Yellow;


            e.Graphics.DrawString(System.Convert.ToString(pole[x, y]),
                new Font("Times New Roman", 16), new SolidBrush(farba),
                x * SIRKA + (BOX_SIRKA / 5) - px, y * SIRKA + (BOX_SIRKA / 5) - py);
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int px = hScrollBar1.Value;
            int py = vScrollBar1.Value;

            for (int y = 0; y < pocy; y++)
                for (int x = 0; x < pocx; x++)
                    KresliBox(e, x, y, px, py);

            for (int i = 0; i < iriesenia; i++)
                KresliCiaru(e, riesenia[i,0], riesenia[i,1],
                               riesenia[i,2], riesenia[i,3],
                               px, py);
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Form my_about = new AboutBox1();
            my_about.Show();
        }


        private void koniecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void načítajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // nacita subor
            if (openFileDialog1.ShowDialog() ==
                System.Windows.Forms.DialogResult.OK )
                try
                {
                    listBoxNevyriesene.Items.Clear();
                    listBoxVyriesene.Items.Clear();
                    NacitajSubor(openFileDialog1.FileName, openFileDialog1.SafeFileName);
                    pictureBox1.Refresh();
                    lúštenieToolStripMenuItem_Click(sender, e);
                }
                catch (Exception pom)
                {
                    MessageBox.Show("Nastala chyba pri načítaní súboru." + pom.ToString());
                }
        }


        private void SetScrollBar()
        {
            hScrollBar1.Maximum = pocx * SIRKA;
            vScrollBar1.Maximum = pocy * SIRKA;
        }


        private void NacitajSubor(string fname, string strip_fname)
        {
            toolStripStatusFileName.Text = strip_fname;
            int poc_riadkov;

            ProgDefault();

            using (StreamReader sr = new StreamReader(fname))
            {
                string line;
                int x, y;

                poc_riadkov = System.Convert.ToInt16(sr.ReadLine());
                while ((poc_riadkov-- > 0)
                 && ((line = sr.ReadLine()) != null))
                {
                    listBoxNevyriesene.Items.Add(line);
                }

                pocx = System.Convert.ToInt32(sr.ReadLine());
                pocy = System.Convert.ToInt32(sr.ReadLine());
                y = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    line = line.ToUpperInvariant();
                    for (x = 0; x < line.Length; x = x + 2)
                    {
                        pole[x / 2, y] = line.Substring(x, 1);

                        if (line[x + 1] != ' ')
                            pole[x / 2, y] = line.Substring(x, 2);
                    }
                    y++;
                }
                SetScrollBar();
            }
            ZatialRiesenie();
        }


        private void UlozSubor(string fname, string strip_fname)
        {
            toolStripStatusFileName.Text = strip_fname;
            int poc_riadkov = listBoxVyriesene.Items.Count
                            + listBoxNevyriesene.Items.Count;

            using (StreamWriter sw = new StreamWriter(fname))
            {
                sw.WriteLine(poc_riadkov);

                for(int i=0; i<listBoxVyriesene.Items.Count; i++)
                    sw.WriteLine(listBoxVyriesene.Items[i]);
                for(int i=0; i<listBoxNevyriesene.Items.Count; i++)
                    sw.WriteLine(listBoxNevyriesene.Items[i]);

                sw.WriteLine(pocx);
                sw.WriteLine(pocy);
                for(int y=0; y<pocy; y++)
                {
                    for(int x=0; x<pocx; x++)
                    {
                        sw.Write(pole[x,y]);
                        if(pole[x,y].Length == 1)
                            sw.Write(" ");
                    }
                    sw.WriteLine();
                }
            }
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            SetScrollBar();
        }


        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }


        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            aktx = System.Convert.ToInt32(e.X / SIRKA);
            akty = System.Convert.ToInt32(e.Y / SIRKA);

            if ((aktx < 0) || (aktx >= pocx)
            || (akty < 0) || (akty >= pocy)
            || !edit)
            {
                return;
            }

            textBox1.Left = pictureBox1.Left + aktx * SIRKA +2;
            textBox1.Top = pictureBox1.Top + akty * SIRKA +2;
            textBox1.Text = "";
            textBox1.Visible = true;
            textBox1.SelectAll();
            textBox1.Focus();
        }


        private void zrušRiešeniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string line;
            int oldpocx, oldpocy;
            int i;

            i = 0;
            while (i < listBoxVyriesene.Items.Count)
            {
                line = listBoxVyriesene.Items[i].ToString();
                listBoxVyriesene.Items.Remove(line);
                listBoxNevyriesene.Items.Add(line);
            }

            oldpocx = pocx;
            oldpocy = pocy;
            ProgDefault();
            pocx = oldpocx;
            pocy = oldpocy;

            ZatialRiesenie();
            pictureBox1.Refresh();
        }


        private void uložToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    UlozSubor(saveFileDialog1.FileName, saveFileDialog1.FileName);
                }
                catch (Exception pom)
                {
                    MessageBox.Show("Nastala chyba pri ukladani súboru." + pom.ToString());
                }
            }
        }


        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) {
                textBox1.Visible = false;
                pictureBox1.Focus();
                return;
            }

            char znak;

            try {
                znak = textBox1.Text[0];
            }
            catch (Exception exce) 
            { 
                znak = ' '; 
            }

            if (pripustne_znaky.IndexOf(znak) >= 0)
            {
                pole[aktx, akty] = znak.ToString().ToUpperInvariant();

                aktx++;
                if (aktx >= pocx)
                {
                    aktx = 0;
                    akty++;
                }
                if (akty >= pocy)
                {
                    aktx = 0;
                    akty = 0;
                }

                textBox1.Left = pictureBox1.Left + aktx * SIRKA + 2;
                textBox1.Top = pictureBox1.Top + akty * SIRKA + 2;
                textBox1.Text = "";
                textBox1.Visible = true;
                textBox1.SelectAll();
                textBox1.Focus();
            }
        }


        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            pictureBox1.Focus();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string slovo = "";

            for (int i = 0; i < textBoxAdd.Text.Length; i++)
            {
                if (pripustne_znaky.IndexOf(textBoxAdd.Text[i]) >= 0)
                    slovo += textBoxAdd.Text[i];
            }

            if (slovo != "")
            {
                listBoxNevyriesene.Items.Add(slovo.ToUpperInvariant());
                textBoxAdd.Text = "";
                textBoxAdd.Focus();
            }
        }

 
        private void listBoxNevyriesene_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)
                listBoxNevyriesene.Items.Remove(listBoxNevyriesene.SelectedItem);
        }


        private void nováSieťToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgDefault();
            pictureBox1.Visible = false;

            labelN1.Visible = true;
            labelN2.Visible = true;
            labelN3.Visible = true;
            labelN4.Visible = true;
            textBoxPocx.Visible = true;
            textBoxPocy.Visible = true;
            button2.Visible = true;
            editáciaToolStripMenuItem_Click(sender, e);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            pocx = System.Convert.ToInt16(textBoxPocx.Text);
            pocy = System.Convert.ToInt16(textBoxPocy.Text);

            labelN1.Visible = false;
            labelN2.Visible = false;
            labelN3.Visible = false;
            labelN4.Visible = false;
            textBoxPocx.Visible = false;
            textBoxPocy.Visible = false;
            button2.Visible = false;

            for (int y = 0; y < pocy; y++)
                for (int x = 0; x < pocx; x++)
                    pole[x, y] = "A";

            pictureBox1.Visible = true;
            listBoxNevyriesene.Items.Clear();
            listBoxVyriesene.Items.Clear();
        }


        private void DrawOld()
        {
            ControlPaint.DrawReversibleLine(
                PointToScreen(new Point(aktx + pictureBox1.Left, akty + pictureBox1.Top)),
                PointToScreen(new Point(posx + pictureBox1.Left, posy + pictureBox1.Top)),
                Color.Coral);
        }


        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!oznac)
            {
                return;
            }

            oznac = false;

            int x1 = (aktx + hScrollBar1.Value) / SIRKA;
            int y1 = (akty + vScrollBar1.Value) / SIRKA;
            int x2 = (posx + hScrollBar1.Value) / SIRKA;
            int y2 = (posy + vScrollBar1.Value) / SIRKA;

            if ((x1 < 0) || (x1 >= pocx)
             || (y1 < 0) || (y1 >= pocy)
             || (x2 < 0) || (x2 >= pocx)
             || (y2 < 0) || (y2 >= pocy))
            {
                DrawOld();

                return;
            }

            if ((System.Math.Abs(x2 - x1) != System.Math.Abs(y2 - y1))
             && (x1 != x2) && (y2 != y1))
            {
                DrawOld();

                return;
            }

            int krokx = x2 - x1;
            int kroky = y2 - y1;
            string slovo = "";

            if (krokx != 0) krokx /= System.Math.Abs(krokx);
            if (kroky != 0) kroky /= System.Math.Abs(kroky);
            slovo = pole[x1, y1];

            while (((x1 != x2) || (y1 != y2))
                && (x1 >= 0) && (x1 <= pocx)
                && (y1 >= 0) && (y1 <= pocy))
            {
                x1 += krokx;
                y1 += kroky;
                slovo += pole[x1, y1];
            }

            
            if( listBoxNevyriesene.Items.IndexOf(slovo)>=0)
            if (NajdiOznacSlovo(slovo)) 
            {
                listBoxNevyriesene.Items.Remove(slovo);
                listBoxVyriesene.Items.Add(slovo);
                ZatialRiesenie();
            }
            pictureBox1.Refresh();
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            aktx = System.Convert.ToInt32(e.X);
            akty = System.Convert.ToInt32(e.Y);
            posx = System.Convert.ToInt32(e.X);
            posy = System.Convert.ToInt32(e.Y);

            if ((((aktx + hScrollBar1.Value) / SIRKA) < pocx)
            && (((akty + vScrollBar1.Value) / SIRKA) < pocy))
            {
                oznac = true;
            }
        }


        private void KresliStvorce(int xx1, int yy1, int xx2, int yy2, bool zlto)
        {
            int x1 = (xx1 + hScrollBar1.Value)/SIRKA;
            int y1 = (yy1 + vScrollBar1.Value)/SIRKA;
            int x2 = (xx2 + hScrollBar1.Value)/SIRKA;
            int y2 = (yy2 + vScrollBar1.Value)/SIRKA;

            if ((x1 < 0) || (x1 > pocx)
             || (y1 < 0) || (y1 > pocy)
             || (x2 < 0) || (x2 > pocx)
             || (y2 < 0) || (y2 > pocy))
            {
                return;
            }

            if ((System.Math.Abs(x2 - x1) != System.Math.Abs(y2 - y1))
             && (x1 != x2) && (y2 != y1))
            {
                return;
            }
             
            int krokx = x2 - x1;
            int kroky = y2 - y1;

            if (krokx!=0) krokx /= System.Math.Abs(krokx);
            if (kroky!=0) kroky /= System.Math.Abs(kroky);
            System.Drawing.Graphics e = pictureBox1.CreateGraphics();

            e.DrawString(System.Convert.ToString(pole[x1, y1]),
                new Font("Times New Roman", 16),
                new SolidBrush((zlto || !bpole[x1, y1]) ? Color.Yellow : Color.Black),
                x1 * SIRKA + (BOX_SIRKA / 5) - hScrollBar1.Value,
                y1 * SIRKA + (BOX_SIRKA / 5) - vScrollBar1.Value);

            while(((x1 != x2) || (y1 != y2))
                && (x1>=0) && (x1<=pocx)
                && (y1>=0) && (y1<=pocy))
            {
                x1 += krokx;
                y1 += kroky;

                e.DrawString(System.Convert.ToString(pole[x1, y1]),
                    new Font("Times New Roman", 16),
                    new SolidBrush((zlto || !bpole[x1, y1])? Color.Yellow: Color.Black),
                    x1 * SIRKA + (BOX_SIRKA / 5) - hScrollBar1.Value, 
                    y1 * SIRKA + (BOX_SIRKA / 5) - vScrollBar1.Value);
            }
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!oznac)
            {
                return;
            }

            ControlPaint.DrawReversibleLine(
                PointToScreen(new Point(aktx + pictureBox1.Left, akty + pictureBox1.Top)),
                PointToScreen(new Point(posx + pictureBox1.Left, posy + pictureBox1.Top)),
                Color.Coral);

            KresliStvorce(aktx, akty, posx, posy, false);

            posx = System.Convert.ToInt32(e.X);
            posy = System.Convert.ToInt32(e.Y);

            ControlPaint.DrawReversibleLine(
                PointToScreen(new Point(aktx + pictureBox1.Left, akty + pictureBox1.Top)),
                PointToScreen(new Point(posx + pictureBox1.Left, posy + pictureBox1.Top)),
                Color.Coral);

            KresliStvorce(aktx, akty, posx, posy, true);
        }


        private void editáciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edit = true;
            editáciaToolStripMenuItem.Checked = edit;
            lúštenieToolStripMenuItem.Checked = !edit;
            textBoxAdd.Enabled = edit;
            textBoxAdd.Visible = edit;
            button1.Enabled = edit;
            button1.Visible = edit;
            toolStripMod.Text = "Editovací mód";
        }


        private void lúštenieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edit = false;
            editáciaToolStripMenuItem.Checked = edit;
            lúštenieToolStripMenuItem.Checked = !edit;
            textBoxAdd.Enabled = edit;
            textBoxAdd.Visible = edit;
            button1.Enabled = edit;
            button1.Visible = edit;
            toolStripMod.Text = "Hrací mód";
        }
    }
}
