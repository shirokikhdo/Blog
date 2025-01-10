using Newtonsoft.Json;

namespace Api.Services;

/// <summary>
/// Сервис для работы с изображениями.
/// </summary>
public class ImageService
{
    /// <summary>
    /// Получает фотографию в виде массива байтов из строки, представляющей изображение.
    /// </summary>
    /// <param name="photo">Строка, содержащая изображение в формате JSON.</param>
    /// <returns>
    /// Массив байтов, представляющий изображение, если преобразование прошло успешно; 
    /// в противном случае возвращает пустой массив байтов.
    /// </returns>
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