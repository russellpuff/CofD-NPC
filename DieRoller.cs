﻿// File provides definitions for the NPC and DieRoller classes. 

using System;
using System.Collections.Generic;
using System.Windows;

namespace CofD_NPC
{
    public class DieRoller
    {
        public DieRoller(int d, bool r, int RA)
        {
            dice = d;
            rote = r;
            rollAgain = (Again)RA;
            chance = (dice <= 0);
            if (chance) { ++dice; }
        }
        private enum Again
        {
            TenAgain = 10,
            NineAgain = 9,
            EightAgain = 8
        }
        private readonly int dice = 0;
        private readonly bool rote;
        private readonly bool chance;
        private readonly Again rollAgain = 0;

        public void Roll()
        {
            var random = new Random();
            List<int> rolls = new();
            List<int> arolls = new();
            string m = chance ? "Chance die" : "Dice";
            m+= " rolled: [";
            int success = 0;

            for (int i = 1; i <= dice; i++) { rolls.Add(random.Next(1, 10)); }
            
            // If a chance die rolls a 1, it does not get rerolled.
            // Chance dice do not benefit from the rollAgain property.
            if (!chance || (chance && (rolls[0] != 1)))
            {
                foreach (int r in rolls)
                {
                    if (((r >= (int)rollAgain) && !chance) || (rote && (r < 8))) { arolls.Add(random.Next(1, 10)); }
                }
                rolls.AddRange(arolls); // Can't edit rolls in a foreach.
            }
            
            foreach (int r in rolls)
            {
                m += r.ToString() + ", ";
                if (((r >= 8) && !chance) || (chance && (r == 10))) { ++success; }
            }

            m = m[..^2]; // Substring, remove last comma and space.
            m += "]\n\nSuccesses: " + success.ToString();

            MessageBox.Show(m, "Dice Roll", MessageBoxButton.OK);
        }
    }
}