using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace GroupProject.Scripts;

public static class ImageHelper
{
    public static Bitmap LoadFromResource(string resourcePath)
    {
        Uri resourceUri;
        if (!resourcePath.StartsWith("avares://"))
        {
            var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
            resourceUri = new Uri($"avares://{assemblyName}/{resourcePath.TrimStart('/')}");
            
            Console.WriteLine($"Loading image '{resourcePath}' from resources");
        }
        else
        {
            resourceUri = new Uri(resourcePath);
            
            Console.WriteLine($"Loading image '{resourcePath}' from assets");
        }
        
        Console.WriteLine($"Resource URI: {resourceUri}");
        return new Bitmap(AssetLoader.Open(resourceUri));
    }

    public static async Task<Bitmap> LoadFromWeb(string resourcePath)
    {
        var uri = new Uri(resourcePath);
        using var httpClient = new HttpClient();
        try
        {
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AvaloniaTest", "0.1"));
            var data = await httpClient.GetByteArrayAsync(uri);
            
            Console.WriteLine($"Downloaded image '{uri}' with {data.Length} bytes");
            
            return new Bitmap(new MemoryStream(data));
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while downloading image '{uri}' : {ex.Message}");
            return null;
        }
    }
}