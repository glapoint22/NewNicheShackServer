﻿using Shared.Common.Dtos;

namespace Shared.PageBuilder.Widgets.GridWidget.Classes
{
    public sealed class SubnicheFilter
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string UrlName { get; set; } = null!;
        public bool Visible { get; set; } = false;

        public SubnicheFilter(SubnicheDto subniche)
        {
            Id = subniche.Id;
            Name = subniche.Name;
            UrlName = subniche.UrlName;
        }
    }
}