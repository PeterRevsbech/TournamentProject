﻿using System.Collections.Generic;
using TournamentProj.Model;

namespace TournamentProj.Services
{
    public interface IPlayerService
    {
        Player Create(Player player);

        Player Get(int id);

        IEnumerable<Player> GetAll();
        
        Player Delete(int id);

        Player Update(Player player);
    }
}