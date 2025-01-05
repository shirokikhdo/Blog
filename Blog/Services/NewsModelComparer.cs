using Blog.Models;

namespace Blog.Services;

public class NewsModelComparer : IComparer<NewsModel>
{
    public int Compare(NewsModel x, NewsModel y)
    {
        if (x.PostDate > y.PostDate)
            return -1;

        return x.PostDate < y.PostDate ? 1 : 0;
    }
}