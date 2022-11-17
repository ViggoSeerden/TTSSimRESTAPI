using System.ComponentModel.DataAnnotations;

namespace TTSSimRESTAPI.Models
{
    public enum NewsType
    {
        General,
        Announcement,
        Update,
        Event
    }

    public class News
    {
        [Key]
        public int Id { get; set; }
        public NewsType NewsType { get; set; } 
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool Edited { get; set; }
    }
}
