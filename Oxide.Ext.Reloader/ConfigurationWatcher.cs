namespace Oxide.Ext.Reloader
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Core;

    internal class ConfigurationWatcher
    {
        private Dictionary<string, string> _checksums;

        private FileSystemWatcher _watcher;

        public void Watch()
        {
            var files = Directory.GetFiles(Interface.Oxide.ConfigDirectory, "*.json");
            _checksums = files.ToDictionary(x => x, Utilities.GetMd5Checksum);

            _watcher = new FileSystemWatcher(Interface.Oxide.ConfigDirectory, "*.json")
            {
                EnableRaisingEvents = true
            };

            _watcher.Changed += HandleConfigurationChanged;
        }

        private void HandleConfigurationChanged(object sender, FileSystemEventArgs e)
        {
            var currentChecksum = Utilities.GetMd5Checksum(e.FullPath);
            if (_checksums.TryGetValue(e.FullPath, out var checksum) && checksum == currentChecksum)
            {
                return;
            }

            _checksums[e.FullPath] = currentChecksum;

            var plugin = Path.GetFileName(e.FullPath)
                .Replace(".json", string.Empty);

            var plugins = Interface.Oxide.RootPluginManager.GetPlugins();
            if (plugins.Any(x => x.Name == plugin))
            {
                Interface.Oxide.ReloadPlugin(plugin);
            }
        }
    }
}
