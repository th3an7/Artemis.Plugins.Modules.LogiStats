using Artemis.Core.Modules;

namespace Artemis.Plugins.Modules.LogiStats.DataModels;

public class LogiStatsDataModel : DataModel
{
    public DevicesDataModel Devices { get; set; } = new();
}

public class DevicesDataModel : DataModel { }

public class DeviceBatteryDataModel : DataModel
{
    public double Percentage { get; set; }
    
    public double Millivolts { get; set; }

    public bool Charging { get; set; }
}
