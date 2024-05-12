using DataAccess.MShop.Entities;
using DataAccess.MShop.IServices;
using DataAccess.MShop.Services;
using DataAccess.MShop.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace MShop.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IMShopUnitOfWork _unitOfWork;

        public PostController(IMShopUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetPosts")]

        public async Task<ActionResult> GetPosts()
        {
            try 
            {
                var listPost = await _unitOfWork._postRepository.GetPosts();
                return Ok(listPost);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("CreatePost")]

        public async Task<ActionResult> CreatePost(Post post)
        {
            try
            {
                await _unitOfWork._postRepository.CreatePost(post);
                var result = _unitOfWork.SaveChange();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
