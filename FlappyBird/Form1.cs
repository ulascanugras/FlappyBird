using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        int yerCekimi = 5; // kuş için yer çekimi
        int hiz = 10;
        int score = 0;
        Random rnd = new Random();
        int gelenSecim;
        public Form1(int secim)
        {
            InitializeComponent();
            gelenSecim = secim;
            if (gelenSecim==0)
            {
                tmrGame.Interval = 100;
            }
            else if (gelenSecim == 1)
            {
                tmrGame.Interval = 50;
            }
            else if (gelenSecim == 2)
            {
                tmrGame.Interval = 20;
            }
 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Space && tmrGame.Enabled)
            {
                if (pbBird.Top >0)
                {
                pbBird.Top -= yerCekimi*10;
                    pbBird.Top = pbBird.Top < 0 ? 0 : pbBird.Top; // Ekrandan taşmaması için 0 noktasını geçirmedik.
                }
            }
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            pbBird.Top += yerCekimi;
            pbPipe1.Left -= hiz;
            pbPipe2.Left -= hiz;
            pbPipe3.Left -= hiz;
            pbPipe4.Left -= hiz;
            scoreText.Text = score.ToString();
            if (pbPipe1.Left < 0 && pbPipe2.Left < 0)
            {
                pbPipe1.Left = 1000;
                pbPipe2.Left = 1000;
                score++;
            }
            else if (pbPipe3.Left < 0 && pbPipe4.Left < 0)
            {
                pbPipe3.Left = 1000;
                pbPipe4.Left = 1000;
                score++;

            }


            if (pbPipe1.Right <=0)
            {
                pbPipe1.Left = ClientSize.Width + rnd.Next(200);  // Ekranın dışında rasgele bir yerde konumunu güncelledim.
            }
            if (pbPipe2.Right <= 0)
            {
                pbPipe2.Left = ClientSize.Width + rnd.Next(200); // Ekranın dışında rasgele bir yerde konumunu güncelledim. 
            }
            if (pbPipe3.Right <= 0)
            {
                pbPipe3.Left = pbPipe1.Left+ pbPipe1.Width +rnd.Next(20,200); // Ekranın dışında rasgele bir yerde konumunu güncelledim.
                
            }
            if (pbPipe4.Right <= 0)
            {
                pbPipe4.Left =  pbPipe2.Left + pbPipe2.Width+ rnd.Next(20,200);  // Ekranın dışında rasgele bir yerde konumunu güncelledim.
            }
            if (pbPipe1.Bounds.IntersectsWith(pbBird.Bounds) || pbPipe2.Bounds.IntersectsWith(pbBird.Bounds)|| pbPipe3.Bounds.IntersectsWith(pbBird.Bounds)|| pbPipe4.Bounds.IntersectsWith(pbBird.Bounds) || pbGround.Bounds.IntersectsWith(pbBird.Bounds))
                //   4 boru ve zemin için 
            {
                tmrGame.Stop();
                score = 0;
                DialogResult dr = MessageBox.Show("Game Over! Do you want to play again?", "Flappy Bird", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr==DialogResult.Yes)
                {
                    tmrGame.Start();
                    pbBird.Left = 0;
                    pbPipe1.Left = ClientSize.Width;
                    pbPipe2.Left = ClientSize.Width;
                    pbPipe3.Left = pbPipe1.Left + pbPipe1.Width+ rnd.Next(200);
                    pbPipe4.Left = pbPipe2.Left + pbPipe2.Width + rnd.Next(200);
                }
                else
                {
                    Close();
                    
                }
            }
        }
    }
}
