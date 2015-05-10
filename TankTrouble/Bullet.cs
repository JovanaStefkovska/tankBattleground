using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace TankTrouble
{
  public class Bullet
    {
        //position 
       public int X, Y;
      
      
 
       Direction direction;
      //rectangle of the field 
       Rectangle bounds;
       public Brush brush;

       public bool shouldDraw;
        public Bullet(int X, int Y,Direction d, Rectangle r)
        {
            this.X = X;
            this.Y = Y;

            direction = d;
            bounds = r;
        
            shouldDraw = true;
        }
      //draw bullet
        public void Draw(Graphics g)
        {
            brush = new SolidBrush(Color.Black);
            if (shouldDraw)
            {
                g.FillEllipse(brush, X, Y, 8, 8);
            }
            brush.Dispose();
            
        }
      // define bullet movement
        public void Move(bool[][] blockMatrix, Rectangle[][] rectangleMatrix)
        {
            if (direction.Equals(Direction.Up))
            {
                if (Y - 10 > bounds.Top)
                {
                    Y -= 10;
                }
                else if (Y <= bounds.Top)
                {
                    shouldDraw = false;
                }
                else
                {
                    Y = Y - 5;
                }
               
            }
            else if (direction.Equals(Direction.Down))
            {
               if (Y + 10 < bounds.Bottom)
                {
                    Y += 10;
                }
                else if (Y + 10 >= bounds.Bottom)
                {
                    shouldDraw = false;
                }
               
               
            }
            else if (direction.Equals(Direction.Left))
            {
                if (X - 10 > bounds.Left)
                {
                    X -= 10;
                }
                else if (X <= bounds.Left)
                {
                    shouldDraw = false;
                }
                else
                {
                    X -= 5;
                }
                
               
               
            }
            else if (direction.Equals(Direction.Right))
            {
                if (X + 10 < bounds.Right)
                {
                    X += 10;
                }
                else if (X  + 10 >= bounds.Right)
                {
                    shouldDraw = false;
                }
                
                
               
            }

            if (!checkMove(blockMatrix, rectangleMatrix))
            {
                shouldDraw = false;
            }
            
        }
      // check if the bullet strikes a wall or boundries
        public bool checkMove(bool[][] blockMatrix, Rectangle[][] rectangleMatrix)
        {
            for (int i = 0; i < Scene.FIELD_HEIGHT / Scene.block_HEIGHT; i++)
            {
                for (int j = 0; j < Scene.FIELD_WIDTH / Scene.block_WIDTH; j++)
                {
                    if (blockMatrix[i][j])
                    {
                       if(X >= rectangleMatrix[i][j].Left - 2  && X <= rectangleMatrix[i][j].Right + 10 && Y >= rectangleMatrix[i][j].Top && Y <= rectangleMatrix[i][j].Bottom)
                       {
                           return false;
                       }
                       else if (X <= rectangleMatrix[i][j].Right - 2 && X >= rectangleMatrix[i][j].Left - 10 && Y >= rectangleMatrix[i][j].Top && Y <= rectangleMatrix[i][j].Bottom)
                       {
                           return false;
                       }
                       else if (Y >= rectangleMatrix[i][j].Top - 2  && Y <= rectangleMatrix[i][j].Bottom + 10 && X >= rectangleMatrix[i][j].Left && X <= rectangleMatrix[i][j].Right)
                       {
                           return false;

                       }
                       else if (Y <= rectangleMatrix[i][j].Bottom - 2 && Y >= rectangleMatrix[i][j].Top - 10 && X >= rectangleMatrix[i][j].Left && X <= rectangleMatrix[i][j].Right)
                       {
                           return false;
                       }

                    }
                }
            }
            return true;
        }

    }
}
