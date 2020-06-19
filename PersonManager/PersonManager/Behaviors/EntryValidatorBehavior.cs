using PersonManager.Behaviors.Fluent;
using Xamarin.Forms;

namespace PersonManager.Behaviors
{
    /// <summary>
    /// Clase para validaciones usando Behavior componente entry
    /// </summary>
    public class EntryValidatorBehavior : ValidatorBehavior<Entry>
    {
        private Entry _parentEntry;

        public EntryValidatorBehavior()
        {
            Reset();
        }

        public void Reset()
        {
            IsValid = true;
            Message = string.Empty;
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            if (_parentEntry == null)
            {
                _parentEntry = bindable;
            }

            if (!bindable.IsVisible || !bindable.IsEnabled)
            {
                SetDefaultValidate(bindable);
                return;
            }
            bindable.TextChanged += HandleTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            if (!bindable.IsVisible || !bindable.IsEnabled)
            {
                SetDefaultValidate(bindable);
                return;
            }
            bindable.TextChanged -= HandleTextChanged;
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            var textValue = e.NewTextValue;
            var entry = ((Entry)sender);

            Validate(entry, textValue);
        }

        private void Validate(Entry entry, string newTextValue)
        {
            if (!entry.IsVisible || !entry.IsEnabled)
            {
                SetDefaultValidate(entry);
                return;
            }

            this.When(x => x.IsCheckEmpty)
                .ValidateBy(() => ValidatorsFactory.IsValidEmpty(newTextValue))
                .WithMessage(Messages.FieldCannotBlank)

                .When(this, x => x.IsCheckEmail)
                .ValidateBy(() => ValidatorsFactory.IsValidEmail(newTextValue))
                .WithMessage(Messages.EmailIncorrectFormat)

                .When(this, x => x.IsCheckAlpha)
                .ValidateBy(() => ValidatorsFactory.IsValidAlpha(newTextValue))
                .WithMessage(Messages.AlphaIncorrectFormat)

                .When(this, x => x.IsCheckAlphaNumeric)
                .ValidateBy(() => ValidatorsFactory.IsValidAlphaNumeric(newTextValue))
                .WithMessage(Messages.AlphaNumericIncorrectFormat)

                .When(this, x => x.IsCheckAlphaName)
                .ValidateBy(() => ValidatorsFactory.IsValidAlphaName(newTextValue))
                .WithMessage(Messages.AlphaNameIncorrectFormat)

                .When(this, x => x.IsCheckAlphaDuration)
                .ValidateBy(() => ValidatorsFactory.IsValidAlphaDuration(newTextValue))
                .WithMessage(Messages.AlphaDurationIncorrectFormat)

                .When(this, x => x.IsCheckNumber)
                .ValidateBy(() => ValidatorsFactory.IsValidNumber(newTextValue))
                .WithMessage(Messages.PleaseInputNumber)

                .When(this, x => x.IsCheckTelephone)
                .ValidateBy(() => ValidatorsFactory.IsValidTelephone(newTextValue))
                .WithMessage(Messages.TelephoneIncorrectFormat)

                .When(this, x => x.IsCheckTelephoneCon)
                .ValidateBy(() => ValidatorsFactory.IsValidTelephoneCon(newTextValue))
                .WithMessage(Messages.TelephoneIncorrectFormat)

                .When(this, x => x.IsCheckAddress)
                .ValidateBy(() => ValidatorsFactory.IsValidAddress(newTextValue))
                .WithMessage(Messages.AlphaIncorrectFormat)

                .When(this, x => x.IsCheckSlashName)
                .ValidateBy(() => ValidatorsFactory.IsValidSlashName(newTextValue))
                .WithMessage(Messages.SlashNameIncorrectFormat)

                .When(this, x => x.MinLength > 0)
                .ValidateBy(() => ValidatorsFactory.IsValidMinLength(newTextValue, MinLength))
                .WithMessage(Messages.MinimizeLengthIs + MinLength)

                .When(this, x => x.MaxLength > 0)
                .ValidateBy(() => ValidatorsFactory.IsValidMaxLength(newTextValue, MaxLength))
                .WithMessage(Messages.MaximizeLengthIs + MaxLength)

                .When(this, x => x.MinValue > 0)
                .ValidateBy(() => ValidatorsFactory.IsValidMinValue(newTextValue, MinValue))
                .WithMessage(Messages.MinimizeValueIs + MinValue)

                .When(this, x => x.MaxValue > 0)
                .ValidateBy(() => ValidatorsFactory.IsValidMaxValue(newTextValue, MaxValue))
                .WithMessage(Messages.MaximizeValueIs + MaxValue)

                .When(this, x => x.IsValidRange1_99)
                .ValidateBy(() => ValidatorsFactory.IsValidRange(newTextValue))
                .WithMessage(Messages.RangeValueIs)

                .When(this, x => x.CompareToEntry != null)
                .ValidateBy(() => ValidatorsFactory.IsPasswordMatch(newTextValue, CompareToEntry.Text))
                .WithMessage(Messages.PasswordNotMatch)

                .ApplyResult<EntryValidatorBehavior, Entry>(this);

            if (!IsValid)
            {
                entry.TextColor = Color.Blue;
                return;
            }

            //Default
            SetDefaultValidate(entry);
        }

