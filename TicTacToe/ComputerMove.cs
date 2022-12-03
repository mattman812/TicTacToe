using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
 * cheap and easy code to simulate computers turn using random numbers.  if the random number
 */
namespace TicTacToe
{
    class ComputerMove
    {
        //bool firstTime = false; //use for later logic to make computer better
        // Winners contains all the array locations of
        // the winning combination -- if they are all 
        // either X or O (and not blank)
        static private int[,] Winners = new int[,]
				   {
						{0,1,2},
						{3,4,5},
						{6,7,8},
						{0,3,6},
						{1,4,7},
						{2,5,8},
						{0,4,8},
						{2,4,6}
				   };

        public ComputerMove()
        {
            //constructor
        }
            
        public Button Computer(Button[] butt)
        {
            //grabs a button number from one of three strategies 
            //if there is a winning move make it.
            //if there is a way to block player move make it.
            //if the first two moves can not be made randomly make a move.
            int a = ForTheWinStrategy(butt);
            int b = DefensiveStrategy(butt);
            int c = RandomStrategy(butt);

            if (a < 9)
                return butt[a];
            else if (b < 9)
                return butt[b];
            else
                return butt[c];
        }
        public int ForTheWinStrategy(Button[] butt)
        {   //Checks through winning instances to see if computer has two in a row with an open square to win
            //returns a value 0-8 for the squares if no move availible returns 9 
          int num = 9;
         
                for (int i = 0; i < 8; i++)
                {
                    int a = Winners[i, 0], b = Winners[i, 1], c = Winners[i, 2];		// get the indices

                    Button b1 = butt[a], b2 = butt[b], b3 = butt[c];


                    if (b1.Text == "O" && b2.Text == "O" && b3.Text == "")			// are they the same?
                    {
                        num = c;
                        break;
                    }
                    else if (b1.Text == "" && b2.Text == "O" && b3.Text =="O")
                    {
                        num = a;
                        break;
                    }

                    else if (b1.Text == "O" && b3.Text == "O"  && b2.Text == "")
                    {
                        num = b;
                        break;
                    }
                    else
                        num = 9;
            }//end of for loop
            return num;
        }
        public int DefensiveStrategy(Button[] butt)
        {   //checks instances of player move that against winners to make a blocking move 
            //returns a value 0-8 for the squares if no move availible returns 9 
             int num = 9;
         
                for (int i = 0; i < 8; i++)
                {
                    int a = Winners[i, 0], b = Winners[i, 1], c = Winners[i, 2];		// get the indices

                    Button b1 = butt[a], b2 = butt[b], b3 = butt[c];

                    if (b1.Text == "X" && b2.Text == "X" && b3.Text == "")			// are they the same?
                    {
                        num = c;
                        break;
                    }
                    else if (b1.Text == "" && b2.Text == "X" && b3.Text =="X")
                    {
                        num = a;
                        break;
                    }

                    else if (b1.Text == "X" && b3.Text == "X"  && b2.Text == "")
                    {
                        num = b;
                        break;
                    }
                    else
                        num = 9;
                }//end of for loop
                return num;
        }
        public int RandomStrategy(Button[] butt)
        {
            //find a random number 
            bool findNumber = false;  
             int b =1 ;

         
                while (findNumber == false)
                {
                    //generate a random number
                    Random random = new Random();
                    int randomNumber = random.Next(1, 9);

                    //check to see if button with random number has a text with null value
                    if (butt[randomNumber].Text == "")
                    {
                        b = randomNumber;
                        findNumber = true;
                        //firstTime = true;
                    }

                }//end while
                return b;
        }
    }
}
