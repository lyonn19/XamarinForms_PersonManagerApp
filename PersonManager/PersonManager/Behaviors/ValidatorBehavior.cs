using Xamarin.Forms;

namespace PersonManager.Behaviors
{
    /// <summary>
    /// Clase helper para validaciones
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidatorBehavior<T> : Behavior<T> where T : BindableObject
    {
        //Result Boolean
        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid",
            typeof(bool), typeof(ValidatorBehavior<T>), default(bool));

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set
            {
                SetValue(IsValidPropertyKey, value);
                OnPropertyChanged();
                OnPropertyChanged("IsVisibleMessage");
            }
        }

        //Result message
        public static readonly BindableProperty MessageProperty = BindableProperty.Create("Message",
            typeof(string), typeof(ValidatorBehavior<T>), default(string));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set
            {
                SetValue(MessageProperty, value);
                OnPropertyChanged();
            }
        }

        public bool IsVisibleMessage => !IsValid;

        public void NoValided(string message)
        {
            IsValid = false;
            Message = message;
        }
        public void Valided()
        {
            IsValid = true;
            Message = string.Empty;
        }
    }
}
