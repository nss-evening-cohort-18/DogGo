using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class WalksController : Controller
    {
        private readonly IWalkerRepository _walkerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkRepository _walkRepo;
        public WalksController(IWalkerRepository walkerRepo, IDogRepository dogRepo, IWalkRepository walkRepo)
        {
            _walkerRepo = walkerRepo;
            _dogRepo = dogRepo;
            _walkRepo = walkRepo;
        }
        // GET: WalksController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WalksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WalksController/Create
        public ActionResult Create()
        {
            WalkCreateViewModel vm = new WalkCreateViewModel
            {
                Walk = new Walk(),
                WalkerOptions = _walkerRepo.GetAllWalkers(),
                DogOptions = _dogRepo.GetDogs(),
            };
            return View(vm);
        }

        // POST: WalksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WalkCreateViewModel viewModel)
        {
            try
            {
                foreach (int dogId in viewModel.SelectedDogIds)
                {
                    viewModel.Walk.DogId = dogId;
                    _walkRepo.CreateWalk(viewModel.Walk);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: WalksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
