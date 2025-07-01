using System;
using System.Collections.Generic;

namespace ToolUniversity.Models.db;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Faculty { get; set; }

    public string? Major { get; set; }

    public virtual ICollection<BrokenInfor> BrokenInfors { get; set; } = new List<BrokenInfor>();

    public virtual ICollection<FeeInfor> FeeInfors { get; set; } = new List<FeeInfor>();

    public virtual ICollection<LendInfor> LendInfors { get; set; } = new List<LendInfor>();

    public virtual ICollection<ReturnInfor> ReturnInfors { get; set; } = new List<ReturnInfor>();
}
