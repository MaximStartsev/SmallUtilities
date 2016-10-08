using System;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Core
{
    public abstract class ViewModel
    {
        public bool IsOpened { get; private set; }
        public abstract string Title { get; set; }
        public virtual void Show()
        {
            if (!IsOpened)
            {
                MVVMFactory.Show(this);
                IsOpened = true;
            }
        }
        public virtual void Hide()
        {
            if (IsOpened)
            {
                IsOpened = false;
                MVVMFactory.Hide(this);
            }
        }
    }
}
