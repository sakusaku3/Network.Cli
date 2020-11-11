using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var elements = GetElements(@"../../../Elements.csv");
            var events = GetEvents(@"../../../Events.csv");

            var controller = new Status.StatusController(elements, events);

            var outLines = new List<string[]>();
            outLines.Add(GetStatusSetColumn(elements, events));
            outLines.AddRange(controller.StatusSets.Select(x => GetStatusSetRow(x)));

            XsvConverter.Write(outLines, @"../../../Status.csv", encoding, delemiter);

            Console.WriteLine("Finished");
        }

        private static readonly string delemiter = ","; 
        private static readonly Encoding encoding = Encoding.UTF8; 

        private static IReadOnlyList<Status.StateElement> GetElements(string filepath)
        {
            return XsvConverter.EnumerateLines(filepath, encoding, delemiter)
                .Skip(1)
                .Select(x => new Status.StateElement(x[0], x[1].Replace("[", "").Replace("]", "").Split(",")))
                .ToArray();
        }

        private static IReadOnlyList<Status.StateChangeEvent> GetEvents(string filepath)
        {
            return XsvConverter.EnumerateLines(filepath, encoding, delemiter)
                .Skip(1)
                .Select(x => new Status.StateChangeEvent(x[0]))
                .ToArray();
        }

        private static string[] GetStatusSetColumn(
			IReadOnlyList<Status.StateElement> elements,
			IReadOnlyList<Status.StateChangeEvent> events) 
        {
            return elements.Select(x => x.Name)
                .Concat(events.Select(x => x.Name))
                .ToArray();
        }

        private static string[] GetStatusSetRow(
            Status.StatusSet statusSet)
        {
            return statusSet.Status.States.Select(x => x.Value)
                .Concat(statusSet.EventCostTable.Keys.Select(x => string.Empty))
                .ToArray();
        }
    }
}
