using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace MaximStartsev.SmallUtilities.Common.MVVM
{
    public static class ViewModelFactory
    {
        public static Window MainWindow { get; set; }
        private readonly static Dictionary<ViewModel, Window> _openedWindows = new Dictionary<ViewModel, Window>();

        private static IEnumerable<Type> _views;
        public static IEnumerable<Type> Views
        {
            get
            {
                if (_views == null)
                {
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    _views = Assembly.GetEntryAssembly().GetTypes().Where(t => t.Name.EndsWith("View") && t.IsSubclassOf(typeof(UserControl)));
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
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }
            var name = viewModel.GetType().Name;
            var viewName = name.Remove(name.Length - "Model".Length);
            var view = Views.FirstOrDefault(v => v.Name == viewName);
            if (view == null)
            {
                throw new Exception(String.Format("Не удалось найти представление для модели представления {0}. Список зарегистрированных представлений:", name, String.Join(Environment.NewLine, Views.Select(v => v.Name))));
            }
            var control = (UserControl)Activator.CreateInstance(view);
            var window = new Window();
            window.Content = control;
            window.SetBinding(Window.TitleProperty, "Title");
            window.DataContext = viewModel;
            window.Width = viewModel.DefaultWidth;
            window.Height = viewModel.DefaultHeight;
            _openedWindows.Add(viewModel, window);
            window.Closed += (s, e) =>
            {
                viewModel.Hide();
            };
            window.Owner = MainWindow;
            window.Show();
        }
        public static void Activate(ViewModel viewModel)
        {
            if (_openedWindows.ContainsKey(viewModel))
            {
                _openedWindows[viewModel].Activate();
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
