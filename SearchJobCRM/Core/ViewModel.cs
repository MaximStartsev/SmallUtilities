using System;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Core
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
                MVVMCore.Show(this);
                IsOpened = true;
            }
            else
            {
                MVVMCore.Activate(this);
            }
        }
        public virtual void Hide()
        {
            if (IsOpened)
            {
                IsOpened = false;
                MVVMCore.Hide(this);
            }
        }
    }
}
