namespace HTTP.APIClient
{
    public partial class Client : IDisposable
    {
        #region Properties
        /// <summary>
        /// Base URL for the Client
        /// </summary>
        public Uri BaseUrl { get; private set; }

        /// <summary>
        /// Private set so that the options can only be set in the constructor
        /// </summary>
        public ClientOptions Options { get; private set; } = new();
        #endregion

        private HttpClient _httpClient = null;
        public HttpClient HttpClient
        {
            get
            {
                if (_httpClient == null)
                {
                    _httpClient = new HttpClient();
                }

                return _httpClient;
            }
        }

        private CancellationTokenSource? _cancellationTokenSource = null;

        #region Constructors
        public Client()
        {
        }

        public Client(string baseUrl)
        {
            BaseUrl = new Uri(baseUrl);
        }

        public Client(Uri baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public Client(ClientOptions options)
        {
            //Check if options is null, if it is, create a new instance of ClientOptions
            Options = options ?? new();
        }

        public Client(string baseUrl, ClientOptions options)
        {
            BaseUrl = new Uri(baseUrl);

            //Check if options is null, if it is, create a new instance of ClientOptions
            Options = options ?? new();
        }

        public Client(Uri baseUrl, ClientOptions options)
        {
            BaseUrl = baseUrl;

            //Check if options is null, if it is, create a new instance of ClientOptions
            Options = options ?? new();
        }
        #endregion


        #region Methods

        #endregion


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
                    Options.Dispose();
                }

                BaseUrl = null;

                disposed = true;
            }
        }

        //Destructor
        ~Client()
        {
            Dispose(false);
        }
        #endregion
    }
}
