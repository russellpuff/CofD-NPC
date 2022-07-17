using System.Text.Json;

namespace CofD_NPC
{
    public partial class SheetForm : Form
    {
        private Image? healthy, bashing, lethal, aggriv, dotch, dotunch;
        private bool unsaved;
        public NPC SFNPC { get; }

        public SheetForm(NPC? n = null)
        {
            InitializeComponent();
            InitializeImages();
            InitializeImageControls();
            if (n != null) {
                SFNPC = n;
                LoadNPC();
            }
            else { SFNPC = new NPC(); }

            string title = (sfNameTextBox.Text == String.Empty) ? "Unnamed NPC" : sfNameLabel.Text;
            this.Text = title;
        }

        private void InitializeImages()
        {
            // I know it would have made sense to override checkbox or something to be round.
            // But what works on one computer doesn't often work on another.
            // So I went the easy route. 
            string[] paths = {
                Application.StartupPath + "/Resource/healthy.png",
                Application.StartupPath + "/Resource/bashing.png",
                Application.StartupPath + "/Resource/lethal.png",
                Application.StartupPath + "/Resource/aggrivated.png",
                Application.StartupPath + "/Resource/dotunchecked.png",
                Application.StartupPath + "/Resource/dotchecked.png",
                Application.StartupPath + "/Resource/cofd.ico"
            };
            this.Icon = Icon.ExtractAssociatedIcon(paths[6]);

            healthy = Image.FromFile(paths[0]);
            bashing = Image.FromFile(paths[1]);
            lethal = Image.FromFile(paths[2]);
            aggriv = Image.FromFile(paths[3]);
            dotunch = Image.FromFile(paths[4]);
            dotch = Image.FromFile(paths[5]);
        }

        private void InitializeImageControls()
        {
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
            List<PictureBox> health = new() { 
                sfHealthState1, sfHealthState2, sfHealthState3, sfHealthState4, sfHealthState5,
                sfHealthState6, sfHealthState7, sfHealthState8, sfHealthState9, sfHealthState10 
            };
            foreach(PictureBox h in health) { h.Image = healthy; }
        }

