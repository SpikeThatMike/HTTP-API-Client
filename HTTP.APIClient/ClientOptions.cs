using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HTTP.APIClient
{
    public class ClientOptions : IDisposable
    {
        /// <summary>
        /// Timeout in milliseconds for the request unless specified in the request. Default 30 seconds
        /// </summary>
        public int Timeout { get; set; } = 1000 * 30;

        /// <summary>
        /// Default headers for a request
        /// </summary>
        public Dictionary<string, string> DefaultRequestHeaders { get; set; } = null;

        public JsonSerializerOptions JsonSerializerOptions { get; set; } = null;

        #region Dispose
        private bool disposed = false;
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free any other managed objects here.
                   
                }

                //Set the object references to null

                disposed = true;
            }
        }

        //Destructor
        ~ClientOptions()
        {
            Dispose(false);
        }
        #endregion
    }
}
