using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankTrouble
{
    public enum Direction { Up, Down, Left, Right }
    public enum TankColor { Green, Red }
    public partial class Form1 : Form
    {
  
        
        Scene scene;

        bool drawScene;
        Rectangle rect = new Rectangle(850, 100, 100, 100);
       
        public Form1()
        {
            
            InitializeComponent();
      
            DoubleBuffered = true;
            scene = new Scene();
            this.Height = scene.FIELD_HEIGHT + 2 * scene.frame_HEIGHT;
            this.Width = scene.FIELD_WIDTH + 2 * scene.frame_width + scene.sidePanel;
            drawScene = false;
            scene.Game();
            Timer t = new Timer();
            t.Tick += new EventHandler(timer_tick);
            t.Interval = 25;
            t.Start();

        }
       
        
        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {


            scene.keyDown(sender, e);    
            Invalidate();
                
              
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

            scene.keyPressed(sender, e);
            Invalidate();
        }
        
       

        public void timer_tick(object sender, EventArgs e)
        {
            scene.timerTick();
            Invalidate();
        }


       
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
          //  graphics.DrawImageUnscaledAndClipped(global::TankTrouble.Properties.Resources.welcome1,this.ClientRectangle);
            graphics.FillRectangle(Brushes.DarkBlue,rect);
            
            if (drawScene)
            {
              
                scene.Draw(graphics);
               
            }
        }

       

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            scene.keyUp(e);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Location.X > rect.Left && e.Location.X < rect.Right && e.Location.Y > rect.Top && e.Location.Y < rect.Bottom)
                drawScene = true;
        }

        
    }
}
