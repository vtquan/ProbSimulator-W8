using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probability_Simulator.Common.RPG
{
    public class Attack
    {
        private string name;
        private int critPercent; //value between 0-10
        private double minDamage;
        private double maxDamage;
        private int accuracy;   //value between 0-10
        private bool warning;
        private string warningMessage;  //message to print if warning is true

        //Constructors
        public Attack()
        {
            name = "Attack";
            critPercent = 0;
            minDamage = 0;
            maxDamage = 0;
            warning = false;
            warningMessage = "";
        }

        public Attack(string Name, int CritPercent, double MinDamage, double MaxDamage)
        {
            name = Name;
            critPercent = CritPercent;
            minDamage = MinDamage;
            maxDamage = MaxDamage;
            warning = false;
            warningMessage = "";
        }

        public Attack(string Name, int CritPercent, double MinDamage, double MaxDamage, bool Warning)
        {
            name = Name;
            critPercent = CritPercent;
            minDamage = MinDamage;
            maxDamage = MaxDamage;
            warning = Warning;
            warningMessage = "";
        }

        public Attack(string Name, int CritPercent, double MinDamage, double MaxDamage, bool Warning, string WarningMessage)
        {
            name = Name;
            critPercent = CritPercent;
            minDamage = MinDamage;
            maxDamage = MaxDamage;
            warning = Warning;
            warningMessage = WarningMessage;
        }

        //Get Methods
        public string getName()
        {
            return name;
        }

        public int getCritPercent()
        {
            return critPercent;
        }

        public double getMinDamage()
        {
            return minDamage;
        }

        public double getMaxDamage()
        {
            return maxDamage;
        }

        public bool getWarning()
        {
            return warning;
        }

        public string getWarningMessage()
        {
            return warningMessage;
        }

        //Set Methods
        public void setName(string Name)
        {
            name = Name;
        }

        public void setCritPercent(int CritPercent)
        {
            critPercent = CritPercent;
        }

        public void setMinDamage(double MinDamage)
        {
            minDamage = MinDamage;
        }

        public void setMaxDamage(double MaxDamage)
        {
            maxDamage = MaxDamage;
        }

        public void setWarning(bool Warning)
        {
            warning = Warning;
        }

        public void setWarningMessage(string WarningMessage)
        {
            warningMessage = WarningMessage;
        }
    }
}
