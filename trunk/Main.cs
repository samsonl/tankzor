/*
 * Created by SharpDevelop.
 * User: samsonl
 * Date: 11/01/2008
 * Time: 4:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
	
namespace tankzor
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			
			Application.Run(new Display());
			
		//	Console.In.ReadLine();
		}
	}
}
