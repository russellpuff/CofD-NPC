using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using StrEnum = System.Collections.Generic.IEnumerable<string>;

namespace CofD_NPC
{
    public partial class SheetWindow : Window
    {
#nullable disable
        BitmapImage healthy, bashing, lethal, aggrav, blank;
#nullable enable
        const string alpha = "abcdefghijklmnopqrstuvwxyz";
        string portraitPath = "none";
        readonly Dictionary<string, int> skillboxes = new();
        List<DataItem> dataGridItems = new();
        List<NPC> NPCs = new();
        NPC SNPC;
        bool unsaved = false, deactivateDangerousEvents = false; // On form load and clear, some events fuck shit sideways. 

        #region form-load
        public SheetWindow()
        {
            InitializeComponent();
            InitializeImages();
            LoadSBDictionary();
            LoadNPCList();
            sw10AgainRadio.IsChecked = true;

            SNPC = new();
        }
        
        private void InitializeImages()
        {
            healthy = new BitmapImage(new Uri("pack://application:,,,/Resources/healthy_transparent.png"));
            bashing = new BitmapImage(new Uri("pack://application:,,,/Resources/bashing_yellow.png"));
            lethal = new BitmapImage(new Uri("pack://application:,,,/Resources/lethal_yellow.png"));
            aggrav = new BitmapImage(new Uri("pack://application:,,,/Resources/aggravated_yellow.png"));
            blank = new BitmapImage(new Uri("pack://application:,,,/Resources/blank.png"));
            swPortraitImage.Source = blank;
            foreach (UIElement c in swHealthStateGrid.Children)
            {
                if (c.GetType() == typeof(Image))
                {
                    Image im = (Image)c;
                    if (im.Name.Contains("HealthState")) { im.Source = healthy; }
                }
            }
        }

        private void LoadSBDictionary()
        {
            for (int i = 1; i <= 16; ++i) { skillboxes.Add($"swSkillComboBox{i}", 0); }
        }

        private void LoadNPCList()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/NPC/";
            Directory.CreateDirectory(path);

