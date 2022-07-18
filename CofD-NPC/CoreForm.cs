using System.IO.Compression;
using System.Xml.Serialization;

namespace CofD_NPC
{
    public partial class CoreForm : Form
    {
        private readonly List<NPC> NPCs = new();
        public CoreForm()
        {
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            InitializeComponent();
            if (CheckForResources())
            {
                cfRadio10Again.Checked = true;
                this.Icon = Properties.Resources.cofd;
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
            sf.FormClosed += new FormClosedEventHandler(NewFormClosed);
        }

        private bool CheckForResources()
        {/*
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
            } else { return true; } */
            return true;
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
                    NPC n = DecompressFile(file);
                    NPCs.Add(n);
                    cfDataGrid.Rows.Add(n.Name, n.Description, n.ID);
                    cfDataGrid.Refresh();
                } catch
                {
                    string m = "File " + file.Name + " was corrupted and could not be loaded.\n";
                    MessageBox.Show(m, "Corrupt file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private NPC DecompressFile(FileInfo file)
        {
            using FileStream fs = new(file.FullName, FileMode.Open, FileAccess.Read);
            using MemoryStream ums = new();
            using (DeflateStream ds = new(fs, CompressionMode.Decompress)) { ds.CopyTo(ums); }
            ums.Position = 0;
            XmlSerializer xs = new(typeof(NPC));
            return (NPC)xs.Deserialize(ums);
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
                sf.FormClosed += new FormClosedEventHandler(ExistingFormClosed);
            } catch { }
            
        }

        private void DeleteNPCButton_Click(object sender, EventArgs e)
        {
            string m = "Are you sure you want to delete ";
            m += cfDataGrid.SelectedCells[0].Value + "?";
            var result = MessageBox.Show(m, "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string path = Application.StartupPath + "/NPC/" + cfDataGrid.SelectedCells[2].Value + ".npc";
                    File.Delete(path);
                    NPCs.RemoveAll(p => p.ID == (long)cfDataGrid.SelectedCells[2].Value);
                    cfDataGrid.Rows.RemoveAt(cfDataGrid.CurrentCell.RowIndex);
                }
                catch { }
            }
        }

        private void NewFormClosed(object sender, EventArgs e)
        {
            SheetForm sheet = (SheetForm)sender;
            if (sheet.DialogResult == DialogResult.OK)
            {
                NPCs.Add(sheet.SFNPC);
                cfDataGrid.Rows.Add(sheet.SFNPC.Name, sheet.SFNPC.Description, sheet.SFNPC.ID);
                cfDataGrid.Refresh();
            }
        }

        private void ExistingFormClosed(object sender, EventArgs e)
        {
            SheetForm sheet = (SheetForm)sender;
            if (sheet.DialogResult == DialogResult.OK)
            {
                cfDataGrid.SelectedCells[0].Value = sheet.SFNPC.Name;
                cfDataGrid.SelectedCells[1].Value = sheet.SFNPC.Description;
                cfDataGrid.Refresh();
            }
        }
    }
    
}