//was able to make 2 improvement to existing program seperated util class to own class and changed button event handlers to one handler.
//8/4/21 Progrmam works
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        #region variables
        //variables
        string playOne = "Player-1", playTwo = "Player-2";
        private System.Windows.Forms.Button[] _buttonArray;
        bool playerOneTurn = true;  //if its players one turn its true else false and player two's turn
        bool computer = false;      //if play against computer then equals true
        bool gameOver = false;      //used for computer logic if true then it will stop the computer turn
        int oneCount = 0;           //counters for player one wins
        int twoCount = 0;           //counters for player two wins
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _buttonArray = new Button[9] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };   //we called all the buttons we wish to select from form into button array
            statusLabel.Text = "Player-1 Place the Point";
            foreach (Button butt in _buttonArray)   //when load set all the buttons to disabled and then set text to null so no button id shows
            {
                butt.Enabled = false;
                butt.Text = "";
            }
           
      
        }

        //handles all selection buttons for x's and o's
        private void MyButton_Click(object sender, EventArgs e) 
        {

                //get a computer button
                Button button = sender as Button;
                if (playerOneTurn)
                {
                    button.ForeColor = Color.Blue;                      //set the X's font to Blue     ~!!!!!not working!!!!
                    button.Text = "X";                                  //set square to X
                    button.Enabled = false;                             //disable button so it can not be clicked again
                    statusLabel.Text = "Player-2 Place the Point";      //update the status label to tell player 2 its there turn
                    record_condition(playOne, button.Name, "Move to");  // record the move and send it to the list
                    playerOneTurn = false;                              //set the bool that defines player one's turn to false so it's player twos turn
                    if (TicTacToeUtils.CheckAndProcessWinner(_buttonArray)) //check for win
                    {
                        statusLabel.Text = "Player 1 Won The Match";
                        MessageBox.Show("Game Over.. Player 1 won the Match");
                        playButton.Text = "Start";                      //change reset to start
                        disable();
                        gameOver = true;                                //set game over so that 
                        oneCount++;
                        oneWinsLabel.Text = oneCount.ToString();

                    }
                    CheckForDraw();
                    if (computer && gameOver == false)
                    {   //make a computer move 
                        ComputerMove();
                        CheckForDraw();
                    }
                }
                else
                {
                        button.ForeColor = Color.Red;                       //Set the O's font color to Red         !!!!not working !!!!
                        button.Text = "O";                                  //set square to O
                        button.Enabled = false;                             //disable button so it can not be used again
                        statusLabel.Text = "Player-1 Place the Point";      //update the status to tell player 1 its their turn
                        record_condition(playTwo, button.Name, "Move to");  //record the move and send it to the list
                        playerOneTurn = true;                               //set the bool to true so it identifies player one as the person 
                        if (TicTacToeUtils.CheckAndProcessWinner(_buttonArray)) //check for win
                        {
                            statusLabel.Text = "Player 2 Won The Match";
                            MessageBox.Show("Game Over.. Player 2 won the Match");
                            playButton.Text = "Start";                      //change reset to start
                            disable();
                            twoCount++;
                            twoWinsLabel.Text = twoCount.ToString();
                        }
                }
                CheckForDraw();
            
        }
        void ComputerMove() //playe 2 moves for computer
        {
            CheckForDraw();
            //generate a random button
            ComputerMove comp = new ComputerMove();

            Button computerButton = comp.Computer(_buttonArray);

            //label1.Text = computerButton.Name; //used for testing random numbers

            computerButton.ForeColor = Color.Red;                       //Set the O's font color to Red         !!!!not working !!!!
            computerButton.Text = "O";                                  //set square to O
            computerButton.Enabled = false;                             //disable button so it can not be used again
            statusLabel.Text = "Player-1 Place the Point";      //update the status to tell player 1 its their turn
            record_condition(playTwo, computerButton.Name, "Move to");  //record the move and send it to the list
            playerOneTurn = true;                               //set the bool to true so it identifies player one as the person 
            if (TicTacToeUtils.CheckAndProcessWinner(_buttonArray)) //check for win
            {
                statusLabel.Text = "Computer Won The Match";
                MessageBox.Show("Game Over.. Computer won the Match");
                playButton.Text = "Start";                      //change reset to start
                disable();
                twoCount++;
                twoWinsLabel.Text = twoCount.ToString();

            }
 
        }

        void CheckForDraw()
        {
            //variable for count
            int count = 0;  // count to 9 and if 9 then game is a draw
            foreach(Button B in _buttonArray)
            {
                if (B.Text != "")
                {
                    //if buttons text is not equal to a null value then add to count
                    count++;
                }
            }
            if (count == 9 && gameOver != true)
            {
                statusLabel.Text = "Match is Draw";
                MessageBox.Show("Match is draw");
                playButton.Text = "Start";//change reset to start
                gameOver = true;
                disable();
                twoWinsLabel.Text = twoCount.ToString();
            }
        }

        void record_condition(string player, string position, string condition)     //Show's the players move in the ListBox takes current players name, button's Name, string that says "Move to"
        {
            if (playerOneTurn)   //it checks the player string
            {                      //uses the "Move to  and cuts the button.name down to the number at the beggining nameing convention used 0Button: result Move to 0
                listBox1.Items.Add(condition.ToString() + position.ToString().Substring(6)); //this says add to items and Move to Button_whatever
            }
            else  //else it's Player-2
            {
                listBox2.Items.Add(condition.ToString() + position.ToString().Substring(6));
            }
        }

        void disable() //disables button once game is over
        {
            //disable every button in _buttonArray once game is over
            try
            {
                for (int i = 0; i < 9; i++)
                {
                    _buttonArray[i].Enabled = false;

                }
            }
            catch (Exception ex)
            {
                //leave blank
            }
        }

        #region New Game
        /* Initializes the game board resetting all buttons to null and enabling them all text boxes are reset to default values
         */
        private void InitTicTacToe()
        {
            for (int i = 0; i < 9; i++)
            {
                _buttonArray[i].Text = ""; //set the buttons text to null
                _buttonArray[i].BackColor = Color.LightGray; //set all button colors to light gray
                _buttonArray[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Italic & System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))); //setup the font
            }
            playerOneTurn = true;       //sets player turn to player 1
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            statusLabel.Text = "";
            statusLabel.Text = "Player-1 Place the Point";
            gameOver = false;
            for (int i = 0; i < 9; i++)
            {
                _buttonArray[i].Enabled = true; //reset buttons to enable
            }
            playButton.Text = "Reset";  //change text of playButton from Start to reset
        }
        #endregion
        //starts the game or resets it.
        private void playButton_Click(object sender, EventArgs e)
        {
            InitTicTacToe();
        }
              
        //closes the program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {   //set two play mode
            computer = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //set vs computer mode
            computer = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 AboutBox = new AboutBox1();
            AboutBox.ShowDialog();
        }
    }
    

}
