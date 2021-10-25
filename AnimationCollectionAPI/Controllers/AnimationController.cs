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
        public IActionResult Get()
        {
            var animations = _animationRepository.GetAll();
            return new OkObjectResult(animations);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var product = _animationRepository.GetById(id);
            return new OkObjectResult(product);
        }

        // POST api/<AnimationController>
        [HttpPost]
        public IActionResult Post([FromBody] Animation animation)
        {
            using (var scope = new TransactionScope())
            {
                _animationRepository.Create(animation);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = animation.Id }, animation);
            }
        }

        // PUT api/<AnimationController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Animation animation)
        {
            if (animation != null)
            {
                using (var scope = new TransactionScope())
                {
                    _animationRepository.Update(animation);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE api/<AnimationController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var animation = _animationRepository.GetById(id);
            if(animation == null)
            {
                return new NoContentResult();
            }
            _animationRepository.Delete(animation);
            return new OkResult();
        }
    }
}
