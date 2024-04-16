namespace Cours_Task5_Tree
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 1500;
            const int newHeight = 1000;

            window.Width = newWidth;
            window.Height = newHeight;

            window.X = -7;
            window.Y = 0;

            return window;
        }
    }
}
