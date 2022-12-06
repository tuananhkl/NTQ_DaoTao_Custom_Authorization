using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomAuthorization.Common.Constants;
using CustomAuthorization.CustomAuthorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomAuthorization.Data;
using CustomAuthorization.Models;

namespace CustomAuthorization.Controllers
{
    //[CustomAuthorize(CustomAuthorizationConfig.USER_ROLE_ALL)]
    public class UserRolesController : Controller
    {
        private readonly AppDbContext _context;

        public UserRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserRoles
        public async Task<IActionResult> Index()
        {
            var userRoles = _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role);
            return View(await userRoles.ToListAsync());
        }

        // GET: UserRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserRoles == null)
            {
                return NotFound();
            }

            var userRole = _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.Id == id);
            
            // var userRole = await _context.UserRoles
            //     .FirstOrDefaultAsync(m => m.Id == id);
            if (userRole == null)
            {
                return NotFound();
            }

            return View(await userRole);
        }

        // GET: UserRoles/Create
        public async Task<IActionResult> Create()
        {
            var users = await _context.Users.ToListAsync();
            var roles = await _context.Roles.ToListAsync();
            
            // PopulateControllersDropDownList();
            // PopulateActionsDropDownList();
            PopulateUserDropDownList();
            
            return View();
        }
        
        // private void PopulateControllersDropDownList(object selectedGroup = null)
        // {
        //     var roleControllersQuery = _context.Roles;
        //
        //     ViewBag.ControllersNames = new SelectList(roleControllersQuery.ToList(), "Id", "Controller", selectedGroup);
        // }
        //
        // private void PopulateActionsDropDownList(object selectedGroup = null)
        // {
        //     var roleActionsQuery = _context.Roles;
        //
        //     ViewBag.ActionsNames = new SelectList(roleActionsQuery.ToList(), "Id", "Action", selectedGroup);
        // }
        
        // private void PopulateRolesDropDownList(object selectedGroup = null)
        // {
        //     var rolesQuery = _context.UserRoles;
        //
        //     ViewBag.RoleNames = new SelectList(rolesQuery.ToList(), "Id", "UserName", selectedGroup);
        // }
        
        private void PopulateUserDropDownList(object selectedGroup = null)
        {
            var usersQuery = _context.Users;
        
            ViewBag.UserNames = new SelectList(usersQuery.ToList(), "Id", "UserName", selectedGroup);
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,RoleId,Status")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userRole);
        }

        // GET: UserRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserRoles == null)
            {
                return NotFound();
            }

            var userRole = _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
            
            // var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole == null)
            {
                return NotFound();
            }
            return View(await userRole);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,RoleId,Status")] UserRole userRole)
        {
            if (id != userRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRoleExists(userRole.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userRole);
        }

        // GET: UserRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserRoles == null)
            {
                return NotFound();
            }

            var userRole = _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
            
            if (userRole == null)
            {
                return NotFound();
            }

            return View(await userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserRoles == null)
            {
                return Problem("Entity set 'AppDbContext.UserRoles'  is null.");
            }
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRoleExists(int id)
        {
          return _context.UserRoles.Any(e => e.Id == id);
        }
    }
}
