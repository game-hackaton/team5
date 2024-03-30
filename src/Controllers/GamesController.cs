using System;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers;

[Route("api/games")]
public class GamesController : Controller
{
    private readonly GamesRepository _repository;

    public GamesController(GamesRepository repository)
    {
        _repository = repository;
    }
    
    [HttpPost]
    public IActionResult Start()
    {
        _repository.Add(Guid.Empty);
        return Ok(_repository.Get(Guid.Empty).ToDTO());
    }
    
    [HttpPost("{gameId}")]
    public IActionResult Index([FromRoute] int gameId)
    {
        return Ok(_repository.Get(Guid.Empty).ToDTO());
    }
}