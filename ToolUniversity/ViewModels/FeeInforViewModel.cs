namespace ToolUniversity.ViewModels
{
    public class FeeInforViewModel
    {
        public int FeeId { get; set; }

        public int UserId { get; set; }
        public string? UserName { get; set; }

        public int LendId { get; set; }
        public int ReturnId { get; set; }

        public int Fee { get; set; }
    }
}
