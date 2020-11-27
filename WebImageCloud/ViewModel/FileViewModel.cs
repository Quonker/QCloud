using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageCloud.ViewModel
{
    public class FileViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string Extension { get; set; }
        public FolderViewModel Folder { get; set; }
        public int? FolderId { get; set; }
    }
}
