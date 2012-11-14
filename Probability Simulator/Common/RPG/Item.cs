using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probability_Simulator.Common.RPG
{
    public class Item 
    {
        private int heal;   //how much hp the item heal
        private Attack damage; //does attack on the enemy

        private int poisonStrength; //0-10  how likely to cause poison
        private int paralyzeStrength;   //0-10 how likely to cause paralyze

        private int poisonSelf; //0-10  how likely to be poisoned
        private int paralyzeSelf;   //0-10 how likely to be paralyzed
    }
}
