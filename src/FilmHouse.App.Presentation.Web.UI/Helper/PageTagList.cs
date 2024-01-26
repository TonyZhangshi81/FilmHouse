using System.Text;
using Microsoft.AspNetCore.Html;

namespace FilmHouse.App.Presentation.Web.UI.Helper;

public static class Controls
{
    /// <summary>
    /// 翻页控件
    /// </summary>
    /// <param name="page">当前页码</param>
    /// <param name="pagingSize">单页显示件数</param>
    /// <param name="pagingCount">查询总页数</param>
    /// <param name="url">当前页面url</param>
    /// <returns></returns>
    public static Microsoft.AspNetCore.Html.IHtmlContent PageTagList(int page, int pagingSize, int pagingCount, string url)
    {
        StringBuilder sbPageTag = new StringBuilder();
        sbPageTag.Append("<nav class=\"text-center\"><ul class=\"pagination\">");
        if (page == 1)
        {
            sbPageTag.Append("<li class=\"disabled\"><a href=\"javascript:void(0)\" aria-label=\"Previous\"><span aria-hidden=\"true\">上一页</span></a></li>");
        }
        else
        {
            sbPageTag.Append("<li><a href=\"").Append(ModelUtils.BuildUrl(url, "page", (page - 1).ToString())).Append("\" aria-label=\"Previous\"><span aria-hidden=\"true\">上一页</span></a></li>");
        }

        List<int> sbPageNumbers = new List<int>();
        for (int i = page; i <= pagingCount && sbPageNumbers.Count < pagingSize; i++)
        {
            sbPageNumbers.Add(i);
        }
        if (sbPageNumbers.Count < pagingSize)
        {
            for (int i = page - 1; i >= 1 && sbPageNumbers.Count < pagingSize; i--)
            {
                sbPageNumbers.Insert(0, i);
            }
        }
        foreach (var item in sbPageNumbers)
        {
            if (item == page)
            {
                sbPageTag.Append("<li class=\"active\"><a href=\"").Append(ModelUtils.BuildUrl(url, "page", item.ToString())).Append("\">").Append(item).Append("</a></li>");
            }
            else
            {
                sbPageTag.Append("<li><a href=\"").Append(ModelUtils.BuildUrl(url, "page", item.ToString())).Append("\">").Append(item).Append("</a></li>");
            }
        }

        if (page == pagingCount)
        {
            sbPageTag.Append("<li class=\"disabled\"><a href=\"javascript:void(0)\" aria-label=\"Previous\"><span aria-hidden=\"true\">下一页</span></a></li>");
        }
        else
        {
            sbPageTag.Append("<li><a href=\"").Append(ModelUtils.BuildUrl(url, "page", (page + 1).ToString())).Append("\" aria-label=\"Previous\"><span aria-hidden=\"true\">下一页</span></a></li>");
        }
        sbPageTag.Append("</ul></nav>");
        return new HtmlString(sbPageTag.ToString());
    }
}