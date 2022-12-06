using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomAuthorization.Data;
using CustomAuthorization.Interfaces;

namespace CustomAuthorization.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;

        public UserController(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            // var httpContext = _httpContextAccessor.HttpContext;
            // if(httpContext != null && httpContext.Session.GetString("username") is null)
            // {
            //     //ViewData["authError"] = "You didn't have permission. Please login";   
            //     //TempData["authError"] = "You didn't have permission. Please <a asp-area=\"\" asp-controller=\"Account\" asp-action=\"Index\">Login</a>";
            //     TempData["authError"] = "You didn't have permission. Please Login";
            //     
            //     return RedirectToRoute(new { controller = "Home", action = "Index" });
            // }
            
            var users = await _userRepository.GetAll();
            if (users is null)
            {
                return Problem("Can't find any users");
            }
            
            return View(users.ToList());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id < 0)
            {
                return BadRequest("Id must be >= 0");
            }

            var user = await _userRepository.GetById(id);
            if (user is null)
            {
                return NotFound($"User with id {id} is not found");
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_groupRepository.GetAll(), "Id", "GroupName");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,DateOfBirth,Address,Email,Age,Gender,GroupId,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id < 0)
            {
                return BadRequest("Id must be >= 0");
            }

            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound($"User with id {id} is not found");
            }
            
            PopulateGroupsDropDownList(user.GroupId);
            
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,DateOfBirth,Address,Email,Age,Gender,GroupId,Status")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userRepository.Update(user);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupName"] = new SelectList(_groupRepository.GetAll(), "GroupName", "GroupName", user.Group.GroupName);

            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 0)
            {
                return BadRequest("Id must be >= 0");
            }

            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound($"User with id {id} is not found");
            }
            
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user != null)
            {
                await _userRepository.Delete(user);
                
                //safe delete
                //await _userRepository.SafeDelete(user);
            }

            return RedirectToAction(nameof(Index));
        }

        private void PopulateGroupsDropDownList(object selectedGroup = null)
        {
            var groupsQuery = _groupRepository.GetGroupOrderByName();
        
            ViewBag.GroupId = new SelectList(groupsQuery.AsNoTracking(), "Id", "GroupName", selectedGroup);
        }
    }
}
