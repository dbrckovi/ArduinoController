using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;

public class PinGrid
{
  private List<Pin> _analogPins = new List<Pin>();
  private List<Pin> _digitalPins = new List<Pin>();
  private int _digitalPinsX = 0;
  private int _analogPinsX = 17;
  private Pin? _selectedPin = null;

  public Point Location { get; set; }
  public Pin? SelectedPin { get { return _selectedPin; } }

  public void Add(Pin pin)
  {
    List<Pin> list = pin.PinType == PinType.Analog ? _analogPins : _digitalPins;
    int pinXCoordinate = pin.PinType == PinType.Analog ? _analogPinsX : _digitalPinsX;

    if (list.Contains(pin)) throw new System.Exception("Pin is already added");
    if (_analogPins.Count == 0 && _digitalPins.Count == 0) _selectedPin = pin;
    list.Add(pin);
    list = list.OrderBy(s => s.Index).ToList();

    for (int i = 0; i < list.Count; i++)
    {
      pin.Location = new Point(pinXCoordinate + this.Location.X, i + this.Location.Y);
    }
  }

  public void AddRange(List<Pin> pins)
  {
    foreach (Pin p in pins)
    {
      Add(p);
    }
  }

  public void Render()
  {
    foreach (Pin pin in _analogPins) pin.Render(pin == SelectedPin);
    foreach (Pin pin in _digitalPins) pin.Render(pin == SelectedPin);
  }

  public void MoveSelection(CursorDirection direction)
  {
    if (SelectedPin == null) return;
    List<Pin> parentList = _analogPins.Contains(SelectedPin) ? _analogPins : _digitalPins;

    int pinIndex = 0;
    for (int x = 0; x < parentList.Count; x++)
    {
      if (parentList[x] == SelectedPin)
      {
        pinIndex = x;
        break;
      }
    }

    if (direction == CursorDirection.Left && (parentList == _digitalPins || _digitalPins.Count == 0)) return;
    if (direction == CursorDirection.Right && (parentList == _analogPins || _analogPins.Count == 0)) return;
    if (direction == CursorDirection.Down && pinIndex == parentList.Count - 1) return;
    if (direction == CursorDirection.Up && pinIndex == 0) return;

    SelectedPin.Render(false);

    switch (direction)
    {
      case CursorDirection.Up:
        _selectedPin = parentList[pinIndex - 1];
        break;
      case CursorDirection.Down:
        _selectedPin = parentList[pinIndex + 1];
        break;
      case CursorDirection.Left:
        if (pinIndex >= _digitalPins.Count) _selectedPin = _digitalPins[_digitalPins.Count - 1];
        else _selectedPin = _digitalPins[pinIndex];
        break;
        case CursorDirection.Right:
        if (pinIndex >= _analogPins.Count) _selectedPin = _analogPins[_analogPins.Count -1];
        else _selectedPin = _analogPins[pinIndex];
        break;
    }

    SelectedPin.Render(true);
  }
}

