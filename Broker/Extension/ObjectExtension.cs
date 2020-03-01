using System.Collections.Generic;
using System.ComponentModel;

namespace Broker.Extension
{
    public static class ObjectExtension
    {
        #region Public Methods

        public static IDictionary<string, object> ToDictionary(this object @object)
        {
            var values = new Dictionary<string, object>();
            if (@object == null)
                return values;

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(@object);
            foreach (PropertyDescriptor prop in props)
            {
                object val = prop.GetValue(@object);
                values.Add(prop.Name, val);
            }

            return values;
        }

        #endregion Public Methods
    }
}