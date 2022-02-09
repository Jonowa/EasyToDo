using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EasyToDo.Controller
{
    class ParamContainer
    {
        private static ParamContainer instance;

        private string filename;

        private static XElement paramStorage;

        public static ParamContainer GetOrCreate()
        {
            if (instance == null)
            {
                instance = new ParamContainer();
            }
            return instance;
        }

        private ParamContainer() 
        {
            filename = "";
            paramStorage = new XElement("params");
        }

        public string Get(string section, string key)
        {
            try
            {
                return paramStorage.Element(section)?.Element(key)?.Value;
            }
            catch
            {
                return null;
            }
        }

        public void Set<T>(string section, string key, T value)
        {
            if (paramStorage.Element(section) == null)
            {
                paramStorage.Add(new XElement(section, new XElement(key, value.ToString())));
                return;
            }
            if (paramStorage.Element(section).Element(key) == null)
            {
                paramStorage.Element(section).Add(new XElement(key, value.ToString()));
                return;
            }
            paramStorage.Element(section).Element(key).Value = value.ToString();
        }

        public void Load(string filename)
        {
            this.filename = filename;

            paramStorage = XElement.Load(filename);
        }

        public void Save()
        {
            SaveAs(filename);
        }

        public void SaveAs(string filename)
        {
            try
            {
                paramStorage.Save(filename);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}
