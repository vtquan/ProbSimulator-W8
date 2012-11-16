using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probability_Simulator.Common.RPG
{
    public class Character
    {
        private string name;    //character name
        private int hpStart;    //starting hp
        private int mpStart;    //starting mp
        private int hp; //current hp
        private int mp; //current mp
        private int poisonResist;   //value between 0-10, if character roll above resist, character is not poisoned
        private int paralyzeResist; //value between 0-10, if character roll above resist, character is not paralyzed
        private bool isPoisoned = false;
        private bool isParalyzed = false;


        private int numSpells = 0;
        private Spell[] spellList = new Spell[10];

        private Item[] itemList = new Item[10];    //store item 
        private int[] numItemList = new int[10];    //store number of itemList[i] in numItemList[i]

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
            poisonResist = PoisonResist % 11;      //% 11 makes sure that poisonResist is always 10 or less
            paralyzeResist = ParalyzeResist % 11;   //% 11 makes sure that paralyzeResist is always 10 or less
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

        public Spell[] getSpellList()
        {
            return spellList;
        }

        public int getNumSpells()
        {
            return numSpells;
        }

        public Item[] getItemList()
        {
            return itemList;
        }

        public int[] getNumItemList()
        {
            return numItemList;
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

        //Action Methods
        public void addSpell(Spell spell)
        {
            for (int i = 0; i < spellList.Length; i++)
            {
                if (spell == spellList[i])
                {
                    break;
                }
                else if (spellList[i] == null)
                {
                    spellList[i] = spell;
                    numSpells++;
                    break;
                }
            }
        }

        public KeyValuePair<bool, int[]> useSpell(string spellName, ref Monster Target)  //return whether item was used successfully; int[0] = damage, int[1] = spellIndex; int[i] = -1 if !used
        {
            bool used = false;  //whether item was used
            int[] spellResult = new int[2];
            spellResult[0] = -1;
            spellResult[1] = -1;
            KeyValuePair<bool, int[]> totalResult;

            for (int i = 0; i < spellList.Length; i++)
            {
                if (spellList[i] == null)
                {
                    break;
                }

                else if (spellName == spellList[i].getName())
                {
                    if (mp > spellList[i].getMPCost())
                    {
                        mp -= spellList[i].getMPCost();

                        Random random = new Random();

                        //Calculate Initial Damage
                        double damage = random.Next((int)spellList[i].getMinDamage(), (int)spellList[i].getMaxDamage() + 1);

                        //Crit or not
                        bool crit;
                        crit = (spellList[i].getCritPercent() >= random.Next(1, 11));

                        //Calculate final damage
                        if (crit)
                        {
                            damage = damage * 1.5;
                        }

                        Target.setHP(Target.getHP() - (int)damage);

                        used = true;
                        spellResult[0] = (int)damage;
                        spellResult[1] = i;

                        totalResult = new KeyValuePair<bool, int[]>(used, spellResult);
                    }
                    break;
                }
            }
            totalResult = new KeyValuePair<bool, int[]>(used, spellResult);
            return totalResult;
        }

        public int findSpell(string spellName)  //return whether item was used successfully
        {
            int spellIndex = -1; //store index of the searched item
            for (int i = 0; i < spellList.Length; i++)
            {
                if (spellList[i] == null)
                {
                    break;
                }

                else if (spellName == spellList[i].getName())
                {
                    spellIndex = i;
                    break;
                }
            }
            return spellIndex;
        }

        public void addItem(Item item)
        {
            for (int i = 0; i < itemList.Length; i++)
            {
                if (item == itemList[i])
                {
                    numItemList[i]++;
                    break;
                }
                else if (itemList[i] == null)
                {
                    itemList[i] = item;
                    numItemList[i]++;
                    break;
                }
            }
        }

        public bool useItem(string itemName)  //return whether item was used successfully
        {
            bool used = false;  //whether item was used
            for (int i = 0; i < itemList.Length; i++)
            {
                if (itemList[i] == null)
                {
                    break;
                }

                else if (itemName == itemList[i].getName())
                {
                    if (numItemList[i] > 0)
                    {
                        hp += itemList[i].getHeal();

                        if (hp > hpStart)
                        {
                            hp = hpStart;   //prevent over heal
                        }

                        numItemList[i]--;
                        used = true;
                    }
                    break;
                }
            }
            return used;
        }

        public int findItem(string itemName)  //return whether item was used successfully
        {
            int itemIndex = -1; //store index of the searched item
            for (int i = 0; i < itemList.Length; i++)
            {
                if (itemList[i] == null)
                {
                    break;
                }

                else if (itemName == itemList[i].getName())
                {
                    itemIndex = i;
                    break;
                }
            }
            return itemIndex;
        }
    }
}
