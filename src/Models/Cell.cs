namespace thegame.Models;

public record Cell
{
    public int id;
    public int value;
    private static int maxId = 0;

    public Cell(int value)
    {
        id = ++maxId;
        this.value = value;
    }
};