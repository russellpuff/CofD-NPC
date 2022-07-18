using System.Xml.Serialization;

namespace CofD_NPC
{
    [Serializable]
    [XmlRoot("NPCRoot")]
    public class NPC
    {
        [XmlAttribute("ID")]
        public long ID { get; set; }

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
            ID = DateTime.Now.Ticks;
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
            Skills = new string[12] { "", "", "", "", "", "", "", "", "", "", "", "" };
            SkillDots = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Merits = new string[8] { "", "", "", "", "", "", "", "" };
            MeritDots = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            Conditions = "";
            Aspirations = ""; 
        }
    
    }
}