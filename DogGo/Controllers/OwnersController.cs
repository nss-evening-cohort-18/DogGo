using DogGo.Models;
using DogGo.Models.Filters;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;

        public OwnersController(
            IOwnerRepository ownerRepo,
            IDogRepository dogRepo,
            IWalkerRepository walkerRepo,
            INeighborhoodRepository neighborhoodRepo)
        {
            _ownerRepo = ownerRepo;
            _dogRepo = dogRepo;
            _walkerRepo = walkerRepo;
            _neighborhoodRepo = neighborhoodRepo;
        }

        // GET: OwnersController
        public ActionResult Index()
        {
            var owners = _ownerRepo.GetAllOwners();

            return View(owners);
        }

        // GET: OwnersController/Details/5
        public ActionResult Details(int id)
        {
            //TODO - figure out why this happens twice for owner 1.
            Owner? owner = _ownerRepo.GetOwnerById(id);
            if (owner == null) { return NotFound(); }//added this to get around the random double load for Owner 1
            owner.Dogs = _dogRepo.GetDogs(new DogFilter { OwnerId = id });
            List<Walker> walkers = _walkerRepo.GetWalkersByNeighborhood(owner.NeighborhoodId);

            ProfileViewModel vm = new()
            {
                Owner = owner,
                Walkers = walkers,
            };

            return View(vm);
        }

        // GET: OwnersController/Create
        public ActionResult Create()
        {
            var vm = new OwnerFormViewModel()
            {
                Owner = new Owner(),
                Neighborhoods = _neighborhoodRepo.GetAllNeighborhoods(),
            };

            return View(vm);
        }

        // POST: OwnersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OwnerFormViewModel viewModel)
        {
            try
            {
                _ownerRepo.AddOwner(viewModel.Owner);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                viewModel.Neighborhoods = _neighborhoodRepo.GetAllNeighborhoods();
                return View(viewModel);
            }
        }

        // GET: OwnersController/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = new OwnerFormViewModel()
            {
                Owner = _ownerRepo.GetOwnerById(id),
                Neighborhoods = _neighborhoodRepo.GetAllNeighborhoods(),
            };

            if (vm.Owner == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: OwnersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OwnerFormViewModel viewModel)
        {
            try
            {
                _ownerRepo.UpdateOwner(viewModel.Owner);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(viewModel);
            }
        }

        // GET: OwnersController/Delete/5
        public ActionResult Delete(int id)
        {
            Owner? owner = _ownerRepo.GetOwnerById(id);

            return View(owner);
        }

        // POST: OwnersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Owner ownerToDelete)
        {
            try
            {
                _ownerRepo.DeleteOwner(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(ownerToDelete);
            }
        }
    }
}
