using System.IO.Compression;
using System.Xml.Serialization;

namespace CofD_NPC
{
    public partial class SheetForm : Form
    {
        private Image? healthy, bashing, lethal, aggriv, dotch, dotunch;
        private bool unsaved;
        private readonly List<PictureBox> health_dots = new();
        private readonly List<PictureBox> willpower_dots = new();
        private readonly List<PictureBox> integrity_dots = new();
        private readonly List<PictureBox> intelligence_dots = new();
        private readonly List<PictureBox> wits_dots = new();
        private readonly List<PictureBox> resolve_dots = new();
        private readonly List<PictureBox> strength_dots = new();
        private readonly List<PictureBox> dexterity_dots = new();
        private readonly List<PictureBox> stamina_dots = new();
        private readonly List<PictureBox> presence_dots = new();
        private readonly List<PictureBox> manipulation_dots = new();
        private readonly List<PictureBox> composure_dots = new();
        private readonly List<List<PictureBox>> skill_dots = new();
        private readonly List<List<PictureBox>> merit_dots = new();
        public NPC SFNPC { get; }

        public SheetForm(NPC? n = null)
        {
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            InitializeComponent();
            InitializeLists();
            InitializeImages();
            InitializeImageControls();
            if (n != null) {
                SFNPC = n;
                LoadNPC();
            }
            else { SFNPC = new NPC(); }
            string title = (sfNameTextBox.Text == String.Empty) ? "Unnamed NPC" : sfNameTextBox.Text;
            this.Text = title;
            unsaved = false;

            string path = Application.StartupPath + "/defuck.txt";
            using StreamReader sr = new(path);
            if (sr.ReadLine() == "enable") { UnfuckText(true); }
        }

        private void InitializeLists()
        {
            int point = 1;
            for (int i = 1; i <= 12; ++i)
            {
                if (i <= 10)
                { // Core dots
                    if (i <= 5)
                    { // Attribute dots
                        intelligence_dots.Add((PictureBox)this.Controls[$"sfIntDot{i}"]);
                        wits_dots.Add((PictureBox)this.Controls[$"sfWitsDot{i}"]);
                        resolve_dots.Add((PictureBox)this.Controls[$"sfResDot{i}"]);
                        strength_dots.Add((PictureBox)this.Controls[$"sfStrDot{i}"]);
                        dexterity_dots.Add((PictureBox)this.Controls[$"sfDexDot{i}"]);
                        stamina_dots.Add((PictureBox)this.Controls[$"sfStamDot{i}"]);
                        presence_dots.Add((PictureBox)this.Controls[$"sfPresDot{i}"]);
                        manipulation_dots.Add((PictureBox)this.Controls[$"sfManDot{i}"]);
                        composure_dots.Add((PictureBox)this.Controls[$"sfCompDot{i}"]);
                    }
                    health_dots.Add((PictureBox)this.Controls[$"sfHealthDot{i}"]);
                    willpower_dots.Add((PictureBox)this.Controls[$"sfWillpowerDot{i}"]);
                    integrity_dots.Add((PictureBox)this.Controls[$"sfIntegrityDot{i}"]);
                }
                // Skill and Merit dots.
                List<PictureBox> spb = new();
                List<PictureBox> mpb = new();
                for (int j = 0; j < 5; ++j)
                {
                    spb.Add((PictureBox)this.Controls[$"sfSkillDot{point}"]);
                    if (i <= 8) { mpb.Add((PictureBox)this.Controls[$"sfMeritDot{point}"]); }
                    ++point;
                }
                skill_dots.Add(spb);
                if (i <= 8) { merit_dots.Add(mpb); }
            }
        }

        private void InitializeImages()
        {
            
            this.Icon = Properties.Resources.cofd;
            healthy = Properties.Resources.healthy;
            bashing = Properties.Resources.bashing;
            lethal = Properties.Resources.lethal;
            aggriv = Properties.Resources.aggrivated;
            dotunch = Properties.Resources.dotunchecked;
            dotch = Properties.Resources.dotchecked;
        }

        private void InitializeImageControls()
        {
            // I know it would have made sense to override checkbox or something to be round.
            // But what works on one computer doesn't often work on another.
            // So I went the easy route. 

            // Dots
            foreach (Control c in Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox p = (PictureBox)c;
                    p.Image = dotunch;
                }
            }

