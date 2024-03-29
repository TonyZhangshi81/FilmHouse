﻿using FilmHouse.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmHouse.App.Presentation.Web.UI.Helper;

public static class ModelUtils
{
    /// <summary>
    /// 显示用文件大小
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static DisplayResourceSizeVO CalculateToDiscSize(ResourceSizeVO size)
    {
        if (size.AsPrimitive() > 1024 * 1024 * 1024)
        {
            return new($"{size.AsPrimitive() / (1024 * 1024 * 1024)} G");
        }
        else if (size.AsPrimitive() > 1024 * 1024)
        {
            return new($"{size.AsPrimitive() / (1024 * 1024)} M");
        }
        else if (size.AsPrimitive() > 1024)
        {
            return new($"{size.AsPrimitive() / 1024} K");
        }
        else
        {
            return new($"{size.AsPrimitive()} 字节");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directorsId"></param>
    /// <param name="directorNames"></param>
    /// <returns></returns>
    public static List<SelectListItem> GetDirectors(DirectorsIdVO directorsId, DirectorNamesVO directorNames)
    {
        var list = new List<SelectListItem>();

        if (directorsId == null)
        {
            if (directorNames != null)
            {
                var enumDirectors = directorNames.ToEnumerable().GetEnumerator();
                while (enumDirectors.MoveNext())
                {
                    list.Add(new SelectListItem() { Text = enumDirectors.Current.AsPrimitive(), Value = string.Empty });
                }
            }
        }
        else
        {
            var index = 0;
            var directorIds = directorsId.ToEnumerable().GetEnumerator();
            var enumDirectors = directorNames.ToEnumerable().Cast<CelebrityNameVO>().ToArray();
            while (directorIds.MoveNext())
            {
                list.Add(new SelectListItem() { Text = enumDirectors.ElementAt(index).AsPrimitive(), Value = directorIds.Current.AsPrimitive().ToString() });
                index++;
            }
        }
        return list;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="writersId"></param>
    /// <param name="writersNames"></param>
    /// <returns></returns>
    public static List<SelectListItem> GetWriters(WritersIdVO writersId, WritersNamesVO writersNames)
    {
        var list = new List<SelectListItem>();

        if (writersId == null)
        {
            if (writersNames != null)
            {
                var enumWriters = writersNames.ToEnumerable().GetEnumerator();
                while (enumWriters.MoveNext())
                {
                    list.Add(new SelectListItem() { Text = enumWriters.Current.AsPrimitive(), Value = string.Empty });
                }
            }
        }
        else
        {
            var index = 0;
            var writerIds = writersId.ToEnumerable().GetEnumerator();
            var enumWriters = writersNames.ToEnumerable().Cast<CelebrityNameVO>().ToArray();
            while (writerIds.MoveNext())
            {
                list.Add(new SelectListItem() { Text = enumWriters.ElementAt(index).AsPrimitive(), Value = writerIds.Current.AsPrimitive().ToString() });
                index++;
            }
        }
        return list;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="castsId"></param>
    /// <param name="castsNames"></param>
    /// <returns></returns>
    public static List<SelectListItem> GetCasts(CastsIdVO castsId, CastsNamesVO castsNames)
    {
        var list = new List<SelectListItem>();

        if (castsId == null)
        {
            if (castsNames != null)
            {
                var enumCasts = castsNames.ToEnumerable().GetEnumerator();
                while (enumCasts.MoveNext())
                {
                    list.Add(new SelectListItem() { Text = enumCasts.Current.AsPrimitive(), Value = string.Empty });
                }
            }
        }
        else
        {
            var index = 0;
            var castIds = castsId.ToEnumerable().GetEnumerator();
            var enumCasts = castsNames.ToEnumerable().Cast<CelebrityNameVO>().ToArray();
            while (castIds.MoveNext())
            {
                list.Add(new SelectListItem() { Text = enumCasts.ElementAt(index).AsPrimitive(), Value = castIds.Current.AsPrimitive().ToString() });
                index++;
            }
        }
        return list;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="titleEn"></param>
    /// <returns></returns>
    public static MovieTitleAndEnVO SetTitle(MovieTitleVO title, MovieTitleEnVO titleEn)
    {
        var tle = new MovieTitleAndEnVO(title.AsPrimitive());
        if (titleEn != null)
        {
            tle = new MovieTitleAndEnVO($"{title}\t{titleEn.AsPrimitive()}");
        }
        return tle;
    }

    /// <summary>
    /// url里有key的值，就替换为value,没有的话就追加.
    /// </summary>
    /// <param name="sourceUrl">原url</param>
    /// <param name="ParamText">参数名</param>
    /// <param name="ParamValue">参数值</param>
    /// <returns></returns>
    public static string BuildUrl(string sourceUrl, string ParamText, string ParamValue)
    {
        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(string.Format("{0}=[^&]*", ParamText), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex("[&]{2,}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        string url = reg.Replace(sourceUrl, "");
        //_url = reg1.Replace(_url, "");
        if (url.IndexOf("?") == -1)
        {
            url += string.Format("?{0}={1}", ParamText, ParamValue);//?
        }
        else
        {
            url += string.Format("&{0}={1}", ParamText, ParamValue);//&
        }
        url = reg1.Replace(url, "&");
        url = url.Replace("?&", "?");
        return url;
    }
}
