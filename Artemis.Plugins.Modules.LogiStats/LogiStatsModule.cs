using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Modules.LogiStats.DataModels;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System;
using System.IO;
using Serilog;
using Newtonsoft.Json;

namespace Artemis.Plugins.Modules.LogiStats
{
    public class LogiStatsModule : Module<LogiStatsDataModel>
    {
        public readonly ILogger _logger;
        public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new();
        string dbPath = (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) + @"\LGHUB\settings.db";
        string lghubPath = (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) + @"\LGHUB\";
        FileSystemWatcher watcher;

        public override void ModuleActivated(bool isOverride)
        {

        }

        public override void ModuleDeactivated(bool isOverride)
        {
        }

        public override void Enable()
        {
            watcher = new FileSystemWatcher()
            {
                Path = lghubPath,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.Attributes,
                Filter = "*.db",
                EnableRaisingEvents = true
            };

            watcher.Changed += ReadDatabase;
        }

        public LogiStatsModule(ILogger logger)
        {
            _logger = logger;
        }

        public void ReadDatabase(object sender, FileSystemEventArgs e)
        {
            using (var connection = new SqliteConnection(@"Data Source = " + dbPath))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    SELECT file
                    FROM data
                    ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var json = reader.GetString(0);
                        Root dataModelJson = JsonConvert.DeserializeObject<Root>(json);
                        DataModel.dataModelJson = dataModelJson;
                    }
                }
            }
        }

        public override void Disable()
        {

        }

        public override void Update(double deltaTime)
        {

        }
    }
}