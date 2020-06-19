using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;


namespace PersonManager.Behaviors.Fluent
{
    public class ValidationCollected
    {
        private readonly List<ValidationObject> _validationObjects;

        public ValidationCollected()
        {
            _validationObjects = new List<ValidationObject>();
        }

        /// <summary>
        /// Apply the first invalid validation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TCtrl"></typeparam>
        /// <param name="validatorBehavior"></param>
        /// <returns></returns>
        public T ApplyResult<T, TCtrl>(T validatorBehavior)
            where TCtrl : BindableObject
            where T : ValidatorBehavior<TCtrl>
        {
            var validationObject = _validationObjects.FirstOrDefault(x => !x.IsValid);

            if (validationObject != null)
            {
                validatorBehavior.NoValided(validationObject.Message);
            }
            else
            {
                validatorBehavior.Valided();
            }

            //NOTE: Do set default valid value if needed

            return validatorBehavior;
        }

        /// <summary>
        /// Collect all invalid messages and apply at one
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TCtrl"></typeparam>
        /// <param name="validatorBehavior"></param>
        /// <param name="separate"></param>
        /// <returns></returns>
        public T ApplyAllResults<T, TCtrl>(T validatorBehavior, string separate = ", ")
            where TCtrl : BindableObject
            where T : ValidatorBehavior<TCtrl>
        {
            if (_validationObjects != null && _validationObjects.Any())
            {
                var message = string.Join(separate, _validationObjects.Select(x => x.Message));
                validatorBehavior.NoValided(message);
            }
            else
            {
                validatorBehavior.Valided();
            }

            //NOTE: Do set default valid value if needed

            return validatorBehavior;
        }

        public void Add(
            bool hasValidation,
            List<Func<bool>> validateFuncs,
            string message)
        {
            _validationObjects.Add(new ValidationObject
            {
                HasValidation = hasValidation,
                ValidateFuncs = validateFuncs,
                Message = message
            });
        }

        private class ValidationObject
        {
            public bool IsValid => DoValidate();

            public bool HasValidation { get; set; }
            public List<Func<bool>> ValidateFuncs { get; set; }
            public string Message { get; set; }

            private bool DoValidate()
            {
                if (!HasValidation)
                {
                    return true;
                }

                var isValid = true;
                foreach (var func in ValidateFuncs)
                {
                    isValid = isValid && func();
                }

                return isValid;
            }
        }
    }
}