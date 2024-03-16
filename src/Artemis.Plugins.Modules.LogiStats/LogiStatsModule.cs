using Artemis.Core.Modules;
using Artemis.Plugins.Modules.LogiStats.DataModels;
using Microsoft.Data.Sqlite;
using Serilog;
using Swan;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Artemis.Plugins.Modules.LogiStats;

public class LogiStatsModule : Module<LogiStatsDataModel>
{
    public override List<IModuleActivationRequirement> ActivationRequirements { get; } = [];
    private const string DB_FILE = "settings.db";
    private static readonly string LGHUB_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LGHUB");
    //private static readonly string LGHUB_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"); 
    private readonly ILogger _logger;
    private FileSystemWatcher? watcher;
    private bool _reading;
    private readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public LogiStatsModule(ILogger logger)
    {
        _logger = logger;
    }

    public override void ModuleActivated(bool isOverride)
    {

    }

    public override void ModuleDeactivated(bool isOverride)
    {
    }

    public override void Enable()
    {
        _reading = false;
        ReadDatabase(null, null);

        watcher = new FileSystemWatcher
        {
            Path = LGHUB_PATH,
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.Attributes,
            Filter = DB_FILE,
            EnableRaisingEvents = true
        };
        
        watcher.Changed += ReadDatabase;
    }

    public override void Disable()
    {

    }

    public override void Update(double deltaTime)
    {

    }

    public void ReadDatabase(object sender, FileSystemEventArgs args)
    {
        if (_reading)
            return;
        
        _reading = true;
        
        try
        {
            var json = ReadJsonFromDb();
            if (json is null)
                return;

            using var jObject = JsonSerializer.Deserialize<JsonDocument>(json);
            if (jObject is null)
                return;
            
            var percentages = jObject.RootElement.EnumerateObject()
                .Where(p => p.Name.StartsWith("battery/") && p.Name.EndsWith("percentage"));
            
            foreach (var jsonProperty in percentages)
            {
                //"battery/" - g502x-lightspeed - "/percentage"
                var name = jsonProperty.Name[8..^11];
                var key = jsonProperty.Name;
                
                var percentage = jsonProperty.Value.Deserialize<BatteryPercentage>(options);
                
                if (!DataModel.Devices.TryGetDynamicChild<DeviceBatteryDataModel>(key, out var dm))
                    dm = DataModel.Devices.AddDynamicChild(key, new DeviceBatteryDataModel(), name);

                dm.Value.Percentage = percentage?.Percentage ?? 0;
                dm.Value.Charging = percentage?.IsCharging ?? false;
                dm.Value.Millivolts = percentage?.Millivolts ?? 0;
            }
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error updating from database");
        }
        finally
        {
            _reading = false;
        }
    }

    private static string? ReadJsonFromDb()
    {
        using var connection = new SqliteConnection(@"Data Source = " + Path.Combine(LGHUB_PATH, DB_FILE));
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT file FROM data";

        using var reader = command.ExecuteReader();
        if (!reader.Read())
            return null;

        return reader.GetString(0);
    }
}