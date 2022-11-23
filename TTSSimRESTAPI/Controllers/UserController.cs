using Microsoft.AspNetCore.Mvc;
using TTSSimRESTAPI.Models;
using TTSSimRESTAPI.Data;

namespace TTSSimRESTAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private APIContext context;
        public UserController(APIContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public JsonResult Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();

            return new JsonResult(Ok(user));
        }

        [HttpPut]
        public JsonResult Edit(User putuser)
        {
            var list = context.Users.ToList();
            var result = new User();

            foreach (User user in list)
            {
                if (user.Token == putuser.Token)
                {
                    result = user;
                }
            }
            if (result.Token != putuser.Token)
            {
                return new JsonResult(NotFound());
            }

            result.Username = putuser.Username;
            result.Token = putuser.Token;
            result.UserType = putuser.UserType;
            result.SaveFile = putuser.SaveFile;

            context.SaveChanges();

            return new JsonResult(Ok(putuser));
        }

        [HttpGet]
        public bool CheckExistingToken(string token)
        {
            var list = context.Users.ToList();
            var result = new User();

            foreach (User user in list)
            {
                if (user.Token == token)
                {
                    result = user;
                }
            }

            if (result.Token != token)
            {
                return false;
            }

            return true;
        }

        [HttpGet]
        public bool CheckAdmin(string token)
        {
            var list = context.Users.ToList();
            var result = new User();

            foreach (User user in list)
            {
                if (user.Token == token)
                {
                    result = user;
                }
            }

            if (result.Token != token || result.UserType != UserType.Admin)
            {
                return false;
            }

            return true;
        }

        [HttpGet]
        public bool CheckTakenName(string name)
        {
            var list = context.Users.ToList();
            var result = new User();

            foreach (User user in list)
            {
                if (user.Username == name)
                {
                    result = user;
                }
            }

            if (result.Username == name)
            {
                return true;
            }

            return false;
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = context.Users.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public JsonResult GetByToken(string token)
        {
            var list = context.Users.ToList();
            var result = new User();

            foreach (User user in list)
            {
                if (user.Token == token)
                {
                    result = user;
                }
            }

            if (result.Token != token)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = context.Users.ToList();

            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = context.Users.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            context.Users.Remove(result);
            context.SaveChanges();

            return new JsonResult(NoContent());
        }
    }
}
