using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoolHouse
{

    public class MicroWave : TempereaturedDevice
    {

        public MicroWave(string name, int minT, int maxT)
            : base(name, minT, maxT)
        {
            this.name = name;

        }

        public MWmodes ReturnMode()
        {
            if (Temperature <= 50)
            {
                return MWmodes.defrost;
            }
            else
            {
                return MWmodes.heating;
            }
        }

        public override string ToString()
        {
            string retStr = "";
            if (State)
            {
                retStr = "включена";
            }
            if (!State)
            {
                retStr = "выключена";
            }

            string doorState = "";
            if (door)
            {
                doorState = "открыта";
            }
            if (!door)
            {
                doorState = "закрыта";
            }

            string tempElem = "";
            if (tempElement)
            {
                tempElem = "включено";
            }
            if (!tempElement)
            {
                tempElem = "выключено";
            }
            return "Микроволновка " + name + " " + retStr + " в режиме " + this.ReturnMode() + " температура: " + Temperature + ", дверь " + doorState + ", нагревание " + tempElem;
        }

    }
}

