using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Artemis.Core.Modules;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using FluentAvalonia.Core;

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
