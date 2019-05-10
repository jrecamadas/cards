using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.responses
{
    public class winnerDetails
    {
        public List<winner> Winners { get; set; }
        public class winner
        {
            public Int32 score { get; set; }
            public String playerName { get; set; }
            public Int32 highestCard { get; set; }
            public Int32 secondHighCard { get; set; }
        }
    }
}
