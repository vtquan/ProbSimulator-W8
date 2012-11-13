using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probability_Simulator.Common
{
    public class Player : Character
    {
        private int numSpells = 0;
        private Attack[] spellList;

        //Constructors
        public Player()
            : base()
        {
            numSpells = 1;
            spellList = new Attack[numSpells];
            for (int i = 0; i < numSpells; i++)
            {
                spellList[i] = new Attack();
            }
        }

        public Player(string Name, int HP, int MP, int NumAttacks)
            : base(Name, HP, MP)
        {
            numSpells = NumAttacks;
            spellList = new Attack[numSpells];
            for (int i = 0; i < numSpells; i++)
            {
                spellList[i] = new Attack();
            }
        }

        public Player(string Name, int HP, int MP, int NumAttacks, int PoisonResist, int ParalyzeResist)
            : base(Name, HP, MP, PoisonResist, ParalyzeResist)
        {
            numSpells = NumAttacks;
            spellList = new Attack[numSpells];
            for (int i = 0; i < numSpells; i++)
            {
                spellList[i] = new Attack();
            }
        }

        //Get Methods
        public int getNumAttacks()
        {
            return numSpells;
        }

        public Attack[] getMoveList()
        {
            return spellList;
        }
    }
}
