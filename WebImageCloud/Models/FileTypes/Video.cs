using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebImageCloud.Models.TypesFiles;

namespace WebImageCloud.Models
{
    public class Video : IFile
    {
        public int Id { get; set; }
        public int Lenght { get; set; }
        public int FrameRate { get; set; }
        public DateTime VideoDate { get; set; }
        public EnumVideoTypes Type { get; set; }
    }
}
