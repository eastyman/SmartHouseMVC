using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolHouse;

namespace SmartHouseMVC.Controllers
{
    public class DeviceController : Controller
    {
        public IDictionary<string, Device> deviceList;       
        public DeviceController()
        {
            if (System.Web.HttpContext.Current.Session["Devices"] == null)
            {
                deviceList = new Dictionary<string, Device>();
                System.Web.HttpContext.Current.Session["Devices"] = deviceList;
                System.Web.HttpContext.Current.Session["NextId"] = 0;
            
            }
            else
            {
                deviceList = (Dictionary<string, Device>)System.Web.HttpContext.Current.Session["Devices"];               
            }
        }        
        // GET: Device
        public ActionResult Index()
        {    
              List<string> sources = new List<string>(); 
              var res =
                       from t in deviceList
                       where t.Value is ITVsourced
                       select t.Value.name;
                foreach (var source in res)
                {
                    sources.Add(source);
                }
            SelectList sourceList = new SelectList(sources);
            ViewBag.sourceList = sourceList;
            return View(deviceList);
        }
      

        // GET: Device/Create
        public ActionResult Create()
        {
            ViewBag.dropDownDeviceList = CreateDevList();
            return View();
        }
        
        // POST: Device/Create
        [HttpPost]
        public ActionResult Create(string deviceType, string deviceName)
        {

            var res =
             from t in deviceList
             where t.Value.name == deviceName
             select t.Value;

            int count = 0;
            foreach (var source in res)
            {
                count++;
            } 
            if (deviceName == "")
            {
                ViewBag.ErrorNoname = "Имя устройства необходимо заполнить";
                ViewBag.dropDownDeviceList = CreateDevList();
                return View();
            }
                
            
            else if (count>0)
            {
                ViewBag.ErrorContains = "Устройство с таким именем уже существует!";
                ViewBag.dropDownDeviceList = CreateDevList();
                return View();
            }
            else
            {
                Device newDevice;
                switch (deviceType)
                {
                    default:
                        newDevice = new Fringe(deviceName, -20, 7);
                        Device lamp = new Device("FringeLamp");
                        ((Fringe)newDevice).Lamp = lamp;
                        break;
                    case "tv":
                        newDevice = new TVSet(deviceName, 1, 100);
                        break;
                    case "mw":
                        newDevice = new MicroWave(deviceName, 50, 200);
                        break;
                    case "oven":
                        newDevice = new Oven(deviceName, 50, 300);
                        break;
                    case "satellite":
                        newDevice = new Satellite(deviceName);
                        break;
                    case "gamebox":
                        newDevice = new GameBox(deviceName);
                        break;
                }
                int devCount = (int)System.Web.HttpContext.Current.Session["NextId"];
                string key = "dev" + devCount.ToString();              
                deviceList.Add(key, newDevice);
                devCount++;
                System.Web.HttpContext.Current.Session["NextId"] = devCount;
                return RedirectToAction("Index");
            }

        }

        private SelectListItem[] CreateDevList()
        {
            SelectListItem[] dropDownDeviceList = new SelectListItem[6];
            dropDownDeviceList[0] = new SelectListItem { Text = "Холодильник", Value = "fridge", Selected = true };
            dropDownDeviceList[1] = new SelectListItem { Text = "Телевизор (WebAPI)", Value = "tv" };
            dropDownDeviceList[2] = new SelectListItem { Text = "Микрофолновка", Value = "mw" };
            dropDownDeviceList[3] = new SelectListItem { Text = "Духовка", Value = "oven" };
            dropDownDeviceList[4] = new SelectListItem { Text = "Спутниковый тюнер", Value = "satellite" };
            dropDownDeviceList[5] = new SelectListItem { Text = "Приставка", Value = "gamebox" };
            return dropDownDeviceList;
        }
        
        public ActionResult Delete(string name)
        {
            deviceList.Remove(name);
            return RedirectToAction("Index");
        }

        public ActionResult On(string name)
        {
            deviceList[name].On();
            return RedirectToAction("Index");
        }

        public ActionResult Off(string name)
        {
            deviceList[name].Off();
            return RedirectToAction("Index");
        }

        public ActionResult PrevChannel(string name)
        {
            ((TVSet)deviceList[name]).prevChannel();
            return RedirectToAction("Index");
        }

        public ActionResult NextChannel(string name)
        {
            ((TVSet)deviceList[name]).nextChannel();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Connect(string name, string sourceBox)
        {
            var res =
                     from t in deviceList
                     where t.Value.name == sourceBox.ToString()
                     select t.Value;
            foreach (var source in res)
            {
                ((TVSet)deviceList[name]).SignalSource = (ITVsourced)source;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Disconnect(string name)
        {
            ((TVSet)deviceList[name]).SignalSource = null;
            return RedirectToAction("Index");
        }

        public ActionResult OpenDoor(string name)
        {
            ((TempereaturedDevice)deviceList[name]).OpenDoor();
            return RedirectToAction("Index");
        }

        public ActionResult CloseDoor(string name)
        {
            ((TempereaturedDevice)deviceList[name]).CloseDoor();
            return RedirectToAction("Index");
        }

        public ActionResult DownTemp(string name)
        {
            if (deviceList[name] is Fringe)
            {
                ((TempereaturedDevice)deviceList[name]).lowTemperature(1);
            }
            else
            {
                ((TempereaturedDevice)deviceList[name]).lowTemperature(10);
            }
            return RedirectToAction("Index");
        }

        public ActionResult UpTemp(string name)
        {
            if (deviceList[name] is Fringe)
            {
                ((TempereaturedDevice)deviceList[name]).highTemperature(1);
            }
            else
            {
                ((TempereaturedDevice)deviceList[name]).highTemperature(10);
            }
            
            return RedirectToAction("Index");
        }
    }
}