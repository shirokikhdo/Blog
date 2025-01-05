namespace Blog.Models;

public class NewsModel
{
    public int Id { get; set; }

    public string Text { get; private set; }

    public byte[] Image { get; private set; }

    public int LikesCount { get; set; }

    public DateTime PostDate { get; private set; }

    public NewsModel(int id, string text, byte[] image, DateTime postDate)
    {
        Id = id;
        Text = text;
        Image = image;
        PostDate = postDate;
    }
}