﻿using System.Collections.Generic;
using TournamentProj.Model;

namespace TournamentProj.Services.DrawService
{
    public interface IDrawService
    {
        Draw Create(Draw draw);

        Draw CreateAutomatic(DrawCreation drawCreation);

        Draw Get(int id);

        IEnumerable<Draw> GetAll();

        Draw Delete(int id);

        Draw Update(Draw draw);
    }
}