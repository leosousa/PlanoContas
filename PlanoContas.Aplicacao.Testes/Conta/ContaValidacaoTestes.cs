using Microsoft.EntityFrameworkCore;
using Moq;
using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Aplicacao.Conta.Commands.Base;
using PlanoContas.Aplicacao.Conta.Commands.Conta.Create;

namespace PlanoContas.Aplicacao.Testes.Conta
{
    public class ContaValidacaoTestes
    {
        private IApplicationDbContext GetFakeDatabase()
        {
            var data = new List<Dominio.Entidades.Conta> {
            new Dominio.Entidades.Conta
            {
                AceitaLancamentos = true,
                Codigo = "1",
                Id = 1,
                IdContaPai = null,
                Nome = "Teste 1",
                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA,
                Filhos = new List<Dominio.Entidades.Conta> {
                    new Dominio.Entidades.Conta {
                        AceitaLancamentos = true,
                        Codigo = "1.1",
                        Id = 4,
                        IdContaPai = null,
                        Nome = "Teste 1.1",
                        TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
                    }
                }
            },
            new Dominio.Entidades.Conta
            {
                AceitaLancamentos = false,
                Codigo = "2",
                Id = 2,
                IdContaPai = null,
                Nome = "Teste 2",
                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
            }
        }.AsQueryable();

            var mockSet = new Mock<DbSet<Dominio.Entidades.Conta>>();
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.Contas).Returns(mockSet.Object);

            return mockContext.Object;
        }

        [Fact(DisplayName = "Teste de verificação de código único já cadastrado")]
        public void TesteCodigoUnicoInvalido()
        {
            // Arrange
            var codigo = "1";

            // Act

            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.CodigoPrecisaSerUnicoNoCadastro(codigo);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Teste de verificação de código único válido")]
        public void TesteCodigoUnicoValido()
        {
            // Arrange
            var codigo = "3";

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.CodigoPrecisaSerUnicoNoCadastro(codigo);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Teste de verificação de código inválido contendo letras")]
        public void TesteCodigoInvalidoContendoLetras()
        {
            // Arrange
            var codigo = "CodigoInvalido";

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.CodigoPrecisaSerValido(codigo);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Teste de verificação de código válido com 1 nível")]
        public void TesteCodigoValidoCom1Nivel()
        {
            // Arrange
            var codigo = "1";

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.CodigoPrecisaSerValido(codigo);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Teste de verificação de código válido com mais de 1 nível")]
        public void TesteCodigoValidoComMaisDe1Nivel()
        {
            // Arrange
            var codigo = "1.1.1";

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.CodigoPrecisaSerValido(codigo);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Teste de verificação conta pai já cadastrada")]
        public void TesteContaPaiPrecisaEstarCadastradaValida()
        {
            // Arrange
            int? idContaPai = 1;

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.ContaPaiPrecisaEstarCadastrada(idContaPai);

            // Assert
            Assert.True(result);
        }

        [Theory(DisplayName = "Teste de verificação conta pai não cadastrada")]
        [InlineData(null, true)]
        [InlineData(0, false)]
        [InlineData(3, false)]
        public void TesteContaPaiPrecisaEstarCadastradaInvalida(int? idContaPai, bool expectedResult)
        {
            // Arrange

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.ContaPaiPrecisaEstarCadastrada(idContaPai);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact(DisplayName = "Teste de verificação de conta pai que pode ter contas filhas associadas. Uma conta pai só pode possuir contas filhas associadas caso não aceite lançamentos")]
        public void TesteContaPaiQuePodeTerContasFilhasAssociadas()
        {
            // Arrange
            var idContaPai = 2;

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.ContaPaiPodeTerContasFilhasAssociadas(idContaPai);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Teste de verificação de conta pai que não pode ter contas filhas associadas. Uma conta pai só pode possuir contas filhas associadas caso não aceite lançamentos")]
        public void TesteContaPaiQueNaoPodeTerContasFilhasAssociadas()
        {
            // Arrange
            var idContaPai = 1;

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.ContaPaiPodeTerContasFilhasAssociadas(idContaPai);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Teste de conta nova com o mesmo tipo de conta pai")]
        public void TesteContaNovaPrecisaSerDoMesmoTipoQueContaPaiValida()
        {
            // Arrange
            var createContaCommand = new ContaCreateCommand
            {
                AceitaLancamentos = true,
                Codigo = "1.1",
                IdContaPai = 1,
                Nome = "Teste 1.1",
                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
            };

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.ContaNovaPrecisaSerDoMesmoTipoQueContaPai(createContaCommand);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Teste de conta nova com tipo diferente da conta pai")]
        public void TesteContaNovaPrecisaSerDoMesmoTipoQueContaPaiInvalida()
        {
            // Arrange
            var createContaCommand = new ContaCreateCommand
            {
                AceitaLancamentos = true,
                Codigo = "1.1",
                IdContaPai = 1,
                Nome = "Teste 1.1",
                TipoConta = Dominio.Enumeracoes.ETipoConta.DESPESA
            };

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.ContaNovaPrecisaSerDoMesmoTipoQueContaPai(createContaCommand);

            // Assert
            Assert.False(result);
        }

        [Theory(DisplayName = "Teste de conta nova com tipo de conta válido")]
        [InlineData(1)]
        [InlineData(2)]
        public void TesteTipoReceitaValido(int tipoConta)
        {
            // Arrange
            var createContaCommand = new ContaCreateCommand
            {
                AceitaLancamentos = true,
                Codigo = "1.1",
                IdContaPai = 1,
                Nome = "Teste 1.1",
                TipoConta = (Dominio.Enumeracoes.ETipoConta)tipoConta
            };

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.TipoReceitaPrecisaSerValido(createContaCommand);

            // Assert
            Assert.True(result);
        }

        [Theory(DisplayName = "Teste de conta nova com tipo de conta inválido")]
        [InlineData(3)]
        [InlineData(0)]
        public void TesteTipoReceitaInvalido(int tipoConta)
        {
            // Arrange
            var createContaCommand = new ContaCreateCommand
            {
                AceitaLancamentos = true,
                Codigo = "1.1",
                IdContaPai = 1,
                Nome = "Teste 1.1",
                TipoConta = (Dominio.Enumeracoes.ETipoConta)tipoConta
            };

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.TipoReceitaPrecisaSerValido(createContaCommand);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Teste de conta já existente")]
        public void TesteContaJaExistente()
        {
            // Arrange
            var idContaPai = 1;

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.ContaJaExistente(idContaPai);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Teste de conta não existente")]
        public void TesteContaNaoExistente()
        {
            // Arrange
            var idContaPai = 3;

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.ContaJaExistente(idContaPai);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Teste de conta que possui filhos")]
        public void TesteContaPossuiFilhos()
        {
            // Arrange
            var idContaPai = 1;

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.ContaNaoPossuiFilhos(idContaPai);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Teste de conta que não possui filhos")]
        public void TesteContaNaoPossuiFilhos()
        {
            // Arrange
            var idContaPai = 2;

            // Act
            var validator = new ContaBaseValidator(GetFakeDatabase());
            var result = validator.ContaNaoPossuiFilhos(idContaPai);

            // Assert
            Assert.True(result);
        }
    }
}
