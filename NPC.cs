using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace CofD_NPC
{
    [Serializable]
    [XmlRoot("NPCRoot")]
    public class NPC
    {
        [XmlAttribute("ID")]
        public string ID { get; set; }

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

        [XmlAttribute("HealthStates")]
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

        [XmlAttribute("Skills")]
        public string[] Skills { get; set; }

        [XmlAttribute("SkillDots")]
        public int[] SkillDots { get; set; }

        [XmlAttribute("Merits")]
        public string[] Merits { get; set; }

        [XmlAttribute("MeritDots")]
        public int[] MeritDots { get; set; }

        [XmlAttribute("Conditions")]
        public string Conditions { get; set; }

        [XmlAttribute("Aspirations")]
        public string Aspirations { get; set; }

        public NPC()
        {
            ID = Guid.NewGuid().ToString();
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

    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public class DataItem : PropertyChangedBase
    {
        private string? _name;
        public string? Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        private string? _description;
        public string? Description
        {
            get => _description;
            set => SetField(ref _description, value);
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
    }
}