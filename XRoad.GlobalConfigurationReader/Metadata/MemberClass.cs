using System;
using System.Xml.Serialization;

namespace XRoad.Configuration.Metadata
{
    [Serializable]
    [XmlRoot("memberClass")]
    public class MemberClass
    {
        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}