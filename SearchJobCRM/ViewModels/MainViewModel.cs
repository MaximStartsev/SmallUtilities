using MaximStartsev.SmallUtilities.SearchJobCRM.Models;
using MaximStartsev.SmallUtilities.SearchJobCRM.Utilities;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
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
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                if (_selectedCompany != value)
                {
                    _selectedCompany = value;
                    InvokePropertyChanged("SelectedCompany");
                    InvokePropertyChanged("CanAddVacancy");
                    InvokePropertyChanged("SelectedCompanyVacancies");
                }
            }
        }
        #endregion
        public IEnumerable SelectedCompanyVacancies
        {
            get
            {
                return SelectedCompany == null ? null : SelectedCompany.Vacancies;
            }
        }
        public bool CanAddVacancy
        {
            get
            {
                //  return SelectedCompany != null;
                return true;
            }
        }

        public ICommand AddCompanyCommand { get; set; }
        public ICommand SaveCommand { get; private set; }
        private readonly DatabaseContext _dbContext;
        public MainViewModel()
        {
            try
            {
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
                AddCompanyCommand = new DelegateCommand(o => AddCompany());
                SaveCommand = new DelegateCommand(o=>Save());
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(ex.ToString());
            }
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
                        if(SelectedCompany != null)
                        {
                            item.Company = SelectedCompany;
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
            _dbContext.SaveChanges();
        }
        private void AddCompany()
        {
            _dbContext.Companies.Add(new Company() { Name = "Test" });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
