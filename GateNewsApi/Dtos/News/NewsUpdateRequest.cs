﻿using GateNewsApi.Domain;
using GateNewsApi.Enums;

namespace GateNewsApi.Dtos.News
{
    public class NewsUpdateRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public CategoryEnum Category { get; set; }
    }
}
