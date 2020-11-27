using System;
using System.Collections.Generic;
using WebImageCloud.Models;

namespace WebImageCloud.ViewModel
{
    public class FolderViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreate { get; set; }
        public DateTime DateOfChange { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

        public List<FileViewModel> Files { get; set; }

        public FolderViewModel()
        {
            Files = new List<FileViewModel>();
        }
    }
}