﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoolHouse
{
    public class Oven : TempereaturedDevice
    {
        public Oven(string name, int minT, int maxT)
            : base(name, minT, maxT)
        {
            this.name = name;
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
            return "Духовка " + name + " " + retStr + " температура: " + Temperature + ", дверь " + doorState + ", нагревание " + tempElem;
        }
    }
}
