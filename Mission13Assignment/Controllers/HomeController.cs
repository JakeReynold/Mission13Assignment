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

        //Action that is called when the user navigates to the Index Page
        public IActionResult Index(string teamName)
        {
            var context = _repo.Bowlers
                .Where(x => x.Team.TeamName == teamName || teamName == null)
                .ToList();

            ViewBag.Teams = _repo.Teams.ToList();
            ViewBag.TeamName = teamName;

            return View(context);
        }

        //Action that calls for the Add Bowler view
        [HttpGet]
        public IActionResult AddBowler()
        {
            //Creates a viewbag of teams for the dropdown
            ViewBag.Teams = _repo.Teams.ToList();

            return View();
        }

        //Post action to add a new bowler
        [HttpPost]
        public IActionResult AddBowler(Bowler bowler)
        {
            //If the model state is invalid, the form will reload with error messages
            if (!ModelState.IsValid)
            {
                ViewBag.Teams = _repo.Teams.ToList();
                return View();
            }

            //saves the bowler
            _repo.SaveBowler(bowler);

            //sends user to a confirmation page
            return View("Confirmation", bowler);

        }

        //Action to call the Edit page
        [HttpGet]
        public IActionResult Edit(int bowlerID)
        {
            //Gets information of the bowler selected
            var bowler = _repo.Bowlers.Single(x => x.BowlerID == bowlerID);

            //passes the viewbag of teams
            ViewBag.Teams = _repo.Teams.ToList();

            return View("AddBowler", bowler);
        }

        //Posts the update bowler information to the database
        [HttpPost]
        public IActionResult Edit(Bowler bowler)
        {
            //stops the post from running if the model is invalid
            if (!ModelState.IsValid)
            {
                ViewBag.Teams = _repo.Teams.ToList();
                return View("AddBowler", bowler);
            }

            //posts the updated bowler information to the database
            _repo.EditBowler(bowler);
            return RedirectToAction("Index");

        }

        //Passes the bowler information to the delete page
        [HttpGet]
        public IActionResult Delete(int bowlerID)
        {
            //Gets correct bowler information
            var bowler = _repo.Bowlers.Single(x => x.BowlerID == bowlerID);
            ViewBag.Teams = _repo.Teams.ToList();

            return View(bowler);
        }

        //Deletes bowler information from the database
        [HttpPost]
        public IActionResult Delete(Bowler bowler)
        {
            //Deletes the bowler, returns to the index page
            _repo.DeleteBowler(bowler);
            return RedirectToAction("Index");
        }

    }
}
