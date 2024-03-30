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
        Cells[0, 0] = new Cell(1, 2);
        Cells[1, 0] = new Cell(2, 2);
        Cells[1, 1] = new Cell(3, 4);
        Cells[1, 2] = new Cell(4, 4);
        Cells[2, 2] = new Cell(5, 8);
        Cells[3, 3] = new Cell(6, 2);
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
                else
                {
                    ans.Add(list[i] with{});
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
                else
                {
                    ans.Add(list[i] with{});
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
    
     private void MoveRight()
    {
        for (var y = 0; y < Cells.GetLength(0); y++)
        {

            for (var x = Cells.GetLength(1) - 1; x >= 0; x--)
            {
                if (Cells[y, x] == null)
                    continue;

                var currx = x;
                while (currx != Cells.GetLength(1) - 1)
                {
                    if (IsEmptyCell(currx + 1, y))
                    {
                        Cells[y, currx + 1] = Cells[y, currx];
                        Cells[y, currx] = null;
                        currx++;
                        continue;
                    }

                    if (!IsCanMergeCells(Cells[y, currx + 1], Cells[y, currx]))
                    {
                        currx++;
                        continue;
                    }

                    Cells[y, currx + 1] = Cells[y, currx + 1] with
                    {
                        value = Cells[y, currx + 1].value + Cells[y, currx].value
                    };
                    Cells[y, currx] = null;
                    currx++;
                }
            }
        }
    }

    private void MoveLeft()
    {
        for (var y = 0; y < Cells.GetLength(0); y++)
        {
            for (var x = 0; x <= Cells.GetLength(1) - 1; x++)
            {
                if (Cells[y, x] == null)
                    continue;

                var currx = x;
                while (currx != 0)
                {
                    if (IsEmptyCell(currx - 1, y))
                    {
                        Cells[y, currx - 1] = Cells[y, currx];
                        Cells[y, currx] = null;
                        currx--;
                        continue;
                    }

                    if (!IsCanMergeCells(Cells[y, currx - 1], Cells[y, currx])) continue;

                    Cells[y, currx - 1] = Cells[y, currx - 1] with
                    {
                        value = Cells[y, currx - 1].value + Cells[y, currx].value
                    };
                    Cells[y, currx] = null;
                    currx--;
                }
            }
        }
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