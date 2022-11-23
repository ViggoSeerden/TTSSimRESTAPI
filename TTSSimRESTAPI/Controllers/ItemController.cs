using Microsoft.AspNetCore.Mvc;
using TTSSimRESTAPI.Models;
using TTSSimRESTAPI.Data;

namespace TTSSimRESTAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private APIContext context;
        public ItemController(APIContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public JsonResult Add(Item item)
        {
            context.Items.Add(item);
            context.SaveChanges();

            return new JsonResult(Ok(item));
        }

        [HttpPut]
        public JsonResult Edit(Item putitem)
        {
            var list = context.Items.ToList();
            var result = new Item();

            foreach (Item item in list)
            {
                if (item.Id == putitem.Id)
                {
                    result = item;
                }
            }
            if (result.Id != putitem.Id)
            {
                return new JsonResult(NotFound());
            }

            result.Name = putitem.Name;
            result.ItemType = putitem.ItemType;
            result.Description = putitem.Description;
            result.Price = putitem.Price;

            context.SaveChanges();

            return new JsonResult(Ok(putitem));
        }

        [HttpGet]
        public bool CheckExistingToken(int id)
        {
            var list = context.Items.ToList();
            var result = new Item();

            foreach (Item item in list)
            {
                if (item.Id == id)
                {
                    result = item;
                }
            }

            if (result.Id != id)
            {
                return false;
            }

            return true;
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = context.Items.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = context.Items.ToList();

            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = context.Items.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            context.Items.Remove(result);
            context.SaveChanges();

            return new JsonResult(NoContent());
        }
    }
}
