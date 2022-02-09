using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EasyToDo.Controller
{
    class ItemController
    {
        public class Item
        {
            public string Content { get; set; }
            public DateTime CreatedAt { get; set; }
            public bool Remind { get; set; }
            public DateTime RemindAt { get; set; }
        }

        private readonly Dictionary<int, Item> items;

        public int lastId { get; private set; }

        private string filename;

        public ItemController()
        {
            items = new Dictionary<int, Item>();
            lastId = 0;
            filename = "";
        }

        public int AddItem()
        {
            items.Add(lastId, new Item {
                CreatedAt = DateTime.Now,
            });
            return lastId++;
        }

        public void StoreItem(int id, string content, bool remind, DateTime remindAt)
        {
            items[id].Content = content;
            items[id].Remind = remind;
            items[id].RemindAt = remindAt;
            Save();
        }

        public void RemoveItem(int id)
        {
            items.Remove(id);
            Save();
        }

        public Item GetItem(int id)
        {
            return items[id];
        }

        public void Load(string filename)
        {
            this.filename = filename;
            items.Clear();
            lastId = 0;

            if (!File.Exists(filename)) return;

            XElement entries;
            try
            {
                entries = XElement.Load(filename);

                foreach(XElement entry in entries.Elements())
                {
                    if (entry.Element("Content").Value == "") continue;

                    items.Add(lastId, new Item {
                        CreatedAt = DateTime.Parse(entry.Element("CreatedAt").Value),
                        Content = entry.Element("Content").Value,
                        Remind = entry.Element("Remind").Value == "true",
                        RemindAt = DateTime.Parse(entry.Element("RemindAt").Value)
                    });
                    lastId++;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        public void Save()
        {
            SaveAs(filename);
        }

        public void SaveAs(string filename)
        {
            XElement entries = new XElement("Items");

            foreach (var item in items.Values)
            {
                entries.Add(new XElement("Item",
                    new XElement("CreatedAt", item.CreatedAt.ToString("s")),
                    new XElement("Content", item.Content),
                    new XElement("Remind", item.Remind),
                    new XElement("RemindAt", item.RemindAt.ToString("s"))
                ));
            }

            try
            {
                entries.Save(filename);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}
