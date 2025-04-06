using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Entities.Trilhar;
using TRILHAR.Business.Entities.Trilhar.ViewModel;
using TRILHAR.Business.IO.Turma;

namespace TRILHAR.Business.AutoMapper
{
    public class TurmaMapper : Profile
    {
        public TurmaMapper()
        {
            CreateMap<TurmaEntity, TurmaOutput>()
                .ReverseMap();

        }
    }
}