            // Health states
            for (int i = 1; i <= 10; ++i) { 
                PictureBox p = (PictureBox)this.Controls[$"sfHealthState{i}"];
                p.Image = healthy;
            }

            // All attributes start with one dot.
            List<PictureBox> attr = new()
            {
                sfIntDot1, sfWitsDot1, sfResDot1, sfStrDot1, sfDexDot1, sfStamDot1, sfPresDot1, sfManDot1, sfCompDot1
            };
            foreach(PictureBox a in attr) { a.Image = dotch; }
        }

        private void LoadNPC()
        {
            // Checks for null are because the compiler doesn't trust me and pours warnings out the ass if I don't.
            // Even though it's literally impossible for the values to be null, because if they were, this method doesn't run.
            // Better safe than sorry?
            if (SFNPC != null)
            {
                sfNameTextBox.Text = SFNPC.Name;
                sfAgeNumUpDown.Value = (int)SFNPC.Age;
                sfVirtueComboBox.Text = SFNPC.Virtue;
                sfViceComboBox.Text = SFNPC.Vice;
                sfConceptTextBox.Text = SFNPC.Concept;
                sfSizeNumUpDown.Value = (int)SFNPC.Size;
                sfSpeedNumUpDown.Value = (int)SFNPC.Speed;
                sfDefenseNumUpDown.Value = (int)SFNPC.Defense;
                sfArmorTextBox.Text = SFNPC.Armor;
                sfInitNumUpDown.Value = (int)SFNPC.Initiative;
                sfDescTextBox.Text = SFNPC.Description;
                sfConditionsTextBox.Text = SFNPC.Conditions;
                sfAspirationsTextBox.Text = SFNPC.Aspirations;
                for (int m = 0; m < SFNPC.Intelligence; ++m) { intelligence_dots[m].Image = dotch; }
                for (int n = 0; n < SFNPC.Wits; ++n) { wits_dots[n].Image = dotch; }
                for (int o = 0; o < SFNPC.Resolve; ++o) { resolve_dots[o].Image = dotch; }
                for (int p = 0; p < SFNPC.Strength; ++p) { strength_dots[p].Image = dotch; }
                for (int q = 0; q < SFNPC.Dexterity; ++q) { dexterity_dots[q].Image = dotch; }
                for (int r = 0; r < SFNPC.Stamina; ++r) { stamina_dots[r].Image = dotch; }
                for (int s = 0; s < SFNPC.Presence; ++s) { presence_dots[s].Image = dotch; }
                for (int t = 0; t < SFNPC.Manipulation; ++t) { manipulation_dots[t].Image = dotch; }
                for (int u = 0; u < SFNPC.Composure; ++u) { composure_dots[u].Image = dotch; }

                for (int h = 0; h < SFNPC.WillpowerDots; ++h) { willpower_dots[h].Image = dotch; }

                List<CheckBox> willpower = new()
                {
                    sfWillpowerCheck1, sfWillpowerCheck2, sfWillpowerCheck3, sfWillpowerCheck4, sfWillpowerCheck5,
                    sfWillpowerCheck6, sfWillpowerCheck7, sfWillpowerCheck8, sfWillpowerCheck9, sfWillpowerCheck10
                };
                for (int i = 0; i < SFNPC.WillpowerCurrent; ++i) { willpower[i].Checked = true; }

                for (int j = 0; j < SFNPC.Integrity; ++j) { integrity_dots[j].Image = dotch; }

                for (int k = 0; k < SFNPC.HealthDots; ++k) { health_dots[k].Image = dotch; }

                for (int i = 1; i <= 10; ++i)
                {
                    PictureBox p = (PictureBox)this.Controls[$"sfHealthState{i}"];
                    switch (SFNPC.HealthStates[i - 1])
                    {
                        case 0: p.Image = healthy; break;
                        case 1: p.Image = bashing; break;
                        case 2: p.Image = lethal; break;
                        case 3: p.Image = aggriv; break;
                    }
                }

                for (int i = 1; i <= 12; ++i)
                {
                    ComboBox cb = (ComboBox)sfSkillsTableLayout.Controls[$"sfSkillComboBox{i}"];
                    cb.Text = SFNPC.Skills[i - 1];
                    if (i <= 8)
                    {
                        TextBox tb = (TextBox)sfMeritsTableLayout.Controls[$"sfMeritTextBox{i}"];
                        tb.Text = SFNPC.Merits[i - 1];
                    }
                }

                // Iterate through each of the 12 sets of five skills, marking them if appropriate.
                // Also iterates through the 8 sets of five merits.
                int sval = 0, mval = 0;
                for (int w = 0; w < 12; ++w)
                {
                    if (SFNPC.SkillDots != null) { sval = SFNPC.SkillDots[w]; }
                    if (w < 8) { mval = SFNPC.MeritDots[w]; }
                    for (int x = 0; x < 5; ++x)
                    {
                        if (sval > 0)
                        {
                            skill_dots[w][x].Image = dotch;
                            --sval;
                        }
                        if (mval > 0 && w < 8)
                        {
                            merit_dots[w][x].Image = dotch;
                            --mval;
                        }
                    }
                }
            }
        }

