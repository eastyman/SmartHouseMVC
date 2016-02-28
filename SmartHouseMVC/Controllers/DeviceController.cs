﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolHouse;

namespace SmartHouseMVC.Controllers
{
    public class DeviceController : Controller
    {
        // GET: Device
        public ActionResult Index()
        {   
            IDictionary<string, Device> deviceList;
            if (Session["Devices"] == null)
            {
                deviceList = new Dictionary<string, Device>();
                Session["Devices"] = deviceList;
            }
            else
            {
                deviceList = (Dictionary<string, Device>)Session["Devices"];
            }

            ViewBag.deviceList = deviceList;
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
            SelectListItem[] dropDownDeviceList = new SelectListItem[6];
            dropDownDeviceList[0] = new SelectListItem { Text = "Холодильник", Value = "fridge", Selected = true };
            dropDownDeviceList[1] = new SelectListItem { Text = "Телевизор", Value = "tv" };
            dropDownDeviceList[2] = new SelectListItem { Text = "Микрофолновка", Value = "mw" };
            dropDownDeviceList[3] = new SelectListItem { Text = "Духовка", Value = "oven" };
            dropDownDeviceList[4] = new SelectListItem { Text = "Спутниковый тюнер", Value = "satellite" };
            dropDownDeviceList[5] = new SelectListItem { Text = "Приставка", Value = "gamebox" };
            ViewBag.dropDownDeviceList = dropDownDeviceList;
            return View();
        }

        // POST: Device/Create
        [HttpPost]
        public ActionResult Create(string deviceType, string deviceName)
        {
            SelectListItem[] dropDownDeviceList = new SelectListItem[6];
            dropDownDeviceList[0] = new SelectListItem { Text = "Холодильник", Value = "fridge", Selected = true };
            dropDownDeviceList[1] = new SelectListItem { Text = "Телевизор", Value = "tv" };
            dropDownDeviceList[2] = new SelectListItem { Text = "Микрофолновка", Value = "mw" };
            dropDownDeviceList[3] = new SelectListItem { Text = "Духовка", Value = "oven" };
            dropDownDeviceList[4] = new SelectListItem { Text = "Спутниковый тюнер", Value = "satellite" };
            dropDownDeviceList[5] = new SelectListItem { Text = "Приставка", Value = "gamebox" };

            ViewBag.dropDownDeviceList = dropDownDeviceList;

            IDictionary<string, Device> deviceList = (Dictionary<string, Device>)Session["Devices"];
            if (deviceName == "")
            {
                ViewBag.ErrorNoname = "Имя устройства необходимо заполнить";
                return View();
            }
            else if (deviceList.ContainsKey(deviceName))
            {
                ViewBag.ErrorContains = "Устройство с таким именем уже существует!";
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

                deviceList.Add(deviceName, newDevice);
                Session["Devices"] = deviceList;
                return RedirectToAction("Index");
            }

        }
        
        public ActionResult Delete(string name)
        {
            IDictionary<string, Device> deviceList = (Dictionary<string, Device>)Session["Devices"];
            deviceList.Remove(name);
            Session["Devices"] = deviceList;
            return RedirectToAction("Index");
        }

        public ActionResult On(string name)
        {
            IDictionary<string, Device> deviceList = (Dictionary<string, Device>)Session["Devices"];
            deviceList[name].On();
            Session["Devices"] = deviceList;
            return RedirectToAction("Index");
        }

        public ActionResult Off(string name)
        {
            IDictionary<string, Device> deviceList = (Dictionary<string, Device>)Session["Devices"];
            deviceList[name].Off();
            Session["Devices"] = deviceList;
            return RedirectToAction("Index");
        }

        public ActionResult PrevChannel(string name)
        {
            IDictionary<string, Device> deviceList = (Dictionary<string, Device>)Session["Devices"];
            ((TVSet)deviceList[name]).prevChannel();
            Session["Devices"] = deviceList;
            return RedirectToAction("Index");
        }

        public ActionResult NextChannel(string name)
        {
            IDictionary<string, Device> deviceList = (Dictionary<string, Device>)Session["Devices"];
            ((TVSet)deviceList[name]).nextChannel();
            Session["Devices"] = deviceList;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Connect(string name, string sourceBox)
        {
            IDictionary<string, Device> deviceList = (Dictionary<string, Device>)Session["Devices"];

            var res =
                     from t in deviceList
                     where t.Value.name == sourceBox.ToString()
                     select t.Value;
            foreach (var source in res)
            {
                ((TVSet)deviceList[name]).SignalSource = (ITVsourced)source;
            }
            Session["Devices"] = deviceList;
            return RedirectToAction("Index");
        }

        public ActionResult Disconnect(string name)
        {
            IDictionary<string, Device> deviceList = (Dictionary<string, Device>)Session["Devices"];
            ((TVSet)deviceList[name]).SignalSource = null;
            Session["Devices"] = deviceList;
            return RedirectToAction("Index");
        }

        public ActionResult OpenDoor(string name)
        {
            IDictionary<string, Device> deviceList = (Dictionary<string, Device>)Session["Devices"];
            ((TempereaturedDevice)deviceList[name]).OpenDoor();
            Session["Devices"] = deviceList;
            return RedirectToAction("Index");
        }

        public ActionResult CloseDoor(string name)
        {
            IDictionary<string, Device> deviceList = (Dictionary<string, Device>)Session["Devices"];
            ((TempereaturedDevice)deviceList[name]).CloseDoor();
            Session["Devices"] = deviceList;
            return RedirectToAction("Index");
        }

        public ActionResult DownTemp(string name)
        {
            IDictionary<string, Device> deviceList = (Dictionary<string, Device>)Session["Devices"];
            if (deviceList[name] is Fringe)
            {
                ((TempereaturedDevice)deviceList[name]).lowTemperature(1);
            }
            else
            {
                ((TempereaturedDevice)deviceList[name]).lowTemperature(10);
            }
            Session["Devices"] = deviceList;
            return RedirectToAction("Index");
        }

        public ActionResult UpTemp(string name)
        {
            IDictionary<string, Device> deviceList = (Dictionary<string, Device>)Session["Devices"];
            if (deviceList[name] is Fringe)
            {
                ((TempereaturedDevice)deviceList[name]).highTemperature(1);
            }
            else
            {
                ((TempereaturedDevice)deviceList[name]).highTemperature(10);
            }
            
            Session["Devices"] = deviceList;
            return RedirectToAction("Index");
        }
    }
}