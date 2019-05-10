using System; 
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Net;
using WindowsFormsApp1.responses;
using System.Collections.Generic;
using static WindowsFormsApp1.responses.winnerDetails;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] files;
        List<cardDetails.DeckCard> deckCards = new List<cardDetails.DeckCard>();
        List<winner> Winners = new List<winner>();
        int totalCards = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                hideLabel();
                String path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Input1.txt");
                files = File.ReadAllLines(path);
                showLabel(files.Count());
            }
            catch(Exception)
            {
                MessageBox.Show("File not found");
                this.Close();
            }
            
        }
        private void hideLabel()
        {
            label0.Hide();
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            label9.Hide();
        }
        private void showLabel(int count)
        {
            for(int i = 0; i <= count; i++)
            {
                if (count > 10 || count < 2)
                    MessageBox.Show("2 to 10 Players only.");
                if (count == i)
                    break;
                switch(i.ToString())
                {
                    case "0":
                        
                        label0.Text = files[0].ToString();
                        label0.Show();
                        continue;
                    case "1":
                        label1.Text = files[1].ToString();
                        label1.Show();
                        continue;
                    case "2":
                        label2.Text = files[2].ToString();
                        label2.Show();
                        continue;
                    case "3":
                        label3.Text = files[3].ToString();
                        label3.Show();
                        continue;
                    case "4":
                        label4.Text = files[4].ToString();
                        label4.Show();
                        continue;
                    case "5":
                        label5.Text = files[5].ToString();
                        label5.Show();
                        continue;
                    case "6":
                        label6.Text = files[6].ToString();
                        label6.Show();
                        continue;
                    case "7":
                        label7.Text = files[7].ToString();
                        label7.Show();
                        continue;
                    case "8":
                        label8.Text = files[8].ToString();
                        label8.Show();
                        continue;
                    case "9":
                        label9.Text = files[9].ToString();
                        label9.Show();
                        break;
                }
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            var json = "";
            try
            {
                totalCards = files.Count() * 5;
                // DECK OF CARDS API
                String url = "https://deckofcardsapi.com/api/deck/new/draw/?count=" + totalCards;
                using (WebClient wc = new WebClient())
                {
                    //retrieval of json string response from the API
                    json = wc.DownloadString(url);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to connect to the remote server. Please try again later. , Error : " + ex.ToString());                
            }
            
            //Storing the json string values to cardDetails class
            cardDetails allCard = JsonConvert.DeserializeObject<cardDetails>(json);
            Winners.Clear();
            int count, score = 0;
            Int32 highCard = 0;
            try
            {

                #region Player 1 Deck of Cards
                deckCards.Clear();
                label16.Text = allCard.cards[0].value.ToString() + " " + allCard.cards[0].suit.ToString(); label16.Show();
                label17.Text = allCard.cards[1].value.ToString() + " " + allCard.cards[1].suit.ToString(); label17.Show();
                label18.Text = allCard.cards[2].value.ToString() + " " + allCard.cards[2].suit.ToString(); label18.Show();
                label19.Text = allCard.cards[3].value.ToString() + " " + allCard.cards[3].suit.ToString(); label19.Show();
                label20.Text = allCard.cards[4].value.ToString() + " " + allCard.cards[4].suit.ToString(); label20.Show();
                for (count = 0; count < 5; count++)
                {
                    deckCards.Add(new cardDetails.DeckCard { suit = allCard.cards[count].value.ToString(), value = allCard.cards[count].suit.ToString() });
                }
                if (isRoyalFlush(deckCards))
                    score += 100;
                if (isStraightFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 90;
                    } 
                }
                if (isFourOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 80;
                    } 
                }
                if (isFullHouse(deckCards))
                {
                    if (score == 0)
                    {
                        score += 70;
                    } 
                }
                if (isFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 60;
                    } 
                }
                if (isStraight(deckCards))
                {
                    if (score == 0)
                    {
                        score += 50;
                    } 
                }
                if (isThreeOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 40;
                    } 
                }
                if (isTwopair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 30;
                    } 
                }
                if (isPair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 20;
                    } 
                }
                if(score == 0)
                    highCard = getHighCard(deckCards);
                Winners.Add(new winner { playerName = files[0], highestCard = highCard, score = score });
                label76.Text = score.ToString();
                label76.Show();
                #endregion

                #region Player 2 Deck of Cards
                score = 0;
                deckCards.Clear();
                label21.Text = allCard.cards[5].value.ToString() + " " + allCard.cards[5].suit.ToString(); label21.Show();
                label22.Text = allCard.cards[6].value.ToString() + " " + allCard.cards[6].suit.ToString(); label22.Show();
                label23.Text = allCard.cards[7].value.ToString() + " " + allCard.cards[7].suit.ToString(); label23.Show();
                label24.Text = allCard.cards[8].value.ToString() + " " + allCard.cards[8].suit.ToString(); label24.Show();
                label25.Text = allCard.cards[9].value.ToString() + " " + allCard.cards[9].suit.ToString(); label25.Show();
                for (int we = 0; we < 5; we++)
                {
                    deckCards.Add(new cardDetails.DeckCard { suit = allCard.cards[count].value.ToString(), value = allCard.cards[count].suit.ToString() });
                    count++;
                }
                if (isRoyalFlush(deckCards))
                    score += 100;
                if (isStraightFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 90;
                    } 
                }
                if (isFourOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 80;
                    } 
                }
                if (isFullHouse(deckCards))
                {
                    if (score == 0)
                    {
                        score += 70;
                    } 
                }
                if (isFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 60;
                    } 
                }
                if (isStraight(deckCards))
                {
                    if (score == 0)
                    {
                        score += 50;
                    } 
                }
                if (isThreeOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 40;
                    } 
                }
                if (isTwopair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 30;
                    } 
                }
                if (isPair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 20;
                    } 
                }
                highCard = 0;
                if (score == 0)
                    highCard = getHighCard(deckCards);
                Winners.Add(new winner { playerName = files[1], highestCard = highCard, score = score });
                label75.Text = score.ToString();
                label75.Show();
                #endregion

                #region Player 3 Deck of Cards
                deckCards.Clear();
                score = 0;
                label26.Text = allCard.cards[10].value.ToString() + " " + allCard.cards[10].suit.ToString(); label26.Show();
                label27.Text = allCard.cards[11].value.ToString() + " " + allCard.cards[11].suit.ToString(); label27.Show();
                label28.Text = allCard.cards[12].value.ToString() + " " + allCard.cards[12].suit.ToString(); label28.Show();
                label29.Text = allCard.cards[13].value.ToString() + " " + allCard.cards[13].suit.ToString(); label29.Show();
                label30.Text = allCard.cards[14].value.ToString() + " " + allCard.cards[14].suit.ToString(); label30.Show();
                for (int w = 0; w < 5; w++)
                {
                    deckCards.Add(new cardDetails.DeckCard { suit = allCard.cards[count].value.ToString(), value = allCard.cards[count].suit.ToString() });
                    count++;
                }
                if (isRoyalFlush(deckCards))
                    score += 100;
                if (isStraightFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 90;
                    } 
                }
                if (isFourOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 80;
                    } 
                }
                if (isFullHouse(deckCards))
                {
                    if (score == 0)
                    {
                        score += 70;
                    } 
                }
                if (isFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 60;
                    } 
                }
                if (isStraight(deckCards))
                {
                    if (score == 0)
                    {
                        score += 50;
                    } 
                }
                if (isThreeOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 40;
                    } 
                }
                if (isTwopair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 30;
                    } 
                }
                if (isPair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 20;
                    } 
                }
                highCard = 0;
                if (score == 0)
                    highCard = getHighCard(deckCards);
                Winners.Add(new winner { playerName = files[2], highestCard = highCard, score = score });
                label74.Text = score.ToString();
                label74.Show();
                #endregion

                #region Player 4 Deck of Cards
                score = 0;
                deckCards.Clear();
                label31.Text = allCard.cards[15].value.ToString() + " " + allCard.cards[15].suit.ToString(); label31.Show();
                label32.Text = allCard.cards[16].value.ToString() + " " + allCard.cards[16].suit.ToString(); label32.Show();
                label33.Text = allCard.cards[17].value.ToString() + " " + allCard.cards[17].suit.ToString(); label33.Show();
                label34.Text = allCard.cards[18].value.ToString() + " " + allCard.cards[18].suit.ToString(); label34.Show();
                label35.Text = allCard.cards[19].value.ToString() + " " + allCard.cards[19].suit.ToString(); label35.Show();
                for (int w = 0; w < 5; w++)
                {
                    deckCards.Add(new cardDetails.DeckCard { suit = allCard.cards[count].value.ToString(), value = allCard.cards[count].suit.ToString() });
                    count++;
                }
                if (isRoyalFlush(deckCards))
                    score += 100;
                if (isStraightFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 90;
                    } 
                }
                if (isFourOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 80;
                    } 
                }
                if (isFullHouse(deckCards))
                {
                    if (score == 0)
                    {
                        score += 70;
                    } 
                }
                if (isFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 60;
                    } 
                }
                if (isStraight(deckCards))
                {
                    if (score == 0)
                    {
                        score += 50;
                    } 
                }
                if (isThreeOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 40;
                    } 
                }
                if (isTwopair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 30;
                    } 
                }
                if (isPair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 20;
                    } 
                }
                highCard = 0;
                if (score == 0)
                    highCard = getHighCard(deckCards);
                Winners.Add(new winner { playerName = files[3], highestCard = highCard, score = score });
                label73.Text = score.ToString();
                label73.Show();
                #endregion

                #region Player 5 Deck of Cards
                deckCards.Clear();
                score = 0;
                label36.Text = allCard.cards[20].value.ToString() + " " + allCard.cards[20].suit.ToString(); label36.Show();
                label37.Text = allCard.cards[21].value.ToString() + " " + allCard.cards[21].suit.ToString(); label37.Show();
                label38.Text = allCard.cards[22].value.ToString() + " " + allCard.cards[22].suit.ToString(); label38.Show();
                label39.Text = allCard.cards[23].value.ToString() + " " + allCard.cards[23].suit.ToString(); label39.Show();
                label40.Text = allCard.cards[24].value.ToString() + " " + allCard.cards[24].suit.ToString(); label40.Show();
                for (int w = 0; w < 5; w++)
                {
                    deckCards.Add(new cardDetails.DeckCard { suit = allCard.cards[count].value.ToString(), value = allCard.cards[count].suit.ToString() });
                    count++;
                }
                if (isRoyalFlush(deckCards))
                    score += 100;
                if (isStraightFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 90;
                    } 
                }
                if (isFourOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 80;
                    } 
                }
                if (isFullHouse(deckCards))
                {
                    if (score == 0)
                    {
                        score += 70;
                    } 
                }
                if (isFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 60;
                    } 
                }
                if (isStraight(deckCards))
                {
                    if (score == 0)
                    {
                        score += 50;
                    } 
                }
                if (isThreeOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 40;
                    } 
                }
                if (isTwopair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 30;
                    } 
                }
                if (isPair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 20;
                    } 
                }
                highCard = 0;
                if (score == 0)
                    highCard = getHighCard(deckCards);
                Winners.Add(new winner { playerName = files[4], highestCard = highCard, score = score });
                label72.Text = score.ToString();
                label72.Show();
                #endregion

                #region Player 6 Deck of Cards
                deckCards.Clear();
                score = 0;
                label41.Text = allCard.cards[25].value.ToString() + " " + allCard.cards[25].suit.ToString(); label41.Show();
                label42.Text = allCard.cards[26].value.ToString() + " " + allCard.cards[26].suit.ToString(); label42.Show();
                label43.Text = allCard.cards[27].value.ToString() + " " + allCard.cards[27].suit.ToString(); label43.Show();
                label44.Text = allCard.cards[28].value.ToString() + " " + allCard.cards[28].suit.ToString(); label44.Show();
                label45.Text = allCard.cards[29].value.ToString() + " " + allCard.cards[29].suit.ToString(); label45.Show();
                for (int w = 0; w < 5; w++)
                {
                    deckCards.Add(new cardDetails.DeckCard { suit = allCard.cards[count].value.ToString(), value = allCard.cards[count].suit.ToString() });
                    count++;
                }
                if (isRoyalFlush(deckCards))
                    score += 100;
                if (isStraightFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 90;
                    } 
                }
                if (isFourOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 80;
                    } 
                }
                if (isFullHouse(deckCards))
                {
                    if (score == 0)
                    {
                        score += 70;
                    } 
                }
                if (isFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 60;
                    } 
                }
                if (isStraight(deckCards))
                {
                    if (score == 0)
                    {
                        score += 50;
                    } 
                }
                if (isThreeOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 40;
                    } 
                }
                if (isTwopair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 30;
                    } 
                }
                if (isPair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 20;
                    } 
                }
                highCard = 0;
                if (score == 0)
                    highCard = getHighCard(deckCards);
                Winners.Add(new winner { playerName = files[5], highestCard = highCard, score = score });
                label71.Text = score.ToString();
                label71.Show();
                #endregion

                #region Player 7 Deck of Cards
                deckCards.Clear();
                score = 0;
                label46.Text = allCard.cards[30].value.ToString() + " " + allCard.cards[30].suit.ToString(); label46.Show();
                label47.Text = allCard.cards[31].value.ToString() + " " + allCard.cards[31].suit.ToString(); label47.Show();
                label48.Text = allCard.cards[32].value.ToString() + " " + allCard.cards[32].suit.ToString(); label48.Show();
                label49.Text = allCard.cards[33].value.ToString() + " " + allCard.cards[33].suit.ToString(); label49.Show();
                label50.Text = allCard.cards[34].value.ToString() + " " + allCard.cards[34].suit.ToString(); label50.Show();
                for (int w = 0; w < 5; w++)
                {
                    deckCards.Add(new cardDetails.DeckCard { suit = allCard.cards[count].value.ToString(), value = allCard.cards[count].suit.ToString() });
                    count++;
                }
                if (isRoyalFlush(deckCards))
                    score += 100;
                if (isStraightFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 90;
                    }

                }
                if (isFourOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 80;
                    }

                }
                if (isFullHouse(deckCards))
                {
                    if (score == 0)
                    {
                        score += 70;
                    }

                }
                if (isFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 60;
                    }

                }
                if (isStraight(deckCards))
                {
                    if (score == 0)
                    {
                        score += 50;
                    }

                }
                if (isThreeOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 40;
                    }

                }
                if (isTwopair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 30;
                    }

                }
                if (isPair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 20;
                    }

                }
                highCard = 0;
                if (score == 0)
                    highCard = getHighCard(deckCards);
                Winners.Add(new winner { playerName = files[6], highestCard = highCard, score = score });
                label70.Text = score.ToString();
                label70.Show();
                #endregion

                #region Player 8 Deck of Cards
                deckCards.Clear();
                score = 0;
                label51.Text = allCard.cards[35].value.ToString() + " " + allCard.cards[35].suit.ToString(); label51.Show();
                label52.Text = allCard.cards[36].value.ToString() + " " + allCard.cards[36].suit.ToString(); label52.Show();
                label53.Text = allCard.cards[37].value.ToString() + " " + allCard.cards[37].suit.ToString(); label53.Show();
                label54.Text = allCard.cards[38].value.ToString() + " " + allCard.cards[38].suit.ToString(); label54.Show();
                label55.Text = allCard.cards[39].value.ToString() + " " + allCard.cards[39].suit.ToString(); label55.Show();
                for (int w = 0; w < 5; w++)
                {
                    deckCards.Add(new cardDetails.DeckCard { suit = allCard.cards[count].value.ToString(), value = allCard.cards[count].suit.ToString() });
                    count++;
                }
                if (isRoyalFlush(deckCards))
                    score += 100;
                if (isStraightFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 90;
                    }

                }
                if (isFourOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 80;
                    }

                }
                if (isFullHouse(deckCards))
                {
                    if (score == 0)
                    {
                        score += 70;
                    }

                }
                if (isFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 60;
                    }

                }
                if (isStraight(deckCards))
                {
                    if (score == 0)
                    {
                        score += 50;
                    }

                }
                if (isThreeOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 40;
                    }

                }
                if (isTwopair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 30;
                    }

                }
                if (isPair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 20;
                    }

                }
                highCard = 0;
                if (score == 0)
                    highCard = getHighCard(deckCards);
                Winners.Add(new winner { playerName = files[7], highestCard = highCard, score = score });
                label69.Text = score.ToString();
                label69.Show();
                #endregion

                #region Player 9 Deck of Cards
                deckCards.Clear();
                score = 0;
                label56.Text = allCard.cards[40].value.ToString() + " " + allCard.cards[40].suit.ToString(); label56.Show();
                label57.Text = allCard.cards[41].value.ToString() + " " + allCard.cards[41].suit.ToString(); label57.Show();
                label58.Text = allCard.cards[42].value.ToString() + " " + allCard.cards[42].suit.ToString(); label58.Show();
                label59.Text = allCard.cards[43].value.ToString() + " " + allCard.cards[43].suit.ToString(); label59.Show();
                label60.Text = allCard.cards[44].value.ToString() + " " + allCard.cards[44].suit.ToString(); label60.Show();
                for (int w = 0; w < 5; w++)
                {
                    deckCards.Add(new cardDetails.DeckCard { suit = allCard.cards[count].value.ToString(), value = allCard.cards[count].suit.ToString() });
                    count++;
                }
                if (isRoyalFlush(deckCards))
                    score += 100;
                if (isStraightFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 90;
                    }

                }
                if (isFourOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 80;
                    }

                }
                if (isFullHouse(deckCards))
                {
                    if (score == 0)
                    {
                        score += 70;
                    }

                }
                if (isFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 60;
                    }

                }
                if (isStraight(deckCards))
                {
                    if (score == 0)
                    {
                        score += 50;
                    }

                }
                if (isThreeOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 40;
                    }

                }
                if (isTwopair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 30;
                    }

                }
                if (isPair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 20;
                    }

                }
                highCard = 0;
                if (score == 0)
                    highCard = getHighCard(deckCards);
                Winners.Add(new winner { playerName = files[8], highestCard = highCard, score = score });
                label68.Text = score.ToString();
                label68.Show();
                #endregion

                #region Player 10 Deck of Cards
                deckCards.Clear();
                score = 0;
                label61.Text = allCard.cards[45].value.ToString() + " " + allCard.cards[45].suit.ToString(); label61.Show();
                label62.Text = allCard.cards[46].value.ToString() + " " + allCard.cards[46].suit.ToString(); label62.Show();
                label63.Text = allCard.cards[47].value.ToString() + " " + allCard.cards[47].suit.ToString(); label63.Show();
                label64.Text = allCard.cards[48].value.ToString() + " " + allCard.cards[48].suit.ToString(); label64.Show();
                label65.Text = allCard.cards[49].value.ToString() + " " + allCard.cards[49].suit.ToString(); label65.Show();
                for (int w = 0; w < 5; w++)
                {
                    deckCards.Add(new cardDetails.DeckCard { suit = allCard.cards[count].value.ToString(), value = allCard.cards[count].suit.ToString() });
                    count++;
                }
                if (isRoyalFlush(deckCards))
                    score += 100;
                if (isStraightFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 90;
                    }

                }
                if (isFourOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 80;
                    }

                }
                if (isFullHouse(deckCards))
                {
                    if (score == 0)
                    {
                        score += 70;
                    }

                }
                if (isFlush(deckCards))
                {
                    if (score == 0)
                    {
                        score += 60;
                    }

                }
                if (isStraight(deckCards))
                {
                    if (score == 0)
                    {
                        score += 50;
                    }

                }
                if (isThreeOfaKind(deckCards))
                {
                    if (score == 0)
                    {
                        score += 40;
                    }

                }
                if (isTwopair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 30;
                    }

                }
                if (isPair(deckCards))
                {
                    if (score == 0)
                    {
                        score += 20;
                    }

                }
                highCard = 0;
                if (score == 0)
                    highCard = getHighCard(deckCards);
                Winners.Add(new winner { playerName = files[9], highestCard = highCard, score = score });
                label67.Text = score.ToString();
                label67.Show();
                #endregion

            }
            catch(Exception)
            { }
        }
        private void Button2_Click_1(object sender, EventArgs e)
        {
            Int32[] arr = new int[Winners.Count];
            Int32[] highCard = new int[Winners.Count];
            for (int we = 0; we < Winners.Count; we++)
            {
                arr[we] = Winners[we].score == 0 ? 0 : Winners[we].score;
                highCard[we] = Winners[we].highestCard == 0 ? 0 : Winners[we].highestCard;
            }
            int maxNum = arr.Max();
            int maxCard = highCard.Max();
            String isNumOrString = null;
            if (maxNum == 0)
            {
                if (maxNum == 11)
                    isNumOrString = "JACK";
                if (maxNum == 12)
                    isNumOrString = "QUEEN";
                if (maxNum == 11)
                    isNumOrString = "KING";
                if (maxNum == 11)
                    isNumOrString = "ACE";
                else
                    isNumOrString = maxCard.ToString();
                MessageBox.Show("The card " + isNumOrString + " is the Winner.");
            }
            var query = Winners.GroupBy(x => x.score)
                  .Where(g => g.Count() > 1)
                  .Select(y => y.Key)
                  .ToList();
            int counts = 0;
            int[] getIndex = new int[query.Count];
            for(int re = 0; re < query.Count; re++)
            {
                if(query[re] != 0)
                    getIndex[re] = query[re] == 0 ? 0 : query[re];
            }
            if (maxNum > query[counts])
            {
                MessageBox.Show("Player with " + maxNum + " points is the winner.");
            }
            else
            {
                MessageBox.Show("Player with " + getIndex.Max() + " points is the winner.");
            }

        }
        bool IsSequential(int[] array)
        {
            return array.Zip(array.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        // ----------------------- ROYAL FLUSH ------------------------------------//
        private Boolean isRoyalFlush(List<cardDetails.DeckCard> decks)
        {
            /*Diamonds, Hearts, Clubs, Spades*/
            if (decks.Count < 5 || decks.Count > 5)
                return false;
            int tag = 0;
            if (new[] {
                decks[0].value.ToString(),
                decks[1].value.ToString(),
                decks[2].value.ToString(),
                decks[3].value.ToString(),
                decks[4].value.ToString()}.All(x => x == "Diamonds"))
            {
                for(int q = 0; q < decks.Count; q++)
                {
                    switch (decks[q].suit.ToUpper().ToString())
                    {
                        case "10":
                        case "JACK":
                        case "QUEEN":
                        case "KING":
                        case "ACE":
                            break;
                        default:
                            return false;
                    }

                }
                tag = 1;
                
            }
            else
            {
                return false;
            }
                
            if (new[] {
                decks[0].value.ToString(),
                decks[1].value.ToString(),
                decks[2].value.ToString(),
                decks[3].value.ToString(),
                decks[4].value.ToString()}.All(x => x == "Hearts"))
            {
                for (int q = 0; q < decks.Count; q++)
                {
                    switch (decks[q].suit.ToUpper().ToString())
                    {
                        case "10":
                        case "JACK":
                        case "QUEEN":
                        case "KING":
                        case "ACE":
                            break;
                        default:
                            return false;
                    }

                }
                tag = 1;
            }
            else
            {
                return false;
            }
                
            if (new[] {
                decks[0].value.ToString(),
                decks[1].value.ToString(),
                decks[2].value.ToString(),
                decks[3].value.ToString(),
                decks[4].value.ToString()}.All(x => x == "Clubs"))
            {
                for (int q = 0; q < decks.Count; q++)
                {
                    switch (decks[q].suit.ToUpper().ToString())
                    {
                        case "10":
                        case "JACK":
                        case "QUEEN":
                        case "KING":
                        case "ACE":
                            break;
                        default:
                            return false;
                    }

                }
                tag = 1;
            }
            else
            {
                return false;
            }
                
            if (new[] {
                decks[0].value.ToString(),
                decks[1].value.ToString(),
                decks[2].value.ToString(),
                decks[3].value.ToString(),
                decks[4].value.ToString()}.All(x => x == "Spades"))
            {
                for (int q = 0; q < decks.Count; q++)
                {
                    switch (decks[q].suit.ToUpper().ToString())
                    {
                        case "10":
                        case "JACK":
                        case "QUEEN":
                        case "KING":
                        case "ACE":
                            break;
                        default:
                            return false;
                    }

                }
                tag = 1;
            }
            else
            {
                return false;
            }
            if(tag != 0)
                return true;
            return false;
                

        }

        // ----------------------- STRAIGHT FLUSH ------------------------------------//
        private Boolean isStraightFlush(List<cardDetails.DeckCard> decks)
        {
            if (decks.Count < 5 || decks.Count > 5)
                return false;
            int[] secuencial = null;
            if (new[] {
                decks[0].value.ToString(),
                decks[1].value.ToString(),
                decks[2].value.ToString(),
                decks[3].value.ToString(),
                decks[4].value.ToString()}.All(x => x == "Diamonds"))
            {
                int value = 0;
                for (int q = 0; q < decks.Count; q++)
                {
                    value = decks[q].suit.ToUpper().Equals("JACK") ? 11 : decks[q].suit.ToUpper().Equals("QUEEN") ? 12 : decks[q].suit.ToUpper().Equals("KING") ? 13 : decks[q].suit.ToUpper().Equals("ACE") ? 14 : Convert.ToInt32(decks[q].suit);
                    secuencial[q] = value;
                }
                if (IsSequential(secuencial))
                    return true;
            }
            else
            {
                return false;
            }
            if (new[] {
                decks[0].value.ToString(),
                decks[1].value.ToString(),
                decks[2].value.ToString(),
                decks[3].value.ToString(),
                decks[4].value.ToString()}.All(x => x == "Hearts"))
            {
                int value = 0;
                for (int q = 0; q < decks.Count; q++)
                {
                    value = decks[q].suit.ToUpper().Equals("JACK") ? 11 : decks[q].suit.ToUpper().Equals("QUEEN") ? 12 : decks[q].suit.ToUpper().Equals("KING") ? 13 : decks[q].suit.ToUpper().Equals("ACE") ? 14 : Convert.ToInt32(decks[q].suit);
                    secuencial[q] = value;
                }
                if (IsSequential(secuencial))
                    return true;
            }
            else
            {
                return false;
            }
            if (new[] {
                decks[0].value.ToString(),
                decks[1].value.ToString(),
                decks[2].value.ToString(),
                decks[3].value.ToString(),
                decks[4].value.ToString()}.All(x => x == "Clubs"))
            {
                int value = 0;
                for (int q = 0; q < decks.Count; q++)
                {
                    value = decks[q].suit.ToUpper().Equals("JACK") ? 11 : decks[q].suit.ToUpper().Equals("QUEEN") ? 12 : decks[q].suit.ToUpper().Equals("KING") ? 13 : decks[q].suit.ToUpper().Equals("ACE") ? 14 : Convert.ToInt32(decks[q].suit);
                    secuencial[q] = value;
                }
                if (IsSequential(secuencial))
                    return true;
            }
            else
            {
                return false;
            }
            if (new[] {
                decks[0].value.ToString(),
                decks[1].value.ToString(),
                decks[2].value.ToString(),
                decks[3].value.ToString(),
                decks[4].value.ToString()}.All(x => x == "Spades"))
            {
                int value = 0;
                for (int q = 0; q < decks.Count; q++)
                {
                    value = decks[q].suit.ToUpper().Equals("JACK") ? 11 : decks[q].suit.ToUpper().Equals("QUEEN") ? 12 : decks[q].suit.ToUpper().Equals("KING") ? 13 : decks[q].suit.ToUpper().Equals("ACE") ? 14 : Convert.ToInt32(decks[q].suit);
                    secuencial[q] = value;
                }
                if (IsSequential(secuencial))
                    return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        // ----------------------- FOUR OF A KIND ------------------------------------//
        private Boolean isFourOfaKind(List<cardDetails.DeckCard> decks)
        {
            try
            {
                var query = decks.GroupBy(x => x.suit)
                  .Where(g => g.Count() > 3)
                  .Select(y => y.Key)
                  .ToList();
                if (query.Count == 0)
                    return false;
            }
            
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        // ----------------------- FULL HOUSE ------------------------------------//
        private Boolean isFullHouse(List<cardDetails.DeckCard> decks)
        {
            try
            {
                var query1 = decks.GroupBy(x => x.suit)
                  .Where(g => g.Count() == 3)
                  .Select(y => y.Key)
                  .ToList();
                if (query1.Count == 0)
                    return false;
                else
                {
                    var query2 = decks.GroupBy(x => x.suit)
                    .Where(g => g.Count() == 2)
                    .Select(y => y.Key)
                    .ToList();
                    if (query2.Count == 0)
                        return false;
                }
            }

            catch (Exception)
            {
                return false;
            }
            return true;
        }

        // ----------------------- FLUSH ----------------------------------------//
        private Boolean isFlush(List<cardDetails.DeckCard> decks)
        {
            if (decks.Count < 5 || decks.Count > 5)
                return false;
            if (new[] {
                decks[0].value.ToString(),
                decks[1].value.ToString(),
                decks[2].value.ToString(),
                decks[3].value.ToString(),
                decks[4].value.ToString()}.All(x => x == "Diamonds"))
            {
                return true;
            }
            if (new[] {
                decks[0].value.ToString(),
                decks[1].value.ToString(),
                decks[2].value.ToString(),
                decks[3].value.ToString(),
                decks[4].value.ToString()}.All(x => x == "Hearts"))
            {
                return true;
            }
            if (new[] {
                decks[0].value.ToString(),
                decks[1].value.ToString(),
                decks[2].value.ToString(),
                decks[3].value.ToString(),
                decks[4].value.ToString()}.All(x => x == "Ace"))
            {
                return true;
            }
            if (new[] {
                decks[0].value.ToString(),
                decks[1].value.ToString(),
                decks[2].value.ToString(),
                decks[3].value.ToString(),
                decks[4].value.ToString()}.All(x => x == "Clubs"))
            {
                return true;
            }
            return false;
        }

        // ----------------------- STRAIGHT --------------------------------------//
        private Boolean isStraight(List<cardDetails.DeckCard> decks)
        {
            try
            {
                int[] secuencial = new int[5];
                int value = 0;
                for (int q = 0; q < decks.Count; q++)
                {
                    value = decks[q].suit.ToUpper().Equals("JACK") ? 11 : decks[q].suit.ToUpper().Equals("QUEEN") ? 12 : decks[q].suit.ToUpper().Equals("KING") ? 13 : decks[q].suit.ToUpper().Equals("ACE") ? 14 : Convert.ToInt32(decks[q].suit);
                    secuencial[q] = value;
                }
                if (IsSequential(secuencial))
                    return true;
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }

        // ----------------------- THREE OF A KIND----------------------------------//
        private Boolean isThreeOfaKind(List<cardDetails.DeckCard> decks)
        {
            try
            {
                var query = decks.GroupBy(x => x.suit)
                  .Where(g => g.Count() == 3)
                  .Select(y => y.Key)
                  .ToList();
                if (query.Count == 0)
                    return false;
            }

            catch (Exception)
            {
                return false;
            }
            return true;
        }

        // ----------------------- PAIR --------------------------------------//
        private Boolean isPair(List<cardDetails.DeckCard> decks)
        {
            try
            {
                var query = decks.GroupBy(x => x.suit)
                  .Where(g => g.Count() == 2)
                  .Select(y => y.Key)
                  .ToList();
                if (query.Count == 1)
                    return true;
            }

            catch (Exception)
            {
                return false;
            }
            return false;
        }

        // ----------------------- TWO PAIR----------------------------------//
        private Boolean isTwopair(List<cardDetails.DeckCard> decks)
        {
            try
            {
                var query = decks.GroupBy(x => x.suit)
                  .Where(g => g.Count() == 2)
                  .Select(y => y.Key)
                  .ToList();
                if (query.Count == 2)
                    return true;
            }

            catch (Exception)
            {
                return false;
            }
            return false;
        }

        // ----------------------- HIGH CARD----------------------------------//
        private Int32 getHighCard(List<cardDetails.DeckCard> decks)
        {
            int[] secuencial = new int[5];
            int value = 0;
            for (int q = 0; q < decks.Count; q++)
            {
                value = decks[q].suit.ToUpper().Equals("JACK") ? 11 : decks[q].suit.ToUpper().Equals("QUEEN") ? 12 : decks[q].suit.ToUpper().Equals("KING") ? 13 : decks[q].suit.ToUpper().Equals("ACE") ? 14 : Convert.ToInt32(decks[q].suit);
                secuencial[q] = value;
            }
            int maxValue = secuencial.Max();
            return maxValue;
        }

    }
}

