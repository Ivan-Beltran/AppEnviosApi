using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.User;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }


        // GET: UserController
       

        // GET: UserController/Details/5
        

        // GET: UserController/Create
        

        // POST: UserController/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]  CreateUserDTO createUserDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.CreateAdminGlobal(createUserDTO);

            return Ok(result);
        }

        // GET: UserController/Edit/5
     

        // POST: UserController/Edit/5
       

        // GET: UserController/Delete/5
       

        // POST: UserController/Delete/5
       
        
    }
}
