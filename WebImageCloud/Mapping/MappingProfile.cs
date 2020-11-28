using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebImageCloud.Models;
using WebImageCloud.ViewModel;

namespace WebImageCloud.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<File, FileViewModel>()
                .ForMember(dest =>
                dest.Size,
                opt => opt.MapFrom(src => src.ExtualyFile.Length));
            CreateMap<Folder, FolderViewModel>();

            CreateMap<FileViewModel, File>();
            CreateMap<FolderViewModel, Folder>();

            CreateMap<IEnumerable<File>, IEnumerable<FileViewModel>>();

            CreateMap<IEnumerable<Folder>, IEnumerable<FolderViewModel>>();

            CreateMap<IQueryable<File>, IQueryable<FileViewModel>>();
            CreateMap<IQueryable<Folder>, IQueryable<FolderViewModel>>();

            CreateMap<IEnumerable<FileViewModel>, IEnumerable<File>>();
            CreateMap<IEnumerable<FolderViewModel>, IEnumerable<Folder>>();
        }
       
    }
}
