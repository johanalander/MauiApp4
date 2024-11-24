namespace MauiApp4
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public string xingername { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }


        public MainPage()
        {
            InitializeComponent();

            xingername = "";
            posX = 1;
            posY = 1;

            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(RedrawClock);
            timer.Start();
        }




        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        void OnEntryCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;

            this.xingername = text;

            XingerNameLabel.Text = "Xinger name: " + text + " Position: " + posX + "," + posY;
        }

        public void RedrawClock(object source, ElapsedEventArgs e)
        {
            // Alternative 2)
            // Would like to change so that this function can start MapDrawable (this namespace) instead of ClockDrawable (namespace: MauiApp4.Drawables)
            // This is an alternative to 1)

            //var clock = (ClockDrawable)this.ClockGraphicsView.Drawable;
            var graphicsView = this.ClockGraphicsView;

            /* xaml:
             * <GraphicsView Drawable="{StaticResource clockDrawable}"
                HorizontalOptions="Center"
                x:Name="ClockGraphicsView"
                HeightRequest="400"
                WidthRequest="300">
            </GraphicsView>
 */

            //var thismap = (MapDrawable)this.ClockGraphicsView.Drawable;
            //var graphicsView = this.MapGraphicsView;


            /* xaml:
             * < GraphicsView Drawable = "{StaticResource mapDrawable}"
        HorizontalOptions = "Center"
        x: Name = "MapGraphicsView"
        HeightRequest = "400"
        WidthRequest = "300" >
</ GraphicsView >*/
            graphicsView.Invalidate();

        }

        public class MapDrawable : IDrawable
        {

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {

                
                DateTime curTime = DateTime.Now;
                var clockCenterPoint = new PointF(200, 300);
                var circleRadius = 100;


                canvas.StrokeColor = Colors.Aqua;
                canvas.StrokeSize = 6;

                canvas.DrawRectangle(0, 0, 200, 150);
                

            }

        }


    }
}