        private void SetDefaultValidate(Entry entry)
        {
            IsValid = true;
            Message = string.Empty;
            entry.TextColor = Color.Black;
        }

        public void Validate()
        {
            Validate(_parentEntry, _parentEntry.Text);
        }

        #region Properties

        //Is check empty
        public static BindableProperty IsCheckEmptyProperty = BindableProperty.Create("IsCheckEmpty",
            typeof(bool), typeof(EntryValidatorBehavior), default(bool));

        public bool IsCheckEmpty
        {
            get { return (bool)GetValue(IsCheckEmptyProperty); }
            set { SetValue(IsCheckEmptyProperty, value); }
        }

        //Is check email
        public static BindableProperty IsCheckEmailProperty = BindableProperty.Create("IsCheckEmail",
            typeof(bool), typeof(EntryValidatorBehavior), default(bool));

        public bool IsCheckEmail
        {
            get { return (bool)GetValue(IsCheckEmailProperty); }
            set { SetValue(IsCheckEmailProperty, value); }
        }
        //Is check alphanumeric
        public static BindableProperty IsCheckAlphaNumericProperty = BindableProperty.Create("IsCheckAlphaNumeric",
            typeof(bool), typeof(EntryValidatorBehavior), default(bool));

        public bool IsCheckAlphaNumeric
        {
            get { return (bool)GetValue(IsCheckAlphaNumericProperty); }
            set { SetValue(IsCheckAlphaNumericProperty, value); }
        }

        //Is check alpha
        public static BindableProperty IsCheckAlphaProperty = BindableProperty.Create("IsCheckAlpha",
            typeof(bool), typeof(EntryValidatorBehavior), default(bool));

        public bool IsCheckAlpha
        {
            get { return (bool)GetValue(IsCheckAlphaProperty); }
            set { SetValue(IsCheckAlphaProperty, value); }
        }

        //Is check alphanames
        public static BindableProperty IsCheckAlphaNameProperty = BindableProperty.Create("IsCheckAlphaName",
            typeof(bool), typeof(EntryValidatorBehavior), default(bool));

        public bool IsCheckAlphaName
        {
            get { return (bool)GetValue(IsCheckAlphaNameProperty); }
            set { SetValue(IsCheckAlphaNameProperty, value); }
        }

        //Is check alphaduration
        public static BindableProperty IsCheckAlphaDurationProperty = BindableProperty.Create("IsCheckAlphaDuration",
            typeof(bool), typeof(EntryValidatorBehavior), default(bool));

