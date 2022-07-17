// File provides definitions for the NPC and DieRoller classes. 

namespace CofD_NPC
{
    public class DieRoller
    {
        public DieRoller(int d, bool r, int RA)
        {
            dice = d;
            rote = r;
            rollAgain = (Again)RA;
        }
        private enum Again
        {
            TenAgain = 10,
            NineAgain = 9,
            EightAgain = 8
        }
        private readonly int dice = 0;
        private readonly bool rote = false;
        private readonly Again rollAgain = 0;

        public void Roll()
        {
            var random = new Random();
            List<int> rolls = new();
            List<int> arolls = new();
            string m = "Dice rolled: [";
            int success = 0;

            for (int i = 1; i <= dice; i++) { rolls.Add(random.Next(1, 10)); }

            foreach (int r in rolls)
            {
                if (r >= (int)rollAgain) { arolls.Add(random.Next(1, 10)); }
                else if (rote && r < 8) { arolls.Add(random.Next(1, 10)); }
            }
            rolls.AddRange(arolls); // Can't edit rolls in a foreach.

            foreach (int r in rolls)
            {
                m += r.ToString() + ", ";
                if (r >= 8) { ++success; }
            }

            m = m[..^2]; // Substring, remove last comma and space.
            m += "]\n\nSuccesses: " + success.ToString();

            MessageBox.Show(m, "Dice Roll", MessageBoxButtons.OK);
        }
    }
}