        private void SaveButton_Click(object? sender, EventArgs? e)
        {
            sfSaveLabel.Text = "Save. . .";
            try
            {
                SaveValues();
                SaveDots();
                SaveHealthStates();
                CompressFile();
                sfSaveLabel.Text = "Save. . . OK";
                unsaved = false;
            } catch
            {
                string m = "There was an error trying to save this NPC.\n";
                m += $"\n\nTry deleting  and then try again.";
                MessageBox.Show(m, "Save error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sfSaveLabel.Text = "Save failed";
            }
            
        }

        private void CompressFile()
        {
            string path = Application.StartupPath + "/NPC/" + SFNPC.ID.ToString() + ".npc";
            using MemoryStream ms = new();
            XmlSerializer xs = new(typeof(NPC));
            xs.Serialize(ms, SFNPC);
            ms.Position = 0;
            using MemoryStream cms = new();
            using (DeflateStream ds = new(cms, CompressionMode.Compress, true)) { ms.CopyTo(ds); }
            FileStream fs = File.Create(path);
            cms.WriteTo(fs);
            fs.Close();
        }

        private void SaveValues()
        {
            SFNPC.Name = sfNameTextBox.Text;
            SFNPC.Age = (byte)sfAgeNumUpDown.Value;
            SFNPC.Virtue = sfVirtueComboBox.Text;
            SFNPC.Vice = sfViceComboBox.Text;
            SFNPC.Concept = sfConceptTextBox.Text;
            SFNPC.Size = (byte)sfSizeNumUpDown.Value;
            SFNPC.Speed = (byte)sfSpeedNumUpDown.Value;
            SFNPC.Defense = (byte)sfDefenseNumUpDown.Value;
            SFNPC.Armor = sfArmorTextBox.Text;
            SFNPC.Initiative = (byte)sfInitNumUpDown.Value;
            SFNPC.Description = sfDescTextBox.Text;
            SFNPC.Conditions = sfConditionsTextBox.Text;
            SFNPC.Aspirations = sfAspirationsTextBox.Text;

            for (int i = 1; i <= 12; ++i)
            {
                ComboBox cb = (ComboBox)this.sfSkillsTableLayout.Controls[$"sfSkillComboBox{i}"];
                if (cb.Text != String.Empty) { SFNPC.Skills[i - 1] = cb.Text; }
                if (i < 8)
                {
                    TextBox tb = (TextBox)this.sfMeritsTableLayout.Controls[$"sfMeritTextBox{i}"];
                    if (tb.Text != String.Empty) { SFNPC.Merits[i - 1] = tb.Text; }
                }
            }
        }

        private void SaveDots()
        {
            SFNPC.HealthDots = 0;
            foreach (PictureBox p in health_dots) { if (p.Image == dotch) { ++SFNPC.HealthDots; } }

            SFNPC.WillpowerDots = 0;
            foreach (PictureBox p in willpower_dots) { if (p.Image == dotch) { ++SFNPC.WillpowerDots; } }

            SFNPC.WillpowerCurrent = 0;
            for (int i = 1; i < 10; ++i)
            {
                CheckBox cb = (CheckBox)this.Controls[$"sfWillpowerCheck{i}"];
                if (cb.Checked) { ++SFNPC.WillpowerCurrent; }
            } 

            SFNPC.Integrity = 0;
            foreach (PictureBox p in integrity_dots) { if (p.Image == dotch) { ++SFNPC.Integrity; } }

            SFNPC.Intelligence = 0;
            foreach (PictureBox p in intelligence_dots) { if (p.Image == dotch) { ++SFNPC.Intelligence; } }

            SFNPC.Wits = 0;
            foreach (PictureBox p in wits_dots) { if (p.Image == dotch) { ++SFNPC.Wits; } }

            SFNPC.Resolve = 0;
            foreach (PictureBox p in resolve_dots) { if (p.Image == dotch) { ++SFNPC.Resolve; } }

            SFNPC.Strength = 0;
            foreach (PictureBox p in strength_dots) { if (p.Image == dotch) { ++SFNPC.Strength; } }

            SFNPC.Dexterity = 0;
            foreach (PictureBox p in dexterity_dots) { if (p.Image == dotch) { ++SFNPC.Dexterity; } }

            SFNPC.Stamina = 0;
            foreach (PictureBox p in stamina_dots) { if (p.Image == dotch) { ++SFNPC.Stamina; } }

            SFNPC.Presence = 0;
            foreach (PictureBox p in presence_dots) { if (p.Image == dotch) { ++SFNPC.Presence; } }

            SFNPC.Manipulation = 0;
            foreach (PictureBox p in manipulation_dots) { if (p.Image == dotch) { ++SFNPC.Manipulation; } }

            SFNPC.Composure = 0;
            foreach (PictureBox p in composure_dots) { if (p.Image == dotch) { ++SFNPC.Composure; } }

            for (int i = 0; i < 12; ++i)
            {
                int val = 0;
                foreach (PictureBox p in skill_dots[i]) { if (p.Image == dotch) { ++val; } }
                SFNPC.SkillDots[i] = val;
                val = 0;
                if (i < 8)
                {
                    foreach (PictureBox p in merit_dots[i]) { if (p.Image == dotch) { ++val; } }
                    SFNPC.MeritDots[i] = val;
                }
            }
        }

        private void SaveHealthStates()
        {
            for (int i = 1; i <= 10; ++i)
            {
                PictureBox p = (PictureBox)this.Controls[$"sfHealthState{i}"];
                if (p.Image == healthy) { SFNPC.HealthStates[i - 1] = 0; continue; }
                if (p.Image == bashing) { SFNPC.HealthStates[i - 1] = 1; continue; }
                if (p.Image == lethal) { SFNPC.HealthStates[i - 1] = 2; continue; }
                if (p.Image == aggriv) { SFNPC.HealthStates[i - 1] = 3; continue; }
            }
        }

        private void Dot_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            string name = p.Name;
            int index = name.LastIndexOf('t');
            ++index;
            name = name[..index];
            string number = new (p.Name.Where(Char.IsDigit).ToArray());
            int num = Int32.Parse(number), max, min;

            if (name.Contains("Skill") || name.Contains("Merit"))
            {
                int last = number[^1] - '0';
                if (last is >= 1 and <= 5)
                { max = 5 * (int)Math.Ceiling(num / 5.0); } 
                else { max = 10 * (int)Math.Ceiling(num / 10.0); }
                min = max - 4;
                if (min <= 0) { min = 1; }
            } else
            {
                if (name.Contains("Health") || name.Contains("Willpower") || name.Contains("Integrity")) { max = 10; }
                else { max = 5; }
                min = 1;
                
            }
            for (int i = max; i >= min; --i)
            {
                p = (PictureBox)this.Controls[name + $"{i}"];
                MouseEventArgs ms = (MouseEventArgs)e;
                if (i > num || ms.Button == MouseButtons.Right) { p.Image = dotunch; }
                else { p.Image = dotch; }
            }

            Unsaved();
        }

