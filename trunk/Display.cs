/*
 * Created by SharpDevelop.
 * User: samsonl
 * Date: 11/01/2008
 * Time: 4:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Collections;

namespace tankzor
{
	// -------------------------------------------------------------------------------------------------
	/// <summary>
	/// Description of Display.
	/// </summary>
	public partial class Display : Form
	{
		public Display()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		Thread             updaterThread;
		KeyBoardController kbc = new KeyBoardController();
		Graphics		   g;
		Game			   game;
		
		void Button1Click(object sender, EventArgs e)	{
			//g = Graphics.FromHwnd(this.Handle);
 			//updaterThread = new Thread(new ThreadStart(this.run));
 			//updaterThread.Start();
		}

		
		public void run() {
			Color cb = Color.AntiqueWhite;
			
			ArrayList sprites = game.initialiseSprites();
			
 			while ( true ) {
				
				try {
					
				
				this.Invoke((MethodInvoker) delegate() {
 					    
			        long start = System.DateTime.Now.Ticks;
			        
			        game.redraw(sprites);
				    game.deleteDestroyed(sprites);
				    game.collisions(sprites);
			        
			        System.Threading.Thread.Sleep(10);
					g.Clear(cb);
					
					long end = System.DateTime.Now.Ticks;
                    long elapsed = (end - start) / 10000; // c
                    
                    //Console.WriteLine("Redraw = "+elapsed);

			    });
				
				} catch ( NullReferenceException e ) {
					
					Console.WriteLine(e.ToString());
					Console.WriteLine(e.StackTrace);
					System.Threading.Thread.Sleep(30000);
			
				}
 				
			}
		}
		
		
		void DisplayKeyUp(object sender, KeyEventArgs e) {
		//	System.Windows.Forms.MessageBox.Show("e="+e.KeyValue);
			Console.WriteLine("UP:"+e.KeyValue);
			
			kbc.Key(e.KeyValue);
		}
		

		void DisplayShown(object sender, EventArgs e)
		{
			g = Graphics.FromHwnd(this.Handle);
 			updaterThread = new Thread(new ThreadStart(this.run));
 			updaterThread.Start();
 			
 			game = new Game(g, kbc);
 			
			
		}
		
		void DisplayLoad(object sender, EventArgs e)
		{
			
		}
	}
	
}
