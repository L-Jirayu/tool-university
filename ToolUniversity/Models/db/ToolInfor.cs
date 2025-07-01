using System;
using System.Collections.Generic;

namespace ToolUniversity.Models.db;

public partial class ToolInfor
{
    public int ToolId { get; set; }

    public string ToolName { get; set; } = null!;

    public string ToolType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<BrokenInfor> BrokenInfors { get; set; } = new List<BrokenInfor>();

    public virtual ICollection<LendInfor> LendInfors { get; set; } = new List<LendInfor>();

    public virtual ICollection<ReturnInfor> ReturnInfors { get; set; } = new List<ReturnInfor>();
}
