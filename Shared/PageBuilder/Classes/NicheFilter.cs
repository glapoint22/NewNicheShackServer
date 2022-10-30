﻿namespace Shared.PageBuilder.Classes
{
    public sealed class NicheFilter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlName { get; set; } = string.Empty;
        public List<SubnicheFilter> Subniches { get; set; } = new List<SubnicheFilter>();
    }
}