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

            // Om man gör en egen kontroll (Drawable) så ska kontrollen ta hand om allt , inklusive uppdateringar
            // Lägg en timer som sköter uppdateringen av klockan i kontrollen, sprid inte ut kod som riskerar att dupliceras , tänk på separation of concerns
            // Vid behov skapa metoder (commands) för att exc starta/stoppa klockan.


            var clock = (ClockDrawable)this.ClockGraphicsView.Drawable;
            
            // Om man vill komma åt properties i en drawable från codebehind kan man gå direkt på propertyn, ex nedan
            // clock.Name = "KNAS";
            
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
