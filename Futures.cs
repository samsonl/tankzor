/*
 * Created by SharpDevelop.
 * User: samsonl
 * Date: 25/01/2008
 * Time: 11:10 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections;

namespace tankzor
{
	/// <summary>
	/// Description of Futures.
	/// </summary>
	public interface Future {
		void run();
	}


	public class PrintFuture : Future {
		public void run() {
			System.Console.WriteLine("Run Future");
		}
	}

	public class BombFuture : Future {
		ArrayList sprites;
		Sprite owner;
		
		public BombFuture(ArrayList sprites, Sprite owner) {
			this.sprites = sprites;	
			this.owner = owner;
		}
		public void run() {
			System.Console.WriteLine("BOMB!");
		    
			for ( int i = 0 ; i < 8 ; i++ ) {
				Sprite bullet = new BulletSprite(owner);
 				bullet.setController(new ProjectileController(400,300,i,1));
 				sprites.Add(bullet);
			}
			
		}
	}
	
		
	
}
