using System;
using System.Media;

namespace GroupProject.Scripts;

public class SoundHelper
{
    public void PlaySound(string sound)
    {
        SoundPlayer player = new SoundPlayer();



        switch (sound)
        {
            case "Button":
                player.SoundLocation = @"..\..\..\Assets\Sounds\ClickSFX.wav";
                break;
            case "ButtonAlt":
                player.SoundLocation = @"..\..\..\Assets\Sounds\ClickSFXHighPitch.wav";
                break;
            default:
                Console.WriteLine("String reference not recognised.");
                return;
        }

        try
        {
            player.Load();
            player.Play();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error found - can't play sound: " + ex.Message);
        }
    }
}
