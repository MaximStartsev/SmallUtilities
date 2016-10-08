using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Core
{
    public static class MVVMFactory
    {
        private readonly static Dictionary<ViewModel, Window> _openedWindows = new Dictionary<ViewModel, Window>();

        private static IEnumerable<Type> _views;
        public static IEnumerable<Type> Views
        {
            get
            {
                if (_views == null)
                {
                    _views = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.EndsWith("View") && t.IsSubclassOf(typeof(UserControl)));
                }
                return _views;
            }
        }
        private static IEnumerable<Type> _viewModels;
        public static IEnumerable<Type> ViewModels
        {
            get
            {
                if (_viewModels == null)
                {
                    _viewModels = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.EndsWith("ViewModel") && t.IsSubclassOf(typeof(ViewModel)));
                }
                return _viewModels;
            }
        }
        public static void Show(ViewModel viewModel)
        {
            try
            {
                if (viewModel == null)
                {
                    throw new ArgumentNullException("viewModel");
                }
                var name = viewModel.GetType().Name;
                var viewName = name.Remove(name.Length - "Model".Length);
                var view = Views.FirstOrDefault(v => v.Name == viewName);
                if (view == null)
                {
                    throw new Exception(String.Format("Не удалось найти представление для модели представления {0}", name));
                }
                var control = (UserControl)Activator.CreateInstance(view);
                //var popup = new Popup();
                //popup.Child = control;
                //popup.DataContext = viewModel;
                //popup.IsOpen = true;
                var window = new Window();
                window.Content = control;
                //window.Title = viewModel.Title;
                window.SetBinding(Window.TitleProperty, "Title");
                window.DataContext = viewModel;
                _openedWindows.Add(viewModel,window);
                window.Closed += (s, e) =>
                {
                    viewModel.Hide();

                };
                window.Show();
            }
            catch (Exception ex)
            {
            }
        }

        public static void Hide(ViewModel viewModel)
        {
            if (_openedWindows.ContainsKey(viewModel))
            {
                _openedWindows[viewModel].Close();
                _openedWindows.Remove(viewModel);
            }
        }
    }
}
