using NinjaPlus.Common;
using NinjaPlus.Lib;
using NinjaPlus.Models;
using NinjaPlus.Pages;
using NUnit.Framework;
using System;
using System.Threading;

namespace NinjaPlus.Tests
{
    public class SaveMovieTest : BaseTest
    {
        private LoginPage _loginPage;
        private MoviePage _moviePage;
        
        [SetUp]
        public void Before()
        {
            _loginPage = new LoginPage(Browser);
            _moviePage = new MoviePage(Browser);
            _loginPage.With("papito@ninjaplus.com","pwd123");
        }

        [Test]
        public void ShouldSaveMovie()
        {
        
            var movieData = new MovieModel()
            {
                Title = "Resident Evil",
                Status = "Disponível",
                Year = 2002,
                ReleaseDate = "01/05/2002",
                Cast = {"Milla Jovovich", "Ali Larter", "Ian Glen", "Shawn Roberts"},
                Plot = "Este é o resumo do filme",
                Cover = CoverPath() + "Foto_Raphael_Mantilha_Shop_DPedro.jpg"
            };

            Database.RemoveByTitle(movieData.Title);
       
            _moviePage.Add();
            _moviePage.Save(movieData);
            
            Assert.That(
                _moviePage.HasMovie(movieData.Title), 
                $"Erro ao verificar se o filme {movieData.Title} foi cadastrado."
            );
        }
    }
}