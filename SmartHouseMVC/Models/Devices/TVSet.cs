using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoolHouse
{
    public class TVSet : Device
    {
        public ITVsourced SignalSource { get; set; }  //Свойство для инъекции зависимости (подключение к телевизору внешнего устройства)
        public int currChannel;
        public readonly int channelsCount;
        public TVSet(string devname, int currCh, int chCount)
            : base(devname)
        {
            name = devname;
            currChannel = currCh;
            channelsCount = chCount;
        }

        public void prevChannel()
        {
            if (currChannel - 1 < 0)
            {
                currChannel = channelsCount;
            }
            else
            {
                currChannel -= 1;
            }
        }

        public void nextChannel()
        {
            if (currChannel + 1 > channelsCount)
            {
                currChannel = 0;
            }
            else
            {
                currChannel += 1;
            }
        }

        public string ReturnVideoSource()
        {
            if (SignalSource == null)
            {
                return "зомбоящик, канал: " + currChannel;
            }
            else
            {
                return SignalSource.Identify();
            }
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
            return "Телевизор " + name + " " + retStr + " источник сигнала: " + this.ReturnVideoSource();
        }
    }
}
