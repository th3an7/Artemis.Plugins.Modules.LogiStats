using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Artemis.Core.Modules;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using FluentAvalonia.Core;

namespace Artemis.Plugins.Modules.LogiStats.DataModels
{
    public class LogiStatsDataModel : DataModel
    {
        public Root dataModelJson { get; set; }
    }

    public class Application
    {
        public string applicationId { get; set; }
        public List<Command> commands { get; set; }
        public string databaseId { get; set; }
        public string name { get; set; }
        public string posterTitlePosition { get; set; }
        public string posterUrl { get; set; }
        public string applicationFolder { get; set; }
        public string applicationPath { get; set; }
        public bool? isInstalled { get; set; }
        public int? version { get; set; }
        public List<CategoryColor> categoryColors { get; set; }
        public string lastRunTime { get; set; }
        public int? processId { get; set; }
        public List<Application> applications { get; set; }
    }

    public class ArxControlAuthentication
    {
    }

    public class Assignment
    {
        public string cardId { get; set; }
        public string slotId { get; set; }
    }

    /*public class BatteryG703heroPercentage
    {
        public bool isCharging { get; set; }
        public int millivolts { get; set; }
        public int percentage { get; set; }
        public DateTime time { get; set; }
    }

    public class BatteryG703heroWarning
    {
        public string deviceId { get; set; }
        public string level { get; set; }
        public int percentage { get; set; }
        public string time { get; set; }
    }

    public class BatteryProxwirelessheadsetWarning
    {
        public string deviceId { get; set; }
        public string level { get; set; }
        public int percentage { get; set; }
        public string time { get; set; }
    }*/

    public class BatteryWarning
    {
        public BatteryWarning(string deviceId, string level, int percentage, string time)
        {
            this.deviceId = deviceId;
            this.level = level;
            this.percentage = percentage;
            this.time = time;
        }
        
        public string deviceId { get; set; }
        public string level { get; set; }
        public int percentage { get; set; }
        public string time { get; set; }
    }

    public class BatteryPercentage
    {
        public bool isCharging { get; set; }
        public int millivolts { get; set; }
        public int percentage { get; set; }
        public DateTime time { get; set; }
    }

    public class BlueVoiceSettings
    {
        public Description description { get; set; }
        public Settings settings { get; set; }
    }

    public class Card
    {
        public string applicationId { get; set; }
        public string attribute { get; set; }
        public string category { get; set; }
        public string id { get; set; }
        public Macro macro { get; set; }
        public string name { get; set; }
        public bool readOnly { get; set; }
        public string profileId { get; set; }
        public Equalizer equalizer { get; set; }
        public ProEqualizer proEqualizer { get; set; }
        public BlueVoiceSettings blueVoiceSettings { get; set; }
        public List<Card> cards { get; set; }
    }

    public class CategoryColor
    {
        public string hex { get; set; }
        public string tag { get; set; }
    }

    public class Command
    {
        public string cardId { get; set; }
        public string category { get; set; }
        public string name { get; set; }
    }

    public class Compressor
    {
        public int attack { get; set; }
        public bool enabled { get; set; }
        public int makeup { get; set; }
        public int ratio { get; set; }
        public int release { get; set; }
        public int threshold { get; set; }
    }

    public class CrashReporting
    {
        public bool userPrompted { get; set; }
        public string versionNumber { get; set; }
    }

    public class DeEsser
    {
        public int attack { get; set; }
        public int frequency { get; set; }
        public int ratio { get; set; }
        public int release { get; set; }
        public bool? enabled { get; set; }
        public int? threshold { get; set; }
    }

    public class DePopper
    {
        public bool enabled { get; set; }
        public int frequency { get; set; }
    }

    public class Description
    {
        public string name { get; set; }
        public string tag { get; set; }
        public string slotPrefix { get; set; }
    }

    public class DevicesKnown
    {
        public List<KnownList> knownList { get; set; }
    }

    public class DynamicCards
    {
    }

    public class Equalizer
    {
        public List<double> advanced { get; set; }
        public string deviceType { get; set; }
        public string name { get; set; }
        public List<int> simple { get; set; }
        public string type { get; set; }
    }

    public class Expander
    {
        public int attack { get; set; }
        public int attenuationMax { get; set; }
        public bool enabled { get; set; }
        public int hysteresis { get; set; }
        public int ratio { get; set; }
        public int release { get; set; }
        public int threshold { get; set; }
        public int thresholdAutoSensibility { get; set; }
    }

    public class Feature
    {
        public List<string> configuringSlots { get; set; }
        public string devicePrefix { get; set; }
        public string type { get; set; }
    }

    public class High
    {
        public int frequency { get; set; }
        public double gain { get; set; }
        public double width { get; set; }
    }

    public class Inactivity
    {
        public int seconds { get; set; }
    }

    public class IntegrationManagerSettings
    {
        public bool allowLedSdk { get; set; }
    }

    public class Keystroke
    {
        public int code { get; set; }
        public List<int> modifiers { get; set; }
    }

    public class KnownDevicePrefixes
    {
        public List<string> values { get; set; }
    }

    public class KnownList
    {
        public string modelId { get; set; }
        public bool reportedToAnalytics { get; set; }
        public string serialNumber { get; set; }
        public string timestamp { get; set; }
        public string type { get; set; }
    }

    public class LightingCustomPalette
    {
    }

    /*public class LightingG703heroLowBatteryBehavior
    {
        public bool enabled { get; set; }
    }

    public class LightingG703heroPowerSaving
    {
        public double brightness { get; set; }
        public Inactivity inactivity { get; set; }
        public Sleep sleep { get; set; }
    }*/

    public class Limiter
    {
        public double attack { get; set; }
        public double boost { get; set; }
        public double release { get; set; }
    }

