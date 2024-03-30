using System;
using System.Collections.Generic;
using thegame.Models;

namespace thegame.Services;

public class GamesRepository
{
    private Dictionary<Guid, Game> dict;

    public GamesRepository()
    {
        dict = new Dictionary<Guid, Game>();
    }
    public void Add(Guid id)
    {
        dict.Add(id, new Game());
    }

    public Game Get(Guid id)
    {
        return dict[id];
    }
}