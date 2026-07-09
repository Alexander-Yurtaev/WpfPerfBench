using System.ComponentModel;
using AutoMapper;

namespace WpfPrefBench;

public class Profilers : Profile
{
    public Profilers()
    {
        CreateMap<WpfPrefBench.Data.Models.Category, WpfPrefBench.Data.Entities.Category>().ReverseMap();
        CreateMap<WpfPrefBench.Data.Models.Item, WpfPrefBench.Data.Entities.Item>().ReverseMap();
    }
}