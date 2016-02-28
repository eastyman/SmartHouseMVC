using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoolHouse
{
    public class Satellite : Device, ITVsourced
    {
        public Satellite(string name)
            : base(name)
        {
            this.name = name;
        }
        public string Identify()
        {
            return "Спутниковый тюнер " + this.name;
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
            return "Тюнер "+ name + " " + retStr;
        }
    }
}
