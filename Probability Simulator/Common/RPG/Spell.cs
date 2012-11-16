using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probability_Simulator.Common.RPG
{
    public class Spell : Attack
    {
        private int mpCost; //how much mp to use spell

        //Constructors
        public Spell()
            : base()
        {
            mpCost = 0;
        }

        public Spell(string Name, int CritPercent, double MinDamage, double MaxDamage, int MPCost)
            : base(Name, CritPercent, MinDamage, MaxDamage)
        {
            mpCost = MPCost;
        }

        //Get Methods

        public int getMPCost()
        {
            return mpCost;
        }
    }
}
