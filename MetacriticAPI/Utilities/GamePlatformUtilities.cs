namespace MetacriticAPI.Utilities
{
    internal static class GamePlatformUtilities
    {
        internal static string? GetMetacriticPlatformId(string? platformName) => platformName
            ?.ToLowerInvariant()
            ?.Replace(" ", string.Empty)
            switch
        {
            null or "" or "*" or "all" => null,
            "ps4" or "playstation4" or "72496" => "72496",
            "ps3" or "playstation3" or "1" => "1",
            "xboxone" or "xbox-one" or "xbone" or "80000" => "80000",
            "xbox360" or "2" => "2",
            "pc" or "3" => "3",
            "ds" or "nintendods" or "4" => "4",
            "3ds" or "nintendo3ds" or "16" => "16",
            "vita" or "psvita" or "playstationvita" or "67365" => "67365",
            "psp" or "playstationportable" or "7" => "7",
            "wii" or "nintendowii" or "8" => "8",
            "wiiu" or "nintendowiiu" or "68410" => "68410",
            "switch" or "nintendoswitch" or "268409" => "268409",
            "ps2" or "playstation2" or "6" => "6",
            "ps" or "ps1" or "psone" or "playstationone" or "10" => "10",
            _ => throw new ArgumentException($"Unknown platform id \"{platformName}\".")
        };
    }
}
