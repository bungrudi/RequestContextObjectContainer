namespace RequestContextObjectContainer.Services
{
    public class DummyVisualModelWS
    {
        private HttpContextContainer _contextContainer;

        public DummyVisualModelWS(HttpContextContainer contextContainer)
        {
            this._contextContainer = contextContainer;
        }

        public string DoSomethingElse()
        {
            return
                $"DummyVisualModelWS: This is HttpContextManager.Current.Message {_contextContainer.Current.Message} and HttpCotextManager.Current.Context.HashCode ${_contextContainer.Current.Context.GetHashCode()}";
        }
    }
}