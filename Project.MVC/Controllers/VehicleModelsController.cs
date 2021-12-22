using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class VehicleModelsController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public VehicleModelsController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(FilterModel filterModel, ModelSorting modelSorting, ModelPaging modelPaging)
        {
            await MakesDropdownAsync(filterModel.FilterId);

            var filter = _mapper.Map<IFilterModel>(filterModel);
            var sorting = _mapper.Map<IModelSorting>(modelSorting);
            var paging = _mapper.Map<IModelPaging>(modelPaging);

            var models = await _repository.Model.FindModelsAsync(filter, sorting, paging);

            var modelView = _mapper.Map<IPagedList<IVehicleModel>, IPagedList<VehicleModelViewModel>>(models);


            return View(modelView);
        }

        public async Task<IActionResult> Create()
        {
            await MakesDropdownAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MakeId,Name,Abrv")] VehicleModelViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = _mapper.Map<VehicleModel>(viewModel);
                    await _repository.Model.AddModelAsync(model);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Unable to create. Try again.");
            }
            await MakesDropdownAsync(viewModel.MakeId);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var model = await _repository.Model.GetByIdModelAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var modelView = _mapper.Map<VehicleModelViewModel>(model);
            await MakesDropdownAsync(modelView.MakeId);
            return View(modelView);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleModelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<VehicleModel>(viewModel);
                await _repository.Model.UpdateModelAsync(id, model);
                return RedirectToAction(nameof(Index));
            }
            await MakesDropdownAsync(viewModel.MakeId);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var model = await _repository.Model.GetModelDetailsAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var modelView = _mapper.Map<VehicleModelViewModel>(model);
            return View(modelView);
        }
        public async Task MakesDropdownAsync(object selectMake = null)
        {
            var list = _mapper.Map<IEnumerable<VehicleMakeDropDown>>(await _repository.Make.GetMakesAsync());
            ViewBag.MakesList = new SelectList(list.OrderBy(m => m.Name), "Id", "Name", selectMake);

        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NoContent();
            }

            var vehicleModel = await _repository.Model.GetModelDetailsAsync(id);
            if (vehicleModel == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

            }
            var viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var vehicleModel = await _repository.Model.GetByIdModelAsync(id);
                if (vehicleModel == null)
                {
                    return BadRequest();
                }
                await _repository.Model.DeleteModelAsync(id);
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
