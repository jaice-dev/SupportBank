using System.Collections.Generic;
using System.Xml.Serialization;

namespace SupportBank
{
    public class SupportTransaction
    {
        [XmlAttribute("Date")]
        public string Date { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        [XmlElement("Parties")]
        public Parties PartiesArray { get; set; }
    }

    [XmlRootAttribute("TransactionList")]
    public class SupportTransactionCollection
    {
        [XmlElement("SupportTransaction")]
        public SupportTransaction[] TransactionList { get; set; } 
    }

    public class Parties
    {
        public string From { get; set; }
        public string To { get; set; }
    }
}

/*
<?xml version="1.0" encoding="utf-8" ?>
<MyDocument xmlns="http://www.dotnetcoretutorials.com/namespace">
  <MyProperty>Abc</MyProperty>
  <MyAttributeProperty value="123" />
  <MyList>
    <MyListItem>1</MyListItem>
    <MyListItem>2</MyListItem>
    <MyListItem>3</MyListItem>
  </MyList>
</MyDocument>
*/