using System;
using System.IO;
using System.Text.Json;

namespace FallenTally.Services;

public static class JsonSettingsService
{
    private static string GetSettingsDirectory()
    {
        var appDataDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "FallenTallyAvalon", "settings");
        Directory.CreateDirectory(appDataDir);
        return appDataDir;
    }

    public static void Save<T>(T model, string fileName)
    {
        var dir = GetSettingsDirectory();
        var path = Path.Combine(dir, fileName);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        options.Converters.Add(new AvaloniaColorJsonConverter());

        var json = JsonSerializer.Serialize(model, options);
        File.WriteAllText(path, json);
    }

    public static T? Load<T>(string fileName)
    {
        var dir = GetSettingsDirectory();
        var path = Path.Combine(dir, fileName);

        if (!File.Exists(path))
            return default;

        var options = new JsonSerializerOptions();
        options.Converters.Add(new AvaloniaColorJsonConverter());

        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(json, options);
    }
}