using Microsoft.AspNetCore.Mvc;
using TTSSimRESTAPI.Data;
using TTSSimRESTAPI.ServiceClasses;

namespace TTSSimRESTAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private APIContext context;
        public DataController(APIContext context)
        {
            this.context = context;
        }

        [HttpPatch]
        public JsonResult BuyItems(string saveData, string items)
        {
            string editedSaveData = Shop.BuyItems(saveData, items);

            return new JsonResult(Ok(editedSaveData));
        }

        [HttpPatch]
        public JsonResult SellItems(string saveData, string items)
        {
            string editedSaveData = Shop.SellItems(saveData, items);

            return new JsonResult(Ok(editedSaveData));
        }

        [HttpPatch]
        public JsonResult UpdateParty(string saveData, string party)
        {
            string editedSaveData = PartyMember.UpdateParty(saveData, party);

            return new JsonResult(Ok(editedSaveData));
        }

        [HttpPatch]
        public JsonResult UpdateEquipment(string saveData, string json)
        {
            string editedSaveData = PartyMember.UpdateEquipment(saveData, json);

            return new JsonResult(Ok(editedSaveData));
        }

        [HttpPatch]
        public JsonResult FusePersona(string saveData, string json)
        {
            string editedSaveData = Persona.FusePersona(saveData, json);

            return new JsonResult(Ok(editedSaveData));
        }

        [HttpPatch]
        public JsonResult SacrificePersona(string saveData, string json)
        {
            string editedSaveData = Persona.SacrificePersona(saveData, json);

            return new JsonResult(Ok(editedSaveData));
        }

        [HttpPatch]
        public JsonResult RemoveSkills(string saveData, string json)
        {
            string editedSaveData = Persona.RemoveSkills(saveData, json);

            return new JsonResult(Ok(editedSaveData));
        }
    }
}