using Lab08.MVC.Business.Services;
using Lab08.MVC.Data.Interfaces;
using Moq;
using NUnit.Framework;

namespace Lab08.MVC.Business.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService userService;
        Mock<IUnitOfWork> mockUOW = new Mock<IUnitOfWork>();


        [SetUp]
        protected void SetUp()
        {
            userService = new UserService(mockUOW.Object);
        }
        
        [Test]
        public void UnitOfWork_void_UnitOfWork()
        {
            var managerResult = userService.UnitOfWork.UserManager;
        }
    }
}
