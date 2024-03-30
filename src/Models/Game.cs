using thegame.Services;

namespace thegame.Models;

public class Game
{
    public int[,] Cells;

    public Game()
    {
        Cells = new int[3, 3];
    }
    
    public void Move(Direction direction)
    {
        
    }

    public GameDto ToDTO()
    {
        return TestData.AGameDto(new VectorDto { X = 1, Y = 1 });
    }
}