            DirectoryInfo di = new(path);
            foreach (var file in di.GetFiles("*.npc"))
            {
                try
                {
                    NPC n = DecompressFile(file);
                    NPCs.Add(n);
                    dataGridItems.Add(new DataItem() { Name = n.Name, Description = n.Description, ID = n.ID.ToString(), Visible = true });
                }
                catch
                {
                    string m = "File " + file.Name + " was corrupted and could not be loaded.\n";
                    MessageBox.Show(m, "Corrupt file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            swDataGrid.ItemsSource = dataGridItems;
        }
#nullable disable
        private static NPC DecompressFile(FileInfo file)
        {
            using FileStream fs = new(file.FullName, FileMode.Open, FileAccess.Read);
            using MemoryStream ums = new();
            using (DeflateStream ds = new(fs, CompressionMode.Decompress)) { ds.CopyTo(ums); }
            ums.Position = 0;
            XmlSerializer xs = new(typeof(NPC));
            return (NPC)xs.Deserialize(ums);
#nullable enable
        }
        #endregion

        #region save-npc
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        { SaveNPC(); }

        private void SaveToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (deactivateDangerousEvents) { return; }
            if (!SaveNPC())
            {
                ToggleButton tb = (ToggleButton)sender;
                tb.IsChecked = false;
            }
        }

        private bool PromptToSaveChanges()
        { // True means the user handled their decision and wants to proceed. False means they canclled.
            const string m = "You have unsaved changes. Would you like to save them first?";
            var result = MessageBox.Show(m, "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Cancel) { return false; }
            if (result == MessageBoxResult.Yes) { SaveNPC(); }
            return true;
        }

        private bool SaveNPC()
        {
            try
            {
                SNPC.Name = swNameTextBox.Text;
                SNPC.Age = swAgeNumUpDown.Value != null ? (int)swAgeNumUpDown.Value : 1;
                SNPC.Virtue = swVirtueComboBox.Text;
                SNPC.Vice = swViceComboBox.Text;
                SNPC.Concept = swConceptTextBox.Text;
                SNPC.HealthDots = swHealthDotsTop.Value;
                SNPC.WillpowerDots = swWillpowerDotsTop.Value;
                SNPC.Integrity = swIntegrityDotsTop.Value;
                SNPC.Size = swSizeNumUpDown.Value != null ? (int)swSizeNumUpDown.Value : 1;
                SNPC.Defense = swDefenseNumUpDown.Value != null ? (int)swDefenseNumUpDown.Value : 0;
                SNPC.Armor = swArmorTextBox.Text;
                SNPC.Initiative = swInitiativeNumUpDown.Value != null ? (int)swInitiativeNumUpDown.Value : 0;
                SNPC.Description = swDescriptionTextBox.Text;
                SNPC.Intelligence = swIntelligenceDotsTop.Value;
                SNPC.Wits = swWitsDotsTop.Value;
                SNPC.Resolve = swResolveDotsTop.Value;
                SNPC.Strength = swStrengthDotsTop.Value;
                SNPC.Dexterity = swDexterityDotsTop.Value;
                SNPC.Stamina = swStaminaDotsTop.Value;
                SNPC.Presence = swPresenceDotsTop.Value;
                SNPC.Manipulation = swManipulationDotsTop.Value;
                SNPC.Composure = swComposureDotsTop.Value;
                SNPC.Conditions = swConditionsTextBox.Text;
                SNPC.Aspirations = swAspirationsTextBox.Text;
                SNPC.WillpowerCurrent = 0;

                for (int i = 0; i < 16; ++i)
                {
                    // Skills
                    ComboBox c = (ComboBox)swMiddleGrid.FindName($"swSkillComboBox{i + 1}");
                    SNPC.Skills[i] = c.Text;
                    RatingBar r = (RatingBar)swMiddleGrid.FindName($"swSkill{i + 1}DotsTop");
                    SNPC.SkillDots[i] = r.Value;
                    if (i < 10)
                    {
                        // Merits
                        TextBox t = (TextBox)swMiddleGrid.FindName($"swMeritTextBox{i + 1}");
                        SNPC.Merits[i] = t.Text;
                        r = (RatingBar)swMiddleGrid.FindName($"swMerit{i + 1}DotsTop");
                        SNPC.MeritDots[i] = r.Value;

                        // Health states
                        Image im = (Image)swMiddleGrid.FindName($"swHealthState{i + 1}");
                        SNPC.HealthStates[i] = GetHealthStateAsNumber(im);

                        // Willpower current
                        CheckBox x = (CheckBox)swMiddleGrid.FindName($"swWillpowerCheck{i + 1}");
                        if (x.IsChecked == true) { ++SNPC.WillpowerCurrent; }
                    }
                }

                if (swPortraitImage.Source != blank && !portraitPath.Contains("/NPC/"))
                {
                    string[] oldpath = portraitPath.Split('.');
                    string newpath = AppDomain.CurrentDomain.BaseDirectory + "/NPC/" + SNPC.ID.ToString() + ".png";
                    File.Copy(portraitPath, newpath, true);
                }

                CompressFile();
                swSaveToggle.IsChecked = true;
                unsaved = false;
                // If in list, update. Otherwise, add to list.  
                if (!NPCs.Any(n => n.ID == SNPC.ID)) { 
                    NPCs.Add(SNPC); 
                    DataItem d = new() { Name = SNPC.Name, Description = SNPC.Description, ID = SNPC.ID, Visible = true };
                    dataGridItems.Add(d);
                } else
                {
                    int idx = dataGridItems.FindIndex(di => di.ID == SNPC.ID);
                    dataGridItems[idx].Name = SNPC.Name;
                    dataGridItems[idx].Description = SNPC.Description;
                    swDataGrid.Items.Refresh();
                }
                swNPCSearchTextBox.Text = "";
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                //string m = "There was an error trying to save this NPC.\n";
                //m += $"\n\nTry deleting  and then try again.";
                MessageBox.Show(m, "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void CompressFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/NPC/" + SNPC.ID.ToString() + ".npc";
            using MemoryStream ms = new();
            XmlSerializer xs = new(typeof(NPC));
            xs.Serialize(ms, SNPC);
            ms.Position = 0;
            using MemoryStream cms = new();
            using (DeflateStream ds = new(cms, CompressionMode.Compress, true)) { ms.CopyTo(ds); }
            FileStream fs = File.Create(path);
            cms.WriteTo(fs);
            fs.Close();
        }

        private int GetHealthStateAsNumber(Image im)
        {
            if (im.Source == healthy) { return 0; }
            if (im.Source == bashing) { return 1; }
            if (im.Source == lethal) { return 2; }
            return 3;
        }
        #endregion

        #region events
        private void SheetWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (unsaved)
            {
                var result = MessageBox.Show("Do you really want to exit?", "Unsaved changes", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            string m = "New, amazing, fantastic, strange, and even dubious settings to be added uh, later.";
            MessageBox.Show(m, "", MessageBoxButton.OK);
        }

        private void Dots_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            if (deactivateDangerousEvents) { return; }
            SetUnsaved();
            RatingBar rb = (RatingBar)sender;
            string dotname = rb.Name;
            bool istop = false;

            if (dotname.Contains("Top"))
            {
                dotname = dotname.Replace("Top", "");
                dotname += "Bottom";
                istop = true;
            }
            else
            {
                dotname = dotname.Replace("Bottom", "");
                dotname += "Top";
            }

            RatingBar other = (RatingBar)((Grid)rb.Parent).FindName(dotname);
            // Bottom max switch to 9 and 10 again is to interrupt spinning animation.
            // I can't figure out how else to disable it.
#nullable disable
            var cv = new BrushConverter();
            Brush gold = (Brush)cv.ConvertFromString("#FFFFC107");
            // Prevent valuechanged event from firing lol.
            deactivateDangerousEvents = true;
            if (istop)
            {
                rb.Max = rb.Value;
                rb.Foreground = gold;
                other.Max = 9;
                other.Max = 10;
                other.Value = 0;
            }
            else
            {
                other.Max = rb.Value;
                other.Value = rb.Value;
                other.Foreground = gold;
                rb.Max = 9;
                rb.Max = 10;
                rb.Value = 0;
            }
            deactivateDangerousEvents = false;
#nullable enable
        }

        private void SkillComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (deactivateDangerousEvents) { return; }
            ComboBox cb = (ComboBox)sender;
            ComboBoxItem? cbi = null;
#nullable disable // shut up shut up shut up shut up shut
            if (e.RemovedItems.Count > 0) { cbi = (ComboBoxItem)e.RemovedItems[0]; }
#nullable enable
            CBIterator(true, cb, cbi);
            CBIterator(false);
        }

        private void CBIterator(bool firstpass, ComboBox? original = null, ComboBoxItem? prevItem = null)
        { // If (firstpass), compare dupes. Otherwise update styles.
            foreach (Control c in swMiddleGrid.Children)
            {
                if (c.GetType() == typeof(ComboBox))
                {
                    ComboBox other = (ComboBox)c;
                    if (firstpass && original != null)
                    {
                        if (original == other) { continue; }
                        ComboBoxItem orig = (ComboBoxItem)original.SelectedItem;
                        ComboBoxItem oth = (ComboBoxItem)other.SelectedItem;

                        if (oth != null)
                        {
                            if (orig.Content.Equals(oth.Content))
                            {
                                ++skillboxes[original.Name];
                                ++skillboxes[other.Name];
                            }
                            if (prevItem != null && (String)oth.Content == (String)prevItem.Content)
                            {
                                --skillboxes[original.Name];
                                --skillboxes[other.Name];
                            }
                        }
                    } else
                    {
                        string style = (skillboxes[other.Name] > 0) ? "RedComboBox" : "DefaultComboBox";
                        other.Style = FindResource(style) as Style; 
                    }
                }
            }
        }

        private void HealthState_LeftClick(object sender, MouseButtonEventArgs e)
        {
            SetUnsaved();
            Image im = (Image)sender;
            if (im.Source == healthy) { im.Source = bashing; return; }
            if (im.Source == bashing) { im.Source = lethal; return; }
            if (im.Source == lethal) { im.Source = aggrav; return; }
            if (im.Source == aggrav) { im.Source = healthy; return; }
        }

#nullable disable
        private void Rectangle_MouseEnter(object sender, MouseEventArgs e) 
        {
            var cv = new BrushConverter();
            Brush b = (Brush)cv.ConvertFromString("#FFFFC107");
            try
            {
                System.Windows.Shapes.Rectangle rect = (System.Windows.Shapes.Rectangle)sender;
                rect.Stroke = b;
                return;
            }
            catch { }
            try
            {
                Image im = (Image)sender;
                swPortraitRectangle.Stroke = b;
            }
            catch
            {
                swRollSectionRectangle.Stroke = b;
            }
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e) {
            var cv = new BrushConverter();
            Brush b = (Brush)cv.ConvertFromString("#FF707070");
            swPortraitRectangle.Stroke = b;
            swRollSectionRectangle.Stroke = b;
        }
#nullable enable
        private void AddNPCButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            SNPC = new();
        }

        private void ClearForm()
        {
            if (unsaved && !PromptToSaveChanges()) { return; }
            deactivateDangerousEvents = true;
            IEnumerable<Image> images = GetChildren(swMajorGrid).OfType<Image>();
            foreach(Image image in images) { image.Source = healthy; }
            swPortraitImage.Source = blank;
            ClearExternal(swSideGrid);
            foreach (var x in swMainGrid.Children)
            {
                if (x is Grid g) { ClearExternal(g); }
            }
            deactivateDangerousEvents = false;
        }

        private void ClearExternal(Grid g)
        {
            foreach (var c in g.Children)
            {
                if (c is TextBox tb)
                {
                    tb.Text = "";
                    continue;
                }
                if (c is ComboBox cb)
                {
                    cb.Text = "";
                    continue;
                }
                if (c is RatingBar rb)
                {
                    if (rb.Name.Contains("Top"))
                    {
                        rb.Value = 0;
                        rb.Max = 0;
                    }
                    continue;
                }
                if (c is NumericUpDown nd)
                {
                    nd.Value = 0;
                }
                if (c is Grid d)
                {
                    foreach (var t in d.Children)
                    {
                        if (t is CheckBox cx) { cx.IsChecked = false; }
                    }
                }
            }
        }

        private static IEnumerable<Visual> GetChildren(Visual parent, bool recurse = true)
        {
            if (parent != null)
            {
                int count = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count; i++)
                {
                    // Retrieve child visual at specified index value.
                    var child = VisualTreeHelper.GetChild(parent, i) as Visual;

                    if (child != null)
                    {
                        yield return child;

                        if (recurse)
                        {
                            foreach (var grandChild in GetChildren(child, true))
                            {
                                yield return grandChild;
                            }
                        }
                    }
                }
            }
        }

        private void Portrait_Click(object? sender, MouseButtonEventArgs? e)
        {
            if (e != null && e.ClickCount == 2)
            {
                OpenPortraitFile();
                SetUnsaved();
            }
        }

        private void HealthState_RightClick(object sender, MouseButtonEventArgs e)
        {
            Image im = (Image)sender;
            im.Source = healthy;
        }

        private void FullRollButton_Click(object sender, RoutedEventArgs e)
        {
            if (unsaved)
            {
                const string m = "NPC must be saved before Dice Roller can be used. Save now?";
                var result = MessageBox.Show(m, "Dice Roller", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes && SaveNPC()) { } // Just a funky way of checking both at the same time.
                else { return; }
            }

            if (!ValidateSkillsMerits())
            {
                string m = "Detected skills or merits that have dots but no name, or vice versa.\n";
                m += "The die roller breaks if you have any skills or merits that break this rule. There is currently no fix besides ";
                m += "forcing you to manually change it.";
                MessageBox.Show(m, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DiceWindow dw = new(SNPC.Skills, SNPC.SkillDots, SNPC.Merits, SNPC.MeritDots, SNPC.HealthDots,
                SNPC.WillpowerDots, SNPC.Integrity, SNPC.Intelligence, SNPC.Wits, SNPC.Resolve,
                SNPC.Strength, SNPC.Dexterity, SNPC.Stamina, SNPC.Presence, SNPC.Manipulation, SNPC.Composure);
            dw.Show();
        }

        private bool ValidateSkillsMerits()
        {
            for (int i = 0; i < 16; ++i)
            {
                if (string.IsNullOrEmpty(SNPC.Skills[i]) && (SNPC.SkillDots[i] > 0)) { return false; }
                if (!string.IsNullOrEmpty(SNPC.Skills[i]) && (SNPC.SkillDots[i] == 0)) { return false; }
                if (i < 10)
                {
                    if (string.IsNullOrEmpty(SNPC.Merits[i]) && (SNPC.MeritDots[i] > 0)) { return false; }
                    if (!string.IsNullOrEmpty(SNPC.Merits[i]) && (SNPC.MeritDots[i] == 0)) { return false; }
                }
            }
            return true;
        }
#nullable disable
        // Event fires to warn user of unsaved changed (if applicable) before quick loading
        // next npc. Gives them a chance to atone for their sins and cancel.
        private void DataGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //Get the item being clicked on.
            var myItem = (e.OriginalSource as FrameworkElement).DataContext as DataItem;
            //Check if item is different from currently selected item
            if (myItem != swDataGrid.SelectedItem)
            {
                if (unsaved && !PromptToSaveChanges()) { e.Handled = true; }
                else
                {
                    // Reselect selected item if saved, which flushes that.
                    swDataGrid.SelectedItem = myItem;
                    // Reinvoke event either way.
                    swDataGrid.Dispatcher.BeginInvoke(
                       new Action(() =>
                       {
                           RoutedEventArgs args = new MouseButtonEventArgs(e.MouseDevice, 0, e.ChangedButton);
                           args.RoutedEvent = UIElement.MouseDownEvent;
                           (e.OriginalSource as UIElement).RaiseEvent(args);
                       }),
                       System.Windows.Threading.DispatcherPriority.Input);
                }
            }
        }
#nullable enable

        // Sheet "quick load" function.
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deactivateDangerousEvents = true;

            int idx = swDataGrid.SelectedIndex;
            if (idx == -1) { return; }
            SNPC = NPCs[idx];

            string path = AppDomain.CurrentDomain.BaseDirectory + "/NPC/" + SNPC.ID + ".png";
            if (File.Exists(path)) { swPortraitImage.Source = LoadImage(path); } else
            {
                swPortraitImage.Source = blank;
                portraitPath = "none";
            }

            swNameTextBox.Text = SNPC.Name;
            swAgeNumUpDown.Value = SNPC.Age;
            swVirtueComboBox.Text = SNPC.Virtue;
            swViceComboBox.Text = SNPC.Vice;
            swConceptTextBox.Text = SNPC.Concept;
            swHealthDotsTop.Max = SNPC.HealthDots;
            swHealthDotsTop.Value = SNPC.HealthDots;
            swWillpowerDotsTop.Max = SNPC.WillpowerDots;
            swWillpowerDotsTop.Value = SNPC.WillpowerDots;
            swIntegrityDotsTop.Max = SNPC.Integrity;
            swIntegrityDotsTop.Value = SNPC.Integrity;
            swSizeNumUpDown.Value = SNPC.Size;
            swSpeedNumUpDown.Value = SNPC.Speed;
            swDefenseNumUpDown.Value = SNPC.Defense;
            swArmorTextBox.Text = SNPC.Armor;
            swInitiativeNumUpDown.Value = SNPC.Initiative;
            swDescriptionTextBox.Text = SNPC.Description;
            swIntelligenceDotsTop.Max = SNPC.Intelligence;
            swIntelligenceDotsTop.Value = SNPC.Intelligence;
            swWitsDotsTop.Max = SNPC.Wits;
            swWitsDotsTop.Value = SNPC.Wits;
            swResolveDotsTop.Max = SNPC.Resolve;
            swResolveDotsTop.Value = SNPC.Resolve;
            swStrengthDotsTop.Max = SNPC.Strength;
            swStrengthDotsTop.Value = SNPC.Strength;
            swDexterityDotsTop.Max = SNPC.Dexterity;
            swDexterityDotsTop.Value = SNPC.Dexterity;
            swStaminaDotsTop.Max = SNPC.Stamina;
            swStaminaDotsTop.Value = SNPC.Stamina;
            swPresenceDotsTop.Max = SNPC.Presence;
            swPresenceDotsTop.Value = SNPC.Presence;
            swManipulationDotsTop.Max = SNPC.Manipulation;
            swManipulationDotsTop.Value = SNPC.Manipulation;
            swComposureDotsTop.Max = SNPC.Composure;
            swComposureDotsTop.Value = SNPC.Composure;
            swConditionsTextBox.Text = SNPC.Conditions;
            swAspirationsTextBox.Text = SNPC.Aspirations;
            for (int i = 0; i < 16; ++i)
            {
                // Skills
                ComboBox c = (ComboBox)swMiddleGrid.FindName($"swSkillComboBox{i + 1}");
                c.Text = SNPC.Skills[i];
                RatingBar r = (RatingBar)swMiddleGrid.FindName($"swSkill{i + 1}DotsTop");
                r.Max = SNPC.SkillDots[i];
                r.Value = SNPC.SkillDots[i];
                if (i < 10)
                {
                    // Merits
                    TextBox t = (TextBox)swMiddleGrid.FindName($"swMeritTextBox{i + 1}");
                    t.Text = SNPC.Merits[i];
                    r = (RatingBar)swMiddleGrid.FindName($"swMerit{i + 1}DotsTop");
                    r.Max = SNPC.MeritDots[i];
                    r.Value = SNPC.MeritDots[i];

                    // Health states
                    Image im = (Image)swMiddleGrid.FindName($"swHealthState{i + 1}");
                    switch (SNPC.HealthStates[i])
                    {
                        case 0: im.Source = healthy; break;
                        case 1: im.Source = bashing; break;
                        case 2: im.Source = lethal; break;
                        case 3: im.Source = aggrav; break;
                    }
                }
                for (int j = 1; j <= SNPC.WillpowerCurrent; ++j)
                {
                    CheckBox x = (CheckBox)swMiddleGrid.FindName($"swWillpowerCheck{j}");
                    x.IsChecked = true;
                }
            }
            deactivateDangerousEvents = false;
            unsaved = false;
            swSaveToggle.IsChecked = true;
        }

#nullable disable // Piss off and die. It's not fucking possible for these to be null.
        private void QuickRollButton_Click(object sender, RoutedEventArgs e)
        {
            int die = (int)swQuickRollModNumUpDown.Value;
            bool rote = (bool)swRoteToggleButton.IsChecked;
            int again = 10;
            if ((bool)sw9AgainRadio.IsChecked) { again = 9; }
            if ((bool)sw9AgainRadio.IsChecked) { again = 8; }

            DieRoller dr = new(die, rote, again);
            dr.Roll();
        }
#nullable enable

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            DisplaySearchResults();
        }

