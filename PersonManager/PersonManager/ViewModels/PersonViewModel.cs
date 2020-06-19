using PersonManager.Models;
using PersonManager.ViewModels.Base;
using PersonManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using PersonManager.DataLayer;

namespace PersonManager.ViewModels
{
    public class PersonViewModel : ViewModelBase
    {
        private IPersonService _personService;

        public PersonViewModel(IPersonService personService)
        {
            _personService = personService;
            
            People = new ObservableCollection<Person>();
            SelectedPerson = new Person();
        }

        public ObservableCollection<Person> People { get; set; }

        private string _name;
        public string Name 
        {
            get { return _name; }
            set 
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private int _age;
        public int Age 
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        

        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }

        #region Methods
        public override Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        public async Task GetPeople()
        {
            var collection = await _personService.GetPeople();
            if (collection.Count > 0)
            {
                foreach (var item in collection)
                {
                    People.Add(item);
                }
            }

            PersonCount = People?.Count ?? 0;
        }

        public async Task NewPerson()
        {
            var result = await _personService.AddPerson(new Person()
            {
                Name = Name,
                Age = Age
            });

            if (result)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }

        }

        public async Task DeletePerson()
        {
            await _personService.DeletePerson(SelectedPerson.Id);
        }

        public async Task GetOldestPerson()
        {
            var result = await _personService.GetOldestPerson();

            if (result != null)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", $"Oldest person is: {result.Name} {result.Age} year", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Data Base Empty", "OK");
            }
        }

        private int _personCount;
        public int PersonCount
        {
            get { return _personCount;}
            set
            {
                _personCount = value;
                OnPropertyChanged();
            }
        }
        
        #endregion

        #region Commands

        Command _newPersonCommand;
        public Command NewPersonCommand
        {
            get
            {
                return _newPersonCommand ?? (_newPersonCommand = new Command(async () => await NewPersonAsync(), () => !IsBusy));
            }
        }
        public async Task NewPersonAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await NewPerson();
            }
            finally
            {
                IsBusy = false;
                PeopleCommand.Execute(null);
            }
        }

        Command _deletePersonCommand;
        public Command DeletePersonCommand
        {
            get
            {
                return _deletePersonCommand ?? (_deletePersonCommand = new Command(async () => await DeletePersonAsync(), () => !IsBusy));
            }
        }
        public async Task DeletePersonAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await DeletePerson();
            }
            finally
            {
                IsBusy = false;
                PeopleCommand.Execute(null);
            }
        }

        Command _peopleCommand;
        public Command PeopleCommand
        {
            get
            {
                return _peopleCommand ?? (_peopleCommand = new Command(async () => await GetPeopleAsync(), () => !IsBusy));
            }
        }
        public async Task GetPeopleAsync()
        {
            if (IsBusy)
                return;
            try
            {
                People.Clear();
                IsBusy = true;
                await GetPeople();
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command _oldestPersonCommand;
        public Command OldestPersonCommand
        {
            get
            {
                return _oldestPersonCommand ?? (_oldestPersonCommand = new Command(async () => await GetOldestPersonAsync(), () => !IsBusy));
            }
        }
        private async Task GetOldestPersonAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await GetOldestPerson();
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

    }
}
