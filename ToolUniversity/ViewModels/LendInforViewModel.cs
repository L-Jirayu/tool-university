using System.ComponentModel.DataAnnotations;

namespace ToolUniversity.ViewModels
{
    public class LendInforViewModel
    {
        public int LendId { get; set; }
        public int ToolId { get; set; }
        public string? ToolName { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateLend { get; set; }
    }
}
