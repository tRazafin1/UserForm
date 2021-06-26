using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace UserForm.Infrastructure
{
    public static class XMLGenerator
    {
        public static void Create()
        {
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("USERS");
            document.AppendChild(root);
            document.Save(@"C:\Workstation\C#\UserForm\UserForm\Users.xml");
        }

        public static void AddChild(object childAttributes, string childName = "USER")
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Workstation\C#\UserForm\UserForm\Users.xml");
            XmlNode root = document.SelectSingleNode("USERS");
            XmlElement child = document.CreateElement(childName);

            var propertyList = childAttributes.GetType().GetProperties();
            XmlAttribute id = document.CreateAttribute("ID");
            id.Value = document.SelectNodes($"USERS/{childName}").Count.ToString();
            child.Attributes.Append(id);

            foreach (var property in propertyList)
            {
                XmlAttribute attribute = document.CreateAttribute(property.Name);
                attribute.Value = property.GetValue(childAttributes).ToString();
                child.Attributes.Append(attribute);
            }

            root.AppendChild(child);
            document.Save(@"C:\Workstation\C#\UserForm\UserForm\Users.xml");
        }
    }
}
