using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace RaceCarEstimator
{
    // Channel Data Class
    public class ChannelData
    {
        public double _Value { get; set; }
        public int _Outing { get; set; }

        public ChannelData(double value, int outing)
        {
            _Value = value;
            _Outing = outing;
        }
    }

    // RaceData Class
    public class RaceData
    {
        public double _Time { get; set; }
        public ChannelData _Channel1 { get; set; }
        public ChannelData _Channel2 { get; set; }
        public ChannelData _Channel3 { get; set; }
        public ChannelData _Channel4 { get; set; }
        public ChannelData _Channel5 { get; set; }
        public ChannelData _Channel6 { get; set; }
        public ChannelData _Channel7 { get; set; }
        public RaceData(double time)
        {
            _Time = time;
            _Channel1 = new ChannelData(0, 0);
            _Channel2 = new ChannelData(0, 0);
            _Channel3 = new ChannelData(0, 0);
            _Channel4 = new ChannelData(0, 0);
            _Channel5 = new ChannelData(0, 0);
            _Channel6 = new ChannelData(0, 0);
            _Channel7 = new ChannelData(0, 0);
        }
    }

}
