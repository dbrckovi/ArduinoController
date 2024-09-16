using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;

namespace ArduinoController
{
  class Program
  {
    static void Main(string[] args)
    {
      new MainForm().Run();
      Console.CursorVisible = true;
      Console.ForegroundColor = ConsoleColor.White;
    }

    public static void ColorWrite(string text, ConsoleColor color)
    {
      Console.ForegroundColor = color;
      Console.Write(text);
    }



  }
}