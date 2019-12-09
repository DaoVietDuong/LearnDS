using System.Reflection;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using OpenTracing;
namespace D.MoveOn.Common.Tracer
{
    public class DefaultTracer
    {
        public static ITracer Create()
            => new Jaeger.Tracer.Builder(Assembly.GetEntryAssembly().FullName)
                .WithReporter(new NoopReporter())
                .WithSampler(new ConstSampler(false))
                .Build();
    }
}
