using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers;

[Route("api/games")]
public class GamesController : Controller
{
    public static Game game;
    public GamesController()
    {
        
    }
    
    [HttpPost]
    public IActionResult Start()
    {
        game = new Game();
        return Ok(game.ToDTO());
    }
    
    [HttpPost("{gameId}")]
    public IActionResult Index([FromRoute] int gameId)
    {
        return Ok(game.ToDTO());
    }
}