using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using ArduinoController;

public class Pin
{
  const ConsoleColor COLOR_SELECTION = ConsoleColor.Yellow;
  const ConsoleColor COLOR_INACTIVE = ConsoleColor.Gray;
  const ConsoleColor COLOR_INPUT = ConsoleColor.Blue;
  const ConsoleColor COLOR_OUTPUT = ConsoleColor.Green;

  public Point Location { get; set; }
  public int Index { get; set; }
  public PinDirection Direction { get; set; }
  public PinType PinType { get; set; }
  public int Value { get; set; }
  public bool Active { get; set; }

  public void Render(bool selected)
  {
    Console.SetCursorPosition(Location.X, Location.Y);
    ConsoleColor color = COLOR_INACTIVE;
    if (Active)
    {
      if (Direction == PinDirection.Input) color = COLOR_INPUT;
      else color = COLOR_OUTPUT;
    }

    char selectionChar = selected ? '*' : ' ';
    Program.ColorWrite($"{selectionChar} ", COLOR_SELECTION);
    Program.ColorWrite($"{this} - {Value}".PadRight(10), color);
  }

  public override string ToString()
  {
    StringBuilder sb = new StringBuilder();

    switch (PinType)
    {
      case PinType.Analog:
        sb.Append("A");
        break;
      case PinType.Digital:
        sb.Append("D");
        break;
    }

    switch (Direction)
    {
      case PinDirection.Input:
        sb.Append("I");
        break;
      case PinDirection.Output:
        sb.Append("O");
        break;
    }

    sb.Append(Index.ToString().PadLeft(2));

    return sb.ToString();
  }
}