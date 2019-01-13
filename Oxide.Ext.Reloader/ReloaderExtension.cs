namespace Oxide.Ext.Reloader
{
    using System.Reflection;
    using Core;
    using Core.Extensions;

    public class ReloaderExtension : Extension
    {
        public ReloaderExtension(ExtensionManager manager) : base(manager)
        {
        }

        public override string Name => "Reloader";

        public override string Author => "Jacob Stein";

        public override VersionNumber Version => new VersionNumber(
            (ushort) Assembly.GetExecutingAssembly().GetName().Version.Major,
            (ushort) Assembly.GetExecutingAssembly().GetName().Version.Minor,
            (ushort) Assembly.GetExecutingAssembly().GetName().Version.Build);


        public override void Load() => new ConfigurationWatcher().Watch();
    }
}
