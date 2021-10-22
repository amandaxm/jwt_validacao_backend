using jwt_second_version.Data;
using jwt_second_version.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jwt_second_version.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : Controller
    {
        private readonly ApplicationDataContext context;

        public UserInfoController(ApplicationDataContext context)
        {
            this.context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfo>> GetUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return user;
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<UserInfo>> PostUsers(UserInfo user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = user.UserId, user });
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserInfo>> Put(int id, UserInfo user)
        {
            if (id != user.UserId)
                return BadRequest();
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = user.UserId, user });

        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserInfo>> Delete(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
                return BadRequest();
            await context.SaveChangesAsync();

            return user;
        }
    }
}
