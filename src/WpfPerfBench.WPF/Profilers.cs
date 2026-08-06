using AutoMapper;

namespace WpfPerfBench.WPF;

public class Profilers : Profile
{
    public Profilers()
    {
        CreateMap<Data.Models.Category, Data.Entities.Category>()
            .MaxDepth(2)
            .ReverseMap();

        CreateMap<Data.Models.Item, Data.Entities.Item>()
            .MaxDepth(2)
            .ReverseMap();
    }
}