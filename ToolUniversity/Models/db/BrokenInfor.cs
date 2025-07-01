using System;
using System.Collections.Generic;

namespace ToolUniversity.Models.db;

public partial class BrokenInfor
{
    public int BrokenId { get; set; }

    public int UserId { get; set; }

    public int ToolId { get; set; }

    public string Reason { get; set; } = null!;

    public int OfficerId { get; set; }

    public virtual OfficerInfor Officer { get; set; } = null!;

    public virtual ToolInfor Tool { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
