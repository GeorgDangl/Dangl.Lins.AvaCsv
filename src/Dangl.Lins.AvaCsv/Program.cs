using System;
using System.Threading.Tasks;

namespace Dangl.Lins.AvaCsv
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Ausgabepafe zur erzeugten CSV Datei
            var outputCsvPath = @"";
            // GAEB Eingabedatei
            var gaebInputPath = @"";
            var exporter = new GaebToCsvExporter(gaebInputPath);
            await exporter.ConvertToCsvFileAsync(outputCsvPath);

            Console.WriteLine("Csv Erstellt");
        }
    }
}
