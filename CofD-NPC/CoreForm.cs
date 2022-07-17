using System.Text.Json;

namespace CofD_NPC
{
    public partial class CoreForm : Form
    {
        private readonly List<NPC> NPCs = new();
        public CoreForm()
        {
            InitializeComponent();
            if (CheckForResources())
            {
                cfRadio10Again.Checked = true;
                string path = Application.StartupPath + "/Resource/cofd.ico";
                this.Icon = Icon.ExtractAssociatedIcon(path);
                LoadNPCs();
            }
        }

        private void RollButton_Click(object sender, EventArgs e)
        {
            int die = (int)cfDiceNumUpDown.Value;
            bool rote = cfRoteCheck.Checked;
            int again = 10;
            if (cfRadio9Again.Checked) { again = 9; }
            if (cfRadio8Again.Checked) { again = 8; }

            DieRoller dr = new(die, rote, again);
            dr.Roll();
        }

        private void NewNPCButton_Click(object sender, EventArgs e)
        {
            SheetForm sf = new();
            sf.Show();
            if (sf.DialogResult == DialogResult.OK)
            {
                NPCs.Add(sf.SFNPC);
                cfDataGrid.Rows.Add(sf.SFNPC.Name, sf.SFNPC.Description, sf.SFNPC.ID);
                cfDataGrid.Refresh();
            }
        }

        private bool CheckForResources()
        {
            string[] paths = {
                Application.StartupPath + "/Resource/healthy.png",
                Application.StartupPath + "/Resource/bashing.png",
                Application.StartupPath + "/Resource/lethal.png",
                Application.StartupPath + "/Resource/aggrivated.png",
                Application.StartupPath + "/Resource/dotunchecked.png",
                Application.StartupPath + "/Resource/dotchecked.png",
                Application.StartupPath + "/Resource/cofd.ico"
            };
            if (!File.Exists(paths[0]) || !File.Exists(paths[1]) || !File.Exists(paths[2]) || !File.Exists(paths[3])
                || !File.Exists(paths[4]) || !File.Exists(paths[5]) || !File.Exists(paths[6]))
            {
                string message = "Some resource files appear to be missing. This application relies on all files included in the zip file " +
                    "this application was downloaded in.";

                MessageBox.Show(message, "Missing Files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return false;
            } else { return true; }
        }

        private void LoadNPCs()
        {
            string path = Application.StartupPath + "/NPC/";
            Directory.CreateDirectory(path);

            DirectoryInfo di = new(path);
            foreach (var file in di.GetFiles("*.npc"))
            {
                try
                {
                    NPC n = new();
                    using (StreamReader sr = File.OpenText(file.FullName))
                    {
                        n = JsonSerializer.Deserialize<NPC>(sr.ReadLine());
                        sr.Close();
                    }
                    NPCs.Add(n);
                    cfDataGrid.Rows.Add(n.Name, n.Description, n.ID);
                    cfDataGrid.Refresh();

                } catch
                {
                    string m = "File " + file.Name + "was corrupted and could not be loaded.";
                    MessageBox.Show(m, "Corrupt file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int idx = 0;
                for (int i = 0; i < NPCs.Count; ++i)
                {
                    if (NPCs[i].ID == (long)cfDataGrid.SelectedCells[2].Value)
                    {
                        idx = i;
                        break;
                    }
                }
                SheetForm sf = new(NPCs[idx]);
                sf.Show();
                if (sf.DialogResult == DialogResult.OK)
                {
                    cfDataGrid.SelectedCells[0].Value = NPCs[idx].Name;
                    cfDataGrid.SelectedCells[1].Value = NPCs[idx].Description;
                    cfDataGrid.Refresh();
                }
            } catch { }
            
        }

        private void DeleteNPCButton_Click(object sender, EventArgs e)
        {
            
        }
    }
    
}