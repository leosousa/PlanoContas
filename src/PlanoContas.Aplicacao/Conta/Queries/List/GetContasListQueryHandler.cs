using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Aplicacao.Conta.Queries.Detail;

namespace PlanoContas.Aplicacao.Conta.Queries.List
{
    public class GetContasListQueryHandler : IRequestHandler<GetContasListQuery, ContaListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetContasListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ContaListViewModel> Handle(GetContasListQuery request, CancellationToken cancellationToken)
        {
            return new ContaListViewModel
            {
                List = await _context.Contas
                    .AsNoTracking()
                    .ProjectTo<ContaItemViewModel>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Id)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
