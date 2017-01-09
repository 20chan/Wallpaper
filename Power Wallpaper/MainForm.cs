using System.Windows.Forms;

namespace Power_Wallpaper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            if (PinToBackground())
                FillScreen();
            
        }

        protected bool PinToBackground()
        {
            return BehindDesktopIcon.FixBehindDesktopIcon(Handle);
        }

        protected void FillScreen()
        {
            this.Location = Screen.PrimaryScreen.Bounds.Location;
            this.Size = Screen.PrimaryScreen.Bounds.Size;
        }
    }
}
