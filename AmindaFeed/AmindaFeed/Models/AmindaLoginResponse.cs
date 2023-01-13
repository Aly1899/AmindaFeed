using System.Xml.Serialization;

[XmlRoot(ElementName = "Login")]
public class Login
{

    [XmlElement(ElementName = "Token")]
    public string Token { get; set; }
}

