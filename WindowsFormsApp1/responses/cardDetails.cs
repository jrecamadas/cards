using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.responses
{
    public class cardDetails
    {
        public String deck_id { get; set; }
        public List<card> cards { get; set; }
        public List<DeckCard> deck { get; set; }
        public class card
        {
            public string suit { get; set; }
            public string value { get; set; }
        }
        public class DeckCard
        {
            public string suit { get; set; }
            public string value { get; set; }
        }
    }
}
