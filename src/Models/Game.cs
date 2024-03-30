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
        Cells[1, 0] = new Cell(cellId+1, 2);
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
        var list = new List<Cell>();

        for (var x = 0; x < Cells.GetLength(1); x++)
        {
            for (var y = 0; y < Cells.GetLength(0); y++)
            {
                if (!(Cells[y, x] is null))
                {
                    list.Add(Cells[y, x] with { });
                    Cells[y, x] = null;
                }
            }

            var ans = new List<Cell>();
            
            if (list.Count == 1)
                ans = list;
            
            for (var i = 0; i < list.Count-1; i++)
            {
                if (list[i].value == list[i + 1].value)
                {
                    ans.Add(list[i] with{value = list[i].value + list[i + 1].value});
                    i++;
                }
            }

            for (var yy = 0; yy < ans.Count; yy++)
            {
                Cells[yy, x] = ans[0] with{};
                ans.RemoveAt(0);
            }
            list.Clear();
        }
    }

    private void MoveRight()
    {
        for (var x = Cells.Length; x > 0; x--)
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
        //var y = Cells.GetLength(0);
        //var x = Cells.GetLength(1);

        var list = new List<Cell>();

        for (var x = 0; x < Cells.GetLength(1); x++)
        {
            for (var y = Cells.GetLength(0)-1; y >= 0; y--)
            {
                if (!(Cells[y, x] is null))
                {
                    list.Add(Cells[y, x] with { });
                    Cells[y, x] = null;
                }
            }

            var ans = new List<Cell>();
            
            if (list.Count == 1)
                ans = list;
            
            for (var i = 0; i < list.Count-1; i++)
            {
                if (list[i].value == list[i + 1].value)
                {
                    ans.Add(list[i] with{value = list[i].value + list[i + 1].value});
                    i++;
                }
            }

            for (var yy = Cells.GetLength(1) - 1; yy > Cells.GetLength(1) - ans.Count - 1; yy--)
            {
                Cells[yy, x] = ans[0] with{};
                ans.RemoveAt(0);
            }
            list.Clear();
        }
        
        //throw new System.NotImplementedException();
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