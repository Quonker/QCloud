using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageCloud.Models
{
    public class Folder
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreate { get; set; }
        public DateTime DateOfChange { get; set; }
        //public string Path { get; set; }
        public long Size { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        public List<File> Files { get; set; }

        public Folder()
        {
            Size = 0;
            Files = new List<File>();
        }
    }
}
