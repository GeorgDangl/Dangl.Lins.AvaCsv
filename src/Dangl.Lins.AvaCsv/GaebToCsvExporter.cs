using CsvHelper;
using Dangl.AVA.Contents.ServiceSpecificationContents;
using Dangl.AVA.Converter;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dangl.Lins.AvaCsv
{
    public class GaebToCsvExporter
    {
        private readonly string _gaebFilePath;

        public GaebToCsvExporter(string gaebFilePath)
        {
            _gaebFilePath = gaebFilePath;
        }

        public async Task ConvertToCsvFileAsync(string outputPath)
        {
            using (var gaebStream = File.OpenRead(_gaebFilePath))
            {
                var gaebFile = GAEB.Reader.GAEBReader.ReadGaeb(gaebStream);
                var project = Converter.ConvertFromGaeb(gaebFile);
                var csvEntries = new List<CsvEntry>();
                foreach (var item in project.ServiceSpecifications
                    .Single()
                    .RecursiveElements())
                {
                    switch (item)
                    {
                        case Position position:
                            csvEntries.Add(new CsvEntry
                            {
                                Type = "P",
                                ItemNumber = position.ItemNumber.StringRepresentation,
                                Quantity = position.Quantity,
                                ShortText = position.ShortText,
                                TotalPrice = position.TotalPrice,
                                UnitPrice = position.UnitPrice,
                                UnitTag = position.UnitTag
                            });
                            break;

                        case ServiceSpecificationGroup group:
                            csvEntries.Add(new CsvEntry
                            {
                                Type = "T", // 'T' for German 'Titel'
                                ItemNumber = group.ItemNumber.StringRepresentation,
                                ShortText = group.ShortText,
                            });
                            break;
                    }
                }

                using (var outputStream = File.Create(outputPath))
                {
                    using (var streamWriter = new StreamWriter(outputStream))
                    {
                        using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture))
                        {
                            await csvWriter.WriteRecordsAsync(csvEntries);
                        }
                    }
                }
            }
        }
    }
}
