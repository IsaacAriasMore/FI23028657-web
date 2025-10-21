using System.Xml.Serialization;

[XmlRoot("Result")]
public class Result
{
    [XmlElement("Ori")]
    public string Ori { get; set; } = string.Empty;
    
    [XmlElement("New")]
    public string New { get; set; } = string.Empty;
}