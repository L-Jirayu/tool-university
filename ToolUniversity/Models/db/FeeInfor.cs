using System;
using System.Collections.Generic;

namespace ToolUniversity.Models.db;

public partial class FeeInfor
{
    public int FeeId { get; set; }

    public int UserId { get; set; }

    public int LendId { get; set; }

    public int ReturnId { get; set; }

    public int Fee { get; set; }

    public virtual LendInfor Lend { get; set; } = null!;

    public virtual ReturnInfor Return { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
