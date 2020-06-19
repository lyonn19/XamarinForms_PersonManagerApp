using System;
using System.Collections.Generic;

namespace PersonManager.Behaviors.Fluent
{
    public class FluentValidation
    {
        private ValidationCollected _validationCollected;
        private readonly bool _hasValidation = false;
        private string _message = string.Empty;
        private readonly List<Func<bool>> _validateFuncs;

        public FluentValidation(bool hasValidation)
        {
            _hasValidation = hasValidation;
            _validationCollected = new ValidationCollected();
            _validateFuncs = new List<Func<bool>>();
        }

        public FluentValidation(ValidationCollected validationCollected, bool hasValidation)
        {
            _hasValidation = hasValidation;
            _validationCollected = validationCollected;
            _validateFuncs = new List<Func<bool>>();
        }

        public FluentValidation ValidateBy(Func<bool> func)
        {
            _validateFuncs.Add(func);
            return this;
        }

        public ValidationCollected WithMessage(string message)
        {
            _message = message;

            //collect validation
            if (_validationCollected == null)
            {
                _validationCollected = new ValidationCollected();

            }

            //Only store invalid values, in-case want to get all message
            _validationCollected.Add(_hasValidation, _validateFuncs, _message);


            return _validationCollected;

        }
    }
}