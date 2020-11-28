using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageCloud.ViewModel
{
    public class FolderToolsViewModel
    {
        public int FileId { get; set; }

        public List<FileViewModel> Files { get; set; }
    }
}
