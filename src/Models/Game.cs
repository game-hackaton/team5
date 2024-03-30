using thegame.Services;

namespace thegame.Models;

public class Game
{
    public int[,] Cells;

    public Game()
    {
        Cells = new int[4, 4];
    }
    
    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                MoveUp();
                break;
            case Direction.Right:
                MoveRight();
                break;
            case Direction.Down:
                MoveDown();
                break;
            case Direction.Left:
                MoveLeft();
                break;
            
        }
    }

    private bool IsEmptyCell(int x,int y)
    {
        return Cells[y, x]==0;
    }

    private void MoveUp()
    {
        for (var y = 1; y < Cells.Length; y++)
        {

        }
    }
    
    private void MoveRight()
    {
        throw new System.NotImplementedException();
    }
    
    private void MoveDown()
    {
        throw new System.NotImplementedException();
    }
    
    private void MoveLeft()
    {
        throw new System.NotImplementedException();
    }



    public GameDto ToDTO()
    {
        return TestData.AGameDto(new VectorDto { X = 1, Y = 1 });
    }
}