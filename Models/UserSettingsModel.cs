using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Services;

namespace GroupProject.Models;

public struct UserSettingsModel
{
    private const string _settingsFileName = "settings.json";

    public double MainVolume { get; set; }
    public double MusicVolume { get; set; }
    public double SFXVolume { get; set; }
    public bool IsAudioMuted { get; set; }
    
    public string Theme { get; set; }
    
    public UserSettingsModel()
    {
        MainVolume = 0.5f;
        MusicVolume = 1f;
        SFXVolume = 1f;
        IsAudioMuted = false;
        Theme = "Dark";

        TryCreateInitialSettingsFile();
    }
    
    [RelayCommand]
    public void SaveSettings()
    {
        Console.WriteLine("Saving settings...");
        
        var json = JsonSerializer.Serialize(this);
        Console.WriteLine(json);
        
        File.WriteAllText(_settingsFileName, json);
        Console.WriteLine(File.ReadAllText(_settingsFileName));
        
        Console.WriteLine("Successfully saved settings");
    }
    
    [RelayCommand]
    public void LoadSettings()
    {
        Console.WriteLine("Loading settings...");
        
        if(!File.Exists(_settingsFileName))
        {
            Console.WriteLine("Settings file does not exist");
            return;
        }
        
        var json = File.ReadAllText(_settingsFileName);
        Console.WriteLine(json);

        try
        {
            var settings = JsonSerializer.Deserialize<UserSettingsModel>(json);
            Console.WriteLine(settings);

            MainVolume = settings.MainVolume;
            MusicVolume = settings.MusicVolume;
            SFXVolume = settings.SFXVolume;
            IsAudioMuted = settings.IsAudioMuted;
            Theme = settings.Theme;
        }
        catch(JsonException e)
        {
            Console.WriteLine("Error deserializing settings: " + e.Message);
        }
    }
    
    private bool TryCreateInitialSettingsFile()
    {
        // Check for settings file
        if (!File.Exists("settings.json"))
        {
            File.Create("settings.json");

            Console.WriteLine("Successfully created settings file");

            return true;
        }

        // Create settings file
        Console.WriteLine("Settings file already exists");

        return false;
    }
}