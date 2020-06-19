using Xamarin.Forms;

namespace PersonManager.Behaviors
{
    /// <summary>
    /// Clase para Validaciones de Max Longitud usando Behavior
    /// </summary>
    public class MaxLengthValidatorBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create("MaxLength", typeof(int), typeof(MaxLengthValidatorBehavior), 0);

        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += bindable_TextChanged;
        }
        private void bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue?.Length >= MaxLength)
            {
                ((Entry)sender).Text = e.NewTextValue.Substring(0, MaxLength);
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        {
                            //Xamarin.Forms.DependencyService.Get<IKeyboard>().HideKeyboard();
                            break;
                        }

                }
            }

        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= bindable_TextChanged;
        }
    }
}
