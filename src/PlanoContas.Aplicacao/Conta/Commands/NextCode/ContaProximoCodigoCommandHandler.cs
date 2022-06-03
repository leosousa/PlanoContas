using MediatR;
using Microsoft.EntityFrameworkCore;
using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Aplicacao.Base.Results;

namespace PlanoContas.Aplicacao.Conta.Commands.NextCode
{
    public class ContaProximoCodigoCommandHandler : IRequestHandler<ContaProximoCodigoCommand, NextCodeViewModel>
    {
        private readonly IApplicationDbContext _context;
        private const int MIN_CODE = 1;
        private const int MAX_CODE_SUBLEVEL = 1000;

        public ContaProximoCodigoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<NextCodeViewModel> Handle(ContaProximoCodigoCommand request, CancellationToken cancellationToken)
        {
            if (request.CodigoPai is null)
            {
                request.CodigoPai = "";
            }

            if (_context.Contas == null || !_context.Contas.Any())
            {
                return new NextCodeViewModel
                {
                    Result = Result.Success(),
                    ProximoCodigo = "1"
                };
            }

            var contasCadastradas = _context.Contas.ToList();
            string proximoCodigo = null;
            int contadorInicial = 0;

            if (!string.IsNullOrWhiteSpace(request.CodigoPai) && !contasCadastradas.Any(c => c.Codigo == request.CodigoPai))
            {
                return new NextCodeViewModel
                {
                    Result = Result.Failure(new string[] { "Código pai inválido." }),
                };
            }

            do
            {
                // Contador de numeração do código
                contadorInicial++;

                // Armazena partes do código pai em formato inteiro
                var partesCodigoPaiConvertidas = new List<int>();

                // Validação das partes do código pai como sendo inteiro
                int auxiliarConversao = int.MinValue;
                var partesCodigoPai = request.CodigoPai!.Split(".").ToList();
                foreach (var parteCodigoPai in partesCodigoPai)
                {
                    if (int.TryParse(parteCodigoPai, out auxiliarConversao))
                    {
                        partesCodigoPaiConvertidas.Add(auxiliarConversao);
                    }
                    else if (string.IsNullOrEmpty(parteCodigoPai))
                    {
                        auxiliarConversao = contadorInicial;
                        partesCodigoPaiConvertidas.Add(auxiliarConversao);
                    }
                    else
                    {
                        return new NextCodeViewModel
                        {
                            Result = Result.Failure(new string[] { "Código pai inválido." }),
                        };
                    }
                }

                if (contasCadastradas == null || !contasCadastradas.Any())
                {
                    return new NextCodeViewModel
                    {
                        Result = Result.Success(),
                        ProximoCodigo = "1"
                    };
                }

                var atualizarProximoSubnivel = false;

                atualizarProximoSubnivel = (!string.IsNullOrWhiteSpace(request.CodigoPai));
                if (atualizarProximoSubnivel)
                {
                    partesCodigoPaiConvertidas.Insert(partesCodigoPaiConvertidas.Count, contadorInicial);
                }

                if (contadorInicial == MAX_CODE_SUBLEVEL)
                {
                    for (var indice = 0; indice < partesCodigoPaiConvertidas.Count; indice++)
                    {
                        if (partesCodigoPaiConvertidas.Count == 1)
                        {
                            partesCodigoPaiConvertidas.Insert(0, 1);
                            partesCodigoPaiConvertidas[1] = 0;
                            break;
                        }
                        else
                        {
                            partesCodigoPaiConvertidas[indice] = partesCodigoPaiConvertidas[indice] + 1;
                            partesCodigoPaiConvertidas[indice + 1] = 0;
                            break;
                        }
                    }
                }

                if (partesCodigoPaiConvertidas.LastOrDefault() == 0)
                {
                    partesCodigoPaiConvertidas.RemoveAt(partesCodigoPaiConvertidas.Count - 1);
                }

                proximoCodigo = string.Join(".", partesCodigoPaiConvertidas.ToList());
            }
            while (contasCadastradas.Any(c => c.Codigo == proximoCodigo));
            
            return new NextCodeViewModel
            {
                Result = Result.Success(),
                ProximoCodigo = proximoCodigo
            };
        }
    }
}
