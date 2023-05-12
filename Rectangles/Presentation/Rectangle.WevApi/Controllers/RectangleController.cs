using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rectangle.Application.Rectangles.Queries;
using Rectangle.WebApi.Models;

namespace Rectangle.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class RectangleController : BaseController
    {
        private readonly IMapper _mapper;

        public RectangleController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<RectangleListVm>> SearchRectangles([FromBody] PointsList pointsList)
        {
            var command = _mapper.Map<GetRectanglesSearchQuery>(pointsList);
            var rectangles = await Mediator.Send(command);
            return Ok(rectangles);
        }

    }
}
