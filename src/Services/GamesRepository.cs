using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using thegame.Models;

namespace thegame.Services;

public class GamesRepository
{
    private ConcurrentDictionary<Guid, Game> dict;

    public GamesRepository()
    {
        dict = new ConcurrentDictionary<Guid, Game>();
    }
    public void Add(Guid id, Game game)
    {
        dict.TryAdd(id, game);
    }

    public Game Get(Guid id)
    {
        return dict[id];
    }
}