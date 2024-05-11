using Application.Dtos;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.SeedWork;
using Moq;
using Xunit;

namespace Application.Tests.Services
{
    public class AnimeServiceTests
    {
        private readonly IAnimeService _animeService;
        private readonly Mock<IAnimeRepository> _animeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public AnimeServiceTests()
        {
            _animeRepositoryMock = new Mock<IAnimeRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _animeService = new AnimeService(_animeRepositoryMock.Object, _unitOfWork.Object);
        }

        [Fact]
        public async Task GetAnime_Sucesso_RetornaAnimesComNomeCorrespondente()
        {
            //Arrange
            var parameters = new ParametersDto()
            {
                Nome = "One Piece",
                NumeroPagina = 1,
                QuantidadePaginas = 1,
            };

            const string nomeAnime = "One Piece";

            var animesMock = new List<Anime>()
            {
                new Anime("One Piece","teste","teste"),
                new Anime("Naruto","teste","teste"),
                new Anime("Dragon ball Z","teste","teste"),
            };

            //Act
            _animeRepositoryMock.Setup(r => r.GetAnime(It.IsAny<string>(), It.IsAny<string>() ,It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(animesMock));

            var animesResultado = await _animeService.GetAnimes(parameters, It.IsAny<CancellationToken>());

            //Asserts
            Assert.NotNull(animesResultado);
            Assert.Equal(3, animesResultado.Count);
            Assert.Equal(nomeAnime, animesResultado.First().Nome);

            _animeRepositoryMock.Verify(r => r.GetAnime(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task GetAnime_Sucesso_RetornaAnimesComDiretorCorrespondente()
        {
            //Arrange
            var parameters = new ParametersDto()
            {
                Diretor = "Eiichiro Oda",
                NumeroPagina = 1,
                QuantidadePaginas = 1,
            };

            const string nomeDiretor = "Eiichiro Oda";

            var animesMock = new List<Anime>()
            {
                new Anime("One Piece","teste","Eiichiro Oda"),
                new Anime("Naruto","teste","teste"),
                new Anime("Dragon ball Z","teste","teste"),
            };

            _animeRepositoryMock.Setup(r => r.GetAnime(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(animesMock));

            //Act
            var animesResultado = await _animeService.GetAnimes(parameters, It.IsAny<CancellationToken>());

            // Asserts
            Assert.NotNull(animesResultado);
            Assert.Equal(3, animesResultado.Count); 
            Assert.Equal(nomeDiretor, animesResultado.First().Diretor); 

            _animeRepositoryMock.Verify(r => r.GetAnime(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAnime_Sucesso_RetornaAnimesComPalavraChaveCorrespondente()
        {
            //Arrange
            var parameters = new ParametersDto()
            {
                PalavraChave = "Piratas",
                NumeroPagina = 1,
                QuantidadePaginas = 1,
            };

            const string palavraChave = "Piratas";

            var animesMock = new List<Anime>()
            {
                new Anime("One Piece","Eu vou ser o rei dos Piratas","Eiichiro Oda"),
                new Anime("Naruto","teste","teste"),
                new Anime("Dragon ball Z","teste","teste"),
            };

            //Act
            _animeRepositoryMock.Setup(r => r.GetAnime(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(animesMock));

            
            var animesResultado = await _animeService.GetAnimes(parameters, It.IsAny<CancellationToken>());

            // Asserts
            Assert.NotNull(animesResultado);
            Assert.Equal(3, animesResultado.Count); 
            Assert.Contains(palavraChave, animesResultado.First().Descricao); 

            _animeRepositoryMock.Verify(r => r.GetAnime(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task InsertAnime_Sucesso_RetornaAnimeDto()
        {
            //Arrange
            var requestDto = new RequestDto { nome = "Anime Legal", diretor = "Diretor Legal",descricao = "Descrição do Anime Legal" };

       
            //Act
            _animeRepositoryMock.Setup(r => r.CreateAnime(It.IsAny<Anime>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);


            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

            var animeDto = await _animeService.InsertAnime(requestDto, CancellationToken.None);

            //Asserts
            Assert.NotNull(animeDto);
            Assert.Equal(requestDto.nome, animeDto.Nome);
            Assert.Equal(requestDto.diretor, animeDto.Diretor);
            Assert.Equal(requestDto.descricao, animeDto.Descricao);

            _animeRepositoryMock.Verify(r => r.CreateAnime(It.IsAny<Anime>(), It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task InsertAnime_FalhaNoRepositorio_LancaApplicationException()
        {
            //Arrange
            var requestDto = new RequestDto { nome = "Anime Legal", diretor = "Diretor Legal", descricao = "Descrição do Anime Legal" };

            _animeRepositoryMock.Setup(r => r.CreateAnime(It.IsAny<Anime>(), It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            //Act
            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(0)); // Não será chamada

            //Assert
            await Assert.ThrowsAsync<ApplicationException>(async () => await _animeService.InsertAnime(requestDto, CancellationToken.None));

            
            _animeRepositoryMock.Verify(r => r.CreateAnime(It.IsAny<Anime>(), It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAnime_Sucesso_RetornaTrue()
        {
            //Arrange
            long animeId = 1;

            var requestDto = new RequestDto { diretor = "Diretor Ilegal" }; 

            var anime = new Anime("Yugioh","Anime interessante demais","Diretor Legal");

            _animeRepositoryMock.Setup(r => r.GetAnimeById(animeId, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(anime));

            _animeRepositoryMock.Setup(r => r.UpdateAnime(It.IsAny<long>(), It.IsAny<Anime>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            //Act
            var atualizado = await _animeService.UpdateAnime(animeId, requestDto, CancellationToken.None);

            //Asserts
            Assert.True(atualizado);

            _animeRepositoryMock.Verify(r => r.GetAnimeById(animeId, It.IsAny<CancellationToken>()), Times.Once);
            _animeRepositoryMock.Verify(r => r.UpdateAnime(It.IsAny<long>(), It.IsAny<Anime>(), It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAnime_Sucesso_RetornaTrue()
        {
            //Arrange

            long idAnime = 1;

            _animeRepositoryMock.Setup(r => r.DeleteAnime(idAnime, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            //Act
            var deletado = await _animeService.DeleteAnime(idAnime, CancellationToken.None);

            //Asserts
            Assert.True(deletado);

            _animeRepositoryMock.Verify(r => r.DeleteAnime(idAnime, It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAnime_FalhaNaDelecao_LancaApplicationException()
        {
            //Arrange
            long idAnime = 1;

            _animeRepositoryMock.Setup(r => r.DeleteAnime(idAnime, It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(0)); 

            //Assert
            await Assert.ThrowsAsync<ApplicationException>(async () => await _animeService.DeleteAnime(idAnime, CancellationToken.None));

            _animeRepositoryMock.Verify(r => r.DeleteAnime(idAnime, It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }

}
