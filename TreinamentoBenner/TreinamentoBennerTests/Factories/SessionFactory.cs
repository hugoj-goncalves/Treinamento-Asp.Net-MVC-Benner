using System.IO;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace TreinamentoBennerTests.Factories
{
    public class SessionFactory
    {
        public static HttpContext Create()
        {
            var httpRequest = new HttpRequest("", "http://localhost", "");

            var httpResponse = new HttpResponse(new StringWriter());

            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id",
                new SessionStateItemCollection(), 
                new HttpStaticObjectsCollection(), 
                10, 
                true,
                HttpCookieMode.AutoDetect,
                SessionStateMode.InProc,
                false);

            httpContext.Items["AppSession"] = typeof (HttpSessionState)
                .GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.Instance,
                    null,
                    CallingConventions.Standard,
                    new[] {typeof (HttpSessionStateContainer)},
                    null
                ).Invoke(new object[] {sessionContainer});

            return httpContext;
        }
    }
}