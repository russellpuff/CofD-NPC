using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CofD_NPC
{
    public partial class DiceForm : Form
    {
        List<int> skillvals = new();
        List<int> meritvals = new();
        int[] core = new int[4];
        int[] attr = new int[10];

        public DiceForm(string[] snames, int[] svalues, string[] mnames, int[] mvalues, int health, int wp, int integr,
            int inte, int wits, int res, int str, int dex, int stam, int pres, int man, int comp)
        {
            InitializeComponent();
            dfRadio10Again.Checked = true;
            string path = Application.StartupPath + "/Resource/cofd.ico";
            this.Icon = Icon.ExtractAssociatedIcon(path);

            // Add zero at the top if the user selects none.
            skillvals.Add(0);
            meritvals.Add(0);
            core[0] = 0;
            attr[0] = 0;
            // Add values from the SFNPC object.
            foreach (string name in snames) { if (name != string.Empty) { dfSkillsComboBox.Items.Add(name); } }
            foreach (int value in svalues) { if (value != 0) { skillvals.Add(value); } }
            foreach (string name in mnames) { if (name != string.Empty) { dfMeritsComboBox.Items.Add(name); } }
            foreach (int value in mvalues) { if (value != 0) { meritvals.Add(value); } }
            core[1] = health;
            core[2] = wp;
            core[3] = integr;
            attr[1] = inte;
            attr[2] = wits;
            attr[3] = res;
            attr[4] = str;
            attr[5] = dex;
            attr[6] = stam;
            attr[7] = pres;
            attr[8] = man;
            attr[9] = comp;

            // Set selected indexes to zero to avoid index out of range exception when rollbutton is clicked. 
            dfCoreComboBox.SelectedIndex = 0;
            dfAttributes1ComboBox.SelectedIndex = 0;
            dfAttributes2ComboBox.SelectedIndex = 0;
            dfSkillsComboBox.SelectedIndex = 0;
            dfMeritsComboBox.SelectedIndex = 0;
        }

        private void RollButton_Click(object sender, EventArgs e)
        {
            int dice = 0;
            int idx = dfCoreComboBox.SelectedIndex;
            dice += core[idx];
            idx = dfAttributes1ComboBox.SelectedIndex;
            dice += attr[idx];
            idx = dfAttributes2ComboBox.SelectedIndex;
            dice += attr[idx];
            idx = dfSkillsComboBox.SelectedIndex;
            dice += skillvals[idx];
            idx = dfMeritsComboBox.SelectedIndex;
            dice += meritvals[idx];
            dice += (int)dfModifierNumUpDown.Value;

            int again = 10;
            if (dfRadio9Again.Checked) { again = 9; }
            if (dfRadio8Again.Checked) { again = 8; }

            bool rote = dfRoteCheck.Checked;

            DieRoller dr = new(dice, rote, again);
            dr.Roll();
        }
    }
}
