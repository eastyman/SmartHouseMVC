using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoolHouse
{
    public class Fringe : TempereaturedDevice
    {
        public Device Lamp { get; set; }
        public Fringe(string name, int minT, int maxT)
            : base(name, minT, maxT)
        {
            this.name = name;
        }

        public override void On()
        {
            State = true;
            if (door)
            {

                Lamp.On();
            }
            tempElement = true;
        }

        public override void Off()
        {
            base.Off();
            Lamp.Off();
        }



        public override void OpenDoor()
        {
            if (State)
            {
                Lamp.On();
            }
            door = true;
        }

        public override void CloseDoor()
        {
            Lamp.Off();
            door = false;
        }

        public override string ToString()
        {
            string retStr = "";
            if (State)
            {
                retStr = "включен";
            }
            if (!State)
            {
                retStr = "выключен";
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
            return "Холодильник " + name + " " + retStr + " температура: " + Temperature + " лампочка " + Lamp.ToString() + " Дверь " + doorState;
        }
    }
}
