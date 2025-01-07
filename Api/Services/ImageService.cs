using Newtonsoft.Json;

namespace Api.Services;

public class ImageService
{
    public byte[] GetPhoto(string photo)
    {
        try
        {
            return JsonConvert.DeserializeObject<byte[]>(photo);
        }
        catch
        {
            try
            {
                return JsonConvert.DeserializeObject<byte[]>("[" + photo + "]");
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }
    }
}