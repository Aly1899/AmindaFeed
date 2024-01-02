using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AmindaFeed.Extensions
{
    public static class amindaProdExtension
    {
        public static string ToXmlString(this AmindaProducts amindaProds)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            XmlSerializer amindaSerialization = new XmlSerializer(amindaProds.GetType());
            StringBuilder xmlAmindaProduct = new StringBuilder();
            var amindaWriter = new StringWriter(xmlAmindaProduct);
            var xmlAmindaWriter = XmlWriter.Create(amindaWriter, settings);

            amindaSerialization.Serialize(xmlAmindaWriter, amindaProds);


            Console.WriteLine($"xmlAminda {xmlAmindaProduct}");

            return xmlAmindaProduct.ToString();
        }
    }
}
