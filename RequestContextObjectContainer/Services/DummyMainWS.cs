namespace RequestContextObjectContainer.Services
{
    public class DummyMainWS
    {
        private readonly HttpContextContainer _contextContainer;

        public DummyMainWS(HttpContextContainer contextContainer)
        {
            this._contextContainer = contextContainer;
        }

        public string DoSomething()
        {
            return $"DummyMainWS: This is HttpContextManager.Current.Message {_contextContainer.Current.Message} and HttpCotextManager.Current.Context.HashCode ${_contextContainer.Current.Context.GetHashCode()}";
        }
    }
}