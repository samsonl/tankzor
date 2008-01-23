/*
 * Created by SharpDevelop.
 * User: samsonl
 * Date: 22/01/2008
 * Time: 12:04 AM
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
	public interface Sprite {
		void draw(Graphics g);
		void setController(Controller c);
		Controller getController();
		int getX();
		int getY();
		int getDirection();
		void tick(ArrayList sprites);
		void destroy();
		bool isDestroyed();
		RectangleF getBounds();
		void collision(Sprite sprite);
	}

	public abstract class BaseSprite : Sprite {
		protected int x;
		protected int y;
		protected int direction;
		protected bool isDestroyed2 = false;
		protected RectangleF bounds;
 		protected Controller controller;

		public abstract void draw(Graphics g);
			
		
		public virtual  void setController(Controller c) {
			this.controller = c;
		}
		
		public virtual  Controller getController() {
			return controller;
		}
		public virtual  int getX() {
			this.x = controller.getX();
			
			return this.x;
		}
		public virtual  int getY() {
			this.y = controller.getY();
			
			return this.y;
		}
		
		public virtual  int getDirection() {
			this.direction = controller.getDirection();
			
			return this.direction;
			
		}
		public abstract void tick(ArrayList sprites);
			
		
		public virtual void destroy() {
			isDestroyed2 = true;
			
		}
		public virtual  bool isDestroyed() {
		    return isDestroyed2;	
		}
		
		public virtual RectangleF getBounds() {
			return bounds;
		}
		public abstract void collision(Sprite sprite);
			
		
	}
	
	// -------------------------------------------------------------------------------------------------
	public class TankSprite : BaseSprite  {
		GraphicsPath sPath = new GraphicsPath();
		int health = 90;
		
		public TankSprite() {
			sPath.AddRectangle(new Rectangle(0,0,10,10));
			sPath.AddRectangle(new Rectangle(-2,2,2,8));
			sPath.AddRectangle(new Rectangle(10,2,2,8));
	
			sPath.AddRectangle(new Rectangle(4,-5,2,10));
			
			
		}

		public override void collision(Sprite sprite) {
			
			health = health - 1;
		}
		
    	public override void tick(ArrayList sprites) {
			controller.tick(sprites, this);
		}
		
		public override void draw(Graphics g) {
			int			x         	= getX();
 			int			y         	= getY();
 			int			direction 	= getDirection();
 			Matrix			trans 		= new Matrix();
 			Matrix			transOrig 	= new Matrix();
 			Matrix 			rot 		= new Matrix();
 			GraphicsPath 	path 		= (GraphicsPath)sPath.Clone();
 			Color 			c  			= Color.Black;
			Pen   			p  			= new Pen(c);
 			
			path.AddString(health.ToString(),FontFamily.GenericMonospace,0,10,new PointF(0,12),StringFormat.GenericDefault);
 
			trans.Translate(x,y);
 			transOrig.Translate(-5,-5);
 			rot.Rotate(direction*45);
 		
 			path.Transform(transOrig);
 			path.Transform(rot);
 			path.Transform(trans);
 			g.DrawPath(p,path);
 			
 			this.bounds = path.GetBounds();
		}
	}

	// Controller was defined here, cause NullPtr in gui.invoke, bad error reporting.
	public class BulletSprite : BaseSprite  {
		GraphicsPath sPath = new GraphicsPath();
 		Sprite owner;
		
		public BulletSprite(Sprite owner) {
			this.owner = owner;
			sPath.AddRectangle(new Rectangle(0,0,2,2));
		}
		public override void collision(Sprite sprite) {
			
		}

		public override void tick(ArrayList sprites) {
			controller.tick(sprites, this);
		}
		public override void draw(Graphics g) {
			int			x         	= getX();
 			int			y         	= getY();
 			int			direction 	= getDirection();
 			Matrix			trans 		= new Matrix();
 			Matrix			transOrig 	= new Matrix();
 			Matrix 			rot 		= new Matrix();
 			GraphicsPath 	path 		= (GraphicsPath)sPath.Clone();
 			Color 			c  			= Color.Black;
			Pen   			p  			= new Pen(c);
 			
 			trans.Translate(x,y);
 			transOrig.Translate(-2,-2);
 			rot.Rotate(direction*45);
 		
 			path.Transform(transOrig);
 			path.Transform(rot);
 			path.Transform(trans);
 			g.DrawPath(p,path);
 			
 			this.bounds = path.GetBounds();
		}
	}


	public class TrackerSprite : BaseSprite  {
		GraphicsPath sPath = new GraphicsPath();
 		Sprite owner;
		
		public TrackerSprite(Sprite owner) {
			this.owner = owner;
			sPath.AddEllipse(0,0,10,6);
 		}	 		
 		
		public override void collision(Sprite sprite) {
			
		}

		public override void tick(ArrayList sprites) {
			controller.tick(sprites, this);
		}
		public override void draw(Graphics g) {
			int			x         	= getX();
 			int			y         	= getY();
 			int			direction 	= getDirection();
 			Matrix			trans 		= new Matrix();
 			Matrix			transOrig 	= new Matrix();
 			Matrix 			rot 		= new Matrix();
 			GraphicsPath 	path 		= (GraphicsPath)sPath.Clone();
 			Color 			c  			= Color.Black;
			Pen   			p  			= new Pen(c);
 			
 			trans.Translate(x,y);
 			transOrig.Translate(-5,-3);
 			//rot.Rotate(direction*45);
 		
 			path.Transform(transOrig);
 			//path.Transform(rot);
 			path.Transform(trans);
 			g.DrawPath(p,path);
 			
 			this.bounds = path.GetBounds();
		}
	}

}
