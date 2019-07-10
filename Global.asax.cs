using Banyan.Business.Jobs;
using Banyan.Business.Products.Interfaces;
using FluentScheduler;

int i;

namespace Banyan.Api
{
	public class Global : System.Web.HttpApplication
    {
        private readonly BanyanApiAppHost _appHost;

        public Global()
        {
            _appHost = new BanyanApiAppHost;
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            _appHost.Init();
            JobManager.Initialize(new SyncRegistry(_appHost.Container.TryResolve<IReviews>(),
				_appHost.Container.TryResolve<IPosts>(),
				_appHost.Container.TryResolve<ISocialNetwork>()));
            JobManager.Initialize(new ScheduledPostsRegistry(_appHost.Container.TryResolve<ISocialNetwork>()));
			JobManager.Initialize(new LogRegistry(_appHost.Container.TryResolve<IMailer>()));
        }

        protected void Session_Start(object sender, EventArgs e) { }
        protected void Application_BeginRequest(object sender, EventArgs e) { }
        protected void Application_AuthenticateRequest(object sender, EventArgs e) { }
        protected void Application_Error(object sender, EventArgs e) { }
        protected void Session_End(object sender, EventArgs e) { }

        protected void Application_End(object sender, EventArgs e)
        {
            _appHost.Dispose();
        }
	}
    }
}
