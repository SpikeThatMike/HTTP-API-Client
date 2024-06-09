using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HTTP.APIClient
{
    public class Request
    {
        #region Properties
        /// <summary>
        /// URL for the request. If a base URL is set in the client, this will be appended to the base URL
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Method for the request
        /// </summary>
        public HttpMethod Method { get; set; } = HttpMethod.Get;
        #endregion

        public List<Parameter> Parameters { get; private set; } = new();
        public Dictionary<string, string> Headers { get; private set; } = new();
        public object Content { get; private set; } = null;



        #region Methods
        public Request SetBody(object body)
        {
            Content = body;
            return this;
        }

        public Request AddHeader(string name, string value)
        {
            Headers.Add(name, value);
            return this;
        }

        public Request AddHeaders(Dictionary<string, string> headers)
        {
            foreach(var header in headers)
            {
                Headers.Add(header.Key, header.Value);
            }
            return this;
        }

        public Request RemoveHeader(string name)
        {
            Headers.Remove(name);
            return this;
        }

        public Request RemoveHeaders(IEnumerable<string> headers)
        {
            foreach (var header in headers)
            {
                Headers.Remove(header);
            }
            return this;
        }

        public Request AddParameter(string name, object value)
        {
            Parameters.Add(new(name, value));
            return this;
        }

        public Request AddParameter(string name, object value, ParameterType type)
        {
            Parameters.Add(new(name, value));
            return this;
        }

        public Request AddParameter(Parameter parameter)
        {
            Parameters.Add(parameter);
            return this;
        }

        public Request AddParameters(Dictionary<string, object> parameters)
        {
            Parameters.AddRange(parameters.Select(x => new Parameter(x.Key, x.Value)));
            return this;
        }

        public Request AddParameters(IEnumerable<Parameter> parameters)
        {
            Parameters.AddRange(parameters);
            return this;
        }

        public Request RemoveParameter(string name)
        {
            Parameters.RemoveAll(x => x.Name == name);
            return this;
        }

        public Request RemoveParameter(Parameter parameter)
        {
            Parameters.RemoveAll(x => x.Name == parameter.Name);
            return this;
        }

        public Request RemoveParameters(IEnumerable<string> names)
        {
            Parameters.RemoveAll(x => names.Contains(x.Name));
            return this;
        }

        public Request RemoveParameter(IEnumerable<Parameter> parameter)
        {
            Parameters.RemoveAll(x => parameter.Select(y => y.Name).Contains(x.Name));
            return this;
        }
        #endregion
    }
}
