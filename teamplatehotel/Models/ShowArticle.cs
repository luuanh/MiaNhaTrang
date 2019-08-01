using ProjectLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamplateHotel.Models
{
    public class ShowArticle
    {
        public Article Article { get; set; }
        public List<ArticleGallery> ArticleGalleries { get; set; }
        
    }
}