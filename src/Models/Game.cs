using System;
using System.Collections.Generic;
using System.Linq;


namespace thegame.Models;

public class Game
{
    public Cell[,] Cells;
    public bool isFinished = false;

    private static int maxId = 0;

    public Game()
    {
        Cells = new Cell[4, 4];
        Cells[0, 0] = new Cell(++maxId, 2);
        Cells[1, 0] = new Cell(++maxId, 2);
        Cells[1, 1] = new Cell(++maxId, 2);
        Cells[1, 2] = new Cell(++maxId, 2);
        Cells[1, 3] = new Cell(++maxId, 2);
    }

    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                MoveVertical(false);
                break;
            case Direction.Right:
                MoveHorizontal(true);
                break;
            case Direction.Down:
                MoveVertical(true);
                break;
            case Direction.Left:
                MoveHorizontal(false);
                break;
        }
        
        GenerateNewCell();
    }
    
    private void GenerateNewCell()
    {
        var random = new Random();
        var value = random.NextDouble() < 0.8 ? 2 : 4;
        var emptyCells = new List<int[]>();
        for (var x =0; x<Cells.GetLength(1);x++)
        {
            for (var y = 0; y<Cells.GetLength(0);y++)
            {
                if (Cells[y,x]==null)
                    emptyCells.Add(new int[]{y,x});
            }
        }

        if (emptyCells.Count == 0)
        {
            isFinished = true;
            return;
        }
        var elemToUpdate = emptyCells[random.Next(emptyCells.Count - 1)];
        Cells[elemToUpdate[0], elemToUpdate[1]] = new Cell(++maxId, value);

    }
    
    private void MoveHorizontal(bool isReversed)
    {
        var width = Cells.GetLength(0);
        var height = Cells.GetLength(1);
        var collumns = Enumerable
            .Range(0, width)
            .Select(x => Enumerable
                .Range(0, height)
                .Where(y => Cells[x, y] != null)
                .Select(y => Cells[x, y])
                .ToArray())
            .ToArray();
        collumns = Compress(collumns, isReversed);
        Cells = new Cell[4, 4];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < collumns[x].Length; y++)
            {
                if (isReversed)
                    Cells[x, height - y - 1] = collumns[x][collumns[x].Length - y - 1];
                else
                    Cells[x, y] = collumns[x][y];
            }
        }
    }
    
    private void MoveVertical(bool isReversed)
    {
        var width = Cells.GetLength(0);
        var height = Cells.GetLength(1);
        var rows = Enumerable
            .Range(0, height)
            .Select(y => Enumerable
                .Range(0, width)
                .Where(x => Cells[x, y] != null)
                .Select(x => Cells[x, y])
                .ToArray())
            .ToArray();
        rows = Compress(rows, !isReversed);
        Cells = new Cell[4, 4];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < rows[y].Length; x++)
            {
                if (isReversed)
                    Cells[width - x - 1, y] = rows[y][rows[y].Length - x - 1];
                else
                    Cells[x, y] = rows[y][x];
            }
        }
    }

    private Cell[][] Compress(Cell[][] fragments, bool isReversed)
    {
        var result = new List<Cell[]>();
        foreach (var _fragment in fragments)
        {
            var newHeight = new List<Cell>();
            var fragment = _fragment;
            if (isReversed)
                fragment = fragment.Reverse().ToArray();
            for (int i = 0; i < fragment.Length; i++)
            {
                if (i+1 >= fragment.Length)
                {
                    newHeight.Add(fragment[i]);
                    break;
                }
                if (fragment[i].value == fragment[i+1].value)
                {
                    newHeight.Add(new Cell(id: ++maxId, value: 2 * fragment[i].value));
                    i++;
                }
                else
                    newHeight.Add(fragment[i]);
            }
            if (isReversed)
                newHeight.Reverse();
            result.Add(newHeight.ToArray());
        }

        return result.ToArray();
    }


    public GameDto ToDTO()
    {
        return new GameDto(GetCells(), true, false, Cells.GetLength(0), Cells.GetLength(0), Guid.Empty, isFinished, 0);
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
                res.Add(new CellDto(cell.id.ToString(), new VectorDto { X = j, Y = i }, $"tile-{cell.value}", cell.value.ToString(), 20));
            }
        }

        return res.ToArray();
    }
}