        private void OpenPortraitFile()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            { // I copied these filters from the internet. What the fuck is .jfif?
                FileName = "Image",
                DefaultExt = ".png",
                Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif" +
            "|PNG Portable Network Graphics (*.png)|*.png" +
            "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
            "|BMP Windows Bitmap (*.bmp)|*.bmp"
            };

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                swPortraitImage.Source = LoadImage(dialog.FileName);
                SetUnsaved();
            }
        }

        private BitmapImage LoadImage(string path)
        {
            portraitPath = path;
            BitmapImage? myRetVal = null;
            BitmapImage image = new BitmapImage();
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }
            myRetVal = image;
            return myRetVal;
        }

        #endregion

        #region context-menu
        private void Save_OnClick(object sender, RoutedEventArgs e) { _ = SaveNPC(); }

        private void ChooseImage_OnClick(object sender, RoutedEventArgs e) {
            OpenPortraitFile();
            SetUnsaved();
        }

        private void RemoveImage_OnClick(object sender, RoutedEventArgs e)
        {
            swPortraitImage.Source = blank;
            portraitPath = "none";
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            int idx = swDataGrid.SelectedIndex;
            if (idx == -1) { return; }
            string m = "Are you sure you want to delete " + SNPC.Name + "?";
            var result = MessageBox.Show(m, "Delete NPC?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No) { return; }
            NPCs.RemoveAll(x => x.ID == SNPC.ID);
            dataGridItems.RemoveAll(x => x.ID == SNPC.ID);
            swDataGrid.SelectedIndex = 0;
            string path = AppDomain.CurrentDomain.BaseDirectory + "/NPC/" + SNPC.ID + ".npc";
            File.Delete(path);
            path = AppDomain.CurrentDomain.BaseDirectory + "/NPC/" + SNPC.ID + ".png";
            if (File.Exists(path)) { File.Delete(path); }
            swDataGrid.Items.Refresh();
        }

        private void Clear_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            ContextMenu cm = (ContextMenu)mi.CommandParameter;
            if (cm.PlacementTarget is TextBox tb)
            {
                tb.Text = "";
            }
            if (cm.PlacementTarget is ComboBox cb)
            {
                deactivateDangerousEvents = true;
                cb.SelectedIndex = -1;
                deactivateDangerousEvents = false;
            }
            if (cm.PlacementTarget is RatingBar rb)
            {
                deactivateDangerousEvents = true;
                string dotname = rb.Name;
                bool istop = false;

                if (dotname.Contains("Top"))
                {
                    dotname = dotname.Replace("Top", "");
                    dotname += "Bottom";
                    istop = true;
                }
                else
                {
                    dotname = dotname.Replace("Bottom", "");
                    dotname += "Top";
                }
                RatingBar other = (RatingBar)((Grid)rb.Parent).FindName(dotname);

                if (istop)
                {
                    other.Max = 9;
                    other.Max = 10;
                    other.Value = 0;
                    rb.Max = 0;
                }
                else
                {
                    rb.Max = 9;
                    rb.Max = 10;
                    rb.Value = 0;
                    other.Max = 0;
                    other.Value = 0;
                }
                deactivateDangerousEvents = false;

            }
            if (cm.PlacementTarget is NumericUpDown nd)
            {

            }
        }

        private void RollThis_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void RollInit_OnClick(object sender, RoutedEventArgs e)
        {
            Random r = new();
            int d = r.Next(1, 10);
            int i = swDexterityDotsTop.Value + swComposureDotsTop.Value;
            string m = $"Rolled: {d}\nMod: {i}\nTotal: {d + i}";
            MessageBox.Show(m, "Initiative", MessageBoxButton.OK);
        }
        #endregion

        #region unsaved-jumble
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        { SetUnsaved();}

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        { SetUnsaved(); }

        private void NumUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        { SetUnsaved(); }

        private void ComboBox_LostFocus(object sender, RoutedEventArgs e)
        { SetUnsaved(); }

        private void SetUnsaved()
        {
            unsaved = true;
            swSaveToggle.IsChecked = false;
        }
        #endregion

        #region Peter-Norvig-spellcheck
