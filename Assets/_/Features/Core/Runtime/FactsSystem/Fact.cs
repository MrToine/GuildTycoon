using System;
using Core.Runtime.Interfaces;
using Newtonsoft.Json;

namespace Core.Runtime
{
    public class Fact<T> : IFact
    {

        #region Publics
        
        public T Value;

        public Fact(object value, bool isPersistent)
        {
            Value = (T) value;
            IsPersistent = isPersistent;
        }
        
        public Type ValueType => typeof(T);

        [JsonIgnore]
        public object GetObjectValue => Value;

        public void SetObjectValue(object value)
        {
            if (value is T cast)
            {
                Value = cast;
            }
            else
            {
                throw new InvalidCastException("Cannot asign a value to a fact");           
            }
        }

        public bool IsPersistent { get; set; }

        #endregion
    }
}

