using CatFacts.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatFacts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatFactsController : ControllerBase
{
    private readonly ICatFactService _catFactService;

    public CatFactsController(ICatFactService catFactService)
    {
        _catFactService = catFactService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var fact = await _catFactService.GetAndSaveFactAsync(cancellationToken);

        return Ok(fact);
    }
}
