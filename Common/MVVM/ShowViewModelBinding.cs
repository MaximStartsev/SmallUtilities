using System;
using System.Windows.Markup;

namespace MaximStartsev.SmallUtilities.Common.MVVM
{
    public class ShowViewModelBinding : MarkupExtension
    {
        public Type ViewModel { get; set; }

        public ShowViewModelBinding() { }
        public ShowViewModelBinding(Type viewModel)
        {
            ViewModel = viewModel;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ViewModel == null)
            {
                throw new ArgumentNullException(nameof(ViewModel));
            }
            return new ShowViewModelCommand(ViewModel);
        }
    }
}
