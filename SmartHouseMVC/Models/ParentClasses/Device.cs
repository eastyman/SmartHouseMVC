using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoolHouse
{
    public class Device
    {
        public string name;
        public bool State { get; set; }
        public Device(string name)
        {
            this.name = name;
        }
        public virtual void On()
        {
            State = true;
        }
        public virtual void Off()
        {
            State = false;
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
            return name + " " + retStr;
        }

    }
}
