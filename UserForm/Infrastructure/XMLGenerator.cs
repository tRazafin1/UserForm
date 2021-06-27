using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace UserForm.Infrastructure
{
    public class XMLGenerator
    {
        public XMLGenerator()
        {
            this.Create();
        }

        private void Create()
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\VSWorkplace\C#\UserForm\UserForm\Users.xml");
            XmlNode existingRoot = document.SelectSingleNode("USERS");

            if (existingRoot == null)
            {
                XmlElement root = document.CreateElement("USERS");
                document.AppendChild(root);
            }
            document.Save(@"C:\VSWorkplace\C#\UserForm\UserForm\Users.xml");
        }

        public void AddChild(object childAttributes, string childName = "USER")
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\VSWorkplace\C#\UserForm\UserForm\Users.xml");
            XmlNode root = document.SelectSingleNode("USERS");
            XmlElement child = document.CreateElement(childName);

            var propertyList = childAttributes.GetType().GetProperties();
            XmlAttribute id = document.CreateAttribute("ID");
            id.Value = document.SelectNodes($"USERS/{childName}").Count.ToString();
            child.Attributes.Append(id);

            foreach (var property in propertyList)
            {
                XmlAttribute attribute = document.CreateAttribute(property.Name);
                attribute.Value = property.GetValue(childAttributes)?.ToString();
                child.Attributes.Append(attribute);
            }

            root.AppendChild(child);
            document.Save(@"C:\VSWorkplace\C#\UserForm\UserForm\Users.xml");
        }

        public List<UserForm.Models.Users> GetChildren(string childName = "USER")
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\VSWorkplace\C#\UserForm\UserForm\Users.xml");
            XmlNode root = document.SelectSingleNode("USERS");
            XmlNodeList users = document.SelectNodes($"USERS/{childName}");
            List<UserForm.Models.Users> _arr = new List<UserForm.Models.Users>();

            foreach( XmlNode user in users )
            {
                Models.Users u = new Models.Users();
                u.FName = user.Attributes["FName"].Value;
                u.LName = user.Attributes["LName"].Value;
                u.Number = user.Attributes["Number"].Value;
                u.id = user.Attributes["ID"].Value;

                _arr.Add(u);
            }


            return _arr;
        }

        public void RemoveChild(string id, string childName = "USER")
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\VSWorkplace\C#\UserForm\UserForm\Users.xml");
            XmlNode root = document.SelectSingleNode("USERS");
            XmlNodeList user = document.SelectNodes($"USERS/{childName}[@ID='{id}']");
            
            foreach(XmlNode x in user)
            {
                root.RemoveChild(x);
            }
            document.Save(@"C:\VSWorkplace\C#\UserForm\UserForm\Users.xml");
        }
    }
}
