using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace CofD_Sheet
{
    [XmlRoot("CharacterRoot")]
    public class Character
    {
        [XmlAttribute("ID")]
        public string ID { get; set; }

        [XmlAttribute("Type")]
        public TemplateType Type { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Age")]
        public int Age { get; set; }

        [XmlAttribute("Virtue")]
        public string Virtue { get; set; }

        [XmlAttribute("Vice")]
        public string Vice { get; set; }

        [XmlAttribute("Concept")]
        public string Concept { get; set; }

        [XmlAttribute("HealthDots")]
        public int HealthDots { get; set; }

        [XmlArray("HealthStates")]
        public int[] HealthStates { get; set; }

        [XmlAttribute("WillpowerDots")]
        public int WillpowerDots { get; set; }

        [XmlAttribute("WillpowerCurrent")]
        public int WillpowerCurrent { get; set; }

        [XmlAttribute("Integrity")]
        public int Integrity { get; set; }

        [XmlAttribute("Size")]
        public int Size { get; set; }

        [XmlAttribute("Speed")]
        public int Speed { get; set; }

        [XmlAttribute("Defense")]
        public int Defense { get; set; }

        [XmlAttribute("Armor")]
        public string Armor { get; set; }

        [XmlAttribute("Initiative")]
        public int Initiative { get; set; }

        [XmlAttribute("Description")]
        public string Description { get; set; }

        [XmlAttribute("Intelligence")]
        public int Intelligence { get; set; }

        [XmlAttribute("Wits")]
        public int Wits { get; set; }

        [XmlAttribute("Resolve")]
        public int Resolve { get; set; }

        [XmlAttribute("Strength")]
        public int Strength { get; set; }

        [XmlAttribute("Dexterity")]
        public int Dexterity { get; set; }

        [XmlAttribute("Stamina")]
        public int Stamina { get; set; }

        [XmlAttribute("Presence")]
        public int Presence { get; set; }

        [XmlAttribute("Manipulation")]
        public int Manipulation { get; set; }

        [XmlAttribute("Composure")]
        public int Composure { get; set; }

        [XmlArray("Skills")]
        public string[] Skills { get; set; }

        [XmlArray("SkillDots")]
        public int[] SkillDots { get; set; }

        [XmlArray("Merits")]
        public string[] Merits { get; set; }

        [XmlArray("MeritDots")]
        public int[] MeritDots { get; set; }

        [XmlAttribute("Conditions")]
        public string Conditions { get; set; }

        [XmlAttribute("Aspirations")]
        public string Aspirations { get; set; }

        public Character()
        {
            ID = Guid.NewGuid().ToString();
            Type = TemplateType.NPC;
            Name = "";
            Age = 0;
            Virtue = "";
            Vice = "";
            Concept = "";
            HealthDots = 0;
            HealthStates = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            WillpowerDots = 0;
            WillpowerCurrent = 0;
            Integrity = 0;
            Size = 0;
            Speed = 0;
            Defense = 0;
            Armor = "";
            Initiative = 0;
            Description = "";
            Intelligence = 0;
            Wits = 0;
            Resolve = 0;
            Strength = 0;
            Dexterity = 0;
            Stamina = 0;
            Presence = 0;
            Manipulation = 0;
            Composure = 0;
            Skills = new string[16] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            SkillDots = new int[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Merits = new string[10] { "", "", "", "", "", "", "", "", "", "" };
            MeritDots = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Conditions = "";
            Aspirations = "";
        }
    
    }
    // I am of the major stupid and have no idea what any of this does. 
    // I very cleary just copied it from someone else's suggestion. 
    // Based on context clues this appears to be necessary to bind the DataItem list to the CharactersDataGrid.
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public enum TemplateType
    {
        NPC,
        Mortal,
        Ephemeral,
        Vampire,
        Mage,
        Werewolf
    }

    public class DataItem : PropertyChangedBase, IComparable<DataItem>
    {
        private string? _name;
        public string? Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        private BitmapImage? _image;
        public BitmapImage? Image
        {
            get => _image; 
            set => SetField(ref _image, value);
        }

        private string? _typeTooltip;
        public string? TypeToolTip
        {
            get => _typeTooltip;
            set => SetField(ref _typeTooltip, value);
        }

        public int TypeSortValue
        {
            get
            {
                return _typeTooltip switch
                {
                    "NPC" => 1,
                    "Mortal" => 2,
                    "Ephemeral" => 3,
                    "Vampire" => 4,
                    "Mage" => 5,
                    "Werewolf" => 6,
                    _ => 0,
                };
            }
        }

        private string? _id;
        public string? ID
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        private bool? _visible;
        public bool? Visible
        {
            get => _visible;
            set => SetField(ref _visible, value);
        }

        public int CompareTo(DataItem? other)
        {
            if (other == null) return 0;
            return TypeSortValue.CompareTo(other.TypeSortValue);
        }
    }

    // A custom version of the Expander control that fires an event when the ActualHeight property is changed.
    public class TrackedExpander : Expander
    {
        public event EventHandler? ActualHeightChanged;

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            if (sizeInfo.HeightChanged)
            {
                ActualHeightChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}