    public class Low
    {
        public int frequency { get; set; }
        public double gain { get; set; }
        public double width { get; set; }
    }

    public class Macro
    {
        public string actionName { get; set; }
        public Keystroke keystroke { get; set; }
        public string type { get; set; }
    }

    public class Mid
    {
        public int frequency { get; set; }
        public double gain { get; set; }
        public double width { get; set; }
    }

    public class NoiseReduction
    {
        public int attenuationMax { get; set; }
        public double bias { get; set; }
        public bool enabled { get; set; }
        public int release { get; set; }
        public int sensitivity { get; set; }
    }

    public class NotificationsConfig
    {
        public bool desktopNotificationsDisabled { get; set; }
    }

    public class Persistent
    {
    }

    public class PersistentFeatures
    {
        public List<Assignment> assignments { get; set; }
        public List<Feature> features { get; set; }
        public string syncLightingCard { get; set; }
    }

    public class ProEqualizer
    {
        public List<int> bands { get; set; }
        public string deviceType { get; set; }
        public string name { get; set; }
        public string tag { get; set; }
        public string type { get; set; }
    }

    public class Profile
    {
        public string applicationId { get; set; }
        public List<Assignment> assignments { get; set; }
        public string id { get; set; }
        public string lightingCard { get; set; }
        public string name { get; set; }
        public bool? activeForApplication { get; set; }
        public string syncLightingCard { get; set; }
        public List<Profile> profiles { get; set; }
    }

    public class Root
    {
        [JsonProperty("/notifications/config")]
        public NotificationsConfig notificationsconfig { get; set; }
        public Application applications { get; set; }
        public ArxControlAuthentication arx_control_authentication { get; set; }
        public string arx_control_service_guid { get; set; }
        public bool autostart { get; set; }

        //[JsonProperty("/battery/*/warning")]
        

        //[JsonProperty("/battery/*/percentage")]
        //public BatteryPercentage batterypercentage { get; set; }

        [JsonExtensionData]
        public Dictionary<object, object> _additionalData = new Dictionary<object, object>();//{ get; set; }
        //public Dictionary<dynamic, BatteryWarning> battery = new Dictionary<object, BatteryWarning>();
        public BatteryWarning battery { get; set; }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            var knownListIds = devicesknown.knownList.ToArray();
            foreach (dynamic modelId in knownListIds) {
                dynamic edited = $"battery/{modelId.modelId.Replace("_", "")}/warning";
                if ((_additionalData.TryGetValue($"battery/{modelId.modelId}/warning", out edited)) == true) {
                    var removingBrackets = edited.ToString();
                    removingBrackets = removingBrackets.Substring(0, removingBrackets.Length);
                    battery = JsonConvert.DeserializeObject<BatteryWarning>(removingBrackets);
                }
            }
        }

        /*[JsonProperty("battery/g703hero/percentage")]
        public BatteryG703heroPercentage batteryg703heropercentage { get; set; }

        [JsonProperty("battery/g703hero/warning")]
        public BatteryG703heroWarning batteryg703herowarning { get; set; }

        [JsonProperty("battery/proxwirelessheadset/warning")]
        public BatteryProxwirelessheadsetWarning batteryproxwirelessheadsetwarning { get; set; }*/
        public bool blue_voice_advanced_controls_enabled { get; set; }
        public string brand { get; set; }
        public Card cards { get; set; }
        public CrashReporting crash_reporting { get; set; }
        public DateTime created { get; set; }

        [JsonProperty("devices/known")]
        public DevicesKnown devicesknown { get; set; }
        public DynamicCards dynamic_cards { get; set; }
        public string first_run_version { get; set; }
        public List<string> installed_integrations { get; set; }
        public IntegrationManagerSettings integration_manager_settings { get; set; }
        public KnownDevicePrefixes known_device_prefixes { get; set; }
        public string language { get; set; }
        public string last_run_version { get; set; }

        /*[JsonProperty("lighting/g703hero/low_battery/behavior")]
        public LightingG703heroLowBatteryBehavior lightingg703herolow_batterybehavior { get; set; }

        [JsonProperty("lighting/g703hero/power_saving")]
        public LightingG703heroPowerSaving lightingg703heropower_saving { get; set; }*/
        public LightingCustomPalette lighting_custom_palette { get; set; }
        public bool lighting_rgb_to_custom_migration_done { get; set; }
        public bool lighting_sleep { get; set; }
        public bool lock_notifications_enabled { get; set; }
        public bool low_battery_notifications_enabled { get; set; }
        public bool migration_shown { get; set; }
        public bool mouse_buttons_swapped { get; set; }
        public List<object> non_interactive_prompted { get; set; }
        public bool notifications_enabled { get; set; }
        public bool onboarding_shown { get; set; }
        public Persistent persistent { get; set; }
        public PersistentFeatures persistent_features { get; set; }
        public Profile profiles { get; set; }
        public string release_notes_build_id { get; set; }
        public List<string> seen_coach_marks { get; set; }
        public bool settings_transferred { get; set; }
    }

    public class Settings
    {
        public Compressor compressor { get; set; }
        public DeEsser deEsser { get; set; }
        public DePopper dePopper { get; set; }
        public Expander expander { get; set; }
        public double inputGain { get; set; }
        public Limiter limiter { get; set; }
        public NoiseReduction noiseReduction { get; set; }
        public VoiceEq voiceEq { get; set; }
        public double volume { get; set; }
    }

    public class Sleep
    {
        public int seconds { get; set; }
    }

    public class VoiceEq
    {
        public bool enabled { get; set; }
        public High high { get; set; }
        public Low low { get; set; }
        public Mid mid { get; set; }
    }
}

