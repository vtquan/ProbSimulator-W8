using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probability_Simulator.Common.RPG
{
    public class Character
    {
        string name;    //character name
        int hpStart;    //starting hp
        int mpStart;    //starting mp
        int hp; //current hp
        int mp; //current mp
        int poisonResist;   //value between 0-10, if character roll above resist, character is not poisoned
        int paralyzeResist; //value between 0-10, if character roll above resist, character is not paralyzed

        private Item[,] itemList;    //[item object, item quantity]

        public Character()
        {
            name = "Character";
            hpStart = 1;
            mpStart = 1;
            hp = hpStart;
            mp = mpStart;
            poisonResist = 0;
            paralyzeResist = 0;
            itemList = new Item[0, 0];
             
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
            itemList = new Item[0, 0];
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
            itemList = new Item[0, 0];
        }

        public Character(string Name, int Health, int Mana, int PoisonResist, int ParalyzeResist)
        {
            name = Name;
            hpStart = Health;
            mpStart = Mana;
            hp = hpStart;
            mp = mpStart;
            poisonResist = PoisonResist % 11;      //% 11 makes sure that poisonResist is always 10 or less
            paralyzeResist = ParalyzeResist % 11;   //% 11 makes sure that paralyzeResist is always 10 or less
            itemList = new Item[0, 0];
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
            poisonResist = PoisonResist % 11;      //% 11 makes sure that poisonResist is always 10 or less
        }

        public void setParalyzeResist(int ParalyzeResist)
        {
            paralyzeResist = ParalyzeResist % 11;   //% 11 makes sure that paralyzeResist is always 10 or less
        }
    }
}
