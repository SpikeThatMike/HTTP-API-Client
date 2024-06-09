using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTP.APIClient
{
    public class Parameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public ParameterType Type { get; set; } = ParameterType.GetParameter;

        public Parameter(string name, object value)
        {
            name = Name;
            value = Value;
        }

        public Parameter(string name, object value, ParameterType type)
        {
            name = Name;
            value = Value;
            type = Type;
        }
    }
}
