using Lab08.MVC.Web.Controllers;
using NUnit.Framework;

namespace Lab08.MVC.Web.Tests
{
    [TestFixture]
    public class ErrorHandlerControllerTests
    {
        private ErrorHandlerController errorHandlerController;

        [SetUp]
        protected void SetUp()
        {
            errorHandlerController = new ErrorHandlerController();
        }

        [Test]
        public void Index_Null_ResultNotNull()
        {
            var result = errorHandlerController.Index();
            Assert.NotNull(result);
        }

        [Test]
        public void NotFount_Null_ResultNorNull()
        {
            var result = errorHandlerController.NotFound();
            Assert.NotNull(result);
        }
    }
}
