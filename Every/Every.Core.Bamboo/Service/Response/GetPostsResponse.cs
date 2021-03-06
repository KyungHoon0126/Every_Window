﻿using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Every.Core.Bamboo.Service.Response
{
    public class GetPostsResponse : BindableBase
    {
        private List<Model.Posts> _posts;
        [JsonProperty("posts")]
        public List<Model.Posts> Posts
        {
            get => _posts;
            set
            {
                SetProperty(ref _posts, value);
            }
        }
    }
}