#nullable disable
        private void DisplaySearchResults()
        {
            try
            {
                string[] args = swNPCSearchTextBox.Text.Split(' ');
                args[0] = args[0].ToLower();

                string items = "";
                foreach (DataItem d in dataGridItems) { items += d.Name + " "; }

                var nWords = (from Match m in Regex.Matches(items.ToLower(), "[a-z]+")
                              group m.Value by m.Value)
                             .ToDictionary(gr => gr.Key, gr => gr.Count());

                Func<StrEnum, StrEnum> nullIfEmpty = c => c.Any() ? c : null;

                var candidates =
                    nullIfEmpty(new[] { args[0] }.Where(nWords.ContainsKey))
                    ?? nullIfEmpty(Edits(args[0]).Where(nWords.ContainsKey))
                    ?? nullIfEmpty((from e1 in Edits(args[0])
                                    from e2 in Edits(e1)
                                    where nWords.ContainsKey(e2)
                                    select e2).Distinct());

                IOrderedEnumerable<string> suggestions = args.OrderBy(ar => ar.Length); // Nonsense to avoid exception.
                if (candidates != null)
                {
                    suggestions = (from cand in candidates
                                   orderby (nWords.ContainsKey(cand) ? nWords[cand] : 1)
                                   descending
                                   select cand);
                }
                foreach (DataItem d in dataGridItems)
                {
                    if (d.Name.ToLower().Contains(args[0]) || (candidates != null & suggestions.Contains(d.Name)))
                    {
                        d.Visible = true;
                    }
                    else
                    {
                        d.Visible = false;
                    }
                }
            }
            catch // Deletion fucking dies if it has to check less than 1 character. 
            {
                foreach (DataItem d in dataGridItems) { d.Visible = true; }
            }
            //swDataGrid.Items.Refresh();
        }
#nullable enable
        static StrEnum Edits(string w)
        {
            // Deletion
#pragma warning disable IDE0057 // Use range operator
            return (from i in Enumerable.Range(0, w.Length)
                    select w.Substring(0, i) + w.Substring(i + 1))
             // Transposition
             .Union(from i in Enumerable.Range(0, w.Length - 1)
                    select w.Substring(0, i) + w.Substring(i + 1, 1) +
                           w.Substring(i, 1) + w.Substring(i + 2))
             // Alteration
             .Union(from i in Enumerable.Range(0, w.Length)
                    from c in alpha
                    select w.Substring(0, i) + c + w.Substring(i + 1))
             // Insertion
             .Union(from i in Enumerable.Range(0, w.Length + 1)
                    from c in alpha
                    select w.Substring(0, i) + c + w.Substring(i));
#pragma warning restore IDE0057 // Use range operator

        }
        #endregion
    }
}