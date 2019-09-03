
using AppECM.View;
using System;
using System.Collections.Generic;

namespace AppECM.Model
{
    public class MenuItem
    {
        public MenuItem()
        {
            TargetType = typeof(MainPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public Type TargetType { get; set; }

    }
    public class PageTypeGroup : List<MenuItem>
    {
        public string Title { get; set; }
        public string ShortName { get; set; } //will be used for jump lists
        public string Subtitle { get; set; }
        public PageTypeGroup(string title, string shortName)
        {
            Title = title;
            ShortName = shortName;
        }
        public static IList<PageTypeGroup> All { private set; get; }
    }

}
