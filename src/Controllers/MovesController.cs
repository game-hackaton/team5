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
        var game = _repository.Get(gameId);
        game.Move(dir);
        return Ok(new GameDto(game.GetCells(), true, false, game.Cells.GetLength(1), game.Cells.GetLength(0), gameId, false, 0));
    }
}