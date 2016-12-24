using System;
using System.Windows;
using System.Windows.Interactivity;

namespace MaximStartsev.SmallUtilities.Common.MVVM
{
    public class RunViewModel : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)//mousebuttonEventargs
        {
            try
            {
                var type = parameter as Type;
                var control = Activator.CreateInstance(type) as ViewModel;
                control.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
