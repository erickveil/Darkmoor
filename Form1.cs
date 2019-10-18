using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Darkmoor
{
    public partial class Form1 : Form
    {
        Dice _dice = new Dice();
        HexDataIndex _worldMap;

        public Form1()
        {
            InitializeComponent();

            _worldMap = new HexDataIndex(_dice);
            _worldMap.GenerateWorld(3, 3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void bu_newYear_Click(object sender, EventArgs e)
        {
            _worldMap.ResolveAllMigrations();

        }
    }
}
