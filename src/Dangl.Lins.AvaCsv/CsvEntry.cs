using CsvHelper.Configuration.Attributes;

namespace Dangl.Lins.AvaCsv
{
    // Diese Klasse modelliert eine Zeile in der CSV Datei
    public class CsvEntry
    {
        [Name("Typ")]
        public string Type { get; set; }

        [Name("OZ")]
        public string ItemNumber { get; set; }

        [Name("Menge")]
        public decimal Quantity { get; set; }

        [Name("ME")]
        public string UnitTag { get; set; }

        [Name("Kurztext")]
        public string ShortText { get; set; }

        [Name("EP")]
        public decimal UnitPrice { get; set; }

        [Name("GP")]
        public decimal TotalPrice { get; set; }
    }
}
