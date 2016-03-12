using CoolHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartHouseMVC.Controllers
{
    public class TVController : ApiController
    {
        IDictionary<string, Device> deviceList = (Dictionary<string, Device>)System.Web.HttpContext.Current.Session["Devices"];
        
        [Route("api/TV/{act}/{name}")]
        public string Put(string act, string name)
        {
            switch (act)
            {
                case "on":
                    ((TVSet)deviceList[name]).On();
                    break;
                case "off":
                    ((TVSet)deviceList[name]).Off();
                    break;
                case "prevch":
                    ((TVSet)deviceList[name]).prevChannel();
                    break;
                case "nextch":
                     ((TVSet)deviceList[name]).nextChannel();
                    break;
                case "disconnect":
                    ((TVSet)deviceList[name]).SignalSource = null;
                    break;
            }
            return ((TVSet)deviceList[name]).ToString();
        }
               
        [Route("api/TV/{act}/{name}/{source}")]
        public string Put(string act, string name, string source)
        {
            if (act == "connect")
            {
                var res =
                from t in deviceList
                where t.Value.name == source.ToString()
                select t.Value;
                    foreach (var item in res)
                    {
                        ((TVSet)deviceList[name]).SignalSource = (ITVsourced)item;
                    }
            }
            return ((TVSet)deviceList[name]).ToString();
        }


        [Route("api/TV/{name}")]
        public void Delete(string name)
        {
            IDictionary<string, Device> deviceList = (Dictionary<string, Device>)System.Web.HttpContext.Current.Session["Devices"];
            deviceList.Remove(name);
        }
    }
}
