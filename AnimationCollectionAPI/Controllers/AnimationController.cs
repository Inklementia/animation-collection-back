using AnimationCollectionAPI.DAL.DBO;
using AnimationCollectionAPI.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnimationCollectionAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnimationController : ControllerBase
    {
        // retrieving generic repository of Animation
        private readonly IRepository<Animation> _animationRepository;
        public AnimationController(IRepository<Animation> animationRepository)
        {
            _animationRepository = animationRepository;
        }

        /// <summary>
        /// Gets the list of all Animations.
        /// </summary>
        /// <returns>The list of Animations.</returns>
        /// <response code="200">If all requested Animations are found</response>
        // GET: api/Animation
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get()
        {
            // retrieveing all animations
            var animations = await _animationRepository.GetAllAsync();
            return new OkObjectResult(animations);
        }

        /// <summary>
        /// Gets the details of a single Animation.
        /// </summary>
        /// <returns>A aingle Animation.</returns>
        /// <response code="200">If requested Animation was found</response>
        /// <response code="404">If requested Animation was not found</response>      
        // GET: api/Animation/{id}
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            // retrieveing animation of id
            var product = await _animationRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return new OkObjectResult(product);
        }

        /// <summary>
        /// Create a new Animation.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Animation
        ///     {        
        ///       "title": "The Legend of Vox Machina",
        ///       "author": "John Doe",
        ///       "description": "This is my first award-winning animation of 2020",
        ///       "link" : "(any youtube link)"
        ///     }
        /// </remarks>
        /// <param name="animation"></param>     
        /// <returns>A newly created Animation.</returns>
        /// <response code="201">If returns the newly created Animation</response>
        /// <response code="400">If created Animation is null</response>          
        // POST: api/Animation
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(Animation animation)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // creating new Animation
                    await _animationRepository.CreateAsync(animation);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                finally
                {
                    scope.Complete();
                }
                return CreatedAtAction(nameof(Get), new { id = animation.Id }, animation);
            }
        }
        /// <summary>
        /// Update a selected Animation.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/Animation/{id}
        ///     {  
        ///       "id" : {id}
        ///       "title": "New title",
        ///       "author": "New author",
        ///       "description": "New Description",
        ///       "link" : "New YouTube Link"
        ///     }
        /// </remarks>
        /// <param name="animation"></param>     
        /// <returns>A newly updated Animation.</returns>
        /// <response code="200">If requested Animation was updated</response>
        /// <response code="400">If requested Animation was not updated</response>   
        // PUT: api/Animation/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(int id, Animation animation)
        {
            // if there is an animation of this id
            if (animation != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        // updating Animation of id 
                        await _animationRepository.UpdateAsync(animation);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    finally
                    {
                        scope.Complete();
                    }
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }
        /// <summary>
        /// Delete a selected Animation.
        /// </summary>
        /// <response code="200">If requested Animation was deleted</response>
        /// <response code="404">If requested Animation was not found</response>   
        // DELETE: api/Animation/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            // checking if animation of this id exists
            if (!_animationRepository.Exists(id))
            {
                return NotFound();
            }
            // deleting Animation of id
            await _animationRepository.DeleteAsync(id);
            return new OkResult();
        }
    }
}
