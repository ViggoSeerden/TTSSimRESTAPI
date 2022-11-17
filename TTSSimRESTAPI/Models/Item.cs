using System.ComponentModel.DataAnnotations;

namespace TTSSimRESTAPI.Models
{
    public enum ItemType
    {
        HP,
        SP,
        Ailment,
        Revive,
        Support,
        Weapon,
        Armor,
        Accessory
    }

    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ItemType ItemType { get; set; }
        public int Price { get; set; }
        public int? Stock { get; set; }
    }
}
