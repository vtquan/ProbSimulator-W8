using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probability_Simulator.Common.RPG
{
    public class Monster : Character
    {
        string subtitle;    //monster subtitle
        private int numAttacks = 0; //number of attacks that the monster has
        private Attack[] moveList;  //list of moves that the monster has

        //Constructors
        public Monster()
            : base()
        {
            subtitle = "Boss";
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
            subtitle = "Boss";
            numAttacks = NumAttacks;
            moveList = new Attack[numAttacks];
            for (int i = 0; i < numAttacks; i++)
            {
                moveList[i] = new Attack();
            }
        }

        public Monster(string Name, string Subtitle, int HP, int MP, int NumAttacks)
            : base(Name, HP, MP)
        {
            subtitle = Subtitle;
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
            subtitle = "Boss";
            numAttacks = NumAttacks;
            moveList = new Attack[numAttacks];
            for (int i = 0; i < numAttacks; i++)
            {
                moveList[i] = new Attack();
            }
        }

        public Monster(string Name, string Subtitle, int HP, int MP, int NumAttacks, int PoisonResist, int ParalyzeResist)
            : base(Name, HP, MP, PoisonResist, ParalyzeResist)
        {
            subtitle = Subtitle;
            numAttacks = NumAttacks;
            moveList = new Attack[numAttacks];
            for (int i = 0; i < numAttacks; i++)
            {
                moveList[i] = new Attack();
            }
        }

        //Get Methods
        public string getSubtitle()
        {
            return subtitle;
        }

        public int getNumAttacks()
        {
            return numAttacks;
        }

        public Attack[] getMoveList()
        {
            return moveList;
        }

        //Action Methods
        public KeyValuePair<double,int> attack(ref Player Target)
        {
            Random random = new Random();

            //Calculate Initial Damage
            int moveChosen = random.Next(0, numAttacks); //choose from one of the possible attacks
            double damage = random.Next((int)moveList[moveChosen].getMinDamage(), (int)moveList[moveChosen].getMaxDamage() + 1);

            //Crit or not
            bool crit;
            crit = (moveList[moveChosen].getCritPercent() >= random.Next(1, 11));

            //Calculate final damage
            if (crit)
            {
                damage = damage * 1.5;
            }

            Target.setHP(Target.getHP() - (int)damage);

            KeyValuePair<double,int> result = new KeyValuePair<double,int>(damage, moveChosen);
            return result;
        }

        public KeyValuePair<double, int> attackDefended(ref Player Target)  //for when attacking a defended target
        {
            Random random = new Random();

            //Calculate Initial Damage
            int moveChosen = random.Next(0, numAttacks); //choose from one of the possible attacks
            double damage = random.Next((int)moveList[moveChosen].getMinDamage(), (int)moveList[moveChosen].getMaxDamage() + 1);

            //Crit or not
            bool crit;
            crit = (moveList[moveChosen].getCritPercent() >= random.Next(1, 11));

            //Calculate final damage
            if (crit)
            {
                damage = damage * 1.5;
            }
            damage = damage / 2;

            Target.setHP(Target.getHP() - (int)damage);

            KeyValuePair<double, int> result = new KeyValuePair<double, int>(damage, moveChosen);
            return result;
        }
    }
}