        public bool IsCheckAlphaDuration
        {
            get { return (bool)GetValue(IsCheckAlphaDurationProperty); }
            set { SetValue(IsCheckAlphaDurationProperty, value); }
        }


        //Is check number
        public static BindableProperty IsCheckNumberProperty = BindableProperty.Create("IsCheckNumber",
            typeof(bool), typeof(EntryValidatorBehavior), default(bool));

        public bool IsCheckNumber
        {
            get { return (bool)GetValue(IsCheckNumberProperty); }
            set { SetValue(IsCheckNumberProperty, value); }
        }

        //Is check telephone
        public static BindableProperty IsCheckTelephoneProperty = BindableProperty.Create("IsCheckTelephone",
            typeof(bool), typeof(EntryValidatorBehavior), default(bool));

        public bool IsCheckTelephone
        {
            get { return (bool)GetValue(IsCheckTelephoneProperty); }
            set { SetValue(IsCheckTelephoneProperty, value); }
        }

        //Is check telephone conv
        public static BindableProperty IsCheckTelephoneConvProperty = BindableProperty.Create("IsCheckTelephoneConv",
            typeof(bool), typeof(EntryValidatorBehavior), default(bool));

        public bool IsCheckTelephoneCon
        {
            get { return (bool)GetValue(IsCheckTelephoneConvProperty); }
            set { SetValue(IsCheckTelephoneConvProperty, value); }
        }

        //Is check address
        public static BindableProperty IsCheckAddressProperty = BindableProperty.Create("IsCheckAddress",
            typeof(bool), typeof(EntryValidatorBehavior), default(bool));

        public bool IsCheckAddress
        {
            get { return (bool)GetValue(IsCheckAddressProperty); }
            set { SetValue(IsCheckAddressProperty, value); }
        }

        //Is check slashname
        public static BindableProperty IsCheckSlashNameProperty = BindableProperty.Create("IsCheckSlashName",
            typeof(bool), typeof(EntryValidatorBehavior), default(bool));

        public bool IsCheckSlashName
        {
            get { return (bool)GetValue(IsCheckSlashNameProperty); }
            set { SetValue(IsCheckSlashNameProperty, value); }
        }

        //Is check min length
        public static BindableProperty MinLengthProperty = BindableProperty.Create("MinLength",
            typeof(int), typeof(EntryValidatorBehavior), default(int));

        public int MinLength
        {
            get { return (int)GetValue(MinLengthProperty); }
            set { SetValue(MinLengthProperty, value); }
        }

        //Is check max length
        public static BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength",
            typeof(int), typeof(EntryValidatorBehavior), default(int));

        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        //Is check min value
        public static BindableProperty MinValueProperty = BindableProperty.Create("MinValue",
            typeof(decimal), typeof(EntryValidatorBehavior), default(decimal));

        public decimal MinValue
        {
            get { return (decimal)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        //Is check max value
        public static BindableProperty MaxValueProperty = BindableProperty.Create("MaxValue",
            typeof(decimal), typeof(EntryValidatorBehavior), default(decimal));

        public decimal MaxValue
        {
            get { return (decimal)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Is greater than zero
        public static BindableProperty IsValidRangeValueProperty = BindableProperty.Create("IsValidRange",
            typeof(bool), typeof(EntryValidatorBehavior), default(bool));

        public bool IsValidRange1_99
        {
            get { return (bool)GetValue(IsValidRangeValueProperty); }
            set { SetValue(IsValidRangeValueProperty, value); }
        }

        // compare to entry
        public static readonly BindableProperty CompareToEntryProperty = BindableProperty.Create("CompareToEntry", typeof(Entry), typeof(ConfirmPasswordBehavior), null);

        public Entry CompareToEntry
        {
            get { return (Entry)base.GetValue(CompareToEntryProperty); }
            set { base.SetValue(CompareToEntryProperty, value); }
        }
        #endregion Properties
    }
}
