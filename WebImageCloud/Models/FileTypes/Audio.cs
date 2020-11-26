using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebImageCloud.Models.TypesFiles;

namespace WebImageCloud.Models
{
    public class Audio : IFile
    {
        public int Id { get; set; }
        public int Lenght { get; set; }
        public DateTime VideoDate { get; set; }
        public EnumAudioTypes Type { get; set; }
    }
}
