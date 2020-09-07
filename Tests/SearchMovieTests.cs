using NinjaPlus.Common;
using NinjaPlus.Lib;
using NinjaPlus.Pages;
using NUnit.Framework;

namespace NinjaPlus.Tests
{
    public class SearchMovieTests : BaseTest
    {

        private LoginPage _loginPage;
        private MoviePage _moviePage;

        [SetUp]
        public void Before()
        {
            _loginPage = new LoginPage(Browser);
            _moviePage = new MoviePage(Browser);

            _loginPage.With("papito@ninjaplus.com", "pwd123");

            Database.InsertMovies();
        }

        [Test]
        public void ShouldFindUniqueMovie()
        {
            var target = "Coringa";

            _moviePage.Search(target);

            Assert.That(
               _moviePage.HasMovie(target),
               $"Erro ao verificar se o filme {target} foi encontrado."
           );

            Browser.HasNoContent("Puxa! não encontramos nada aqui :(");

            Assert.AreEqual(1, _moviePage.CountMovie());
        }

        [Test]
        public void ShouldFindMovies()
        {
            var target = "Batman";

            _moviePage.Search(target);

            Assert.That(
               _moviePage.HasMovie("Batman Begins"),
               $"Erro ao verificar se o filme {target} foi encontrado."
           );

            Assert.That(
              _moviePage.HasMovie("Batman O Cavaleiro das Trevas"),
              $"Erro ao verificar se o filme {target} foi encontrado."
          );

            Browser.HasNoContent("Puxa! não encontramos nada aqui :(");
            Assert.AreEqual(2, _moviePage.CountMovie());
        }

        [Test]
        public void ShouldDisplayNoMovieFound()
        {
            _moviePage.Search("Os Trapalhões");
            Assert.AreEqual("Puxa! não encontramos nada aqui :(", _moviePage.SearchAlert());
        }
    }
}