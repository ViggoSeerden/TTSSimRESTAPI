using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TTSSimRESTAPI.GameClasses;
using TTSSimRESTAPI.Models;
using TTSSimRESTAPI.ServiceClasses;

namespace TTSSim.Test
{
    [TestClass]
    public class UnitShopTests
    {
        [TestMethod]
        public void Test_BuyOwnedItem()
        {
            PlayerData playerData = new PlayerData("Tester");

            ItemData itemData = new ItemData()
            { 
                name = "Test",
                amount = 1
            };

            GameData gameData = new GameData()
            { 
                playerData = playerData,
                itemData = new List<ItemData> {itemData}
            };

            JsonData jsonData = new JsonData()
            { 
                itemName = "Test",
                amount = 1,
                price = 100
            };
            string gamedatastring = JsonConvert.SerializeObject(gameData);
            string jsonstring = JsonConvert.SerializeObject(jsonData);

            string editedsavedata = Shop.BuyItems(gamedatastring, jsonstring);
            GameData? newData = JsonConvert.DeserializeObject<GameData>(editedsavedata);

            Assert.AreEqual(900, newData.playerData.yen);
            Assert.AreEqual(2, newData.itemData[0].amount);
            Assert.IsTrue(newData.itemData.Count == 1);
        }
        
        [TestMethod]
        public void Test_BuyNewItem()
        {
            PlayerData playerData = new PlayerData("Tester");

            GameData gameData = new GameData()
            { 
                playerData = playerData,
                itemData = new List<ItemData>()
            };

            JsonData jsonData = new JsonData()
            { 
                itemName = "Test",
                amount = 1,
                price = 100
            };
            string gamedatastring = JsonConvert.SerializeObject(gameData);
            string jsonstring = JsonConvert.SerializeObject(jsonData);

            string editedsavedata = Shop.BuyItems(gamedatastring, jsonstring);
            GameData? newData = JsonConvert.DeserializeObject<GameData>(editedsavedata);

            Assert.AreEqual(900, newData.playerData.yen);
            Assert.AreEqual(1, newData.itemData[0].amount);
            Assert.IsTrue(newData.itemData.Count == 1);
        }

        [TestMethod]
        public void Test_SellItem()
        {
            PlayerData playerData = new PlayerData("Tester");

            ItemData itemData = new ItemData()
            {
                name = "Test",
                amount = 2
            };

            GameData gameData = new GameData()
            {
                playerData = playerData,
                itemData = new List<ItemData> { itemData }
            };

            JsonData jsonData = new JsonData()
            {
                itemName = "Test",
                amount = 1,
                price = 100
            };
            string gamedatastring = JsonConvert.SerializeObject(gameData);
            string jsonstring = JsonConvert.SerializeObject(jsonData);

            string editedsavedata = Shop.SellItems(gamedatastring, jsonstring);
            GameData? newData = JsonConvert.DeserializeObject<GameData>(editedsavedata);

            Assert.AreEqual(1100, newData.playerData.yen);
            Assert.AreEqual(1, newData.itemData[0].amount);
            Assert.IsTrue(newData.itemData.Count == 1);
        }

        [TestMethod]
        public void Test_SellFinalItem()
        {
            PlayerData playerData = new PlayerData("Tester");

            ItemData itemData = new ItemData()
            {
                name = "Test",
                amount = 2
            };

            GameData gameData = new GameData()
            {
                playerData = playerData,
                itemData = new List<ItemData> { itemData }
            };

            JsonData jsonData = new JsonData()
            {
                itemName = "Test",
                amount = 2,
                price = 100
            };
            string gamedatastring = JsonConvert.SerializeObject(gameData);
            string jsonstring = JsonConvert.SerializeObject(jsonData);

            string editedsavedata = Shop.SellItems(gamedatastring, jsonstring);
            GameData? newData = JsonConvert.DeserializeObject<GameData>(editedsavedata);

            Assert.AreEqual(1200, newData.playerData.yen);
            Assert.IsTrue(newData.itemData.Count == 0);
        }
    }
}