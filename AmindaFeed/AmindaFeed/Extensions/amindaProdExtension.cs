﻿using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace AmindaFeed.Extensions
{
    public static class amindaProdExtension
    {
        public static string ToXmlString(this AmindaProducts amindaProd)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            XmlSerializer amindaSerialization = new XmlSerializer(amindaProd.GetType());
            StringBuilder xmlAmindaProduct = new StringBuilder();
            var amindaWriter = new StringWriter(xmlAmindaProduct);
            var xmlAmindaWriter = XmlWriter.Create(amindaWriter, settings);

            amindaSerialization.Serialize(xmlAmindaWriter, amindaProd);


            Console.WriteLine($"xmlAminda {xmlAmindaProduct}");

            return xmlAmindaProduct.ToString();
        }
    }
}
