/*
 * Created by SharpDevelop.
 * User: samsonl
 * Date: 11/01/2008
 * Time: 4:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace tankzor
{
	partial class Display
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// Display
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 566);
			this.DoubleBuffered = true;
			this.KeyPreview = true;
			this.Name = "Display";
			this.Text = "tankzor";
			this.Load += new System.EventHandler(this.DisplayLoad);
			this.Shown += new System.EventHandler(this.DisplayShown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DisplayKeyUp);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DisplayFormClosing);
			this.ResumeLayout(false);
		}
		
		void DisplayFormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			updaterThread.Abort();
				
		}
	}
}
