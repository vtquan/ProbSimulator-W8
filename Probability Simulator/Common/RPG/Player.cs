using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probability_Simulator.Common.RPG
{
    public class Player : Character
    {
        Attack defaultAttack;   //default attack when pressing the attack button
        private int numSpells = 0;
        private Attack[] spellList;

        //Constructors
        public Player()
            : base()
        {
            defaultAttack = new Attack(1, 1, 15);
            numSpells = 1;
            spellList = new Attack[numSpells];
            for (int i = 0; i < numSpells; i++)
            {
                spellList[i] = new Attack();
            }
        }

        public Player(string Name, int HP, int MP, int NumSpells)
            : base(Name, HP, MP)
        {
            defaultAttack = new Attack(1, 1, 15);
            numSpells = NumSpells;
            spellList = new Attack[numSpells];
            for (int i = 0; i < numSpells; i++)
            {
                spellList[i] = new Attack();
            }
        }

        public Player(string Name, int HP, int MP, int NumSpells, int PoisonResist, int ParalyzeResist)
            : base(Name, HP, MP, PoisonResist, ParalyzeResist)
        {
            defaultAttack = new Attack(1, 1, 15);
            numSpells = NumSpells;
            spellList = new Attack[numSpells];
            for (int i = 0; i < numSpells; i++)
            {
                spellList[i] = new Attack();
            }
        }

        //Get Methods
        public int getNumSpells()
        {
            return numSpells;
        }

        public Attack[] getMoveList()
        {
            return spellList;
        }

        //Action Methods
        public double attack(ref Monster Target)
        {
            Random random = new Random();

            //Calculate Initial Damage
            double damage = random.Next((int)defaultAttack.getMinDamage(), (int)defaultAttack.getMaxDamage() + 1);

            //Crit or not
            bool crit;
            crit = (defaultAttack.getCritPercent() >= random.Next(1, 11));

            //Calculate final damage
            if (crit)
            {
                damage = damage * 1.5;
            }

            Target.setHP(Target.getHP() - (int)damage);

            return damage;
        }
    }
}
