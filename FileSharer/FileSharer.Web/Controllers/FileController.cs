using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Constants;
using FileSharer.Common.Entities;
using FileSharer.Web.Models.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSharer.Web.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileItemService _fileItemService;

        private readonly IFileCategoryService _fileCategoryService;

        private readonly IService<FileExtension> _fileExtensionService;

        private readonly IUserService _userService;

        public FileController(
            IFileItemService fileItemService,
            IFileCategoryService fileCategoryService,
            IService<FileExtension> fileExtensionService,
            IUserService userService)
        {
            _fileItemService = fileItemService;
            _fileCategoryService = fileCategoryService;
            _fileExtensionService = fileExtensionService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            var categoriesList = _fileCategoryService.GetAll();
            ViewData["Categories"] = new SelectList(categoriesList, "Name", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Upload(UploadFileViewModel uploadModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Upload");
            }

            var category = _fileCategoryService.GetByName(uploadModel.Category);

            if (category is null)
            {
                ModelState.AddModelError("", ErrorMessage.NoSuchCategory);

                return RedirectToAction("Upload");
            }

            var extension = uploadModel.File.FileName.Split('.')[^1];
            var extensionFromDatabase = _fileExtensionService.GetAll().
                SingleOrDefault(ext => ext.Name == "." + extension);

            if (extensionFromDatabase is null)
            {
                ModelState.AddModelError("", ErrorMessage.UnsupportedFormat);

                return RedirectToAction("Upload");
            }

            var file = CreateFile(uploadModel, category, extensionFromDatabase);
            _fileItemService.Add(file);

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var file = _fileItemService.GetById(id);
            var fileModel = MapToModel(file);

            return View(fileModel);
        }

        [HttpGet]
        public IActionResult List()
        {
            var fileModels = new List<FileViewModel>();
            var fileItems = _fileItemService.GetAll();

            foreach (var file in fileItems)
            {
                var fileModel = MapToModel(file);
                fileModels.Add(fileModel);
            }

            return View(fileModels);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var file = _fileItemService.GetById(id);
            var editFileModel = MapToEditModel(file);

            return View(editFileModel);
        }

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
                ModelState.AddModelError("", ErrorMessage.NoSuchCategory);

                return View(editFileModel);
            }

            var file = MapToFile(editFileModel);
            _fileItemService.Update(editFileModel.Id, file);

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _fileItemService.Delete(id);

            return RedirectToAction("List");
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
    }
}
