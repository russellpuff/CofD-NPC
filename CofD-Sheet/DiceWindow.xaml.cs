using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CofD_Sheet
{
    /// <summary>
    /// Interaction logic for DiceWindow.xaml
    /// </summary>
    public partial class DiceWindow : Window
    {
        readonly List<int> skillvals = new();
        readonly List<int> meritvals = new();
        readonly int[] attr = new int[10];
        readonly int[] core = new int[3];
        bool dontclose = false; // I am very dumb. 
        public DiceWindow(string[] snames, int[] svalues, string[] mnames, int[] mvalues, int health, int wp, int integr,
            int inte, int wits, int res, int str, int dex, int stam, int pres, int man, int comp)
        {
            InitializeComponent();
            dwNoneRadioButton.IsChecked = true;
            dw10AgainRadioButton.IsChecked = true;
            dwAttributesComboBox1.SelectedIndex = 0;
            dwAttributesComboBox2.SelectedIndex = 0;
            dwSkillsComboBox.SelectedIndex = 0;
            dwMeritsComboBox.SelectedIndex = 0;

            // Add zero at the top if the user selects none.
            skillvals.Add(0);
            meritvals.Add(0);
            attr[0] = 0;
            // Add values
            foreach (string name in snames) { if (name != string.Empty) { dwSkillsComboBox.Items.Add(name); } }
            foreach (int value in svalues) { if (value != 0) { skillvals.Add(value); } }
            foreach (string name in mnames) { if (name != string.Empty) { dwMeritsComboBox.Items.Add(name); } }
            foreach (int value in mvalues) { if (value != 0) { meritvals.Add(value); } }
            core[0] = health;
            core[1] = wp;
            core[2] = integr;
            attr[1] = inte;
            attr[2] = wits;
            attr[3] = res;
            attr[4] = str;
            attr[5] = dex;
            attr[6] = stam;
            attr[7] = pres;
            attr[8] = man;
            attr[9] = comp;

        }

        private void DiceWindow_Deactivated(object sender, EventArgs e)
        {
            if (!dontclose) { this.Close(); } 
        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            int dice = 0;
            if (dwHealthRadioButton.IsChecked == true) { dice += core[0]; }
            if (dwWillpowerRadioButton.IsChecked == true) { dice += core[1]; }
            if (dwIntegrityRadioButton.IsChecked == true) { dice += core[2]; }


            dice += attr[dwAttributesComboBox1.SelectedIndex];
            dice += attr[dwAttributesComboBox2.SelectedIndex];
            dice += skillvals[dwSkillsComboBox.SelectedIndex];
            dice += meritvals[dwMeritsComboBox.SelectedIndex];
            dice += dwDiceModNumUpDown.Value != null ? (int)dwDiceModNumUpDown.Value : 0;

            int again = 10;
            if (dw9AgainRadioButton.IsChecked == true) { again = 9; }
            if (dw8AgainRadioButton.IsChecked == true) { again = 8; }

            bool rote = dwRoteToggleButton.IsChecked == true;

            DieRoller dr = new(dice, rote, again);
            dontclose = true;
            dr.Roll();
            dontclose = false;
        }
    }
}
