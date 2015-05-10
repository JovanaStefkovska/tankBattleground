using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace TankTrouble
{
    public class Scene
    {

        public static readonly int FIELD_WIDTH = 900;
        public static readonly int FIELD_HEIGHT = 600;
        public static readonly int block_WIDTH = 10;
        public static readonly int block_HEIGHT = 10;
        public static readonly int frame_HEIGHT = 50;
        public static readonly int frame_width = 50;
        public static readonly int sidePanel = 300;
        public List<Keys> pressedKeys;
        public Rectangle boundsRectangle;
        public bool[][] blockMatrix;
        public Rectangle[][] rectangleMatrix;
        public Tank Tank1, Tank2;
        SoundPlayer fireSound;

        public  int countPlayer1 ;
        public  int countPlayer2 ;
        public int countTwoTimes = 0;
        Image brick, redtank, greenTank;
        Timer newGame;
        Image ground;
        public Scene()
        {
            fireSound = new SoundPlayer(global::TankTrouble.Properties.Resources.fire);
             newGame = new Timer();
            newGame.Interval = 1500;
            newGame.Tick += new EventHandler(newGame_tick);
            brick = global::TankTrouble.Properties.Resources.brick;
            redtank = global::TankTrouble.Properties.Resources.redTank_Left;
            greenTank = global::TankTrouble.Properties.Resources.greenTank_left;
            ground = global::TankTrouble.Properties.Resources._89F_ground_bottomjpg;
            countPlayer1 = 0;
            countPlayer2 = 0;
        }
        public void newGame_tick(object sender, EventArgs e)
        {
            Game();
            newGame.Stop();
        }
        public void Game()
        {
           
            boundsRectangle = new Rectangle(frame_width, frame_HEIGHT, FIELD_WIDTH, FIELD_HEIGHT);
           
            Tank1 = new Tank(TankColor.Green, Direction.Right, boundsRectangle, 30, 20);
            Tank2 = new Tank(TankColor.Red, Direction.Left, boundsRectangle, FIELD_WIDTH -80, FIELD_HEIGHT-60);
            pressedKeys = new List<Keys>();
            Tank1.addOtherTank(Tank2);
            Tank2.addOtherTank(Tank1);
           
            generateLayout();


        }
        public void generateLayout()
        {

            blockMatrix = new bool[FIELD_HEIGHT / block_HEIGHT][];
            rectangleMatrix = new Rectangle[FIELD_HEIGHT / block_HEIGHT][];
            for (int i = 0; i < FIELD_HEIGHT / block_HEIGHT; i++)
            {
                blockMatrix[i] = new bool[FIELD_WIDTH / block_WIDTH];
                rectangleMatrix[i] = new Rectangle[FIELD_WIDTH / block_WIDTH];
            }

            for (int i = 0; i < FIELD_HEIGHT/block_HEIGHT; i++)
            {
                for (int j = 0; j < FIELD_WIDTH / block_WIDTH; j++)
                {
                    if (i == 10 && j > 33 && j < 60)
                    {
                        blockMatrix[i][j] = true;
                       
                    }
                    if (i == 10 && j < 25)
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (j == 10 && i > 5 && i < 10)
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (j == 30 && i >= 20 && i < 28)
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (j == 50 && i >= 18 && i <= 20)
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (i == 20 && j >10 && j < 30)
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (j == 38 && i > 42 && i <= 50)
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (j == 60 && i >= 30 && i <= 40)
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (j == 75 && i >= 20 && i <= 30)
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (i == 20 && j > 50)
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (i == 40 && j > 60 && j < 75)
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (i == 50 && j < 38)
                    {
                        blockMatrix[i][j] = true;
                    }

                    if (i == 50 && j > 70)
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (i == 35 && j < 35) 
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (j == 80 && i < 10 )
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (i == 40 && j >= 50 && j <= 70)
                    {
                        blockMatrix[i][j] = true;
                    }
                    if (j == 30 && i > 56)
                    {
                        blockMatrix[i][j] = true;
                    }

                }

            }
        }
       public void MoveTanks()
        {


            if (pressedKeys.Contains(Keys.W))
            {
                if (Tank1.canMove(blockMatrix, rectangleMatrix) || Tank1.tankDirection != Direction.Up)
                {
                    Tank1.tankDirection = Direction.Up;
                    Tank1.Move(boundsRectangle, Direction.Up);
                }
            }
            else if (pressedKeys.Contains(Keys.S))
            {
                if (Tank1.canMove(blockMatrix, rectangleMatrix) || Tank1.tankDirection != Direction.Down)
                {
                    Tank1.Move(boundsRectangle, Direction.Down);
                    Tank1.tankDirection = Direction.Down;
                }
            }
            else if (pressedKeys.Contains(Keys.A))
            {
                if (Tank1.canMove(blockMatrix, rectangleMatrix) || Tank1.tankDirection != Direction.Left)
                {
                    Tank1.tankDirection = Direction.Left;
                    Tank1.Move(boundsRectangle, Direction.Left);
                }
            }
            else if (pressedKeys.Contains(Keys.D))
            {
                if (Tank1.canMove(blockMatrix, rectangleMatrix) || Tank1.tankDirection != Direction.Right)
                {
                    Tank1.tankDirection = Direction.Right;
                   Tank1.Move(boundsRectangle, Direction.Right);
                }
            }


            if (pressedKeys.Contains(Keys.Up))
            {
                if (Tank2.canMove(blockMatrix, rectangleMatrix) || Tank2.tankDirection != Direction.Up)
                {
                    Tank2.tankDirection = Direction.Up;
                    Tank2.Move(boundsRectangle, Direction.Up);
                }
            }

            else if (pressedKeys.Contains(Keys.Down))
            {
                if (Tank2.canMove(blockMatrix, rectangleMatrix) || Tank2.tankDirection != Direction.Down)
                {
                    Tank2.Move(boundsRectangle, Direction.Down);
                  Tank2.tankDirection = Direction.Down;
                }
            }
            else if (pressedKeys.Contains(Keys.Left))
            {
                if (Tank2.canMove(blockMatrix, rectangleMatrix) || Tank2.tankDirection != Direction.Left)
                {
                    Tank2.tankDirection = Direction.Left;
                   Tank2.Move(boundsRectangle, Direction.Left);
                }
            }
            else if (pressedKeys.Contains(Keys.Right))
            {
                if (Tank2.canMove(blockMatrix, rectangleMatrix) || Tank2.tankDirection != Direction.Right)
                {
                    Tank2.tankDirection = Direction.Right;
                    Tank2.Move(boundsRectangle, Direction.Right);
                }
            }


        }



       public void keyPressed(object sender, KeyPressEventArgs e)
       {
           if (e.KeyChar == (char)Keys.Tab)
           {
               //Fire sound
               if (!Tank1.isDead)
               {
                   fireSound.Play();
               }

               if (!Tank1.isDead)
               {
                   if (Tank1.tankDirection == Direction.Right)
                       Tank1.bullets.Add(new Bullet(Tank1.X + Tank1.tankImage.Width, Tank1.Y + Tank1.tankImage.Height / 2 - 5, Tank1.tankDirection, boundsRectangle));
                   if (Tank1.tankDirection == Direction.Left)
                       Tank1.bullets.Add(new Bullet(Tank1.X, Tank1.Y + Tank1.tankImage.Height / 2, Tank1.tankDirection, boundsRectangle));
                   if (Tank1.tankDirection == Direction.Up)
                       Tank1.bullets.Add(new Bullet(Tank1.X + Tank1.tankImage.Width / 2, Tank1.Y, Tank1.tankDirection, boundsRectangle));
                   if (Tank1.tankDirection == Direction.Down)
                       Tank1.bullets.Add(new Bullet(Tank1.X + Tank1.tankImage.Width / 2, Tank1.Y + Tank1.tankImage.Height, Tank1.tankDirection, boundsRectangle));
               }

           }
           if (e.KeyChar == (char) Keys.Space)
           {
               //Fire sound
               if (!Tank2.isDead)
               {
                   fireSound.Play();
               }

               if (!Tank2.isDead)
               {
                   if (Tank2.tankDirection == Direction.Right)
                       Tank2.bullets.Add(new Bullet(Tank2.X + Tank2.tankImage.Width, Tank2.Y + Tank2.tankImage.Height / 2 - 5, Tank2.tankDirection, boundsRectangle));
                   if (Tank2.tankDirection == Direction.Left)
                       Tank2.bullets.Add(new Bullet(Tank2.X, Tank2.Y + Tank2.tankImage.Height / 2, Tank2.tankDirection, boundsRectangle));
                   if (Tank2.tankDirection == Direction.Up)
                       Tank2.bullets.Add(new Bullet(Tank2.X + Tank2.tankImage.Width / 2, Tank2.Y, Tank2.tankDirection, boundsRectangle));
                   if (Tank2.tankDirection == Direction.Down)
                       Tank2.bullets.Add(new Bullet(Tank2.X + Tank2.tankImage.Width / 2, Tank2.Y + Tank2.tankImage.Height, Tank2.tankDirection, boundsRectangle));
               }

           }
       }

       public bool timerTick()
       {
           Tank1.Fire(blockMatrix, rectangleMatrix);
           Tank2.Fire(blockMatrix, rectangleMatrix);
           
           if (Tank1.Destroy())
           {
               countPlayer1++;
               if (countPlayer1 == 3)
               {
                   DialogResult rez = MessageBox.Show("Player 1 is Victorious !", "We have a winner !",
                    MessageBoxButtons.OK);
                   if (rez == DialogResult.OK)
                       return true;
               }
               else
                   newGame.Start();
            
           }
           else if (Tank2.Destroy())
           {
               countPlayer2++;
               if (countPlayer2 == 3)
               {
                   DialogResult rez = MessageBox.Show("Player 2 is Victorious !", "We have a winner !",
                    MessageBoxButtons.OK);
                   if (rez == DialogResult.OK)
                       return true;
               }
               else
                   newGame.Start();
               
           }
           MoveTanks();
           return false;
       }


       public void Draw(Graphics g)
       {
           Pen p = new Pen(new SolidBrush(Color.Black), 8);
           Color background = Color.FromArgb(116, 77, 40);
           g.Clear(background);
           Color grassColor = Color.FromArgb(0, 92, 9);
           Brush b = new SolidBrush(grassColor);

           g.DrawRectangle(p, boundsRectangle);
           g.DrawImageUnscaledAndClipped(greenTank, new Rectangle(FIELD_WIDTH + 2 * frame_width + 50, 140, greenTank.Width, greenTank.Height));
           g.DrawImageUnscaledAndClipped(redtank, new Rectangle(FIELD_WIDTH + 2 * frame_width + 50, FIELD_HEIGHT - 160,redtank.Width, redtank.Height));
           g.DrawImageUnscaledAndClipped(ground, boundsRectangle);
            
           for (int i = 0; i < FIELD_HEIGHT / block_HEIGHT; i++)
           {

               for (int j = 0; j < FIELD_WIDTH / block_WIDTH; j++)
                   if (blockMatrix[i][j])
                   {
                       rectangleMatrix[i][j] = new Rectangle(j * block_WIDTH + frame_width, i * block_HEIGHT + frame_HEIGHT, block_WIDTH, block_HEIGHT);
                       g.DrawImageUnscaledAndClipped(brick, rectangleMatrix[i][j]);
                   }
           }
           Tank1.addMatrix(rectangleMatrix);
           Tank2.addMatrix(rectangleMatrix);
           Tank1.Draw(g);
           Tank2.Draw(g);

           p.Dispose();
           b.Dispose();
          
       }

       public void keyUp(KeyEventArgs e)
       {
               pressedKeys.Remove(e.KeyCode);
       }

       public void keyDown(object sender, KeyEventArgs e)
       {
           if (!pressedKeys.Contains(e.KeyCode))
           {
               pressedKeys.Add(e.KeyCode);

           }
           

       }
    }
}
