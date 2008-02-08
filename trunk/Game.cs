/*
 * Created by SharpDevelop.
 * User: samsonl
 * Date: 22/01/2008
 * Time: 10:54 AM
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
	/// <summary>
	/// Description of Game.
	/// </summary>
	public class Game
	{
		KeyBoardController kbc;
		Graphics		   g;
		ArrayList 		   futures = new ArrayList();
		ArrayList		   sprites;
		int 			   tick = 0;
		
		public Game(Graphics g, KeyBoardController kbc)
		{
			this.g = g;
			this.kbc = kbc;
			
		}
		
		
		public ArrayList initialiseSprites() {
			ArrayList sprites = new ArrayList();
			
 			Sprite sprite = new TankSprite();
 			sprite.setController(kbc);
 			
 			Sprite sprite2 = new TankSprite();
 			sprite2.setController(new SimpleAIController(100,100,2));
 			
 			Sprite sprite3 = new TankSprite();
 			sprite3.setController(new SimpleAIController(150,200,2));

 			sprites.Add(sprite);
 			sprites.Add(sprite2);
 			sprites.Add(sprite3);

 			sprite = new TextDisplaySprite();
 			sprite.setController(new TextDisplayController());
 			sprites.Add(sprite);

 			this.sprites = sprites;
 			
 			return sprites;
		}
		
		public void collisions(ArrayList sprites) {
			   // Collisions
				    // a,b ( a and b collided )
				    // or sprite.collisions ( b,c,d ) ?
				    //
				
		    foreach ( Sprite sprite in sprites ) {
				RectangleF bounds = sprite.getBounds();
				foreach ( Sprite sprite2 in sprites ) {
					if ( sprite != sprite2 ) {
					   	RectangleF bounds2 = sprite2.getBounds();
					   	if ( bounds.IntersectsWith(bounds2) ) {
					   		//Console.WriteLine("*HIT*");
					   		sprite.collision(sprite2);
					   		g.FillRectangle(System.Drawing.Brushes.Aquamarine,bounds.X,bounds.Y,bounds.Width,bounds.Height);
					   	}
				    }
				    		
			   	}
			}

		}
		
		public void deleteDestroyed(ArrayList sprites) {
			ArrayList delete = new ArrayList();
			
			foreach ( Sprite sprite in sprites ) {
			  	if ( sprite.isDestroyed() )
			   	    delete.Add(sprite);
			}
			
			foreach ( Sprite sprite in delete ) {
				sprites.Remove(sprite);
			}

		}
		
		public void redraw(ArrayList sprites) {
		 	tick++;
		 	
			for ( int x=0;x<sprites.Count;x++ ) {
			    Sprite csprite = (Sprite)sprites[x];
 				csprite.tick(sprites, this);
 				csprite.draw(g);
 						
			}
			
		}
		
		public void addFuture(int when, Future f) {
			futures.Add(new Object[]{tick+when,f});
		}
		
		public void runFutures() {
			ArrayList deletes = new ArrayList();
			
			foreach ( Object [] f in futures ) {
				if ( tick == (int)(f[0]) ) {
					((Future)f[1]).run();
					deletes.Add(f);
				}
			}
			
			foreach ( Object [] f in deletes ) 
				futures.Remove(f);
		}

	}
	
	
}
