using Microsoft.Maui.Graphics.Text;
using System.Diagnostics;

namespace MauiApp4.Drawables;

// Alt 1)
// Would like to access data from Mainpage.cs
// for example Name, posX, posY
// This is an alternative to 2)


public class ClockDrawable : BindableObject, IDrawable
{

  /* deklarera property */
  /* alla properties som ska kunna accessas via bindningar i xaml måste vara av typen BindableProperty */

  public static readonly BindableProperty NameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(ClockDrawable));
  
  /* om propertyn endast ska användas från codebehind (.cs) så kan man gå direkt på propertyn nedan */ 
  public string Name
  {
    get => (string)GetValue(NameProperty);
    set => SetValue(NameProperty, value);
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {

    DateTime curTime = DateTime.Now;
    var clockCenterPoint = new PointF(200, 300);
    var circleRadius = 100;

   
    canvas.StrokeColor = Colors.Aqua;
    canvas.StrokeSize = 6;

    canvas.DrawCircle(clockCenterPoint, circleRadius);
    canvas.DrawCircle(clockCenterPoint, 5);

    canvas.StrokeSize = 4;
    var hourPoint = GetHourHand(curTime, circleRadius, clockCenterPoint);
    canvas.DrawLine(clockCenterPoint, hourPoint);


    var minutePoint = GetMinuteHand(curTime, circleRadius, clockCenterPoint);
    canvas.DrawLine(clockCenterPoint, minutePoint);

    var secondPoint = GetSecondHand(curTime, circleRadius, clockCenterPoint);
    canvas.DrawLine(clockCenterPoint, secondPoint);

    /* Test , skriv ut property */
    //Debug.WriteLine(this.Name);
    canvas.DrawString(this.Name, 20, 20, 380, 100, HorizontalAlignment.Left, VerticalAlignment.Top);

  }

  internal static PointF GetHourHand(DateTime curTime, int radius, PointF center)
  {

    int currentHour = curTime.Hour;

    if (currentHour > 12)
      currentHour -= 12;

    var angleDegrees = (currentHour * 360) / 12;
    var angle = (Math.PI / 180.0) * angleDegrees;

    var hourShorter = radius * .8;
    PointF outerPoint = new((float)(hourShorter * Math.Sin(angle)) + center.X, (float)(-hourShorter * Math.Cos(angle)) + center.Y);

    return outerPoint;
  }

  internal static PointF GetMinuteHand(DateTime curTime, int radius, PointF center)
  {

    int currentMin = curTime.Minute;

    var angleDegrees = (currentMin * 360) / 60;
    var angle = (Math.PI / 180.0) * angleDegrees;

    PointF outerPoint = new((float)(radius * Math.Sin(angle)) + center.X, (float)(-radius * Math.Cos(angle)) + center.Y);

    return outerPoint;
  }

  internal static PointF GetSecondHand(DateTime curTime, int radius, PointF center)
  {

    int currentSecond = curTime.Second;

    var angleDegrees = (currentSecond * 360) / 60;
    var angle = (Math.PI / 180.0) * angleDegrees;

    PointF outerPoint = new((float)(radius * Math.Sin(angle) + center.X), (float)(-radius * Math.Cos(angle)) + center.Y);

    return outerPoint;
  }
}