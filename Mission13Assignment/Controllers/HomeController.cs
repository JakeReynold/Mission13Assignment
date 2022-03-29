using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13Assignment.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13Assignment.Controllers
{
    public class HomeController : Controller
    {
        private IBowlersRepository _repo { get; set; }

        public HomeController(IBowlersRepository temp)
        {
            _repo = temp;  
        }

        public IActionResult Index(string teamName)
        {
            var context = _repo.Bowlers
                .Where(x => x.Team.TeamName == teamName || teamName == null)
                .ToList();

            ViewBag.Teams = _repo.Teams.ToList();
            ViewBag.TeamName = teamName;

            return View(context);
        }

        [HttpGet]
        public IActionResult AddBowler()
        {
            ViewBag.Teams = _repo.Teams.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult AddBowler(Bowler bowler)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Teams = _repo.Teams.ToList();
                return View();
            }

            _repo.SaveBowler(bowler);
            return View("Confirmation", bowler);

        }

        [HttpGet]
        public IActionResult Edit(int bowlerID)
        {
            var bowler = _repo.Bowlers.Single(x => x.BowlerID == bowlerID);

            ViewBag.Teams = _repo.Teams.ToList();

            return View("AddBowler", bowler);
        }

        [HttpPost]
        public IActionResult Edit(Bowler bowler)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Teams = _repo.Teams.ToList();
                return View();
            }

            _repo.EditBowler(bowler);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int bowlerID)
        {
            var bowler = _repo.Bowlers.Single(x => x.BowlerID == bowlerID);
            ViewBag.Teams = _repo.Teams.ToList();

            return View(bowler);
        }

        [HttpPost]
        public IActionResult Delete(Bowler bowler)
        {
            _repo.DeleteBowler(bowler);
            return RedirectToAction("Index");
        }

    }
}
