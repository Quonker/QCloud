using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageCloud.Models
{
    public class File
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }
        //public string Path { get; set; }
        public int Size { get => ExtualyFile.Length; }
        public byte[] ExtualyFile { get; set; }
        public string Extension { get; set; }
        public string Icon { get; set; }
        public Folder Folder { get; set; }
        public int? FolderId { get; set; }

    }
}