        private void Dot_DoubleClick(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            p.Image = dotunch;
        }

        private void InitButton_Click(object sender, EventArgs e)
        {
            Random r = new();
            int d = r.Next(1, 10);
            int i = 0;
            List<PictureBox> dexcomp = new();
            dexcomp.AddRange(dexterity_dots);
            dexcomp.AddRange(composure_dots);
            foreach(PictureBox p in dexcomp) { if (p.Image == dotch) { ++i; } }

            string m = $"Rolled: {d}\nMod: {i}\nTotal: {d+i}";
            MessageBox.Show(m, "Initiative", MessageBoxButtons.OK);
        }

        private void RollDiceButton_Click(object sender, EventArgs e)
        {
            if (unsaved)
            {
                const string m = "NPC must be saved before Dice Roller can be used. Save now?";
                var result = MessageBox.Show(m, "Dice Roller", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                { SaveButton_Click(null, null); }
                else { return; }
            }

            if (!ValidateSkillsMerits()) {
                string m = "Detected skills or merits that have dots but no name, or vice versa.\n";
                m += "The die roller breaks if you have any skills or merits that break this rule. There is currently no fix besides ";
                m += "forcing you to manually change it.";
                MessageBox.Show(m, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            DiceForm ds = new(SFNPC.Skills, SFNPC.SkillDots, SFNPC.Merits, SFNPC.MeritDots, SFNPC.HealthDots, SFNPC.WillpowerDots,
                SFNPC.Integrity, SFNPC.Intelligence, SFNPC.Wits, SFNPC.Resolve, SFNPC.Strength, SFNPC.Dexterity, SFNPC.Stamina,
                SFNPC.Presence, SFNPC.Manipulation, SFNPC.Composure);
            ds.ShowDialog();
        }

        private bool ValidateSkillsMerits()
        {
            for (int i = 0; i < 12; ++i)
            {
                if ((SFNPC.Skills[i] == String.Empty) && (SFNPC.SkillDots[i] > 0)) { return false; }
                if ((SFNPC.Skills[i] != String.Empty) && (SFNPC.SkillDots[i] == 0)) { return false; }
            }
            for (int j = 0; j < 8; ++j)
            {
                if ((SFNPC.Merits[j] == String.Empty) && (SFNPC.MeritDots[j] > 0)) { return false; }
                if ((SFNPC.Merits[j] != String.Empty) && (SFNPC.MeritDots[j] == 0)) { return false; }
            }
            return true;
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            string m = "Is the text on the form fucked? If so would you like to fix it?\n";
            m += "Click \"no\" to put it back the way it was. This setting is persistent.";
            var result = MessageBox.Show(m, "?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            string path = Application.StartupPath + "/defuck.txt";
            if (result == DialogResult.Cancel) { return; }
            UnfuckText((result == DialogResult.Yes));
            var allLines = File.ReadAllLines(path);
            allLines[0] = (result == DialogResult.Yes) ? "enable" : "disable";
            File.WriteAllLines(path, allLines);
        }

        private void UnfuckText(bool fuck) // true means text is unfucked, false means text is fucked.
        {
            foreach (Control c in Controls)
            {
                if (c.GetType() == typeof(Label))
                {
                    Label l = (Label)c;
                    string f = l.Font.Name;
                    double s = l.Font.Size;
                    if (fuck) { s /= 1.25; }
                    else { s *= 1.25; }
                    l.Font = new Font(f, (float)s);
                }
                else if (c.GetType() == typeof(TableLayoutPanel))
                {
                    foreach (Control control in c.Controls)
                    {
                        if (control.GetType() == typeof(Label))
                        {
                            Label l = (Label)control;
                            string f = l.Font.Name;
                            double s = l.Font.Size;
                            if (fuck) { s /= 1.25; }
                            else { s *= 1.25; }
                            l.Font = new Font(f, (float)s);
                        }
                    }
                }
            }
        }

        private void HealthState_Click(object sender, EventArgs e)
        {
            PictureBox im = (PictureBox)sender;
            if (im.Image == healthy) { im.Image = bashing; return; }
            if (im.Image == bashing) { im.Image = lethal; return; }
            if (im.Image == lethal) { im.Image = aggriv; return; }
            if (im.Image == aggriv) { im.Image = healthy; return; }
        }

        private void SheetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (unsaved)
            {
                var result = MessageBox.Show("Do you really want to exit?", "Unsaved changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                } else { this.DialogResult = DialogResult.Cancel; }
            } else
            { this.DialogResult = DialogResult.OK; }
            // Begone, memory leaks.
            // It gets mad if I don't declare them as nullable, then gets mad if I don't check if they're null before disposing.
            // I wouldn't have had to check if I didn't need to declare them as nullable.
            if (healthy != null) { healthy.Dispose(); }
            if (bashing != null) { bashing.Dispose(); }
            if (lethal != null) { lethal.Dispose(); }
            if (aggriv != null) { aggriv.Dispose(); }
            if (dotch != null) { dotch.Dispose(); }
            if (dotunch != null) { dotunch.Dispose(); }
        }

        private void NumUpDown_ValueChanged(object sender, EventArgs e) { Unsaved(); }

        private void TextBox_TextChanged(object sender, EventArgs e) { Unsaved(); }

        private void Unsaved()
        {
            unsaved = true;
            sfSaveLabel.Text = string.Empty;
        }
    }
}
