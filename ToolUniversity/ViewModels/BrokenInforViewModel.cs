using System.ComponentModel.DataAnnotations;

namespace ToolUniversity.ViewModels
{
    public class BrokenInforViewModel
    {
        public int BrokenId { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public int ToolId { get; set; }
        public string? ToolName { get; set; }
        public string? Reason { get; set; }
        public int OfficerId { get; set; }
        public string? OfficerName { get;set; }
    }
}
