using System;
using System.Media;

namespace GroupProject.Scripts;

public static class SoundHelper
{
    private static SoundPlayer _player = new SoundPlayer();
    
    public static void PlaySound(string sound)
    {
        // Check to make sure user is on windows
        if (Environment.OSVersion.Platform != PlatformID.Win32NT)
        {
            Console.WriteLine("Sound can only be played on Windows.");
            return;
        }
        
        switch (sound)
        {
            case "Button":
                _player.SoundLocation = @"..\..\..\Assets\Sounds\ClickSFX.wav";
                break;
            case "ButtonAlt":
                _player.SoundLocation = @"..\..\..\Assets\Sounds\ClickSFXHighPitch.wav";
                break;
            default:
                Console.WriteLine("Sound reference not recognised.");
                return;
        }

        try
        {
            _player.Load();
            _player.Play();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error found - can't play sound: " + ex.Message);
        }
    }
}
