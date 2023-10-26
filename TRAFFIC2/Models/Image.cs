using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRAFFIC2.Models;

public partial class Image
{
    public decimal ImageId { get; set; }

    public decimal? HomePageId { get; set; }

    [NotMapped]
    public virtual IFormFile ImageFile1 { get; set; }
    public string? ImagePath1 { get; set; }
    [NotMapped]
    public virtual IFormFile ImageFile2 { get; set; }
    public string? ImagePath2 { get; set; }
    [NotMapped]
    public virtual IFormFile ImageFile3 { get; set; }
    public string? ImagePath3 { get; set; }
    [NotMapped]
    public virtual IFormFile ImageFile4 { get; set; }
    public string? ImagePath4 { get; set; }

    public virtual ICollection<HomePage> HomePages { get; set; } = new List<HomePage>();
}
