using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
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
        BitmapImage healthy, bashing, lethal, aggriv;
#nullable enable
        const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        readonly Dictionary<string, int> skillboxes = new();
        List<DataItem> dataGridItems = new();
        List<NPC> NPCs = new();
        NPC SFNPC;


        public SheetWindow()
        {
            InitializeComponent();
            InitializeImages();
            LoadSBDictionary();
            LoadNPCList();
            sw10AgainRadio.IsChecked = true;
        }

        private void InitializeImages()
        {
            healthy = new BitmapImage(new Uri("pack://application:,,,/Resources/healthy_transparent.png"));
            bashing = new BitmapImage(new Uri("pack://application:,,,/Resources/bashing_yellow.png"));
            lethal = new BitmapImage(new Uri("pack://application:,,,/Resources/lethal_yellow.png"));
            aggriv = new BitmapImage(new Uri("pack://application:,,,/Resources/aggrivated_yellow.png"));
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
                    dataGridItems.Add(new DataItem() { Name = n.Name, Description = n.Description, ID = n.ID.ToString() });
                }
                catch
                {
                    string m = "File " + file.Name + " was corrupted and could not be loaded.\n";
                    MessageBox.Show(m, "Corrupt file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            swDataGrid.ItemsSource = dataGridItems;
            SetAllDataGridCellStyles();
            swDataGrid.Items.Refresh();
        }

        private static NPC DecompressFile(FileInfo file)
        {
            using FileStream fs = new(file.FullName, FileMode.Open, FileAccess.Read);
            using MemoryStream ums = new();
            using (DeflateStream ds = new(fs, CompressionMode.Decompress)) { ds.CopyTo(ums); }
            ums.Position = 0;
            XmlSerializer xs = new(typeof(NPC));
            return (NPC)xs.Deserialize(ums);
        }

        private void Main_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) { }

        private void Dots_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
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
            rb.ValueChanged -= Dots_ValueChanged;
            other.ValueChanged -= Dots_ValueChanged;
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
            rb.ValueChanged += Dots_ValueChanged;
            other.ValueChanged += Dots_ValueChanged;
#nullable enable
        }

        private void SkillComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

        private void HealthState_Click(object sender, MouseButtonEventArgs e)
        {
            Image im = (Image)sender;
            if (im.Source == healthy) { im.Source = bashing; return; }
            if (im.Source == bashing) { im.Source = lethal; return; }
            if (im.Source == lethal) { im.Source = aggriv; return; }
            if (im.Source == aggriv) { im.Source = healthy; return; }
        }

        private void DataGridContextMenu_Opened(object sender, EventArgs e)
        {

        }

        private void DataGridMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

#nullable disable
        private void RollRectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            var cv = new BrushConverter();
            swRollSectionRectangle.Stroke = (Brush)cv.ConvertFromString("#FFFFC107");
        }

        private void RollRectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            var cv = new BrushConverter();
            swRollSectionRectangle.Stroke = (Brush)cv.ConvertFromString("#FF707070");
        }

        private void AddNPCButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SetAllDataGridCellStyles()
        {
            /*
            foreach (DataGridCell cell in swDataGrid.Items)
            {
                cell.Style = FindResource("DataGridCellNoFocus") as Style;
            }*/
        }


        #region 
        static StrEnum Edits(string w)
        {
            // Deletion
            return (from i in Enumerable.Range(0, w.Length)
                    select w.Substring(0, i) + w.Substring(i + 1))
             // Transposition
             .Union(from i in Enumerable.Range(0, w.Length - 1)
                    select w.Substring(0, i) + w.Substring(i + 1, 1) +
                           w.Substring(i, 1) + w.Substring(i + 2))
             // Alteration
             .Union(from i in Enumerable.Range(0, w.Length)
                    from c in Alphabet
                    select w.Substring(0, i) + c + w.Substring(i + 1))
             // Insertion
             .Union(from i in Enumerable.Range(0, w.Length + 1)
                    from c in Alphabet
                    select w.Substring(0, i) + c + w.Substring(i));
        }

        static void OutputIsThisMethod(string[] args)
        {
            args = new string[1];
            args[0] = Console.ReadLine();
            var nWords = (from Match m in Regex.Matches(File.ReadAllText("big.txt").ToLower(), "[a-z]+")
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

            Console.WriteLine(
                candidates == null ? args[0]
                    : (from cand in candidates orderby (nWords.ContainsKey(cand) ? nWords[cand] : 1) descending select cand).First());
        }
        #endregion
    }
}
