using NUnit.Framework;
using NinjaPlus.Pages;
using NinjaPlus.Common;

namespace NinjaPlus.Tests
{
    public class LoginTests : BaseTest
    {
        private LoginPage _login;
        private Sidebar _side;

        [SetUp]
        public void Start()
        {
            _login = new LoginPage(Browser);
            _side = new Sidebar(Browser);
        }

        [Test]
        [Category("Critical")]
        public void ShouldSeeLoggedUser()
        {
            _login.With("papito@ninjaplus.com", "pwd123");
            Assert.AreEqual("Papito", _side.LoggedUser());
        }

        [TestCase("papito@ninjaplus.com","123456","Usuário e/ou senha inválidos")]
        [TestCase("404@ninjaplus.com","pwd123","Usuário e/ou senha inválidos")]
        [TestCase("","pwd123","Opps. Cadê o email?")]
        [TestCase("papito@ninjaplus.com","","Opps. Cadê a senha?")]
        public void ShouldSeeAlertMessage(string email,string pass,string expectMessage)
        {
             _login.With(email, pass);
            Assert.AreEqual(expectMessage, _login.AlertMessage());
        }
    }
}