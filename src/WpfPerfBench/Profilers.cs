using System.ComponentModel;
using AutoMapper;

namespace WpfPerfBench;

public class Profilers : Profile
{
    public Profilers()
    {
        CreateMap<WpfPerfBench.Data.Models.Category, WpfPerfBench.Data.Entities.Category>().ReverseMap();
        CreateMap<WpfPerfBench.Data.Models.Item, WpfPerfBench.Data.Entities.Item>().ReverseMap();
    }
}