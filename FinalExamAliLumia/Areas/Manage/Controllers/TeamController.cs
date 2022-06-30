using FinalExamAliLumia.DAL;
using FinalExamAliLumia.Models;
using FinalExamAliLumia.Utili;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace FinalExamAliLumia.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _evo;

        public TeamController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _evo = environment;
        }
        public IActionResult Index(int page =1)
        {
            return View(_context.Teams.ToList().ToPagedList(page, 3));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {
            if (team.Photo == null)
            {
                ModelState.AddModelError("Photo", "cant null");
                return View(team);
            }
            if (team.Photo.CheckSize(5000))
            {
                ModelState.AddModelError("Photo", "cant up 5000");
                return View(team);
            }
            if (team.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("Photo", "Only image");
                return View(team);
            }
            string path = Path.Combine(_evo.WebRootPath,"assets","imgs","team");
            team.Image = await team.Photo.SaveFile(path);
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            Team team = _context.Teams.Find(id);
            if (team == null) return NotFound();
            return View(team);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Team team)
        {
            Team teamDB = _context.Teams.FirstOrDefault(x=>x.Id == team.Id);
            if (teamDB == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest();
            if(team.Photo != null)
            {
                if (!team.Photo.CheckSize(5000))
                {
                    ModelState.AddModelError("Photo", "cant up 5000");
                    return View(team);
                }
                if (team.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "Only image");
                    return View(team);
                }
                string path = Path.Combine(_evo.WebRootPath, "assets", "imgs", "team");
                team.Image =await team.Photo.SaveFile(path);

                if (System.IO.File.Exists(Path.Combine(path, teamDB.Image)))
                {
                    System.IO.File.Delete(Path.Combine(path, teamDB.Image));
                }
                teamDB.Image = team.Image;
            }
            teamDB.FullName = team.FullName;
            teamDB.Posission = team.Posission;
            teamDB.SocialIcon = team.SocialIcon;
            teamDB.FeedBack = team.FeedBack;
             await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Team team = _context.Teams.Find(id);
            if (team == null) return NotFound();
            string path = Path.Combine(_evo.WebRootPath, "assets", "imgs", "team");
            if (System.IO.File.Exists(Path.Combine(path, team.Image)))
            {
                System.IO.File.Delete(Path.Combine(path, team.Image));
            }
            _context.Teams.Remove(team);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
