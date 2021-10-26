using AnimationCollectionAPI.DAL.DBO;
using AnimationCollectionAPI.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IRepository<Animation> _animationRepository;
        public AnimationController(IRepository<Animation> animationRepository)
        {
            _animationRepository = animationRepository;
        }

        // GET: api/<AnimationController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var animations = await _animationRepository.GetAllAsync();
            return new OkObjectResult(animations);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _animationRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return new OkObjectResult(product);
        }

        // POST api/<AnimationController>
        [HttpPost]
        public async Task<IActionResult> Post(Animation animation)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
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

        // PUT api/<AnimationController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Animation animation)
        {
            if (animation != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
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

        // DELETE api/<AnimationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_animationRepository.Exists(id))
            {
                return NotFound();
            }
            await _animationRepository.DeleteAsync(id);
            return new OkResult();
        }
    }
}
