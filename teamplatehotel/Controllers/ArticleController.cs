using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using System.Linq;
using ProjectLibrary.Security;
using TeamplateHotel.Models;

namespace TeamplateHotel.Controllers
{
    public  class ArticleController : Controller
    {
        //public static ShowDetailArticle GetDetailArticle(Menu menu, int idArticle)
        //{
        //    using (var db = new MyDbDataContext())
        //    {
        //        var detailArticle = db.Articles.FirstOrDefault(a => a.ArticleId == idArticle && a.Status);
        //        if (detailArticle == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            //lấy danh sách bài viết liên quan
        //            var listOtherArticle =
        //                db.Articles.Where(a => a.MenuId == detailArticle.MenuId && a.ArticleId != detailArticle.ArticleId).
        //                    Select(a => new ShowObject
        //                                    {
        //                                        Alias = a.AliasArticle,
        //                                        Id = a.ArticleId,
        //                                        Description = a.Description,
        //                                        Image = a.ImageArticle,
        //                                        MenuAlias = menu.AliasMenu,
        //                                        Name = a.TitleArticle
        //                                    }).Take(3).ToList();
        //            ShowDetailArticle showDetailArticle = new ShowDetailArticle
        //                                                      {
        //                                                          Article = detailArticle,
        //                                                          ListObject = listOtherArticle
        //                                                      };
        //            return showDetailArticle;
        //        }
               
        //    }
        //}

        ////Damh sách bài viết
        //public static PagingOject ListArticle(Menu menu, int page)
        //{
        //    using (var db = new MyDbDataContext())
        //    {
        //           var listArticle = db.Articles.Where(a => a.MenuId == menu.MenuId).Select(a => new ShowObject
        //            {
        //                Alias = a.AliasArticle,
        //                Description = a.Description,
        //                Id = a.ArticleId,
        //                Image = a.ImageArticle,
        //                MenuAlias = menu.AliasMenu,
        //                Name = a.TitleArticle
        //            }).ToList();
        //        //Lấy danh sách và phân trang
        //        PagingOject pagingOject = CommentController.PagingOnject(listArticle, page);
        //        return pagingOject;
        //    }
        //}
    }

}
