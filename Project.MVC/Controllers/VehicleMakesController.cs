using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.MVC.ViewModels;
using Project.Service.Models;
using Project.Service.Models.Interface;
using Project.Service.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.MVC.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public VehicleMakesController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(FilterModel filterModel, ModelSorting modelSorting, ModelPaging modelPaging)
        {
            ViewBag.Filter = filterModel;
            var filter = _mapper.Map<IFilterModel>(filterModel);
            var sorting = _mapper.Map<IModelSorting>(modelSorting);
            var paging = _mapper.Map<IModelPaging>(modelPaging);

            var makes = await _repository.Make.FindMakeAsync(filter, sorting, paging);
            var makeView = _mapper.Map<IPagedList<IVehicleMake>, IPagedList<VehicleMakeViewModel>>(makes);
            return View(makeView);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Abrv")] VehicleMakeViewModel makeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var make = _mapper.Map<VehicleMake>(makeViewModel);
                    await _repository.Make.AddAsync(make);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Unable to create. Try again.");
            }
            return View(makeViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var make = await _repository.Make.GetByIdAsync(id);
            if (id == 0)
            {
                return NotFound();
            }
            var makeView = _mapper.Map<VehicleMakeViewModel>(make);
            return View(makeView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleMakeViewModel makeViewModel)
        {
            if (ModelState.IsValid)
            {
                var make = _mapper.Map<VehicleMake>(makeViewModel);
                await _repository.Make.UpdateAsync(id, make);
                return RedirectToAction(nameof(Index));
            }
            return View(makeViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var make = await _repository.Make.GetByIdAsync(id);
            if (make == null)
            {
                return NotFound();
            }
            var makeView = _mapper.Map<VehicleMakeViewModel>(make);
            return View(makeView);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NoContent();
            }
            var make = await _repository.Make.GetByIdAsync(id);
            if (make == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            var makeView = _mapper.Map<VehicleMakeViewModel>(make);
            return View(makeView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var vehicleMake = await _repository.Make.GetByIdAsync(id);
                if (vehicleMake == null)
                {
                    return BadRequest();
                }
                await _repository.Make.DeleteMakeAsync(id);
            }
            catch (DbUpdateException)
            {

                return RedirectToAction(nameof(Delete));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
