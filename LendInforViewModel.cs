namespace ToolUniversity.ViewModels

public class LendInforViewModel
{
    public int LendId { get; set; }

    public int ToolId { get; set; }
    public string ToolName { get; set; }

    public int UserId { get; set; }
    public string FullName { get; set; }

    public DateTime DateLend { get; set; }
}

