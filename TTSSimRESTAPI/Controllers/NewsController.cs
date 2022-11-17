using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTSSimRESTAPI.Models;
using TTSSimRESTAPI.Data;

namespace TTSSimRESTAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private APIContext context;
        public NewsController(APIContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public JsonResult Add(News news)
        {
            context.News.Add(news);
            context.SaveChanges();

            return new JsonResult(Ok(news));
        }

        [HttpPut]
        public JsonResult Edit(News putnews)
        {
            var list = context.News.ToList();
            var result = new News();

            foreach (News news in list)
            {
                if (news.Id == putnews.Id)
                {
                    result = news;
                }
            }
            if (result.Id != putnews.Id)
            {
                return new JsonResult(NotFound());
            }

            result.Title = putnews.Title;
            result.Text = putnews.Text;

            context.SaveChanges();

            return new JsonResult(Ok(putnews));
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = context.News.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public JsonResult GetById(int id)
        {
            var list = context.News.ToList();
            var result = new News();

            foreach (News news in list)
            {
                if (news.Id == id)
                {
                    result = news;
                }
            }

            if (result.Id != id)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = context.News.ToList();

            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = context.News.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            context.News.Remove(result);
            context.SaveChanges();

            return new JsonResult(NoContent());
        }
    }
}
