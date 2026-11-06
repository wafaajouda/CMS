using CMS.Core.Dtos;
using CMS.Core.Constant;
using CMS.Infrastructure.Services.Users;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using CMS.Infrastructure.Services.Categories;
using CMS.Infrastructure.Services.Advertisements;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMS.Web.Controllers
{
    public class AdvertisementController : Controller
    {
        private IAdvertisementService _advertisementService;

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAdvertisementData(Pagination pagination, Query query)
        {
            var result = await _advertisementService.GetAll(pagination, query);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["owners"] = new SelectList(await _advertisementService.GetAdvertisementOwner(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertisementDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.OwnerId))
            {
                ModelState.Remove("Owner.FullName");
                ModelState.Remove("Owner.Email");
                ModelState.Remove("Owner.PhoneNumber");
            }
            if (ModelState.IsValid)
            {
                await _advertisementService.Create(dto);
                return Ok(Result.AddSuccessResult());
            }
            return View(dto);
        }

        //[HttpGet]
        //public async Task<IActionResult> Update(int id)
        //{
        //    var advertisement = await _advertisementService.Get(id);
        //    return View(advertisement);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(UpdateAdvertisementDto dto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _advertisementService.Update(dto);
        //        return Ok(Result.EditSuccessResult());
        //    }
        //    return View(dto);
        //}

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _advertisementService.Delete(id);
            return Ok(Result.DeleteSuccessResult());
        }
    }
}
