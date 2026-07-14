using System.ComponentModel;
using AutoMapper;

namespace WpfPerfBench;

public class Profilers : Profile
{
    public Profilers()
    {
        CreateMap<WpfPerfBench.Data.Models.Category, WpfPerfBench.Data.Entities.Category>()
            .MaxDepth(2)
            .ReverseMap();

        CreateMap<WpfPerfBench.Data.Models.Item, WpfPerfBench.Data.Entities.Item>()
            .MaxDepth(2)
            .ReverseMap();
    }
}