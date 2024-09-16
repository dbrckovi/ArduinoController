using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

public class MainForm
{
  //TODO: Refactor entire GUI.
  //If you stumbled here somehow, ignore this entire code. It's bad

  PinGrid _grid = new PinGrid();

  public MainForm()
  {
    Console.Clear();

    _grid.Location = new System.Drawing.Point(5, 5);

    List<Pin> pins = new List<Pin>();
    for (int x = 1; x <= 5; x++)
    {
      Pin p = new Pin();
      p.Direction = PinDirection.Input;
      p.PinType = PinType.Analog;
      p.Index = x;
      p.Location = new System.Drawing.Point(5, x);
      pins.Add(p);
    }

    for (int x = 1; x <= 13; x++)
    {
      Pin p = new Pin();
      p.Direction = PinDirection.Input;
      p.PinType = PinType.Digital;
      p.Index = x;
      p.Location = new System.Drawing.Point(20, x);
      pins.Add(p);
    }

    _grid.AddRange(pins);
  }

  public void Run()
  {
    Console.CursorVisible = false;
    Console.Clear();
    Console.WriteLine("ESC - exit");
    Console.WriteLine("I - set pin mode to input");
    Console.WriteLine("O - set pin mode to output");
    

    _grid.Render();

    while (true)
    {
      ConsoleKeyInfo keyInfo = Console.ReadKey();

      switch (keyInfo.Key)
      {
        case ConsoleKey.UpArrow: _grid.MoveSelection(CursorDirection.Up); break;
        case ConsoleKey.DownArrow: _grid.MoveSelection(CursorDirection.Down); break;
        case ConsoleKey.LeftArrow: _grid.MoveSelection(CursorDirection.Left); break;
        case ConsoleKey.RightArrow: _grid.MoveSelection(CursorDirection.Right); break;
        case ConsoleKey.Escape: Console.Clear(); return;
      }
    }
  }
}