using Castle.Core.Logging;
namespace WinFormsMvp
{
    public class Presenter<TView> : IPresenter<TView>
        where TView : class, IView
    {
        private readonly TView view;
        private readonly ILogger logger;

        protected Presenter(TView view, ILogger logger)
        {
            this.logger = logger;
            this.view = view;
        }

        public TView View
        {
            get { return view; }
        }

        public ILogger Logger { get { return logger; } }
    }
}
