﻿using System.Collections.Generic;
using TournamentProj.DTO.Draw;
using TournamentProj.Model;


public interface IDrawMapper
{
    public Draw FromDTO(DrawDTO dto);
    public DrawDTO ToDTO(Draw draw);

    public IEnumerable<DrawDTO> ToDtoArray(IEnumerable<Draw> draws);
}