/************************
 * 
 * @author KSH © Sayawan.com
 * @date  12.28.2014
 * 
 ************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048_hoca
{
    public partial class Form1 : Form
    {

        private volatile bool toBreak = false;
        private volatile bool toContinue = false;
        private volatile int boardSize = 4;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ekrana_kutu_koy(4);

            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                Subscribe(item, gameTypeToolStripMenuItem_Click);
            }
        }

        private static void Subscribe(ToolStripMenuItem item, EventHandler eventHandler)
        {
            // If leaf, add click handler
            if (item.DropDownItems.Count == 0) 
                item.Click += eventHandler;

            // Otherwise recursively subscribe
            else foreach (ToolStripMenuItem subItem in item.DropDownItems)
                    Subscribe(subItem, eventHandler);
        }

        // See more http://stackoverflow.com/a/5696497
        private void gameTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tiklama = (sender as ToolStripMenuItem).Text;
            Console.WriteLine(tiklama);

            if (tiklama != "Start")
            {
                boardSize = int.Parse(tiklama.Substring(0, 1));
                ekrana_kutu_koy(boardSize);
            }
        }

        void ekrana_kutu_koy(int n)
        {
            int kutusay = 1;

            for (int i = this.Controls.Count - 1; i >= 0; i--)
                if (this.Controls[i] is TextBox)
                    this.Controls.Remove(this.Controls[i]);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    TextBox tb = new TextBox();
                    tb.Name = "txt_kutu" + kutusay;
                    tb.Multiline = true;
                    tb.Height = 50;
                    tb.Width = 50;
                    //tb.MaxLength = 1;
                    //tb.Text = kutusay.ToString();
                    tb.TextAlign = HorizontalAlignment.Center;
                    tb.Font = new System.Drawing.Font(tb.Font, FontStyle.Bold);
                    tb.Font = new System.Drawing.Font(tb.Font.FontFamily, 16);
                    Point yer;
                    yer = new System.Drawing.Point(j * tb.Height + 30, i * tb.Width + 30);
                    tb.Location = yer;
                    this.Controls.Add(tb);
                    kutusay++;
                }
            }
            this.Width = 50 * n + 75;
            this.Height = 50 * n + 90;

            ilk_rand_sayi();
        }

        //void oyuna_basla(int n)
        //{
            //Random random = new Random();
            //while(true) {
            //    int sayi1 = random.Next(1, n*n + 1);
            //    int sayi2 = random.Next(1, n*n + 1);

            //    if (sayi1 != sayi2) {
            //        this.Controls["txt_kutu"+sayi1].Text = "2";
            //        this.Controls["txt_kutu"+sayi2].Text = "2";
            //        break;
            //    }
            //}
            //this.Controls["txt_kutu6"].Text = "2";
            //this.Controls["txt_kutu11"].Text = "2";
        //}

        void rand_sayi()
        {
            List<int> list = new List<int>();
            Random r = new Random();

            //for (int i = this.Controls.Count - 1; i >= 0; i--)
            //{
            //    if (this.Controls[i] is TextBox && this.Controls[i].Text == "")
            //    {
            //        string ad = this.Controls[i].Name;
            //        string adx = ad.Substring(8, ad.Length-8);
            //        int sayi = int.Parse(adx);
            //        list.Add(sayi);
            //    }
            //}

            for (int i = 1; i <= boardSize * boardSize; i++)
            {
                if (this.Controls["txt_kutu" + i].Text == "")
                {
                    list.Add(i);
                }
            }

                if (list.Count > 0)
                {
                    int kutu1 = r.Next(0, list.Count);
                    int item1 = list[kutu1];
                    list.RemoveAt(kutu1);

                    this.Controls["txt_kutu" + item1].Text = (r.NextDouble() < 0.9 ? 2 : 4).ToString();
                    this.Controls["txt_kutu" + item1].BackColor = Color.LightGray;
                }
                else if (list.Count == 0)
                {
                    oyun_bittimi();
                }
        }

        void oyun_bittimi()
        {
            bool satir = false;
            bool sutun = false;

            int satir_basi = 1;
            int sutun_basi = 1;
           
            for (int i = 1; i <= boardSize; i++)
            {
                for (int j = satir_basi; j < i * boardSize; j++)
                {
                    if (this.Controls["txt_kutu" + j].Text == this.Controls["txt_kutu" + (j+1)].Text)
                    {
                        //MessageBox.Show((j) + " " + (j+1));
                        satir = true;
                        break;
                    }
                }
                satir_basi = satir_basi + boardSize;
            }

            for (int i = 1; i <= boardSize; i++)
            {
                for (int j = sutun_basi; j <= boardSize*boardSize; j = j + boardSize)
                {
                    if (j + boardSize > boardSize * boardSize) continue;
  
                    if (this.Controls["txt_kutu" + j].Text == this.Controls["txt_kutu" + (j + boardSize)].Text)
                    {
                        //MessageBox.Show((j) + " " + (j + boardSize));
                        sutun = true;
                        break;
                    }
                }
                sutun_basi = sutun_basi + 1;
            }

            if (satir || sutun)
            {
                //MessageBox.Show("daha devam et" + satir + " " + sutun);
            }
            else
            {
                MessageBox.Show("OYUN BITTI");
            }
        }

        void ilk_rand_sayi()
        {
            Random random = new Random();

            while (true)
            {
                int sayi1 = random.Next(1, boardSize * boardSize + 1);
                int sayi2 = random.Next(1, boardSize * boardSize + 1);

                if (sayi1 != sayi2)
                {
                    TextBox birinci = this.Controls["txt_kutu" + sayi1] as TextBox;
                    TextBox ikinci = this.Controls["txt_kutu" + sayi2] as TextBox;

                    birinci.Text = "2";
                    birinci.BackColor = Color.Aqua;

                    ikinci.Text = "2";
                    ikinci.BackColor = Color.Aquamarine;

                    break;
                }
            }
        }

        void renklendir()
        {
            for (int i = 1; i <= boardSize*boardSize; i++)
            {
                TextBox kutu = this.Controls["txt_kutu" + i] as TextBox;
                int sayi = 0;

                if (kutu.Text != "")
                {
                    sayi = int.Parse(kutu.Text);
                }
                else
                {
                    kutu.BackColor = Color.White;
                    kutu.ForeColor = Color.Black;
                    continue;
                }

                switch (sayi)
                {
                    case 2:
                        {
                            kutu.BackColor = Color.GhostWhite;
                            kutu.ForeColor = Color.Black;
                            break;
                        }
                    case 4:
                        {
                            kutu.BackColor = Color.PapayaWhip;
                            kutu.ForeColor = Color.Black;
                            break;
                        }
                    case 8:
                        {
                            kutu.BackColor = Color.LightSalmon;
                            kutu.ForeColor = Color.White;
                            break;
                        }
                    case 16:
                        {
                            kutu.BackColor = Color.Coral;
                            kutu.ForeColor = Color.White;
                            break;
                        }
                    case 32:
                        {
                            kutu.BackColor = Color.Tomato;
                            kutu.ForeColor = Color.White;
                            break;
                        }
                    case 64:
                        {
                            kutu.BackColor = Color.Red;
                            kutu.ForeColor = Color.White;
                            break;
                        }
                    case 128:
                        {
                            kutu.BackColor = Color.Gold;
                            kutu.ForeColor = Color.Black;
                            break;
                        }
                    case 256:
                        {
                            kutu.BackColor = Color.Goldenrod;
                            kutu.ForeColor = Color.Black;
                            break;
                        }
                    default:
                        {
                            kutu.BackColor = Color.White;
                            kutu.ForeColor = Color.Black;
                            break;
                        }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                Console.WriteLine("****** UP KEY ******");
                yukari_tus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                Console.WriteLine("****** DOWN KEY ******");
                asagi_tus();
            }
            else if (e.KeyCode == Keys.Right)
            {
                Console.WriteLine("****** RIGHT KEY ******");
                sag_tus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                Console.WriteLine("****** LEFT KEY ******");
                sol_tus();
            }

            renklendir();
            rand_sayi();
        }

        void asagi_tus()
        {
            int basla = boardSize * boardSize - boardSize + 1;

            for (int i = 1; i <= boardSize; i++)
            {
                for (int j = basla; j >= i; j = j - boardSize)
                {
                    toBreak = false;
                    toContinue = false;

                    for (int k = j - boardSize; k >= i; k = k - boardSize)
                    {
                        if (j <= k) { continue; }

                        string islem = "DOWN";
                        tekrarlanan_kisim(islem, j, k);

                        if (toBreak) break;

                        if (toContinue) continue;
                    }
                }
                basla = basla + 1;
            }
        }

        void yukari_tus()
        {
            int basla = 1;

            for (int i = 1; i <= boardSize; i++)
            {
                for (int j = basla; j <= basla + (boardSize * (boardSize - 1)); j = j + boardSize)
                {
                    toBreak = false;
                    toContinue = false;

                    for (int k = j + boardSize; k <= basla + (boardSize * boardSize - 1); k = k + boardSize)
                    {
                        if (j >= k) { continue; }

                        string islem = "UP";
                        tekrarlanan_kisim(islem, j, k);

                        if (toBreak) break;

                        if (toContinue) continue;

                    }
                }
                basla = basla + 1;
            }
        }

        void sol_tus()
        {
            int basla = 1;

            for (int i = 1; i <= boardSize; i++)
            {
                for (int j = basla; j <= basla + (boardSize - 1); j++)
                {
                    toBreak = false;
                    toContinue = false;

                    for (int k = j + 1; k <= basla + (boardSize - 1); k++)
                    {
                        Console.WriteLine("deneme " + j + " " + k);
                        if (j >= k) { continue; }

                        string islem = "LEFT";
                        tekrarlanan_kisim(islem, j, k);

                        if (toBreak) break;

                        if (toContinue) continue;

                    }
                }
                basla = basla + boardSize;
            }
        }

        void sag_tus()
        {
            int basla = 1;

            for (int i = 1; i <= boardSize; i++)
            {
                for (int j = basla + (boardSize - 1); j >= basla; j--)
                {
                    toBreak = false;
                    toContinue = false;

                    for (int k = j - 1; k >= basla; k--)
                    {
                        string islem = "RIGHT";
                        tekrarlanan_kisim(islem, j, k);

                        if (toBreak) break;

                        if (toContinue) continue;

                    }
                }
                basla = basla + boardSize;
            }
        }

        // TODO: clean up here
        // [islem] just for loggin
        // [toContinue, toBreak] for breaking loops above
        // See more http://stackoverflow.com/a/14226843
        void tekrarlanan_kisim(string islem, int j, int k)
        {
            //Console.WriteLine("[" + islem + "] - " + j + " " + k);
            TextBox birinci = this.Controls["txt_kutu" + j] as TextBox;
            TextBox ikinci = this.Controls["txt_kutu" + k] as TextBox;

            if (birinci.Text == "" && ikinci.Text != "")
            {
                //Console.WriteLine("[BOS] - " + j + " " + k);
                birinci.Text = ikinci.Text;
                ikinci.Text = "";
                //toContinue = true;
            }

            if (birinci.Text != "" && ikinci.Text != "" && birinci.Text != ikinci.Text)
            {
                //Console.WriteLine("[BREAK] - " + j + " " + k);
                toBreak = true;
            }

            if (birinci.Text != "" && ikinci.Text != "" && birinci.Text == ikinci.Text)
            {
                //Console.WriteLine("[TOPLA] - " + j + " " + k);
                int sum = Int32.Parse(birinci.Text) + Int32.Parse(ikinci.Text);
                birinci.Text = sum.ToString();
                ikinci.Text = "";
                toBreak = true;
            }
        }

    }
}

//for (int i = 1; i <= 4; i++)
//{
//    for (int j = basla; j < basla+3; j++)
//    {
//        if (j + 1 > basla + 4) break;
//        TextBox birinci = this.Controls["txt_kutu" + j] as TextBox;
//        TextBox ikinci = this.Controls["txt_kutu" + (j + 1)] as TextBox;

//        if (birinci.Text != "" && ikinci.Text == "")
//        {
//            ikinci.Text = birinci.Text;
//            birinci.Text = "";
//            Console.WriteLine("bosluk" + j);
//            //break;
//        }
//        else if (birinci.Text != "" && ikinci.Text != "" && birinci.Text == ikinci.Text)
//        {
//            int sum = int.Parse(birinci.Text) + int.Parse(ikinci.Text);
//            ikinci.Text = sum.ToString();
//            birinci.Text = "";
//            Console.WriteLine("arti" + j);
//            break;
//        }
//        else
//        {
//            Console.WriteLine("else" + j);
//        }

//    }
//    basla = basla + 4;
//}
