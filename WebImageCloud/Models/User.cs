using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageCloud.Models
{
    public class User : IdentityUser
    {
        public List<Folder> Folders { get; set; }

        public User()
        {
            Folders = new List<Folder>() { new Folder { Name = "Pictures", DateOfCreate = DateTime.Now, DateOfChange = DateTime.Now }, new Folder { Name = "Sounds", DateOfCreate = DateTime.Now, DateOfChange = DateTime.Now }, new Folder { Name = "Videos", DateOfCreate = DateTime.Now, DateOfChange = DateTime.Now } };
        }
    }
}
