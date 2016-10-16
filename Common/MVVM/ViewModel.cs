
namespace MaximStartsev.SmallUtilities.Common.MVVM
{
    public abstract class ViewModel
    {
        public bool IsOpened { get; private set; }
        public abstract string Title { get; set; }
        public abstract double DefaultWidth { get; }
        public abstract double DefaultHeight { get; }
        public virtual void Show()
        {
            if (!IsOpened)
            {
                ViewModelFactory.Show(this);
                IsOpened = true;
            }
            else
            {
                ViewModelFactory.Activate(this);
            }
        }
        public virtual void Hide()
        {
            if (IsOpened)
            {
                IsOpened = false;
                ViewModelFactory.Hide(this);
            }
        }
    }
}