        private void LoadNPC()
        {
            // Checks for null are because the compiler doesn't trust me and pours warnings out the ass if I don't.
            // Even though it's literally impossible for the values to be null, because if they were, this method doesn't run.
            // Better safe than sorry?
            if (SFNPC != null)
            {
                sfNameTextBox.Text = SFNPC.Name;
                if (SFNPC.Age != null) { sfAgeNumUpDown.Value = (decimal)SFNPC.Age; }
                sfVirtueComboBox.Text = SFNPC.Virtue;
                sfViceComboBox.Text = SFNPC.Vice;
                sfConceptTextBox.Text = SFNPC.Concept;
                if (SFNPC.Size != null) { sfSizeNumUpDown.Value = (decimal)SFNPC.Size; }
                if (SFNPC.Speed != null) { sfSpeedNumUpDown.Value = (decimal)SFNPC.Speed; }
                if (SFNPC.Defense != null) { sfDefenseNumUpDown.Value = (decimal)SFNPC.Defense; }
                sfArmorTextBox.Text = SFNPC.Armor;
                if (SFNPC.Initiative != null) { sfInitNumUpDown.Value = (decimal)SFNPC.Initiative; }
                sfDescTextBox.Text = SFNPC.Description;
                sfConditionsTextBox.Text = SFNPC.Conditions;
                sfAspirationsTextBox.Text = SFNPC.Aspirations;

                List<PictureBox> wpdots = new()
                {
                    sfWillpowerDot1, sfWillpowerDot2, sfWillpowerDot3, sfWillpowerDot4, sfWillpowerDot5,
                    sfWillpowerDot6, sfWillpowerDot7, sfWillpowerDot8, sfWillpowerDot9, sfWillpowerDot10
                };
                for (int h = 0; h < SFNPC.WillpowerDots; ++h) { wpdots[h].Image = dotch; }

                List<CheckBox> willpower = new()
                {
                    sfWillpowerCheck1, sfWillpowerCheck2, sfWillpowerCheck3, sfWillpowerCheck4, sfWillpowerCheck5,
                    sfWillpowerCheck6, sfWillpowerCheck7, sfWillpowerCheck8, sfWillpowerCheck9, sfWillpowerCheck10
                };
                for (int i = 0; i < SFNPC.WillpowerCurrent; ++i) { willpower[i].Checked = true; }

                List<PictureBox> integrity = new()
                {
                    sfIntegrityDot1, sfIntegrityDot2, sfIntegrityDot3, sfIntegrityDot4, sfIntegrityDot5,
                    sfIntegrityDot6, sfIntegrityDot7, sfIntegrityDot8, sfIntegrityDot9, sfIntegrityDot10
                };
                for (int j = 0; j < SFNPC.Integrity; ++j) { integrity[j].Image = dotch; }

                List<PictureBox> healthdot = new()
                {
                    sfHealthDot1, sfHealthDot2, sfHealthDot3, sfHealthDot4, sfHealthDot5,
                    sfHealthDot6, sfHealthDot7, sfHealthDot8, sfHealthDot9, sfHealthDot10
                };
                for (int k = 0; k < SFNPC.HealthDots; ++k) { healthdot[k].Image = dotch; }

                List<PictureBox> healthstate = new()
                {
                    sfHealthState1, sfHealthState2, sfHealthState3, sfHealthState4, sfHealthState5,
                    sfHealthState6, sfHealthState7, sfHealthState8, sfHealthState9, sfHealthState10
                };
                if (SFNPC.HealthStates != null)
                {
                    for (int l = 0; l < SFNPC.HealthDots; ++l)
                    {
                        switch (SFNPC.HealthStates[l])
                        {
                            case 0: healthstate[l].Image = healthy; break;
                            case 1: healthstate[l].Image = bashing; break;
                            case 2: healthstate[l].Image = lethal; break;
                            case 3: healthstate[l].Image = aggriv; break;
                        }
                    }
                }
                List<PictureBox> inte = new() { sfIntDot1, sfIntDot2, sfIntDot3, sfIntDot4, sfIntDot5 };
                for (int m = 0; m < SFNPC.Intelligence; ++m) { inte[m].Image = dotch; }

                List<PictureBox> wits = new() { sfWitsDot1, sfWitsDot2, sfWitsDot3, sfWitsDot4, sfWitsDot5 };
                for (int n = 0; n < SFNPC.Wits; ++n) { wits[n].Image = dotch; }

                List<PictureBox> res = new() { sfResDot1, sfResDot2, sfResDot3, sfResDot4, sfResDot5 };
                for (int o = 0; o < SFNPC.Resolve; ++o) { res[o].Image = dotch; }

                List<PictureBox> str = new() { sfStrDot1, sfStrDot2, sfStrDot3, sfStrDot4, sfStrDot5 };
                for (int p = 0; p < SFNPC.Strength; ++p) { str[p].Image = dotch; }

                List<PictureBox> dex = new() { sfDexDot1, sfDexDot2, sfDexDot3, sfDexDot4, sfDexDot5 };
                for (int q = 0; q < SFNPC.Dexterity; ++q) { dex[q].Image = dotch; }

                List<PictureBox> stam = new() { sfStamDot1, sfStamDot2, sfStamDot3, sfStamDot4, sfStamDot5 };
                for (int r = 0; r < SFNPC.Stamina; ++r) { stam[r].Image = dotch; }

                List<PictureBox> pres = new() { sfPresDot1, sfPresDot2, sfPresDot3, sfPresDot4, sfPresDot5 };
                for (int s = 0; s < SFNPC.Presence; ++s) { pres[s].Image = dotch; }

                List<PictureBox> man = new() { sfManDot1, sfManDot2, sfManDot3, sfManDot4, sfManDot5 };
                for (int t = 0; t < SFNPC.Manipulation; ++t) { man[t].Image = dotch; }

                List<PictureBox> comp = new() { sfCompDot1, sfCompDot2, sfCompDot3, sfCompDot4, sfCompDot5 };
                for (int u = 0; u < SFNPC.Composure; ++u) { comp[u].Image = dotch; }

                List<ComboBox> skills = new() { sfSkillComboBox1, sfSkillComboBox2, sfSkillComboBox3, sfSkillComboBox4, sfSkillComboBox5, sfSkillComboBox6,
                    sfSkillComboBox7, sfSkillComboBox8, sfSkillComboBox9, sfSkillComboBox10, sfSkillComboBox11, sfSkillComboBox12
                };
                if (SFNPC.Skills != null) { for (int v = 0; v < 12; ++v) { skills[v].Text = SFNPC.Skills[v]; } }

                // Create a list of all 60 skilldot pictureboxes.
                List<PictureBox> skilldots = new();
                foreach (Control c in Controls)
                {
                    if (c.GetType() == typeof(PictureBox))
                    {
                        PictureBox p = (PictureBox)c;
                        if (p.Name.ToString().Contains("SkillDot"))
                        {
                            skilldots.Add(p);
                        }
                    }
                }
                // It collects the list in proper numerical order, but backwards for some reason.
                // It is only through the grace of this strange logic that everything is sorted properly.
                skilldots.Reverse();
                // Iterate through each of the 12 sets of five, marking them if appropriate.
                int point = 0, val = 0;
                for (int w = 0; w < 12; ++w)
                {
                    if (SFNPC.SkillDots != null) { val = SFNPC.SkillDots[w]; }
                    for (int x = 0; x < 5; ++x)
                    {
                        if (val > 0)
                        {
                            skilldots[point].Image = dotch;
                            --val;
                        }
                        ++point;
                    }
                }

                List<TextBox> merits = new()
                {
                    sfMeritTextBox1, sfMeritTextBox2, sfMeritTextBox3, sfMeritTextBox4,
                    sfMeritTextBox5, sfMeritTextBox6, sfMeritTextBox7, sfMeritTextBox8
                };
                if (SFNPC.Merits != null) { for (int y = 0; y < 8; ++y) { merits[y].Text = SFNPC.Merits[y]; } }

                // Do the same exact thing as with skills.
                List<PictureBox> meritdots = new();
                foreach (Control c in Controls)
                {
                    if (c.GetType() == typeof(PictureBox))
                    {
                        PictureBox q = (PictureBox)c;
                        if (q.Name.ToString().Contains("MeritDot"))
                        {
                            meritdots.Add(q);
                        }
                    }
                }
                // This list too, is sorted automatically but backwards.
                // Don't question why it does this, only be grateful it sorted properly.
                meritdots.Reverse();
                point = 0;
                for (int z = 0; z < 8; ++z)
                {
                    if (SFNPC.MeritDots != null) { val = SFNPC.MeritDots[z]; }
                    for (int a = 0; a < 5; ++a)
                    {
                        if (val > 0)
                        {
                            meritdots[point].Image = dotch;
                            --val;
                        }
                        ++point;
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            sfSaveLabel.Text = "Save. . .";
            try
            {
                SaveValues();
                SaveDots();
                SaveHealthStates();
                string path = Application.StartupPath + "/NPC/" + SFNPC.ID.ToString() + ".npc";
                using (StreamWriter file = File.CreateText(path))
                {
                    string cereal = JsonSerializer.Serialize(SFNPC);
                    file.Write(cereal);
                    file.Close();
                }
                sfSaveLabel.Text = "Save. . . OK";
                unsaved = false;
            } catch (Exception ex)
            {
                string m = "There was an error trying to save this NPC.\n";
                m += ex.Message;
                m += "\n\nTry deleting " + SFNPC.ID.ToString() + ".npc and then try again.";
                MessageBox.Show(m, "Save error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sfSaveLabel.Text = "Save failed";
            }
            
        }

        private void SaveValues()
        {
            SFNPC.Name = sfNameTextBox.Text;
            SFNPC.Age = (int)sfAgeNumUpDown.Value;
            SFNPC.Virtue = sfVirtueComboBox.Text;
            SFNPC.Vice = sfViceComboBox.Text;
            SFNPC.Concept = sfConceptTextBox.Text;
            SFNPC.Size = (int)sfSizeNumUpDown.Value;
            SFNPC.Speed = (int)sfSpeedNumUpDown.Value;
            SFNPC.Defense = (int)sfDefenseNumUpDown.Value;
            SFNPC.Armor = sfArmorTextBox.Text;
            SFNPC.Initiative = (int)sfInitNumUpDown.Value;
            SFNPC.Description = sfDescTextBox.Text;
            SFNPC.Conditions = sfConditionsTextBox.Text;
            SFNPC.Aspirations = sfAspirationsTextBox.Text;
            if (SFNPC.Skills != null)
            {
                SFNPC.Skills[0] = sfSkillComboBox1.Text;
                SFNPC.Skills[1] = sfSkillComboBox2.Text;
                SFNPC.Skills[2] = sfSkillComboBox3.Text;
                SFNPC.Skills[3] = sfSkillComboBox4.Text;
                SFNPC.Skills[4] = sfSkillComboBox5.Text;
                SFNPC.Skills[5] = sfSkillComboBox6.Text;
                SFNPC.Skills[6] = sfSkillComboBox7.Text;
                SFNPC.Skills[7] = sfSkillComboBox8.Text;
                SFNPC.Skills[8] = sfSkillComboBox9.Text;
                SFNPC.Skills[9] = sfSkillComboBox10.Text;
                SFNPC.Skills[10] = sfSkillComboBox11.Text;
                SFNPC.Skills[11] = sfSkillComboBox12.Text;
            }
            if (SFNPC.Merits != null)
            {
                SFNPC.Merits[0] = sfMeritTextBox1.Text;
                SFNPC.Merits[1] = sfMeritTextBox2.Text;
                SFNPC.Merits[2] = sfMeritTextBox3.Text;
                SFNPC.Merits[3] = sfMeritTextBox4.Text;
                SFNPC.Merits[4] = sfMeritTextBox5.Text;
                SFNPC.Merits[5] = sfMeritTextBox6.Text;
                SFNPC.Merits[6] = sfMeritTextBox7.Text;
                SFNPC.Merits[7] = sfMeritTextBox8.Text;
            }
        }

        private void SaveDots()
        {
            SFNPC.HealthDots = 0;
            bool[] h = new bool[10];
            h[0] = (sfHealthDot1.Image == dotch);
            h[1] = (sfHealthDot2.Image == dotch);
            h[2] = (sfHealthDot3.Image == dotch);
            h[3] = (sfHealthDot4.Image == dotch);
            h[4] = (sfHealthDot5.Image == dotch);
            h[5] = (sfHealthDot6.Image == dotch);
            h[6] = (sfHealthDot7.Image == dotch);
            h[7] = (sfHealthDot8.Image == dotch);
            h[8] = (sfHealthDot9.Image == dotch);
            h[9] = (sfHealthDot10.Image == dotch);
            foreach (bool b in h) { if (b) { ++SFNPC.HealthDots; } }

            SFNPC.WillpowerDots = 0;
            h[0] = (sfWillpowerDot1.Image == dotch);
            h[1] = (sfWillpowerDot2.Image == dotch);
            h[2] = (sfWillpowerDot3.Image == dotch);
            h[3] = (sfWillpowerDot4.Image == dotch);
            h[4] = (sfWillpowerDot5.Image == dotch);
            h[5] = (sfWillpowerDot6.Image == dotch);
            h[6] = (sfWillpowerDot7.Image == dotch);
            h[7] = (sfWillpowerDot8.Image == dotch);
            h[8] = (sfWillpowerDot9.Image == dotch);
            h[9] = (sfWillpowerDot10.Image == dotch);
            foreach (bool b in h) { if (b) { ++SFNPC.WillpowerDots; } }

            SFNPC.WillpowerCurrent = 0;
            h[0] = (sfWillpowerCheck1.Checked);
            h[1] = (sfWillpowerCheck2.Checked);
            h[2] = (sfWillpowerCheck3.Checked);
            h[3] = (sfWillpowerCheck4.Checked);
            h[4] = (sfWillpowerCheck5.Checked);
            h[5] = (sfWillpowerCheck6.Checked);
            h[6] = (sfWillpowerCheck7.Checked);
            h[7] = (sfWillpowerCheck8.Checked);
            h[8] = (sfWillpowerCheck9.Checked);
            h[9] = (sfWillpowerCheck10.Checked);
            foreach (bool b in h) { if (b) { ++SFNPC.WillpowerCurrent; } }

            SFNPC.Integrity = 0;
            h[0] = (sfIntegrityDot1.Image == dotch);
            h[1] = (sfIntegrityDot2.Image == dotch);
            h[2] = (sfIntegrityDot3.Image == dotch);
            h[3] = (sfIntegrityDot4.Image == dotch);
            h[4] = (sfIntegrityDot5.Image == dotch);
            h[5] = (sfIntegrityDot6.Image == dotch);
            h[6] = (sfIntegrityDot7.Image == dotch);
            h[7] = (sfIntegrityDot8.Image == dotch);
            h[8] = (sfIntegrityDot9.Image == dotch);
            h[9] = (sfIntegrityDot10.Image == dotch);
            foreach (bool b in h) { if (b) { ++SFNPC.Integrity; } }

            bool[] d = new bool[5];
            SFNPC.Intelligence = 0;
            d[0] = (sfIntDot1.Image == dotch);
            d[1] = (sfIntDot2.Image == dotch);
            d[2] = (sfIntDot3.Image == dotch);
            d[3] = (sfIntDot4.Image == dotch);
            d[4] = (sfIntDot5.Image == dotch);
            foreach (bool b in d) { if (b) { ++SFNPC.Intelligence; } }

            SFNPC.Wits = 0;
            d[0] = (sfWitsDot1.Image == dotch);
            d[1] = (sfWitsDot2.Image == dotch);
            d[2] = (sfWitsDot3.Image == dotch);
            d[3] = (sfWitsDot4.Image == dotch);
            d[4] = (sfWitsDot5.Image == dotch);
            foreach (bool b in d) { if (b) { ++SFNPC.Wits; } }

            SFNPC.Resolve = 0;
            d[0] = (sfResDot1.Image == dotch);
            d[1] = (sfResDot2.Image == dotch);
            d[2] = (sfResDot3.Image == dotch);
            d[3] = (sfResDot4.Image == dotch);
            d[4] = (sfResDot5.Image == dotch);
            foreach (bool b in d) { if (b) { ++SFNPC.Resolve; } }

            SFNPC.Strength = 0;
            d[0] = (sfStrDot1.Image == dotch);
            d[1] = (sfStrDot2.Image == dotch);
            d[2] = (sfStrDot3.Image == dotch);
            d[3] = (sfStrDot4.Image == dotch);
            d[4] = (sfStrDot5.Image == dotch);
            foreach (bool b in d) { if (b) { ++SFNPC.Strength; } }

            SFNPC.Dexterity = 0;
            d[0] = (sfDexDot1.Image == dotch);
            d[1] = (sfDexDot2.Image == dotch);
            d[2] = (sfDexDot3.Image == dotch);
            d[3] = (sfDexDot4.Image == dotch);
            d[4] = (sfDexDot5.Image == dotch);
            foreach (bool b in d) { if (b) { ++SFNPC.Dexterity; } }

            SFNPC.Stamina = 0;
            d[0] = (sfStamDot1.Image == dotch);
            d[1] = (sfStamDot2.Image == dotch);
            d[2] = (sfStamDot3.Image == dotch);
            d[3] = (sfStamDot4.Image == dotch);
            d[4] = (sfStamDot5.Image == dotch);
            foreach (bool b in d) { if (b) { ++SFNPC.Stamina; } }

            SFNPC.Presence = 0;
            d[0] = (sfPresDot1.Image == dotch);
            d[1] = (sfPresDot2.Image == dotch);
            d[2] = (sfPresDot3.Image == dotch);
            d[3] = (sfPresDot4.Image == dotch);
            d[4] = (sfPresDot5.Image == dotch);
            foreach (bool b in d) { if (b) { ++SFNPC.Presence; } }

            SFNPC.Manipulation = 0;
            d[0] = (sfIntDot1.Image == dotch);
            d[1] = (sfIntDot2.Image == dotch);
            d[2] = (sfIntDot3.Image == dotch);
            d[3] = (sfIntDot4.Image == dotch);
            d[4] = (sfIntDot5.Image == dotch);
            foreach (bool b in d) { if (b) { ++SFNPC.Manipulation; } }

            SFNPC.Composure = 0;
            d[0] = (sfCompDot1.Image == dotch);
            d[1] = (sfCompDot2.Image == dotch);
            d[2] = (sfCompDot3.Image == dotch);
            d[3] = (sfCompDot4.Image == dotch);
            d[4] = (sfCompDot5.Image == dotch);
            foreach (bool b in d) { if (b) { ++SFNPC.Composure; } }

            // Repurposed code from the load function. 
            List<PictureBox> skilldots = new();
            foreach (Control c in Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox p = (PictureBox)c;
                    if (p.Name.ToString().Contains("SkillDot")) { skilldots.Add(p); }
                }
            }
            skilldots.Reverse();
            int point = 0, val = 0;
            if (SFNPC.SkillDots != null)
            {
                for (int i = 0; i < 12; ++i)
                {
                    for (int j = 0; j < 5; ++j)
                    {
                        if (skilldots[point].Image == dotch) { ++val; }
                        ++point;
                    }
                    SFNPC.SkillDots[i] = val;
                    val = 0;
                }
            }

            List<PictureBox> meritdots = new();
            foreach (Control c in Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox p = (PictureBox)c;
                    if (p.Name.ToString().Contains("MeritDot")) { meritdots.Add(p); }
                }
            }
            meritdots.Reverse();
            point = 0; 
            val = 0;
            if (SFNPC.MeritDots != null)
            {
                for (int i = 0; i < 8; ++i)
                {
                    for (int j = 0; j < 5; ++j)
                    {
                        if (meritdots[point].Image == dotch) { ++val; }
                        ++point;
                    }
                    SFNPC.MeritDots[i] = val;
                    val = 0;
                }
            }
        }

        private void SaveHealthStates()
        {
            List<PictureBox> hs = new()
            {
                sfHealthState1, sfHealthState2, sfHealthState3, sfHealthState4, sfHealthState5,
                sfHealthState6, sfHealthState7, sfHealthState8, sfHealthState9, sfHealthState10
            };
            for (int i = 0; i < 10; ++i)
            {
                if (SFNPC.HealthStates != null)
                {
                    if (hs[i].Image == healthy) { SFNPC.HealthStates[i] = 0; continue; }
                    if (hs[i].Image == bashing) { SFNPC.HealthStates[i] = 1; continue; }
                    if (hs[i].Image == lethal) { SFNPC.HealthStates[i] = 2; continue; }
                    if (hs[i].Image == aggriv) { SFNPC.HealthStates[i] = 3; continue; }
                }
            }
        }

        private void Dot_Click(object sender, EventArgs e)
        {
            PictureBox im = (PictureBox)sender;
            if (im.Image == dotunch) { im.Image = dotch; }
            else { im.Image = dotunch; }
            Unsaved();
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
