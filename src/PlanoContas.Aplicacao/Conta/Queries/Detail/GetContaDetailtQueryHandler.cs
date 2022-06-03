using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlanoContas.Aplicacao.Base.Interfaces;

namespace PlanoContas.Aplicacao.Conta.Queries.Detail
{
    public class GetContaDetailtQueryHandler : IRequestHandler<GetContaDetailQuery, ContaDetailViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetContaDetailtQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ContaDetailViewModel> Handle(GetContaDetailQuery request, CancellationToken cancellationToken)
        {
            return await _context.Contas
                    .AsNoTracking()
                    .Include(c => c.Filhos)
                    .Where(x => x.Id == request.Id)
                    .ProjectTo<ContaDetailViewModel>(_mapper.ConfigurationProvider)
                    .FirstAsync(cancellationToken);
        }
    }
}
