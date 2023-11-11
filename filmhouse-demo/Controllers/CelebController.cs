using demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace demo.Controllers
{
    public class CelebController : Controller
    {
        private readonly List<Celeb> Celebs;

        public CelebController()
        {
            this.Celebs = new List<Celeb>() 
            {
                new Celeb() { Id = "01", Name = "约翰尼·德普" },
                new Celeb() { Id = "02", Name = "娜塔莉·波特曼" },
                new Celeb() { Id = "03", Name = "蒂姆·波顿" },
                new Celeb() { Id = "04", Name = "卡罗琳·汤普森" },
                new Celeb() { Id = "05", Name = "薇诺娜·瑞德" },
                new Celeb() { Id = "06", Name = "黛安·韦斯特" },
                new Celeb() { Id = "07", Name = "达伦·阿伦诺夫斯基" },
                new Celeb() { Id = "11", Name = "塔伊加·维迪提" },
                new Celeb() { Id = "12", Name = "詹尼弗·凯廷·罗宾逊" },
                new Celeb() { Id = "13", Name = "斯坦·李" },
                new Celeb() { Id = "14", Name = "杰森·亚伦" },
                new Celeb() { Id = "15", Name = "克里斯·海姆斯沃斯" },
                new Celeb() { Id = "16", Name = "米拉·库尼斯" },
            };
        }

        public IActionResult Index(string id)
        {
            var celeb = this.Celebs.Where(_ => _.Id == id).FirstOrDefault();
            if(celeb == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            ViewBag.Name = celeb.Name;
            ViewBag.CelebId = celeb.Id;
            ViewBag.PageType = 2;

            return View();
        }
    }

    public class Celeb
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}