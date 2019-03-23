using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfSimpleMVVM.Exeptions;
using WpfSimpleMVVM.Models;
using WpfSimpleMVVM.Tools;

namespace WpfSimpleMVVM.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Fields



        private string _nameInput;
        private string _secondnameInput;
        private string _emailInput;
        private string _dateInput;

        private string _selectedID;
        private string _nameInputAdd;
        private string _secondnameInputAdd;
        private string _emailInputAdd;
        private DateTime? _dateInputAdd;

        private DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Person>));
        private ICommand _addCommand;
        private ICommand _filtreCommand;

        private ICommand _deleteCommand;
        private ICommand _saveCommand;
        private bool flag = true;
        private const string path = "../../DataBase/person.json";
        private string _adultOutput;
        private string _birthDayOutput;
        private string _sunSignOutput;
        private string _chineseSignOutput;

        private Person _SelectedItem;
        private ObservableCollection<Person> _personList = new ObservableCollection<Person>();
        public ObservableCollection<Person> PersonList
        {
            get { return _personList; }
            set
            {
                _personList = value;

            }
        }
        public Person SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();

            }
        }
        public string Nameinput
        {
            get => _nameInput;
            set
            {
                _nameInput = value;
                OnPropertyChanged();
            }
        }
        public string Secondnameinput
        {
            get => _secondnameInput;
            set
            {
                _secondnameInput = value;

                OnPropertyChanged();
            }
        }
        public string Emailinput
        {
            get => _emailInput;
            set
            {
                _emailInput = value;
                OnPropertyChanged();
            }
        }
        public string Dateinput
        {
            get => _dateInput;
            set
            {
                _dateInput = value;
                OnPropertyChanged();
            }
        }
        public string SelectedID
        {
            get => _selectedID;
            set
            {
                _selectedID = value;
                OnPropertyChanged();
            }
        }
        public string AdultOutput
        {
            get => _adultOutput;
            set
            {
                _adultOutput = value;
                OnPropertyChanged();
            }
        }
        public string BirthDayOutput
        {
            get => _birthDayOutput;
            set
            {
                _birthDayOutput = value;
                OnPropertyChanged();
            }
        }
        public string SunSignOutput
        {
            get => _sunSignOutput;
            set
            {
                _sunSignOutput = value;
                OnPropertyChanged();
            }
        }
        public string ChineseSignOutput
        {
            get => _chineseSignOutput;
            set
            {
                _chineseSignOutput = value;
                OnPropertyChanged();
            }
        }
        public string NameinputAdd
        {
            get => _nameInputAdd;
            set
            {
                _nameInputAdd = value;
                OnPropertyChanged();
            }
        }
        public string SecondnameinputAdd
        {
            get => _secondnameInputAdd;
            set
            {
                _secondnameInputAdd = value;

                OnPropertyChanged();
            }
        }
        public string EmailinputAdd
        {
            get => _emailInputAdd;
            set
            {
                _emailInputAdd = value;
                OnPropertyChanged();
            }
        }
        public DateTime? DateinputAdd
        {
            get => _dateInputAdd;
            set
            {
                _dateInputAdd = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public MainWindowViewModel()
        {

            PersonList.CollectionChanged += m_people_CollectionChanged;

            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    ObservableCollection<Person> persons = (ObservableCollection<Person>)jsonFormatter.ReadObject(fs);

                    foreach (Person p in persons)
                    {
                        PersonList.Add(p);
                    }
                }
            }
            else
            {

                var tmp = GetPersonList();
                foreach (var item in tmp)
                {
                    PersonList.Add(item);
                }


                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, PersonList);
                }
            }

            var item2 = PersonList.Max(x => x.ThisID);


            Person.ID = item2+1;


        }
        private void m_people_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (INotifyPropertyChanged item in e.NewItems.OfType<INotifyPropertyChanged>())
                {
                    item.PropertyChanged += person_PropertyChanged;
                }
            }
            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (INotifyPropertyChanged item in e.OldItems.OfType<INotifyPropertyChanged>())
                {
                    item.PropertyChanged -= person_PropertyChanged;
                }
            }

        }

        private void person_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var row = sender as Person;
            SaveData(row);
        }
        private void SaveData(Person row)
        {
            MessageBox.Show(row.ThisID.ToString() + "ID are changed");
           
        }
        private ObservableCollection<Person> GetPersonList()
        {
            ObservableCollection<Person> personList = new ObservableCollection<Person>();
            int n = 50;
            Person p;
            string[] names;
            string emailS, fnameS, snameS;
            string[] endEmails = { "@ukma.edu.ua", "@gmail.com", "@mail.ru", "@carpe.diem" };
            int sname, fname, day, month, year, endEmail;

            var textFile = "../../DataBase/names.txt";

            if (File.Exists(textFile))
            {


                names = File.ReadAllLines(textFile);

                Random random = new Random();
                for (int i = 0; i < n; ++i)
                {

                    endEmail = random.Next(0, endEmails.Length);
                    fname = random.Next(0, names.Length);
                    do
                    {
                        sname = random.Next(0, names.Length);
                    } while (sname == fname);

                    year = random.Next(1980, 2005);
                    month = random.Next(1, 13);
                    if (month == 2)
                    {
                        day = random.Next(1, 29);
                    }
                    else if (month == 4 || month == 6 || month == 9 || month == 11)
                    {

                        day = random.Next(1, 31);
                    }
                    else
                    {

                        day = random.Next(1, 32);
                    }

                    fnameS = names[fname];
                    snameS = names[sname];
                    emailS = fnameS.First() + '.' + snameS + endEmails[endEmail];
                    p = new Person(fnameS, snameS, emailS, new DateTime(year, month, day));
                    personList.Add(p);

                }
            }

            return personList;
        }

        #region Commands

        private void UpdateColections()
        {

        }

        public ICommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand<object>(o => Add(), o => !string.IsNullOrEmpty(_emailInputAdd) && !string.IsNullOrEmpty(_dateInputAdd.ToString()) && !string.IsNullOrEmpty(_nameInputAdd) && !string.IsNullOrEmpty(_secondnameInputAdd)));

        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand<object>(o => Save(), o => flag));

        public ICommand FiltreCommand => _filtreCommand ?? (_filtreCommand = new RelayCommand<object>(o => Filtre(), o => flag));

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand<object>(o => Delete(), o => flag));

        public async void Filtre()
        {
            var copy = PersonList;
            flag = false;
            await Task.Run(() => FiltreMethod());
            await Task.Run(() => Thread.Sleep(100));
            PersonList = copy;
            flag = true;
        }

        public async void Delete()
        {
            flag = false;
            DeleteMethod();
            await Task.Run(() => Thread.Sleep(100));
            flag = true;
        }

        public void DeleteMethod()
        {
            PersonList.Remove(SelectedItem);
            OnPropertyChanged("PersonList");
        }
        public async void Save()
        {
            flag = false;
            await Task.Run(() => SaveMethod());
            await Task.Run(() => Thread.Sleep(100));
            flag = true;

        }

        public void SaveMethod()
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, PersonList);
            }

        }
        public async void Add()
        {
            flag = false;
            AddMethod();
            await Task.Run(() => Thread.Sleep(100));
            flag = true;
        }

        public void AddMethod()
        {
            try
            {
                DateTime date = DateinputAdd ?? DateTime.Now;
                Person p = new Person(NameinputAdd, SecondnameinputAdd, EmailinputAdd, date);
                PersonList.Add(p);
                OnPropertyChanged("PersonList");


            }
            catch (AgeExeption ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " -> " + ex.Value);
            }
            catch (DateExeption ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + " -> " + ex.Value);
            }
            catch (EMailExeption ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }
        public void FiltreMethod()
        {

            var selectedPerson = PersonList.Where(t =>
                (string.IsNullOrEmpty(_nameInput) ||
                t.FirstName.Contains(_nameInput))).Where(t => (string.IsNullOrEmpty(_secondnameInput) ||
               t.SecondName.Contains(_secondnameInput)) &&
                                                            (string.IsNullOrEmpty(_emailInput) ||
                                                             t.EMail.Contains(_emailInput)) &&
                                                            (string.IsNullOrEmpty(_dateInput) ||
                                                             t.Birthday.Contains(_dateInput)) &&
                                                            (string.IsNullOrEmpty(_adultOutput) ||
                                                             t.IsAdult.ToString().Contains(_adultOutput)) &&
                                                            (string.IsNullOrEmpty(_birthDayOutput) ||
                                                             t.IsBirthday.ToString().Contains(_birthDayOutput)) &&
                                                            (string.IsNullOrEmpty(_sunSignOutput) ||
                                                             t.SunSign.Contains(_sunSignOutput)) &&
                                                            (string.IsNullOrEmpty(_chineseSignOutput) ||
                                                             t.ChineseSign.Contains(_chineseSignOutput)));
            PersonList = new ObservableCollection<Person>(); PersonList.CollectionChanged += m_people_CollectionChanged;
            foreach (var vPerson in selectedPerson)
            {
                PersonList.Add(vPerson);
            }
            OnPropertyChanged("PersonList");


        }

        #endregion
        #region INotifyPropertyChanged Impl
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

}
