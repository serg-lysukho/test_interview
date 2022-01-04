using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace InterviewApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IMapper Mapper;

        public ApiControllerBase(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
