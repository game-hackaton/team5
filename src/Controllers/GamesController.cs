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
        var gameId = Guid.Empty;//Guid.NewGuid();
        _repository.Add(gameId);
        return Ok(_repository.Get(gameId).ToDTO());
    }
    
    [HttpPost("{gameId}")]
    public IActionResult Index([FromRoute] Guid gameId)
    {
        return Ok(_repository.Get(gameId).ToDTO());
    }
}