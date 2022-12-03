using System;
using System.Windows.Forms;
using System.Drawing;
namespace TicTacToe
{
    class TicTacToeUtils
    {
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
        //--------------------------------------------------------------
            // CheckAndProcessWinner determines if either X or O has won.
            // Once a winner has been determined, play stops.
            //--------------------------------------------------------------
            static public bool CheckAndProcessWinner(Button[] myControls)
            {
                bool gameOver = false;
                for (int i = 0; i < 8; i++)
                {
                    int a = Winners[i, 0], b = Winners[i, 1], c = Winners[i, 2];		// get the indices
                    // of the winners

                    Button b1 = myControls[a], b2 = myControls[b], b3 = myControls[c];// just to make the 
                    // the code readable

                    if (b1.Text == "" || b2.Text == "" || b3.Text == "")	// any of the squares blank
                        continue;											// try another -- no need to go further

                    if (b1.Text == b2.Text && b2.Text == b3.Text)			// are they the same?
                    {														// if so, they WIN!
                        b1.BackColor = b2.BackColor = b3.BackColor = Color.LightCoral;
                        b1.Font = b2.Font = b3.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Italic & System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
                        gameOver = true;
                        break;  // don't bother to continue
                    }
                }
                return gameOver;
                
            }
        }
    }

