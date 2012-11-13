using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probability_Simulator.Common
{
    public class Monster : Character
    {
        private int numAttacks = 0;
        private Attack[] moveList;

        //Constructors
        public Monster()
            : base()
        {
            numAttacks = 1;
            moveList = new Attack[numAttacks];
            for (int i = 0; i < numAttacks; i++)
            {
                moveList[i] = new Attack();
            }
        }

        public Monster(string Name, int HP, int MP, int NumAttacks)
            : base(Name, HP, MP)
        {
            numAttacks = NumAttacks;
            moveList = new Attack[numAttacks];
            for (int i = 0; i < numAttacks; i++)
            {
                moveList[i] = new Attack();
            }
        }

        public Monster(string Name, int HP, int MP, int NumAttacks, int PoisonResist, int ParalyzeResist)
            : base(Name, HP, MP, PoisonResist, ParalyzeResist)
        {
            numAttacks = NumAttacks;
            moveList = new Attack[numAttacks];
            for (int i = 0; i < numAttacks; i++)
            {
                moveList[i] = new Attack();
            }
        }

        //Get Methods
        public int getNumAttacks()
        {
            return numAttacks;
        }

        public Attack[] getMoveList()
        {
            return moveList;
        }
    }
}
