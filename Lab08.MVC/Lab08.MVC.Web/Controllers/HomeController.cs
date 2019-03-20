using System;
using System.Web.Mvc;
using System.Linq;
using System.Transactions;
using System.Web;
using Lab08.MVC.Business.Interfaces;
using Lab08.MVC.Business.Models;
using Lab08.MVC.Web.Converters;
using Lab08.MVC.Web.Filters;
using Lab08.MVC.Web.Models;

namespace Lab08.MVC.Web.Controllers
{
    [ExceptionLogger]
    public class HomeController : Controller
    {
        private const string TraderRole = "Trader";
        private const int defaultNumberOfElementsOnPage = 2;

        private readonly IFileService fileService;
        private readonly IAdvertisementService advertisementService;
        private readonly IPrincipalService principleService;

        public HomeController(IAdvertisementService advertisementService, IFileService fileService, IPrincipalService principleService)
        {
            this.advertisementService = advertisementService ?? throw new ArgumentNullException(nameof(advertisementService));
            this.fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            this.principleService = principleService ?? throw new ArgumentNullException(nameof(principleService));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RemoveFile(int fileId, int advertisementId)
        {
            fileService.RemoveFileById(fileId);
            return RedirectToAction("AdvertisementEdit", "Home", new { advertisementId });
        }

        [Authorize]
        public ActionResult Advertisements(int pageNumber = 0, int elementsOnPage = defaultNumberOfElementsOnPage)
        {
            var paginationData = new PaginationModel
            {
                TotalPages = advertisementService.GetAdvertisementsTotalPagesNumber(elementsOnPage),
                CurrentPage = pageNumber,
                ActionNavigationName = "Advertisements",
                ElementsOnPage = elementsOnPage
            };
            ViewData["PaginationData"] = paginationData;
            var advertisements = advertisementService.GetAdvertisements(pageNumber, elementsOnPage);
            var headers = advertisements.Select(AdvertisementEntityToViewConverter.ConvertToAdvertisementHeader).ToList();
            return View(headers);
        }

        [Authorize]
        public ActionResult AdvertisementDetails(int advertisementId)
        {
            var advertisement = advertisementService.GetAdvertisementById(advertisementId);
            if (advertisement == null)
            {
                return RedirectToAction("NotFound", "ErrorHandler");
            }
            var viewModel = AdvertisementEntityToViewConverter.ConvertToAdvertisementView(advertisement);
            if(advertisement.Files != null)
            {
                viewModel.Pictures = advertisement.Files.Select(FileConverter.ConvertFileToPictureView).ToList();
            }
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = TraderRole)]
        public ActionResult AdvertisementCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = TraderRole)]
        public ActionResult AdvertisementCreate(AdvertisementView advertisement, HttpPostedFileBase[] upload)
        {
            if (ModelState.IsValid)
            {
                var advertisementEntity = AdvertisementViewToEntityConverter.Convert(advertisement);
                using (var scope = new TransactionScope())
                {
                    if (upload?[0] != null)
                    {
                        var files = upload.Select(FileConverter.ConvertPostedFileBaseToFile).ToList();
                        advertisementEntity.Files = files;
                    }
                    advertisementService.Create(advertisementEntity);
                    
                    scope.Complete();
                }
                return RedirectToAction("AdvertisementsForCurrentUser", new { userId = principleService.GetUserIdFromPrincipal(User) });
            }
            return View(advertisement);
        }

        [Authorize]
        public ActionResult AdvertisementsForCurrentUser(string userId, int pageNumber = 0, int elementsOnPage = defaultNumberOfElementsOnPage)
        {
            var paginationData = new PaginationModel
            {
                TotalPages = advertisementService.GetAdvertisementsTotalPagesNumberForCurrentUser(elementsOnPage, userId),
                CurrentPage = pageNumber,
                ActionNavigationName = "AdvertisementsForCurrentUser",
                UserId = userId,
                ElementsOnPage = elementsOnPage
            };
            ViewData["PaginationData"] = paginationData;
            var advertisements = advertisementService.GetAdvertisementsForCurrentUser(userId, pageNumber, elementsOnPage);
            var headers = advertisements.Select(AdvertisementEntityToViewConverter.ConvertToAdvertisementHeader).ToList();
            return View("Advertisements", headers);
        }

        [HttpGet]
        [Authorize(Roles = TraderRole)]
        public ActionResult AdvertisementEdit(int advertisementId)
        {
            var advertisement = advertisementService.GetAdvertisementById(advertisementId);
            if (advertisement == null)
            {
                return View("Index", "ErrorHandler");
            }
            #region securityCheck
            if (IsInvalidOwner(advertisement.CreatorUserId))
            {
                return View("NotFound", "ErrorHandler");
            }
            #endregion
            var model = AdvertisementEntityToViewConverter.ConvertToAdvertisementView(advertisement);
            if (advertisement.Files != null)
            {
                model.Pictures = advertisement.Files.Select(FileConverter.ConvertFileToPictureView).ToList();
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = TraderRole)]
        public ActionResult AdvertisementEdit(AdvertisementView advertisement, HttpPostedFileBase[] upload)
        {
            #region securityCheck
            if (IsInvalidOwner(advertisement.OwnerId))
            {
                return View("NotFound", "ErrorHandler");
            }
            #endregion


            if (ModelState.IsValid)
            {
                var advertisementEntity = AdvertisementViewToEntityConverter.Convert(advertisement);
                using (var scope = new TransactionScope())
                {
                    if (upload?[0] != null)
                    {
                        var files = upload.Select((p) => (FileConverter.ConvertPostedFileBaseToFileWithId(p, advertisement.Id))).ToList();
                        if(advertisement.Pictures != null)
                        {
                            advertisementEntity.Files = advertisementEntity.Files.Concat(files).ToList();
                        }
                        else
                        {
                            advertisementEntity.Files = files;
                        }
                    }
                    advertisementService.EditAdvertisementById(advertisementEntity);
                    scope.Complete();
                }
                return RedirectToAction("AdvertisementsForCurrentUser", new { userId = principleService.GetUserIdFromPrincipal(User) });
            }
            return View("AdvertisementEdit", advertisement);
        }

        [Authorize(Roles = TraderRole)]
        public ActionResult AdvertisementRemove(int advertisementId)
        {
            #region securityCheck
            if (IsInvalidOwner(advertisementService.GetAdvertisementById(advertisementId)?.CreatorUserId))
            {
                return View("NotFound", "ErrorHandler");
            }
            #endregion

            advertisementService.RemoveAdvertisementById(advertisementId);
            return RedirectToAction("AdvertisementsForCurrentUser", new { userId = principleService.GetUserIdFromPrincipal(User) });
        }

        [Authorize]
        public ActionResult AdvertisementSearch(string searchWord, int pageNumber = 0, int elementsOnPage = defaultNumberOfElementsOnPage)
        {
            searchWord = searchWord ?? string.Empty;
            var paginationData = new PaginationModel
            {
                TotalPages = advertisementService.GetAdvertisementsTotalPagesNumberByPartialHeader(searchWord, elementsOnPage),
                CurrentPage = pageNumber,
                ActionNavigationName = "AdvertisementSearch",
                SearchWord = searchWord,
                ElementsOnPage = elementsOnPage
            };
            ViewData["PaginationData"] = paginationData;
            var advertisements = advertisementService.GetAdvertisementsByPartialHeader(searchWord, pageNumber, elementsOnPage);
            var headers = advertisements.Select(AdvertisementEntityToViewConverter.ConvertToAdvertisementHeader).ToList();
            return View("Advertisements", headers);
        }

        #region securityCheck
        private bool IsInvalidOwner(string ownerId)
        {
            return principleService.GetUserIdFromPrincipal(User) != ownerId;
        }
        #endregion
    }
}