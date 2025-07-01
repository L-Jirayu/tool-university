using System;
using System.Collections.Generic;

namespace ToolUniversity.Models.db;

public partial class OfficerInfor
{
    public int OfficerId { get; set; }

    public string Name { get; set; } = null!;

    public string Position { get; set; } = null!;

    public string Department { get; set; } = null!;

    public virtual ICollection<BrokenInfor> BrokenInfors { get; set; } = new List<BrokenInfor>();
}
