using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebImageCloud.Models.TypesFiles;

namespace WebImageCloud.Models
{
    public class Picture : IFile
    {
        public int Id { get; set; }
     
        public DateTime ImageDate { get; set; }
        public EnumImageTypes Type { get; set; }
    }
}
