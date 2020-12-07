using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageCloud.Models
{
    public class User : IdentityUser
    {
        public long Storage { get; set; }
        public long UseStorage { get => Folders.Sum(f => f.Size); }
        public List<Folder> Folders { get; set; }

        public User()
        {
            Storage = 5368709120;
            Folders = new List<Folder>() { new Folder { Name = "Pictures", DateOfCreate = DateTime.Now, DateOfChange = DateTime.Now }, new Folder { Name = "Sounds", DateOfCreate = DateTime.Now, DateOfChange = DateTime.Now }, new Folder { Name = "Videos", DateOfCreate = DateTime.Now, DateOfChange = DateTime.Now }, new Folder { Name = "Other", DateOfCreate = DateTime.Now, DateOfChange = DateTime.Now } };
        }
    }
}
