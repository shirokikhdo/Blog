using Api.Models;

namespace Api.Services;

public class NewsViewComparer : IComparer<NewsView>
{
    public int Compare(NewsView x, NewsView y)
    {
        if (x.PostDate > y.PostDate)
            return -1;

        return x.PostDate < y.PostDate ? 1 : 0;
    }
}