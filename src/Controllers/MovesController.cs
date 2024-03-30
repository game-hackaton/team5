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
        _repository.Get(Guid.Empty).Move(Direction.Down);
        return Ok(1);
    }
}