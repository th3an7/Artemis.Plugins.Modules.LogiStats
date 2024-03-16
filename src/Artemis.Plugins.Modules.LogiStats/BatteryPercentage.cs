using System;

namespace Artemis.Plugins.Modules.LogiStats;

public class BatteryPercentage
{
    public bool? IsCharging { get; set; }

    public float? Millivolts { get; set; }

    public float Percentage { get; set; }

    public DateTime Time { get; set; }
}