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

        public readonly int FIELD_WIDTH = 900;
        public readonly int FIELD_HEIGHT = 600;
        public readonly int block_WIDTH = 10;
        public readonly int block_HEIGHT = 10;
        public readonly int frame_HEIGHT = 50;
        public readonly int frame_width = 50;
        public readonly int sidePanel = 300;
        public List<Keys> pressedKeys;
        public Rectangle boundsRectangle;
        public bool[][] blockMatrix;
        public Rectangle[][] rectangleMatrix;
        public Tank Tank1, Tank2;
        public SoundPlayer backgroundMusic;
        public SoundPlayer firedBullet;


        public Scene()
        {
            backgroundMusic = new SoundPlayer(global::TankTrouble.Properties.Resources.warMusic1);
            firedBullet = new SoundPlayer(global::TankTrouble.Properties.Resources.fire);
        }

        public void Game()
        {
           
            boundsRectangle = new Rectangle(frame_width, frame_HEIGHT, FIELD_WIDTH, FIELD_HEIGHT);
           
            Tank1 = new Tank(TankColor.Green, Direction.Right, boundsRectangle, 30, 30);
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
                    if (i == 10 && j > 30 && j < 60)
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
                    if (j == 30 && i >= 20 && i <= 28)
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
               this.firedBullet.Play();

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
               this.firedBullet.Play();

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

       public void timerTick()
       {
           Tank1.Fire(blockMatrix, rectangleMatrix);
           Tank2.Fire(blockMatrix, rectangleMatrix);
           Tank1.Destroy();
           Tank2.Destroy();
           MoveTanks();
       }


       public void Draw(Graphics g)
       {
           
          
           g.Clear(Color.DarkOrange);
           g.FillRectangle(Brushes.DarkGreen, boundsRectangle);
            
           for (int i = 0; i < FIELD_HEIGHT / block_HEIGHT; i++)
           {

               for (int j = 0; j < FIELD_WIDTH / block_WIDTH; j++)
                   if (blockMatrix[i][j])
                   {
                       rectangleMatrix[i][j] = new Rectangle(j * block_WIDTH + frame_width, i * block_HEIGHT + frame_HEIGHT, block_WIDTH, block_HEIGHT);
                       g.FillRectangle(Brushes.Maroon, rectangleMatrix[i][j]);
                   }
           }
           Tank1.addMatrix(rectangleMatrix);
           Tank2.addMatrix(rectangleMatrix);
           Tank1.Draw(g);
           Tank2.Draw(g);
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
