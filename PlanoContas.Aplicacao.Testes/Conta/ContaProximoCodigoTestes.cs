using Microsoft.EntityFrameworkCore;
using Moq;
using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Aplicacao.Conta.Commands.NextCode;

namespace PlanoContas.Aplicacao.Testes.Conta
{
    public class ContaProximoCodigoTestes
    {
        private IApplicationDbContext GetDatabaseEmpty()
        {
            var data = new List<Dominio.Entidades.Conta>
            {
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

        private IApplicationDbContext GetDatabaseWithOneFatherNodeRegister()
        {
            var data = new List<Dominio.Entidades.Conta> {
                new Dominio.Entidades.Conta
                {
                    AceitaLancamentos = true,
                    Codigo = "1",
                    Id = 1,
                    IdContaPai = null,
                    Nome = "Teste",
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

        private IApplicationDbContext GetDatabaseWithOneFatherAndChildNodeRegister()
        {
            var data = new List<Dominio.Entidades.Conta> {
                new Dominio.Entidades.Conta
                {
                    AceitaLancamentos = true,
                    Codigo = "1",
                    Id = 1,
                    IdContaPai = null,
                    Nome = "Teste",
                    TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
                },
                new Dominio.Entidades.Conta
                {
                    AceitaLancamentos = true,
                    Codigo = "1.1",
                    Id = 1,
                    IdContaPai = null,
                    Nome = "Teste Filho",
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

        private IApplicationDbContext GetDatabaseWithLastChildRegisterIs9()
        {
            var listaDados = new List<Dominio.Entidades.Conta>()
            {
                new Dominio.Entidades.Conta
                {
                    AceitaLancamentos = true,
                    Codigo = "1",
                    Id = 1,
                    IdContaPai = null,
                    Nome = "Teste",
                    TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
                }
            };

            for (var indice = 1; indice < 10; indice++)
            {
                listaDados.Add(new Dominio.Entidades.Conta
                {
                    AceitaLancamentos = true,
                    Codigo = "1." + indice.ToString(),
                    Id = 1,
                    IdContaPai = null,
                    Nome = "Teste Filho " + indice.ToString(),
                    TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
                });
            }

            var data = listaDados.AsQueryable();

            var mockSet = new Mock<DbSet<Dominio.Entidades.Conta>>();
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.Contas).Returns(mockSet.Object);

            return mockContext.Object;
        }

        private IApplicationDbContext GetDatabaseWithLastChildRegisterIs99()
        {
            var listaDados = new List<Dominio.Entidades.Conta>()
            {
                new Dominio.Entidades.Conta
                {
                    AceitaLancamentos = true,
                    Codigo = "1",
                    Id = 1,
                    IdContaPai = null,
                    Nome = "Teste",
                    TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
                }
            };

            for (var indice = 1; indice < 100; indice++)
            {
                listaDados.Add(new Dominio.Entidades.Conta
                {
                    AceitaLancamentos = true,
                    Codigo = "1." + indice.ToString(),
                    Id = 1,
                    IdContaPai = null,
                    Nome = "Teste Filho " + indice.ToString(),
                    TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
                });
            }

            var data = listaDados.AsQueryable();

            var mockSet = new Mock<DbSet<Dominio.Entidades.Conta>>();
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.Contas).Returns(mockSet.Object);

            return mockContext.Object;
        }

        private IApplicationDbContext GetDatabaseWithLastChildRegisterIs999()
        {
            var listaDados = new List<Dominio.Entidades.Conta>()
            {
                new Dominio.Entidades.Conta
                {
                    AceitaLancamentos = true,
                    Codigo = "1",
                    Id = 1,
                    IdContaPai = null,
                    Nome = "Teste",
                    TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
                }
            };

            for (var indice = 1; indice < 1000; indice++)
            {
                listaDados.Add(new Dominio.Entidades.Conta
                {
                    AceitaLancamentos = true,
                    Codigo = "1." + indice.ToString(),
                    Id = 1,
                    IdContaPai = null,
                    Nome = "Teste Filho " + indice.ToString(),
                    TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
                });
            }

            var data = listaDados.AsQueryable();

            var mockSet = new Mock<DbSet<Dominio.Entidades.Conta>>();
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.Contas).Returns(mockSet.Object);

            return mockContext.Object;
        }

        private IApplicationDbContext GetDatabaseWithLastChildRegisterIs9_9_999_999_999()
        {
            var listaDados = new List<Dominio.Entidades.Conta>();

            // Nós principais
            for (var indice = 1; indice < 10; indice++)
            {
                listaDados.Add(new Dominio.Entidades.Conta
                {
                    AceitaLancamentos = true,
                    Codigo = indice.ToString(),
                    Id = indice,
                    IdContaPai = null,
                    Nome = "Teste " + indice.ToString(),
                    TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
                });
            }

            // Nós de segundo nível
            for (var indiceNivel1 = 1; indiceNivel1 < 10; indiceNivel1++)
            {
                for (var indiceNivel2 = 1; indiceNivel2 < 10; indiceNivel2++)
                {
                    listaDados.Add(new Dominio.Entidades.Conta
                    {
                        AceitaLancamentos = true,
                        Codigo = indiceNivel1.ToString() + "." + indiceNivel2.ToString(),
                        Id = 1,
                        IdContaPai = null,
                        Nome = "Teste Nível " + indiceNivel1.ToString() + "." + indiceNivel2.ToString(),
                        TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
                    });
                }
            }

            // Nós de terceiro nível
            for (var indiceNivel1 = 1; indiceNivel1 < 10; indiceNivel1++)
            {
                for (var indiceNivel2 = 1; indiceNivel2 < 10; indiceNivel2++)
                {
                    for (var indiceNivel3 = 1; indiceNivel3 < 10; indiceNivel3++)
                    {
                        listaDados.Add(new Dominio.Entidades.Conta
                        {
                            AceitaLancamentos = true,
                            Codigo = indiceNivel1.ToString() + "." + indiceNivel2.ToString() + "." + indiceNivel3.ToString(),
                            Id = 1,
                            IdContaPai = null,
                            Nome = "Teste Nível " + indiceNivel1.ToString() + "." + indiceNivel2.ToString() + "." + indiceNivel3.ToString(),
                            TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
                        });
                    }
                }
            }

            listaDados.Add(new Dominio.Entidades.Conta
            {
                AceitaLancamentos = true,
                Codigo = "9.9.999.999.998",
                Id = 1,
                IdContaPai = null,
                Nome = "Teste 9.9.999.999.998",
                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
            });
            listaDados.Add(new Dominio.Entidades.Conta
            {
                AceitaLancamentos = true,
                Codigo = "9.9.999.999.999",
                Id = 1,
                IdContaPai = null,
                Nome = "Teste 9.9.999.999.999",
                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
            });
            listaDados.Add(new Dominio.Entidades.Conta
            {
                AceitaLancamentos = true,
                Codigo = "9.10",
                Id = 1,
                IdContaPai = null,
                Nome = "Teste 9.10",
                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
            });

            //// Nós de quarto nível
            //for (var indiceNivel1 = 1; indiceNivel1 < 10; indiceNivel1++)
            //{
            //    for (var indiceNivel2 = 1; indiceNivel2 < 10; indiceNivel2++)
            //    {
            //        for (var indiceNivel3 = 1; indiceNivel3 < 1000; indiceNivel3++)
            //        {
            //            for (var indiceNivel4 = 1; indiceNivel4 < 998; indiceNivel4++)
            //            {
            //                listaDados.Add(new Dominio.Entidades.Conta
            //                {
            //                    AceitaLancamentos = true,
            //                    Codigo = indiceNivel1.ToString() + "." + indiceNivel2.ToString() + "." + indiceNivel3.ToString() + "." + indiceNivel4.ToString(),
            //                    Id = 1,
            //                    IdContaPai = null,
            //                    Nome = "Teste Nível " + indiceNivel1.ToString() + "." + indiceNivel2.ToString() + "." + indiceNivel3.ToString() + "." + indiceNivel4.ToString(),
            //                    TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
            //                });
            //            }
            //        }
            //    }
            //}

            //// Nós de quinto nível
            //for (var indiceNivel1 = 1; indiceNivel1 < 10; indiceNivel1++)
            //{
            //    for (var indiceNivel2 = 1; indiceNivel2 < 10; indiceNivel2++)
            //    {
            //        for (var indiceNivel3 = 1; indiceNivel3 < 1000; indiceNivel3++)
            //        {
            //            for (var indiceNivel4 = 1; indiceNivel4 < 1000; indiceNivel4++)
            //            {
            //                for (var indiceNivel5 = 1; indiceNivel5 < 998; indiceNivel5++)
            //                {
            //                    listaDados.Add(new Dominio.Entidades.Conta
            //                    {
            //                        AceitaLancamentos = true,
            //                        Codigo = indiceNivel1.ToString() + "." + indiceNivel2.ToString() + "." + indiceNivel3.ToString() + "." + indiceNivel4.ToString() + "." + indiceNivel5.ToString(),
            //                        Id = 1,
            //                        IdContaPai = null,
            //                        Nome = "Teste Nível " + indiceNivel1.ToString() + "." + indiceNivel2.ToString() + "." + indiceNivel3.ToString() + "." + indiceNivel4.ToString() + "." + indiceNivel5.ToString(),
            //                        TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
            //                    });
            //                }
            //            }
            //        }
            //    }
            //}

            var data = listaDados.AsQueryable();

            var mockSet = new Mock<DbSet<Dominio.Entidades.Conta>>();
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.Contas).Returns(mockSet.Object);

            return mockContext.Object;
        }

        private IApplicationDbContext GetDatabaseWithFatherRegister()
        {
            var listaDados = new List<Dominio.Entidades.Conta>();

            listaDados.Add(new Dominio.Entidades.Conta
            {
                AceitaLancamentos = true,
                Codigo = "1",
                Id = 1,
                IdContaPai = null,
                Nome = "Teste 1",
                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
            });
            listaDados.Add(new Dominio.Entidades.Conta
            {
                AceitaLancamentos = true,
                Codigo = "2",
                Id = 2,
                IdContaPai = null,
                Nome = "Teste 2",
                TipoConta = Dominio.Enumeracoes.ETipoConta.RECEITA
            });

            var data = listaDados.AsQueryable();

            var mockSet = new Mock<DbSet<Dominio.Entidades.Conta>>();
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Dominio.Entidades.Conta>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.Contas).Returns(mockSet.Object);

            return mockContext.Object;
        }


        [Fact(DisplayName = "Obter próximo código de contas quando o banco estiver vazio")]
        public async Task ValidaProximoCodigoQuandoCodigoPaiForVazio()
        {
            //Arrange
            var contaPaiVazia = new ContaProximoCodigoCommand { CodigoPai = "" };
            var cancellationToken = new CancellationToken();

            //Act
            var handler = new ContaProximoCodigoCommandHandler(GetDatabaseEmpty());
            var result = await handler.Handle(contaPaiVazia, cancellationToken);

            //Assert
            Assert.NotNull(result.Result);
            Assert.True(result.Result!.Succeeded);
            Assert.Equal("1", result.ProximoCodigo);
        }

        [Fact(DisplayName = "Obter próximo código de contas quando o banco estiver pelo menos 1 registro")]
        public async Task ValidaProximoCodigoQuandoCodigoPaiForVazioEBancoJaTiver1No()
        {
            //Arrange
            var contaPaiVazia = new ContaProximoCodigoCommand { CodigoPai = "" };
            var cancellationToken = new CancellationToken();

            //Act
            var handler = new ContaProximoCodigoCommandHandler(GetDatabaseWithOneFatherNodeRegister());
            var result = await handler.Handle(contaPaiVazia, cancellationToken);

            //Assert
            Assert.NotNull(result.Result);
            Assert.True(result.Result!.Succeeded);
            Assert.Equal("2", result.ProximoCodigo);
        }

        [Fact(DisplayName = "Obter próximo código filho quando o banco estiver pelo menos 1 registro")]
        public async Task ValidaProximoCodigoQuandoCodigoPaiFor1EBancoJaTiver1NoPai()
        {
            //Arrange
            var contaPaiVazia = new ContaProximoCodigoCommand { CodigoPai = "1" };
            var cancellationToken = new CancellationToken();

            //Act
            var handler = new ContaProximoCodigoCommandHandler(GetDatabaseWithOneFatherNodeRegister());
            var result = await handler.Handle(contaPaiVazia, cancellationToken);

            //Assert
            Assert.NotNull(result.Result);
            Assert.True(result.Result!.Succeeded);
            Assert.Equal("1.1", result.ProximoCodigo);
        }

        [Fact(DisplayName = "Obter próximo código filho quando o banco estiver pelo menos 1 registro")]
        public async Task ValidaProximoCodigoQuandoCodigoPaiFor1EBancoJaTiver1NoFilho()
        {
            //Arrange
            var contaPaiVazia = new ContaProximoCodigoCommand { CodigoPai = "1" };
            var cancellationToken = new CancellationToken();

            //Act
            var handler = new ContaProximoCodigoCommandHandler(GetDatabaseWithOneFatherAndChildNodeRegister());
            var result = await handler.Handle(contaPaiVazia, cancellationToken);

            //Assert
            Assert.NotNull(result.Result);
            Assert.True(result.Result!.Succeeded);
            Assert.Equal("1.2", result.ProximoCodigo);
        }

        [Fact(DisplayName = "Obter próximo código quando o último código filho for 9")]
        public async Task ValidaProximoCodigoQuandoUltimoCodigoFilho9()
        {
            //Arrange
            var contaPaiVazia = new ContaProximoCodigoCommand { CodigoPai = "1" };
            var cancellationToken = new CancellationToken();

            //Act
            var handler = new ContaProximoCodigoCommandHandler(GetDatabaseWithLastChildRegisterIs9());
            var result = await handler.Handle(contaPaiVazia, cancellationToken);

            //Assert
            Assert.NotNull(result.Result);
            Assert.True(result.Result!.Succeeded);
            Assert.Equal("1.10", result.ProximoCodigo);
        }

        [Fact(DisplayName = "Obter próximo código quando o último código filho for 99")]
        public async Task ValidaProximoCodigoQuandoUltimoCodigoFilho99()
        {
            //Arrange
            var contaPaiVazia = new ContaProximoCodigoCommand { CodigoPai = "1" };
            var cancellationToken = new CancellationToken();

            //Act
            var handler = new ContaProximoCodigoCommandHandler(GetDatabaseWithLastChildRegisterIs99());
            var result = await handler.Handle(contaPaiVazia, cancellationToken);

            //Assert
            Assert.NotNull(result.Result);
            Assert.True(result.Result!.Succeeded);
            Assert.Equal("1.100", result.ProximoCodigo);
        }

        [Fact(DisplayName = "Obter próximo código quando o último código filho for 999")]
        public async Task ValidaProximoCodigoQuandoUltimoCodigoFilho999()
        {
            //Arrange
            var contaPaiVazia = new ContaProximoCodigoCommand { CodigoPai = "1" };
            var cancellationToken = new CancellationToken();

            //Act
            var handler = new ContaProximoCodigoCommandHandler(GetDatabaseWithLastChildRegisterIs999());
            var result = await handler.Handle(contaPaiVazia, cancellationToken);

            //Assert
            Assert.NotNull(result.Result);
            Assert.True(result.Result!.Succeeded);
            Assert.Equal("2", result.ProximoCodigo);
        }

        [Fact(DisplayName = "Obter próximo código quando o último código filho for 9.9.999.999.999")]
        public async Task ValidaProximoCodigoQuandoUltimoCodigoFilho9_9_999_999_999()
        {
            //Arrange
            var contaPaiVazia = new ContaProximoCodigoCommand { CodigoPai = "9" };
            var cancellationToken = new CancellationToken();

            //Act
            var handler = new ContaProximoCodigoCommandHandler(GetDatabaseWithLastChildRegisterIs9_9_999_999_999());
            var result = await handler.Handle(contaPaiVazia, cancellationToken);

            //Assert
            Assert.NotNull(result.Result);
            Assert.True(result.Result!.Succeeded);
            Assert.Equal("9.11", result.ProximoCodigo);
        }

        [Fact(DisplayName = "Obter próximo código pai")]
        public async Task ValidaProximoCodigoPai()
        {
            //Arrange
            var contaPaiVazia = new ContaProximoCodigoCommand { CodigoPai = "" };
            var cancellationToken = new CancellationToken();

            //Act
            var handler = new ContaProximoCodigoCommandHandler(GetDatabaseWithFatherRegister());
            var result = await handler.Handle(contaPaiVazia, cancellationToken);

            //Assert
            Assert.NotNull(result.Result);
            Assert.True(result.Result!.Succeeded);
            Assert.Equal("3", result.ProximoCodigo);
        }
    }
}