using MaximStartsev.SmallUtilities.SearchJobCRM.Models;
using MaximStartsev.SmallUtilities.SearchJobCRM.Utilities;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.ViewModels
{
    class MainViewModel:INotifyPropertyChanged
    {
        public string StatusBar { get; private set; }
        public ObservableCollection<Vacancy> Vacancies { get; private set; }
        public ObservableCollection<Company> Companies { get; private set; }

        #region Selected company
        private Company _selectedCompany;
        public object SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                if (_selectedCompany != value)
                {
                    if(value as Company != null)
                    {
                        _selectedCompany = (Company)value;
                    }
                    else
                    {
                        _selectedCompany = null;
                        SelectedVacancy = null;
                    }
                    InvokePropertyChanged(nameof(SelectedCompany));
                    InvokePropertyChanged(nameof(SelectedCompanyVacancies));
                }
            }
        }
        public IEnumerable SelectedCompanyVacancies { get { return SelectedCompany == null ? null : _selectedCompany.Vacancies; } }
        #endregion
        #region Selected vacancy
        private Vacancy _selectedVacancy;
        public object SelectedVacancy
        {
            get { return _selectedVacancy; }
            set
            {
                if (_selectedVacancy != value)
                {
                    if(value as Vacancy != null)
                    {
                        _selectedVacancy = (Vacancy)value;
                    }
                    else
                    {
                        _selectedVacancy = null;
                    }
                    InvokePropertyChanged(nameof(SelectedVacancy));
                    InvokePropertyChanged(nameof(SelectedVacancyDialog));
                    InvokePropertyChanged(nameof(VacancySelected));
                }
            }
        }
        public IEnumerable SelectedVacancyDialog { get { return SelectedVacancy == null ? null : _selectedVacancy.Dialog; } }
        public bool VacancySelected { get { return SelectedVacancyDialog != null; } }
        #endregion
        private int _tabIndex = 0;
        public int TabIndex
        {
            get { return _tabIndex; }
            set
            {
                if (_tabIndex != value)
                {
                    _tabIndex = value;
                    InvokePropertyChanged(nameof(TabIndex));
                    SelectedCompany = null;
                    SelectedVacancy = null;
                }
            }
        }
        public ICommand ShowCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        private readonly DatabaseContext _dbContext;
        private ErrorViewModel _errors;

        public MainViewModel()
        {
            try
            {
                _errors = new ErrorViewModel();
                StatusBar = "Готово";
                _dbContext = new DatabaseContext();
                _dbContext.SaveChanges();
                Vacancies = new ObservableCollection<Vacancy>(_dbContext.Vacancies.ToList());
                Vacancies.CollectionChanged += Vacancies_CollectionChanged;
                Companies = new ObservableCollection<Company>(_dbContext.Companies.ToList());
                Companies.CollectionChanged += Companies_CollectionChanged;
                foreach (var company in Companies)
                {
                    company.Vacancies.CollectionChanged += Vacancies_CollectionChanged1;
                }
                ShowCommand = new DelegateCommand(o => Show());
                SaveCommand = new DelegateCommand(o=>Save());
            }
            catch(Exception ex)
            {
                ex.Show();
            }
        }
        public bool Close()
        {
            if (_dbContext.HasUnsavedChanges())
            {
                var result = MessageBox.Show("Сохранить изменения?", "Сохранение изменений", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Save();
                        return false;
                    case MessageBoxResult.No: return false;
                    default: return true;
                }
            }
            return false;
        }
        private void Vacancies_CollectionChanged1(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Vacancy vacancy in e.NewItems)
                    {
                        if (!Vacancies.Contains(vacancy))
                        {
                            Vacancies.Add(vacancy);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Vacancy vacancy in e.OldItems)
                    {
                        if (Vacancies.Contains(vacancy))
                        {
                            Vacancies.Remove(vacancy);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }

        private void Vacancies_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Vacancy item in e.NewItems)
                    {
                        if(_selectedCompany != null)
                        {
                            item.Company = _selectedCompany;
                        }
                        _dbContext.Vacancies.Add(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Vacancy item in e.OldItems)
                    {
                        _dbContext.Vacancies.Add(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }

        private void Companies_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Company item in e.NewItems)
                    {
                        _dbContext.Companies.Add(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Company item in e.OldItems)
                    {
                        _dbContext.Companies.Add(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }

        private void Save()
        {
            try
            {
                //todo: диалоги не сохраняются
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ex.Show();
            }
        }
        private void Show()
        {
            try
            {
                if (SelectedVacancy as Vacancy != null)
                {
                    var di = new VacancyDetailedViewModel((Vacancy)SelectedVacancy);
                    di.Show();
                }
            }
            catch (Exception ex)
            {
                ex.Show();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
