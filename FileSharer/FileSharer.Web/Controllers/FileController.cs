using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Constants;
using FileSharer.Common.Entities;
using FileSharer.Web.Helpers.LoggingHelpers;
using FileSharer.Web.Models.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace FileSharer.Web.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly IFileItemService _fileItemService;

        private readonly IFileCategoryService _fileCategoryService;

        private readonly IService<FileExtension> _fileExtensionService;

        private readonly IUserService _userService;

        private readonly ILogger<FileController> _logger;

        private readonly ILogHelper _logHelper;

        public FileController(
            IFileItemService fileItemService,
            IFileCategoryService fileCategoryService,
            IService<FileExtension> fileExtensionService,
            IUserService userService,
            ILogger<FileController> logger,
            ILogHelper logHelper)
        {
            _fileItemService = fileItemService;
            _fileCategoryService = fileCategoryService;
            _fileExtensionService = fileExtensionService;
            _userService = userService;
            _logger = logger;
            _logHelper = logHelper;
        }
        
        [HttpGet]
        public IActionResult Upload()
        {
            _logger.LogInformation(
                _logHelper.GetUserActionString(User, "File", nameof(Upload)));

            return View();
        }

        [HttpPost]
        public IActionResult Upload(UploadFileViewModel uploadModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var category = _fileCategoryService.GetByName(uploadModel.Category);

            if (category is null)
            {
                ModelState.AddModelError("", ErrorMessages.NoSuchCategory);

                return View();
            }

            var extension = uploadModel.File.FileName.Split('.')[^1];
            var extensionFromDatabase = _fileExtensionService.GetAll().
                SingleOrDefault(ext => ext.Name == "." + extension);

            if (extensionFromDatabase is null)
            {
                ModelState.AddModelError("", ErrorMessages.UnsupportedFormat);

                return View();
            }

            var file = CreateFile(uploadModel, category, extensionFromDatabase);
            _fileItemService.Add(file);

            _logger.LogInformation(
                _logHelper.GetUserActionString(User, "File", nameof(Upload), "POST"));

            return RedirectToAction(nameof(List));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var file = _fileItemService.GetById(id);
            var fileModel = MapToModel(file);

            _logger.LogInformation(
                _logHelper.GetUserActionString(User, "File", nameof(Details)));

            return View(fileModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult List()
        {
            var fileItems = _fileItemService.GetAll();
            var fileModels = new List<FileViewModel>();

            foreach (var file in fileItems)
            {
                var fileModel = MapToModel(file);
                fileModels.Add(fileModel);
            }

            ViewData["Files"] = fileModels;
            var selectList = GetSelectCategoryList();
            ViewData["Categories"] = selectList;

            _logger.LogInformation(
                _logHelper.GetUserActionString(User, "File", nameof(List)));

            return View();
        }

        [Authorize (Roles = Roles.OnlyEditors)]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var file = _fileItemService.GetById(id);
            var editFileModel = MapToEditModel(file);

            _logger.LogInformation(
                _logHelper.GetUserActionString(User, "File", nameof(Edit)));

            return View(editFileModel);
        }

        [Authorize(Roles = Roles.OnlyEditors)]
        [HttpPost]
        public IActionResult Edit(EditFileViewModel editFileModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editFileModel);
            }

            var category = _fileCategoryService.GetByName(editFileModel.NewCategory);

            if (category is null)
            {
                ModelState.AddModelError("", ErrorMessages.NoSuchCategory);

                return View(editFileModel);
            }

            var file = MapToFile(editFileModel);
            _fileItemService.Update(editFileModel.Id, file);

            _logger.LogInformation(
                _logHelper.GetUserActionString(User, "File", nameof(Edit), "POST"));

            return RedirectToAction(nameof(List));
        }

        [Authorize(Roles = Roles.OnlyEditors)]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _fileItemService.Delete(id);

            _logger.LogInformation(
                _logHelper.GetUserActionString(User, "File", nameof(Delete), "POST"));

            return RedirectToAction(nameof(List));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Search(SearchViewModel searchModel)
        {
            var category = _fileCategoryService.GetByName(searchModel.Category);
            var fileName = searchModel.FileName;

            var files = _fileItemService.GetAll();

            if (category != null)
            {
                var filesByCategory = _fileItemService.GetAllByCategoryId(category.Id);

                files = filesByCategory is null ? new List<FileItem>() : files.Where(
                    file => filesByCategory.Any(fileByCategory => fileByCategory.Id == file.Id));
            }

            if (searchModel.FileName != null)
            {
                files = files.Where(file => file.Name.Contains(fileName));
            }

            var fileModels = new List<FileViewModel>();

            foreach (var file in files)
            {
                var fileModel = MapToModel(file);
                fileModels.Add(fileModel);
            }

            ViewData["Files"] = fileModels;
            var selectList = GetSelectCategoryList();
            ViewData["Categories"] = selectList;

            _logger.LogInformation(
                _logHelper.GetUserActionString(User, "File", nameof(Search), "POST"));

            return View(nameof(List));
        }

        private FileItem MapToFile(EditFileViewModel editModel)
        {
            var file = _fileItemService.GetById(editModel.Id);
            var category = _fileCategoryService.GetByName(editModel.NewCategory);

            file.Name = editModel.NewName;
            file.FileCategoryId = category.Id;
            file.Description = editModel.NewDescription;

            return file;
        }

        private EditFileViewModel MapToEditModel(FileItem file)
        {
            var editFileModel = new EditFileViewModel()
            {
                Id = file.Id,
                NewName = file.Name,
                NewCategory = _fileCategoryService.GetById(file.FileCategoryId).Name,
                NewDescription = file.Description,
            };

            return editFileModel;
        }

        private FileViewModel MapToModel(FileItem file)
        {
            string extension = _fileExtensionService.GetById(file.FileExtensionId).Name;
            string name = file.Name + extension;
            string category = _fileCategoryService.GetById(file.FileCategoryId).Name;
            string author = _userService.GetById(file.UserId).Name;

            var fileModel = new FileViewModel()
            {
                Id = file.Id,
                Name = name,
                Category = category,
                Author = author,
                Description = file.Description,
                Size = file.Size,
                DownloadsCount = file.DownloadsCount,
                CreateDate = file.CreateDate,
            };

            return fileModel;
        }

        private FileItem CreateFile(UploadFileViewModel uploadFile, FileCategory category, FileExtension extension)
        {
            var name = uploadFile.File.FileName.Split('.')[^1];
            var userId = int.Parse(User.Claims.Single(
                     claim => claim.Type == CustomClaimTypes.Id).Value);

            var file = new FileItem()
            {
                 Name = name,
                 FileCategoryId = category.Id,
                 FileExtensionId = extension.Id,
                 Description = uploadFile.Description,
 
                 UserId = userId,
            };

            return file;
        }

        private SelectList GetSelectCategoryList()
        {
            var categoriesList = _fileCategoryService.GetAll();

            var allCategoriesItem = new FileCategory()
            {
                Id = 0,
                Name = "All categories",
            };

            categoriesList = categoriesList.Prepend(allCategoriesItem);
            var selectList = (new SelectList(categoriesList, "Name", "Name"));

            return selectList;
        }
    }
}
