﻿
namespace SimpleFramework.Web.Navigation
{
    public static class NavigationNodeExtensions
    {

        public static string ResolveUrl(this NavigationNode node)
        {
            if (node.Url.Length > 0) return node.Url;
            string url = string.Empty;
            if((node.Controller.Length > 0)&&(node.Action.Length > 0))
            {
                if(node.Action == "Index")
                {
                    url = "~/" + node.Controller;
                }
                else
                {
                    url = "~/" + node.Controller + "/" + node.Action;
                }
                
            }

            return url;
        }
    }
}
