using System.Windows.Controls;
using Screen = System.Windows.Forms.Screen;

namespace WindowMovier
{
    class ScreenItem: ComboBoxItem
    {
        public Screen Screen { get; private set; }
        public ScreenItem(Screen screen)
        {
            Screen = screen;
            Content = screen.DeviceName;
        }
    }
}
