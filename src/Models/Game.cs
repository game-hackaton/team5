using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using thegame.Services;

namespace thegame.Models;

public class Game
{
    public Cell[,] Cells;
    public int cellId = 1;

    public Game()
    {
        Cells = new Cell[4, 4];
        Cells[0, 0] = new Cell(cellId, 2);
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

    private bool IsEmptyCell(int x, int y)
    {
        return Cells[y, x] == null;
    }

    private bool IsCanMergeCells(Cell cell1, Cell cell2)
    {
        return cell1.value == cell2.value;
    }

    private void MoveUp()
    {
        for (var y = 1; y < Cells.Length; y++)
        {
            for (var x = 0; x < Cells.Length; x++)
            {
                if (Cells[y, x] == null)
                    continue;
                while (y != 0)
                {
                    if (IsEmptyCell(x, y - 1))
                    {
                        Cells[y - 1, x] = Cells[y, x];
                        Cells[y, x] = null;
                        continue;
                    }

                    if (!IsCanMergeCells(Cells[y - 1, x], Cells[y, x])) continue;
                    
                    Cells[y - 1, x] = Cells[y - 1, x] with { value = Cells[y - 1, x].value + Cells[y, x].value };
                    Cells[y, x] = null;

                    y--;
                }
            }
        }
    }

    private void MoveRight()
    {
        for (var x = Cells.Length; x >0; x--)
        {
            for (var y = 0; y < Cells.Length; y++)
            {
                if (Cells[y, x] == null)
                    continue;
                while (x != Cells.Length)
                {
                    if (IsEmptyCell(x, y - 1))
                    {
                        Cells[y - 1, x] = Cells[y, x];
                        Cells[y, x] = null;
                        continue;
                    }

                    if (!IsCanMergeCells(Cells[y - 1, x], Cells[y, x])) continue;
                    
                    Cells[y - 1, x] = Cells[y - 1, x] with { value = Cells[y - 1, x].value + Cells[y, x].value };
                    Cells[y, x] = null;
                    x++;
                }
            }
        }
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
        return new GameDto(GetCells(), true, false, Cells.GetLength(0), Cells.GetLength(0), Guid.Empty, false, 0);
        return TestData.AGameDto(new VectorDto { X = 1, Y = 1 });
    }

    public CellDto[] GetCells()
    {
        var res = new List<CellDto>();
        Cell cell;
        for (var i = 0; i < Cells.GetLength(0); i++)
        for (var j = 0; j < Cells.GetLength(1); j++)
        {
            if (!(Cells[i,j] is null) && Cells[i, j].id != 0)
            {
                cell = Cells[i, j];
                res.Add(new CellDto(cell.id.ToString(), new VectorDto { X = j, Y = i }, "color1", cell.value.ToString(), 20));
            }
        }

        return res.ToArray();
    }
}