using System;
using System.Collections.Generic;

namespace ToolUniversity.Models.db;

public partial class ReturnInfor
{
    public int ReturnId { get; set; }

    public int ToolId { get; set; }

    public int UserId { get; set; }

    public DateTime DateReturn { get; set; }

    public virtual ICollection<FeeInfor> FeeInfors { get; set; } = new List<FeeInfor>();

    public virtual ToolInfor Tool { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
