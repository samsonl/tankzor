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
		
		public void run() {
			Color cb = Color.AntiqueWhite;
			
			ArrayList sprites = game.initialiseSprites();
			
 			while ( true ) {
				
				try {
					
				// TODO: Does this help framerates/speed?
				for ( int i = 0 ; i < 60;i++ ) {
						
				this.Invoke((MethodInvoker) delegate() {
 					 
			        long start = System.DateTime.Now.Ticks;
			        game.redraw(sprites);
				    game.deleteDestroyed(sprites);
				    game.collisions(sprites);
				    game.runFutures();
        			
                    int st = 20-(int)((System.DateTime.Now.Ticks-start)/10000);
                    if ( st > 0 )
                    	System.Threading.Thread.Sleep(st);
                    
					g.Clear(cb);
                    //Console.WriteLine("Redraw = "+elapsed);

			    });
					}
					
					
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
 			game = new Game(g, kbc);

 			updaterThread = new Thread(new ThreadStart(this.run));
 			updaterThread.Start();
 			
 			
			
		}
		
		void DisplayLoad(object sender, EventArgs e)
		{
			
		}
	}
	
}
