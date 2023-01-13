using System.Xml.Serialization;

[XmlRoot(ElementName = "Params")]
public class Params
{

    [XmlElement(ElementName = "ApiKey")]
    public string ApiKey { get; set; }
}

