using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex_Endless_Runner
{
    //Class with varibles//
    public partial class Form1 : Form
    {
        //varibles needed for this game//
        bool jumping = false; // boolean to check if player is jumping or not, This is a Boolean called jumping. This Boolean will be used to judge whether the T Rex has jumped in the game. We can only change this between true and false. //
        int jumpSpeed; //integer to set jump speed, This integer is called jump speed. This will contain a value of 10. The T Rex will jump 10 pixels from its location and it will be used to pull the player down using the jump speed as gravity.//
        int force = 12; //force of the jump in an integer,This integer called force will be used to figure out how faster the T Rex jumps up and how high he can do before coming down.//
        int score = 0; //default score integer set to 0,This integer will be keeping the score for the game. Each time a obstacles leaves the form successfully without hitting the player it 1 will be added to this integer.//
        int obstacleSpeed = 10;//the default speed for the obstacles,This integer will be used to animate the obstacles. This will pull the obstacles towards the left of the screen towards the player.//
        Random rand = new Random();// create a new random class,This random number generator called rnd will be used to calculate a random location for the obstacles to spawn once the game starts and when the reach the fast left of the screen.//
        int position;
        bool isGameOver = false;

        //constructor thats being called when the game loads//
        public Form1()
        {
            InitializeComponent();
            // The reset function is being called from the forms main function. This basically means that we are setting the reset function to run when the game starts.//
            GameReset();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       // linking jumpspeed to trex//
        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            // linking the jumpspeed integer with the player picture boxes to location//
            trex.Top += jumpSpeed;

            // show the score on the score text label//
            textScore.Text = "Score:" + score;

            // if jumping is true and force is less than 0//
            // then change jumping to false//


            // if jumping is true//
            // then change jump speed to -12 //
            // reduce force by 1//
            if (jumping == true && force < 0)
            {
                jumping= false;
            }

            if(jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }

            else
            {
                // else change the jump speed to 12//
                jumpSpeed = 12;
            }

            if (trex.Top > 354 && jumping == false)
            {
                force = 12;
                trex.Top = 355;
                jumpSpeed= 0;
            }
            // running foreach inside of the timer and making obstacles move down//
            foreach (Control x in this.Controls)
            {
                // is X is a picture box and it has a tag of obstacle//
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed; // move the obstacles towards the left//

                    if (x.Left < -100)
                    {
                        // if the obstacles have gone off the screen//
                        // then we respawn it off the far right//
                        // in this case we are calculating the form width and a random number between//
                        x.Left = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 15);

                        // we will add one to the score
                        score++;
                    }
                    // if the t rex collides with the obstacles//
                        if (trex.Bounds.IntersectsWith(x.Bounds))
                        {
                        gameTimer.Stop();
                        trex.Image = Properties.Resources.dead;
                        textScore.Text +=  " Press R to restart the game!";
                        isGameOver= true;
                        }
                }
            }

        }
        // spacekey fumctionality//
        //In this function we are checking if the space key is press and if jumping is equals to false then we change jumping back to false. It may seem simple but we are triggering the jump function//
        private void keyisdown(object sender, KeyEventArgs e)
        {
          if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }
        //First we are checking if the R key is pressed and released we run the game reset function, also we check if jumping is true then we can change jumping back to false.//
        private void keyisup(object sender, KeyEventArgs e)
        {
            if (jumping == true)
            {
                jumping = false;
            }
            // if the R key is pressed and released then we run the reset function//
            if (e.KeyCode == Keys.R && isGameOver == true)
            {
                GameReset();
            }
        }

        // GameReset Function //
        private void GameReset()
        {
            //putting default values inside of the function//
            force= 12;
            jumpSpeed= 0;
            jumping= false;
            score= 0;
            obstacleSpeed= 10;
            //using the textScore method made in the design portion we are pulling textScore from .text and adding that to the score value//
            textScore.Text = "Score:" + score;
            trex.Image = Properties.Resources.running;
            isGameOver = false;
            //setting the default location of trex from information given from design portion y location//
            trex.Top = 353;


            foreach (Control x in this.Controls)
            {
                // is X is a picture box and it has a tag of obstacle//
                if (x is PictureBox &&(string)x.Tag == "obstacle")
                {
                    // taking the position defined in the class and finding the width of the form and then on top of the width add rand number between 500 - 800 pixels and multiplying by 10, makes sure the obstacles are far enough away when game starts//
                    // all of the code below is being saved inside of the position//
                    position = this.ClientSize.Width + rand.Next(500,800) + (x.Width * 10); 

                    x.Left = position;
                }
            }
            //enabling the game timer//
            gameTimer.Start();
        }
    }
}
