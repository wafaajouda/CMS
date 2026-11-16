using CMS.Core.Dtos;
using CMS.Core.Constant;
using CMS.Infrastructure.Services.Users;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using CMS.Infrastructure.Services.Categories;
using CMS.Infrastructure.Services.Advertisements;
using Microsoft.AspNetCore.Mvc.Rendering;
using CMS.Infrastructure.Services.Tracks;
using CMS.Infrastructure.Services.Posts;
using CMS.Core.Enums;

namespace CMS.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public PostController(IPostService trackService, ICategoryService categoryService, IUserService userService)
        {
            _postService = trackService;
            _categoryService = categoryService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetPostData(Pagination pagination, Query query)
        {
            var result = await _postService.GetAll(pagination, query);
            return Json(result);
        }

        public async Task<IActionResult> GetLog(int Id)
        {
            var logs = await _postService.GetLog(Id);
            return View(logs);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");
            ViewData["authors"] = new SelectList(await _userService.GetAuthorList(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto dto)
        {
            if (ModelState.IsValid)
            {
                await _postService.Create(dto);
                return Ok(Result.AddSuccessResult());
            }

            ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");
            ViewData["authors"] = new SelectList(await _userService.GetAuthorList(), "Id", "FullName");
            return View(dto);
        }

        //[HttpGet]
        //public async Task<IActionResult> Update(int id)
        //{
        //    var track = await _postService.Get(id);
        //    ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");
        //    ViewData["authors"] = new SelectList(await _userService.GetAuthorList(), "Id", "FullName");
        //    return View(track);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(UpdatePostDto dto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _postService.Update(dto);
        //        return Ok(Result.EditSuccessResult());
        //    }

        //    ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");
        //    ViewData["authors"] = new SelectList(await _userService.GetAuthorList(), "Id", "FullName");
        //    return View(dto);
        //}

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.Delete(id);
            return Ok(Result.DeleteSuccessResult());
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id, ContentStatus status)
        {
            await _postService.UpdateStatus(id, status);
            return Ok(Result.UpdateStatusSuccessResult());
        }
    }
}
