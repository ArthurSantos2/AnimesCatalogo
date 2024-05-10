using Application.Dtos;
using Application.Services;
using Domain.Interfaces.Repositories;
using Domain.SeedWork;
using Moq;
using Xunit;

namespace Application.Tests.Services
{
    public class AnimeServiceTests
    {
        private readonly AnimeService _animeService;
        private readonly Mock<IAnimeRepository> _animeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public AnimeServiceTests()
        {
            _animeRepositoryMock = new Mock<IAnimeRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _animeService = new AnimeService(_animeRepositoryMock.Object, _unitOfWork.Object);
        }

        //todo: continuar
        [Fact]
        public async void GetAnime_test()
        {
            //um teste divide-se em Arrange(os dados) -> Act (o que será feito)-> Assert(o resultado)

            //Arrange

            var parameters = new ParametersDto()
            {
                Diretor = "Anonimo",
                Nome = "Picachu",
                NumeroPagina = 1,
                PalavraChave = "japones",
                QuantidadePaginas = 1,
            };

            var cancellationToken = new CancellationToken();

            //O ACT
            var retorno = await _animeService.GetAnimes(parameters, cancellationToken);

            //Assert
            _animeRepositoryMock.Verify(repo => repo.GetAnime(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),cancellationToken), Times.Once);

            Assert.True(true);


        }
    }
}
