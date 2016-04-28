using Owin;

using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace GitHubExtension.Infrastructure.Extensions.Owin
{
    public static class UseOwinContextInjectorExtension
    {
        public static void UseSimpleInjectorContext(this IAppBuilder app, Container container)
        {
            // Create an OWIN middleware to create an execution context scope
            app.Use(
                async (context, next) =>
                    {
                        using (container.BeginExecutionContextScope())
                        {
                            await next.Invoke();
                        }
                    });
        }
    }
}