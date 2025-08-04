using System;

namespace Item.Runtime
{
    public class Item
    {
        #region Getters and Setters

        public Guid ID
        {
            get { return _id; }
        }

        public ItemDefinitionSO Definition
        {
            get { return _definition; }
            set { _definition = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public float EffectValue
        {
            get { return _effectValue; }
            set { _effectValue = value; }
        }

        #endregion
        
        #region Parameters

        Guid _id;
        ItemDefinitionSO _definition;
        int _quantity;
        float _effectValue;

        #endregion
        
        #region Constructor
        
        public Item(Guid id, ItemDefinitionSO definition, int quantity, float effectValue)
        {
            _id = id;
            _definition = definition;
            _quantity = quantity;
            _effectValue = effectValue;
        }
        
        #endregion
    }
}
