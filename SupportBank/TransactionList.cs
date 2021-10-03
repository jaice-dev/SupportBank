using System.Xml.Serialization;

namespace SupportBank
{
    [XmlRoot("TransactionList")]

    public class TransactionList
    {
        public SupportTransaction SupportTransaction { get; set; }
    }

    public class SupportTransaction
    {
        [XmlAttribute("Date")]
        public string Date { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public Parties Parties { get; set; }
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