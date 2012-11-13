using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probability_Simulator.Common
{
    public class Character
    {
        string name;    //character name
        int hpStart;    //starting hp
        int mpStart;    //starting mp
        int hp;
        int mp;
        int poisonResist;   //value between 0-10, if character roll above resist, character is not poisoned
        int paralyzeResist; //value between 0-10, if character roll above resist, character is not paralyzed

        public Character()
        {
            name = "Character";
            hpStart = 1;
            mpStart = 1;
            hp = hpStart;
            mp = mpStart;
            poisonResist = 0;
            paralyzeResist = 0;
        }

        public Character(string Name)
        {
            name = Name;
            hpStart = 1;
            mpStart = 1;
            hp = hpStart;
            mp = mpStart;
            poisonResist = 0;
            paralyzeResist = 0;
        }

        public Character(string Name, int Health, int Mana)
        {
            name = Name;
            hpStart = Health;
            mpStart = Mana;
            hp = hpStart;
            mp = mpStart;
            poisonResist = 0;
            paralyzeResist = 0;
        }

        public Character(string Name, int Health, int Mana, int PoisonResist, int ParalyzeResist)
        {
            name = Name;
            hpStart = Health;
            mpStart = Mana;
            hp = hpStart;
            mp = mpStart;
            poisonResist = PoisonResist;
            paralyzeResist = ParalyzeResist;
        }

        //Get Methods
        public string getName()
        {
            return name;
        }

        public int getHPStart()
        {
            return hpStart;
        }

        public int getMPStart()
        {
            return mpStart;
        }

        public int getHP()
        {
            return hp;
        }

        public int getMP()
        {
            return mp;
        }

        public int getPoisonResist()
        {
            return poisonResist;
        }

        public int getParalyzeResist()
        {
            return paralyzeResist;
        }

        //Set Methods
        public void setName(string Name)
        {
            this.name = Name;
        }

        public void setHPStart(int HPStart)
        {
            hpStart = HPStart;
        }

        public void setMPStart(int MPStart)
        {
            mpStart = MPStart;
        }

        public void setHP(int HP)
        {
            hp = HP;
        }

        public void setMP(int MP)
        {
            mp = MP;
        }

        public void setPoisonResist(int PoisonResist)
        {
            this.poisonResist = poisonResist;
        }

        public void setParalyzeResist(int ParalyzeResist)
        {
            this.paralyzeResist = ParalyzeResist;
        }

    }
}
