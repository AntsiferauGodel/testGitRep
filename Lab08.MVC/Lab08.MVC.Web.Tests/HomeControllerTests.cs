using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Lab08.MVC.Business.Interfaces;
using Lab08.MVC.Business.Models;
using Lab08.MVC.Domain;
using Lab08.MVC.Web.Controllers;

namespace Lab08.MVC.Web.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController homeController;
        private Advertisement fakeAdvertisement;
        private Advertisement fakeNullAdvertisement;
        private Advertisement fakeAdvertisementWithWrongOwnerId;
        private AdvertisementView fakeAdvertisementView;
        private AdvertisementView fakeAdvertisementViewWithWrongOwnerId;
        private AdvertisementView invalidAdvertisementViewModel;
        private IList<Advertisement> fakeAdvertisements;
        private Mock<IAdvertisementService> advertisementServiceMock;
        private Mock<IFileService> fileServiceMock;
        private Mock<IPrincipalService> principalServiceMock;

        [OneTimeSetUp]
        protected void SetUp()
        {
            fakeNullAdvertisement = null;
            fakeAdvertisementWithWrongOwnerId = new Advertisement
            {
                CreatorUserId = "WrongOwnerId"
            };
            fakeAdvertisementViewWithWrongOwnerId = new AdvertisementView
            {
                OwnerId = "WrongOwnerId"
            };
            fakeAdvertisement = new Advertisement
            {
                AdvertisementId = 1,
                CreatorUserId = "UserId",
                Description = "testDescritption",
                Header = "testHeader",
                Price = 30
            };
            fakeAdvertisementView = new AdvertisementView
            {
                OwnerId = "UserId",
                Header = "Header",
                Description = "description",
                Price = 222.4m
            };
            invalidAdvertisementViewModel = new AdvertisementView
            {
                Header = null,
                Description = null,
                Price = 0
            };
            fakeAdvertisements = new List<Advertisement> {
                fakeAdvertisement,
                fakeAdvertisement
            };
            advertisementServiceMock = new Mock<IAdvertisementService>();
            fileServiceMock = new Mock<IFileService>();
            principalServiceMock = new Mock<IPrincipalService>();
            homeController = new HomeController(advertisementServiceMock.Object, fileServiceMock.Object, principalServiceMock.Object);
            fileServiceMock.Setup(m => m.RemoveFileById(1)).Verifiable();
            advertisementServiceMock
                .Setup(m => m.GetAdvertisementsTotalPagesNumber(It.IsAny<int>()))
                .Returns(1).Verifiable();
            advertisementServiceMock
                .Setup(m => m.GetAdvertisements(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(fakeAdvertisements).Verifiable();
            advertisementServiceMock
                .Setup(m => m.GetAdvertisementById(-1))
                .Returns(fakeNullAdvertisement).Verifiable();
            advertisementServiceMock
                .Setup(m => m.GetAdvertisementById(It.Is<int>(c => c > 0)))
                .Returns(fakeAdvertisement).Verifiable();
            advertisementServiceMock
                .Setup(m => m.GetAdvertisementById(It.Is<int>(c => c == 0)))
                .Returns(fakeAdvertisementWithWrongOwnerId).Verifiable();
            advertisementServiceMock
                .Setup(m => m.Create(It.IsAny<Advertisement>())).Verifiable();
            advertisementServiceMock
                .Setup(m => m.EditAdvertisementById(It.IsAny<Advertisement>())).Verifiable();
            advertisementServiceMock
                .Setup(m => m.RemoveAdvertisementById(It.IsAny<int>())).Verifiable();
            advertisementServiceMock
                .Setup(m => m.GetAdvertisementsTotalPagesNumberByPartialHeader(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(10).Verifiable();
            advertisementServiceMock
                .Setup(m => m.GetAdvertisementsByPartialHeader(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(fakeAdvertisements).Verifiable();
            advertisementServiceMock
                .Setup(m => m.GetAdvertisementsTotalPagesNumberForCurrentUser(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(10).Verifiable();
            advertisementServiceMock
                .Setup(m => m.GetAdvertisementsForCurrentUser(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(fakeAdvertisements).Verifiable();
            principalServiceMock
                .Setup(m => m.GetUserIdFromPrincipal(It.IsAny<IPrincipal>()))
                .Returns("UserId").Verifiable();
        }

        [TearDown]
        protected void ClearModelState()
        {
            homeController.ModelState.Clear();
        }

        [Test]
        public void Index_Null_NotNull()
        {
            var result = homeController.Index();
            Assert.NotNull(result);
        }

        [Test]
        public void Index_Null_ReturnViewResult()
        {
            var result = homeController.Index();
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void RemoveFile_CorrectValues_ResultNotNull()
        {
            var result = homeController.RemoveFile(1, 1);
            Assert.NotNull(result);
        }

        [Test]
        public void RemoveFile_CorrectValues_ReturnsRedirectToRouteResult()
        {
            var result = homeController.RemoveFile(1, 1);
            Assert.IsInstanceOf(typeof(RedirectToRouteResult), result);
        }

        [Test]
        public void RemoveFile_CorrectValues_ReturnNotViewResult()
        {
            var result = homeController.RemoveFile(1, 1);
            Assert.IsNotInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void RemoveFile_CorrectValues_InvokesFileService()
        {
            homeController.RemoveFile(1, 1);
            fileServiceMock.Verify(m => m.RemoveFileById(It.IsAny<int>()));
        }

        [Test]
        public void Advertisements_CorrectValues_ResultNotNull()
        {
            var result = homeController.Advertisements(1, 1);
            Assert.NotNull(result);
        }

        [Test]
        public void Advertisements_CorrectValues_InvokesAdvertisementServiceGetAdvertisements()
        {
            homeController.Advertisements(1, 1);
            advertisementServiceMock.Verify(m => m.GetAdvertisements(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void Advertisements_CorrectValues_InvokesAdvertisementsTotalPagesNumber()
        {
            homeController.Advertisements(1, 1);
            advertisementServiceMock.Verify(m => m.GetAdvertisementsTotalPagesNumber(It.IsAny<int>()));
        }

        [Test]
        public void AdvertisementDetails_CorrectValues_ResultNotNull()
        {
            var result = homeController.AdvertisementDetails(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void AdvertisementDetails_CorrectValues_InvokesAdvertisementServiceGetAdvertisementById()
        {
            homeController.AdvertisementDetails(1);
            advertisementServiceMock.Verify(m => m.GetAdvertisementById(It.IsAny<int>()));
        }

        [Test]
        public void AdvertisementDetails_CorrectValues_ReturnsRedirectToRouteResult()
        {
            var result = homeController.AdvertisementDetails(1);
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void AdvertisementDetails_InvalidId_ReturnRedirectToRouteResult()
        {
            var result = homeController.AdvertisementDetails(-1);
            Assert.IsInstanceOf(typeof(RedirectToRouteResult), result);
        }

        [Test]
        public void AdvertisementCreateGet_Null_ResultNotNull()
        {
            var result = homeController.AdvertisementCreate();
            Assert.NotNull(result);
        }

        [Test]
        public void AdvertisementCreateGet_Null_ReturnsViewResult()
        {
            var result = homeController.AdvertisementCreate();
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void AdvertisementCreatePost_CorrectValues_ResultNotNull()
        {
            var result = homeController.AdvertisementCreate(fakeAdvertisementView, null);
            Assert.NotNull(result);
        }

        [Test]
        public void AdvertisementCreatePost_CorrectValues_ReturnsRedirectToRouteResult()
        {
            var result = homeController.AdvertisementCreate(fakeAdvertisementView, null);
            Assert.IsInstanceOf(typeof(RedirectToRouteResult), result);
        }

        [Test]
        public void AdvertisementCreatePost_CorrectValues_InvokesAdvertisementServiceCreate()
        {
            homeController.AdvertisementCreate(fakeAdvertisementView, null);
            advertisementServiceMock.Verify(m => m.Create(It.IsAny<Advertisement>()));
        }

        [Test]
        public void AdvertisementCreatePost_CorrectValues_InvokesPrincipalSerciveGetUserIdFromPrincipal()
        {
            homeController.AdvertisementCreate(fakeAdvertisementView, null);
            principalServiceMock.Verify(m => m.GetUserIdFromPrincipal(It.IsAny<IPrincipal>()));
        }

        [Test]
        public void AdvertisementCreatePost_InvalidModel_ResultNotNull()
        {
            homeController.ViewData.ModelState.AddModelError("foo", "foo");
            var result = homeController.AdvertisementCreate(invalidAdvertisementViewModel, null);
            Assert.NotNull(result);
        }

        [Test]
        public void AdvertisementCreatePost_InvalidModel_ReturnsViewResult()
        {
            homeController.ViewData.ModelState.AddModelError("foo", "foo");
            var result = homeController.AdvertisementCreate(invalidAdvertisementViewModel, null);
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void AdvertisementsForCurrentUser_CorrectValues_ResultNotNull()
        {
            var result = homeController.AdvertisementsForCurrentUser("UserId");
            Assert.NotNull(result);
        }

        [Test]
        public void AdvertisementsForCurrentUser_CorrectValues_InvokesAdvertisementServiceGetAdvertisementsTotalPagesNumberForCurrentUser()
        {
            homeController.AdvertisementsForCurrentUser("UserId");
            advertisementServiceMock
                .Verify(m => m.GetAdvertisementsTotalPagesNumberForCurrentUser(It.IsAny<int>(), It.IsAny<string>()));
        }

        [Test]
        public void AdvertisementsForCurrentUser_CorrectValues_InvokesAdvertisementServiceGetAdvertisementsForCurrentUser()
        {
            homeController.AdvertisementsForCurrentUser("UserId");
            advertisementServiceMock
                .Verify(m => m.GetAdvertisementsForCurrentUser(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void AdvertisementEditGet_CorrectValues_ResultNotNull()
        {
            var result = homeController.AdvertisementEdit(1);
            Assert.NotNull(result);
        }

        [Test]
        public void AdvertisementEditGet_CorrectValues_InvokesAdvertisementServiceGetAdvertisementUserById()
        {
            homeController.AdvertisementEdit(1);
            advertisementServiceMock.Verify(m => m.GetAdvertisementById(It.IsAny<int>()));
        }

        [Test]
        public void AdvertisementEditGet_InvalidId_ResultNotNull()
        {
            var result = homeController.AdvertisementEdit(-1);
            Assert.NotNull(result);
        }

        [Test]
        public void AdvertisementEditGet_InvalidId_ReturnsErrorIndex()
        {
            var result = homeController.AdvertisementEdit(-1);
            Assert.AreEqual("ErrorHandler", ((ViewResult)result).MasterName);
            Assert.AreEqual("Index", ((ViewResult)result).ViewName);
        }

        [Test]
        public void AdvertisementEditGet_InvalidOwner_ReturnsErrorHandlerNotFound()
        {
            var result = homeController.AdvertisementEdit(0);
            Assert.AreEqual("ErrorHandler", ((ViewResult)result).MasterName);
            Assert.AreEqual("NotFound", ((ViewResult)result).ViewName);
        }

        [Test]
        public void AdvertisementEditPost_CorrectValues_ResultNotNull()
        {
            var result = homeController.AdvertisementEdit(fakeAdvertisementView, null);
            Assert.NotNull(result);
        }

        [Test]
        public void AdvertisementEditPost_CorrectValues_InvokesAdvertisementServiceEditById()
        {
            homeController.AdvertisementEdit(fakeAdvertisementView, null);
            advertisementServiceMock.Verify(m => m.EditAdvertisementById(It.IsAny<Advertisement>()));
        }

        [Test]
        public void AdvertisementEditPost_InvalidModel_ReturnsEditView()
        {
            homeController.ViewData.ModelState.AddModelError("MODEL", "wrong model in edit!");
            var result = homeController.AdvertisementEdit(fakeAdvertisementView, null);
            Assert.AreEqual("AdvertisementEdit", ((ViewResult)result).ViewName);
        }

        [Test]
        public void AdvertisementEditPost_InvalidOwner_ReturnsErrorHandlerNotFound()
        {
            var result = homeController.AdvertisementEdit(fakeAdvertisementViewWithWrongOwnerId, null);
            Assert.AreEqual("ErrorHandler", ((ViewResult)result).MasterName);
            Assert.AreEqual("NotFound", ((ViewResult)result).ViewName);
        }

        [Test]
        public void AdvertisementRemove_CorrectValues_ResultNotNull()
        {
            var result = homeController.AdvertisementRemove(1);
            Assert.NotNull(result);
        }

        [Test]
        public void AdvertisementRemove_CorrectValues_ReturnsRedirectToRouteResult()
        {
            var result = homeController.AdvertisementRemove(1);
            Assert.IsInstanceOf(typeof(RedirectToRouteResult), result);
        }

        [Test]
        public void AdvertisementRemove_CorrectValues_InvokesAdvertisementServiceRemoveAdvertisementById()
        {
            homeController.AdvertisementRemove(1);
            advertisementServiceMock.Verify(m => m.RemoveAdvertisementById(It.IsAny<int>()));
        }

        [Test]
        public void AdvertisementRemove_InvalidFileOwner_ReturnsNotFoundErrorHandler()
        {
           var result = homeController.AdvertisementRemove(-1);
            Assert.AreEqual("ErrorHandler", ((ViewResult)result).MasterName);
            Assert.AreEqual("NotFound", ((ViewResult)result).ViewName);
        }

        [Test]
        public void AdvertisementSearch_CorrectValues_ResultNotNull()
        {
            var result = homeController.AdvertisementSearch("SearchWord");
            Assert.NotNull(result);
        }

        [Test]
        public void AdvertisementSearch_CorrectValues_InvokesAdvertisementServiceGetAdvertisementsTotalPagesNumberByPartialHeader()
        {
            homeController.AdvertisementSearch("SearchWord");
            advertisementServiceMock
                .Verify(m => m.GetAdvertisementsTotalPagesNumberByPartialHeader(It.IsAny<string>(), It.IsAny<int>()));
        }

        [Test]
        public void AdvertisementSearch_CorrectValues_InvokesAdvertisementServiceGetAdvertisementsByPartialHeader()
        {
            homeController.AdvertisementSearch("SearchWord");
            advertisementServiceMock
                .Verify(m => m.GetAdvertisementsByPartialHeader(It.IsAny<string>(), It.IsAny<int>() , It.IsAny<int>()));
        }
    }
}
