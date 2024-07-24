using System;
using System.Threading;
using System.Threading.Tasks;

namespace ArduinoController
{
  class Program
  {
    private static int _previousBufferWidth;
    private static int _previousBufferHeight;
    private static CancellationTokenSource _cts;

    static void Main(string[] args)
    {
      Console.Clear();
      DrawGUI();

      // Start a task to monitor the console size
      _cts = new CancellationTokenSource();
      Task.Run(() => MonitorConsoleSize(_cts.Token));

      while (true)
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
        if (keyInfo.Key == ConsoleKey.Escape)
        {
          _cts.Cancel();  // Cancel the monitoring task
          break;
        }
      }
    }

    static void DrawGUI()
    {
      Console.Clear(); // Clear the console before drawing the new GUI
      for (int x = 0; x < Console.BufferWidth; x++)
      {
        Console.SetCursorPosition(x, 0);
        Console.Write('X');
        Console.SetCursorPosition(x, Console.BufferHeight - 1);
        Console.Write('X');
      }

      for (int y = 0; y < Console.BufferHeight; y++)
      {
        Console.SetCursorPosition(0, y);
        Console.Write('X');
        Console.SetCursorPosition(Console.BufferWidth - 1, y);
        Console.Write('X');
      }
    }

    static async Task MonitorConsoleSize(CancellationToken token)
    {
      // Initialize previous buffer size
      _previousBufferWidth = Console.BufferWidth;
      _previousBufferHeight = Console.BufferHeight;

      while (!token.IsCancellationRequested)
      {
        await Task.Delay(500);  // Check every 500ms

        if (_previousBufferWidth != Console.BufferWidth || _previousBufferHeight != Console.BufferHeight)
        {
          _previousBufferWidth = Console.BufferWidth;
          _previousBufferHeight = Console.BufferHeight;
          DrawGUI();  // Redraw the GUI if the buffer size changes
        }
      }
    }
  }
}