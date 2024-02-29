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
    public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new();

    private static readonly string LGHUB_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LGHUB");
    private const string DB_FILE = "settings.db";
    private readonly ILogger _logger;
    private FileSystemWatcher? watcher;

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
        ReadDatabase(null, null);

        watcher = new FileSystemWatcher()
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
        try
        {
            var json = ReadJsonFromDb();
            if (json is null)
                return;

            using var jObject = JsonSerializer.Deserialize<JsonDocument>(json);
            if (jObject is null)
                return;
            
            var batteryData = jObject.RootElement.EnumerateObject()
                                     .Where(p => p.Name.StartsWith("battery/"))
                                     .OrderBy(p => p.Name)
                                     .ToList();

            var percentages = batteryData
                .Where(p => p.Name.EndsWith("percentage"))
                .Select(j => (j.Name, j.Value.Deserialize<BatteryPercentage>()));

            foreach (var batteryPercentage in percentages)
            {
                var name = batteryPercentage.Name.Slice("battery/".Length, batteryPercentage.Name.Length - "/percentage".Length - 1);
                if (!DataModel.Devices.TryGetDynamicChild<DeviceBatteryDataModel>(batteryPercentage.Name, out var dm))
                    dm = DataModel.Devices.AddDynamicChild<DeviceBatteryDataModel>(batteryPercentage.Name, new(), name);

                if (batteryPercentage.Item2 is null)
                    continue;

                dm.Value.Percentage = batteryPercentage.Item2.Percentage;
                dm.Value.Charging = batteryPercentage.Item2.IsCharging;
                dm.Value.Millivolts = batteryPercentage.Item2.Millivolts;
            }
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error updating from database");
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

public class BatteryWarning
{
    public string DeviceId { get; set; }

    public string Level { get; set; }

    public float Percentage { get; set; }

    public string Time { get; set; }
}

public class BatteryPercentage
{
    public bool IsCharging { get; set; }

    public float Millivolts { get; set; }

    public float Percentage { get; set; }

    public DateTime Time { get; set; }
}