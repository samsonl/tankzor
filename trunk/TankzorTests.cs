/*
 * Created by SharpDevelop.
 * User: samsonl
 * Date: 21/01/2008
 * Time: 2:02 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace TankzorTests  {

  [TestFixture] public class TestRegex : Assertion{

    [Test] public void TwoPlusTwo() {
      AssertEquals(4, 2+2);
    }
  	
    [Test] public void SimplePattern() {
      Regex r = new Regex("<p>");
      Match m = r.Match("contains <p> here");
      Assert(m.Success);
      m = r.Match("contains no para");
      Assert(!m.Success);
    }
  	
  }
}
  	
