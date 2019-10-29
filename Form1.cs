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
        DataSaver _gameData = new DataSaver();

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void bu_newYear_Click(object sender, EventArgs e)
        {
            ++_gameData.ProgramData.TimeObj.Year;
            _gameData.ProgramData.TimeObj.Month = 1;
            _gameData.ProgramData.TimeObj.Day = 1;
            _gameData.ProgramData.IncreaseAllPopulations();
            _gameData.ProgramData.ResolveAllMigrations();

        }

        private void bu_save_Click(object sender, EventArgs e)
        {
            _gameData.Save();

        }

        private void bu_load_Click(object sender, EventArgs e)
        {
            _gameData.Load();
        }

        private void but_newWorld_Click(object sender, EventArgs e)
        {
            _gameData = new DataSaver();
            _gameData.ProgramData.ClearAllData();
            _gameData.ProgramData = new HexDataIndex();
            int width = (int)(nud_startWidth.Value);
            int height = (int)(nud_startHeight.Value);
            var origin = new Tuple<int, int>(
                (int)nud_originX.Value,
                (int)nud_originY.Value);
            int tierWidth = (int)nud_tierWidth.Value;
            _gameData.ProgramData.GenerateWorld(width, height, origin, 
                tierWidth);
        }

        private void but_resizeWorld_Click(object sender, EventArgs e)
        {
            int width = (int)(nud_resizeWidth.Value);
            int height = (int)(nud_resizeHeight.Value);
            _gameData.ProgramData.ResizeWorld(width, height);
        }
    }
}
