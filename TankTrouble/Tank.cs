using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankTrouble
{
    public class Tank
    {
        
        public int X;
        public int Y;
        public Image tankImage;
        public Direction tankDirection;
        Rectangle bounds, tankRectangle;
        public List<Bullet> bullets;
        public bool shouldDraw;
        Timer timer_explosion;
        TankColor color;
        public bool isDead;
        public Tank otherTank;
        public Rectangle[][] rectangleMatrix;
        SoundPlayer explodeSound;
        
        public int countTwoTimes;
        
        public Tank(TankColor c, Direction d, Rectangle r, int x, int y)
        {
            this.color = c;
            tankDirection = d;
            bullets = new List<Bullet>();
            bounds = r;
            if (color == TankColor.Green)
                tankImage = global::TankTrouble.Properties.Resources.greenTank_right;
            else
                tankImage = global::TankTrouble.Properties.Resources.redTank_Left;
            X = x+tankImage.Width/2;

            Y = y+tankImage.Height/2;
            shouldDraw=true;
            isDead = false;
            timer_explosion = new Timer();
            timer_explosion.Interval = 200;
            timer_explosion.Tick += new EventHandler(timer_explosion_tick);
            explodeSound = new SoundPlayer(global::TankTrouble.Properties.Resources.Explosion);
            //countTwoTimes = 0;
        }

        public void addOtherTank(Tank t)
        {
            otherTank = t;
        }
        public void addMatrix(Rectangle[][] rectangleMatrix)
        {
            this.rectangleMatrix = rectangleMatrix;
        }
        void timer_explosion_tick(object sender, EventArgs e)
        {
            otherTank.shouldDraw = false;
            if (!shouldDraw)
                timer_explosion.Stop();
            
            
        }

        
      
        public void Draw(Graphics g)
        {
          

            if (shouldDraw)
            {
                 if (isDead)
                {
                    tankImage = global::TankTrouble.Properties.Resources.kaboom1;
                    tankRectangle = new Rectangle(X, Y, tankImage.Width, tankImage.Height);
                    g.DrawImageUnscaledAndClipped(tankImage, tankRectangle);
                    
                 }
               else
                {
                    tankRectangle = new Rectangle(X, Y, tankImage.Width, tankImage.Height);
                    g.DrawImageUnscaledAndClipped(tankImage, tankRectangle);
                    foreach (Bullet b in bullets)
                    {
                        b.Draw(g);
                    }
                }
            }
            

            
        }
        public bool canMove(bool [][]blockMatrix, Rectangle [][]rectangleMatrix)
        {
           

            for (int i = 0; i < Scene.FIELD_HEIGHT / Scene.block_HEIGHT; i++)
            {
                for (int j = 0; j < Scene.FIELD_WIDTH / Scene.block_WIDTH; j++)
                {
                    
                   if (blockMatrix[i][j])
                    {
                        

                        if (tankDirection == Direction.Up)
                        {
                           Rectangle tnkRectangle = new Rectangle(X, Y - 10, tankImage.Width, tankImage.Height );
                            if (rectangleMatrix[i][j].IntersectsWith(tnkRectangle))
                            {
                                return false;
                            }
                            if (otherTank.tankRectangle.IntersectsWith(tankRectangle))

                            {
                                return false;
                            }
                           

                        }
                        else if (tankDirection == Direction.Down)
                        {
                            Rectangle tnkRectangle = new Rectangle(X, Y + 10, tankImage.Width, tankImage.Height);
                            if (rectangleMatrix[i][j].IntersectsWith(tnkRectangle))
                            {
                                return false;
                            }

                            

                            if (otherTank.tankRectangle.IntersectsWith(tankRectangle))
                            {
                                return false;
                            }
                           

                        }
                        else if (tankDirection == Direction.Left)
                        {
                            Rectangle tnkRectangle = new Rectangle(X - 10, Y, tankImage.Width, tankImage.Height);
                            if (rectangleMatrix[i][j].IntersectsWith(tnkRectangle))
                            {
                                return false;
                            }
                            if (otherTank.tankRectangle.IntersectsWith(tankRectangle))
                            {
                                return false;
                            }
                           
                            
                        }
                        else if (tankDirection == Direction.Right)
                        {
                           Rectangle tnkRectangle = new Rectangle(X + 10, Y, tankImage.Width, tankImage.Height);
                            if (rectangleMatrix[i][j].IntersectsWith(tnkRectangle))
                            {
                                return false;
                            }
                            
                            if (otherTank.tankRectangle.IntersectsWith(tankRectangle))
                            {
                                return false;
                            }
                            

                        }
                   
                    }
                     
                }
            }

       
         
           
                return true;
        }


        public bool check(Direction direction)
        {
            for (int i = 0; i < Scene.FIELD_HEIGHT / Scene.block_HEIGHT; i++)
            {
                for (int j = 0; j < Scene.FIELD_WIDTH / Scene.block_WIDTH; j++)
                {
                    Rectangle rect = new Rectangle(X, Y, tankImage.Width, tankImage.Height);
                    if (direction == Direction.Up)
                    {
                    
                        rect = new Rectangle(X, Y - 3, tankImage.Width, tankImage.Height);
                    }
                    if (direction == Direction.Left)
                    {
                        rect = new Rectangle(X - 3, Y, tankImage.Width, tankImage.Height);
                    }
                    if (direction == Direction.Right)
                    {
                        rect = new Rectangle(X  + 3, Y, tankImage.Width, tankImage.Height);
                    }
                    if (direction == Direction.Down)
                    {
                        rect = new Rectangle(X , Y + 3, tankImage.Width, tankImage.Height);
                    }
                    if (rect.IntersectsWith(rectangleMatrix[i][j]))
                    {
                        return false;
                    }
                    if (rect.IntersectsWith(otherTank.tankRectangle))
                    {
                        return false;
                    }
                }
            }
            return true;

        }
        public void Move(Rectangle bounds, Direction direction)
        {
            if (!isDead)
            {

                if (!check(direction))
                {
                    return ;
                }
                if (direction.Equals(Direction.Up))
                {

                  

                    if (color == TankColor.Green)
                        tankImage = global::TankTrouble.Properties.Resources.greenTank_up;
                    else
                        tankImage = global::TankTrouble.Properties.Resources.redTank_Up;
                    if (Y > bounds.Top)
                    {
                        Y -= 3;

                    }

                }
                else if (direction.Equals(Direction.Down))
                {
                   
                    if (color == TankColor.Green)
                        tankImage = global::TankTrouble.Properties.Resources.greenTank_down;
                    else
                        tankImage = global::TankTrouble.Properties.Resources.redTank_Down;

                    if (Y + tankImage.Height < bounds.Bottom)
                        Y += 3;
                }
                else if (direction.Equals(Direction.Left))
                {

                    if (color == TankColor.Green)
                        tankImage = global::TankTrouble.Properties.Resources.greenTank_left;
                    else
                        tankImage = global::TankTrouble.Properties.Resources.redTank_Left;
                    if (X > bounds.Left)
                        X -= 3;

                }
                else if (direction.Equals(Direction.Right))
                {
                    if (color == TankColor.Green)
                        tankImage = global::TankTrouble.Properties.Resources.greenTank_right;
                    else
                        tankImage = global::TankTrouble.Properties.Resources.redTank_Right;
                    if (X + tankImage.Width < bounds.Right)
                        X += 3;
                }


                tankDirection = direction;
            }
            
        }
        public void Fire(bool[][] blockMatrix, Rectangle[][] rectangleMatrix)
        {
            foreach (Bullet b in bullets)
                b.Move(blockMatrix, rectangleMatrix);
        }

        public void clearBullets()
        {
            bullets.Clear();
        }
      
        public bool Destroy()
        {

            foreach (Bullet b in bullets)
            {
                if (b.shouldDraw)
                {
                    if (b.X > otherTank.X && b.X < otherTank.X + otherTank.tankImage.Width && b.Y > otherTank.Y && b.Y < otherTank.Y + otherTank.tankImage.Height && otherTank.isDead != true)
                    {
                        b.shouldDraw = false;
                       // otherTank.tankImage = global::TankTrouble.Properties.Resources.kaboom1;

                        //Play explode sound
                        if (!otherTank.isDead)
                        {
                            explodeSound.Play();
                            otherTank.isDead = true;
                        }
                        
                      
                        otherTank.clearBullets();
                        timer_explosion.Start();
                       
                       
                        return true;
                        
                    }
                        
                }
                
            }
            return false;

            
        }
    }
}
