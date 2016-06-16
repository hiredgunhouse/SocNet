using Autofac;
using SocNet.Formatting;
using SocNet.Storage;

namespace SocNet.ConsoleClient
{
    public class Bootstrapper
    {
        public IContainer Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<PostStore>().As<IPostStore>();
            builder.RegisterType<FollowStore>().As<IFollowStore>();
            builder.RegisterType<PostFormatter>().As<IPostFormatter>();
            builder.RegisterType<SocApp>().AsSelf();

            var container = builder.Build();

            return container;
        }
    }
}