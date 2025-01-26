using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceCarEstimator
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class DataProcessor
    {
        // ReadData function that read all channel data from dat file
        public List<RaceData> ReadData(string filePath)
        {
            var raceDataEntries = new List<RaceData>();
            int index = 0;
            List<double> times = new List<double>();
            string[] allLines = File.ReadLines(filePath).ToArray();
            List<string> dataLines = new List<string>();
            foreach (var line in allLines)
            {
                if(index == 0)
                {
                    index++;
                    continue;
                }
                var parts = line.Split('\t');
                double time = Double.Parse(parts[0]);
                if (!times.Contains(time))
                {
                    times.Add(time);
                }
                
                dataLines.Add(line);
                index++;
            }
            times.Sort();   
            foreach (double time in times)
            {
                RaceData raceData = new RaceData(time);
                foreach (var line in dataLines)
                {
                    var parts = line.Split('\t');
                    double ttime = Double.Parse(parts[0]);
                    int touting = Int32.Parse(parts[2]);
                    int tchannel = Int32.Parse(parts[3]);
                    double tvalue = Double.Parse(parts[1]);
                    if(time == ttime)
                    {
                        switch (tchannel)
                        {
                            case 1:
                                raceData._Channel1._Value = tvalue;
                                raceData._Channel1._Outing= touting;
                                break;
                            case 2:
                                raceData._Channel2._Value = tvalue;
                                raceData._Channel2._Outing = touting;
                                break;
                            case 3:
                                raceData._Channel3._Value = tvalue;
                                raceData._Channel3._Outing = touting;
                                break;
                            case 4:
                                raceData._Channel4._Value = tvalue;
                                raceData._Channel4._Outing = touting;
                                break;
                            case 5:
                                raceData._Channel5._Value = tvalue;
                                raceData._Channel5._Outing = touting;
                                break;
                            case 6:
                                raceData._Channel6._Value = tvalue;
                                raceData._Channel6._Outing = touting;
                                break;
                            case 7:
                                raceData._Channel7._Value = tvalue;
                                raceData._Channel7._Outing = touting;
                                break;
                        }

                    }

                }
                raceDataEntries.Add(raceData);
            }

            return raceDataEntries;
        }
    }
}
