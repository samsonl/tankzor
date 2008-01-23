/*
 * Created by SharpDevelop.
 * User: samsonl
 * Date: 22/01/2008
 * Time: 12:07 AM
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
	// -------------------------------------------------------------------------------------------------
	public interface Controller {
		int getX();
		int getY();
		int getDirection();
		void tick(ArrayList sprites, Sprite sprite);
		
	}

	abstract public class BaseController : Controller {
		protected int direction;
		protected int x;
		protected int y;

		protected int [,]lookup = { {0,-1},{1,-1},{1,0},{1,1},{0,1},{-1,1},{-1,0},{-1,-1} };

		public  virtual int getX() {
			return x;
				
		}
		public  virtual int getY() {
			return y;
			
		}

		public  virtual int getDirection() {
			return direction;
			
		}
		public  virtual void tick(ArrayList sprites, Sprite sprite) {
			
		}
		
	}
	
	// -------------------------------------------------------------------------------------------------
	public class KeyBoardController : BaseController {
		int vel;
		int xinc;
		int yinc;
		bool fire = false;
		
		public void Key(int code) {
			if ( code == 37 ) {
				direction = direction - 1; // left
				if (direction < 0 ) direction = 7;
			}
			if ( code == 39 ) {
				direction = ( direction +1 ) % 8; // right
			}
			if ( code == 38 ) {
				vel = 1; // up
			}
			if ( code == 40 ) {
				vel = -1; // down
			}
			if ( code == 70 ) {
				fire = true; // 'F' fire
			}
			Console.WriteLine("DIR:"+direction);
			
			xinc = lookup[direction,0];
			yinc = lookup[direction,1];

		}
		
		public override void tick(ArrayList sprites, Sprite sprite) {
			x = x + xinc;
			y = y + yinc;
		
			if ( fire ) {
 	   		    //Sprite bullet = new BulletSprite(sprite);
 	   		    Sprite bullet = new TrackerSprite(sprite);
 			    //bullet.setController(new ProjectileController(x,y,direction, 3));
 			    bullet.setController(new TrackerController(x,y,direction, 3));
				
				sprites.Add(bullet);
				
				fire = false;

			}
		}
	}
	
	// -------------------------------------------------------------------------------------------------
	class SimpleAIController : BaseController {
		int xinc = 1;
		int yinc = 0;

		public SimpleAIController(int x, int y, int direction) {
			this.x = x;
			this.y = y;
			this.direction = direction;
		}

		public override void tick(ArrayList sprites, Sprite sprite) {
			x=x+xinc;
			y=y+yinc;

			
			if ( x > 400 || x < 10 ) {
				xinc = -xinc;
				direction = ( direction == 2 ) ? 6 : 2;
			}
			
		}
	}
	
	class ProjectileController : BaseController {
		int velocity = 0;
		int lifeTime = 30;
		
		public ProjectileController(int x, int y, int direction, int speed) {
			this.x = x;
			this.y = y;
			this.direction = direction;
			this.velocity = speed;
		}

		public override void tick(ArrayList sprites, Sprite sprite) {
			int xinc = lookup[direction,0]*velocity;
			int yinc = lookup[direction,1]*velocity;

			x=x+xinc;
			y=y+yinc;
			
			if ( lifeTime-- == 0 ) {
				sprite.destroy();
			}
			
		}
	}

	class TrackerController : BaseController {
		int velocity = 0;
		int lifeTime = 30;
		Sprite target = null;
		
		public TrackerController(int x, int y, int direction, int speed) {
			this.x = x;
			this.y = y;
			this.direction = direction;
			this.velocity = speed;
		}

		public override void tick(ArrayList sprites, Sprite sprite) {
			
			if ( target == null ) {
				foreach ( Sprite csp in sprites ) {
					if ( csp is TankSprite && csp != sprite ) 
						target = csp;
				}
			}
			
			int xinc = lookup[direction,0]*velocity;
			int yinc = lookup[direction,1]*velocity;

			x=x+xinc;
			y=y+yinc;
			
			if ( target != null ) {
				x = target.getX()+10;
				y = target.getY()+10;
				
			}
			
			if ( lifeTime-- == 0 ) {
				sprite.destroy();
			}
			
		}
	}


}
