using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game f2 = new Game();
            f2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBoxButtons msb = MessageBoxButtons.YesNo;
            String message = "Вы действительно хотите выйти?";
            String caption = "Выход";
            if (MessageBox.Show(message, caption, msb) == DialogResult.Yes)
                this.Close();
        }      
    }
}
