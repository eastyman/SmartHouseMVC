using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoolHouse
{
    public class GameBox :Device, ITVsourced
    {
        public GameBox(string name):base(name)
        {
            this.name = name;
        }
        public string Identify()
        {
            return "GameBox "+ this.name;
        }

        public override string ToString()
        {
            string retStr = "";
            if (State)
            {
                retStr = "вкл.";
            }
            if (!State)
            {
                retStr = "выкл.";
            }
            return "Приставка " + name + " " + retStr;
        }
    }
}
