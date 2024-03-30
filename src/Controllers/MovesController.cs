using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers;

[Route("api/games/{gameId}/moves")]
public class MovesController : Controller
{
    private readonly GamesRepository _repository;

    public MovesController(GamesRepository repository)
    {
        _repository = repository;
    }
    [HttpPost]
    public IActionResult Moves(Guid gameId, [FromBody]UserInputDto userInput)
    {
        var keyIndex = userInput.KeyPressed;
        var dir = keyIndex switch
        {
            37 => Direction.Left,
            38 => Direction.Up,
            39 => Direction.Right,
            40 => Direction.Down,
            _ => throw new Exception("Wrong key code")
        };
        _repository.Get(gameId).Move(dir);
        return Ok();
    }
}