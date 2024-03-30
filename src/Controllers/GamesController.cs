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
        var gameId = Guid.NewGuid();//Guid.NewGuid();
        _repository.Add(gameId, new Game());
        var game = _repository.Get(gameId);
        return Ok(new GameDto(game.GetCells(), true, false, game.Cells.GetLength(1), game.Cells.GetLength(0), gameId, false, 0));
    }
    
    [HttpPost("{gameId}")]
    public IActionResult Index([FromRoute] Guid gameId)
    {
        var game = _repository.Get(gameId);
        return Ok(new GameDto(game.GetCells(), true, false, game.Cells.GetLength(1), game.Cells.GetLength(0), gameId, false, 0));
    }
}