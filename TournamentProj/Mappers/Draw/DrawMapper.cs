﻿using System.Collections.Generic;
using TournamentProj.DTO.Draw;
using TournamentProj.Model;

public class DrawMapper : IDrawMapper
{
    public Draw FromDTO(DrawDTO dto)
    {
        var draw = new Draw()
        {
            Name = dto.Name,
            Id = dto.Id
                
        };
       
        return draw;
    }
    
    public DrawDTO ToDTO(Draw draw)
    {
        var dto = new DrawDTO()
        {
            Id = draw.Id,
            Name = draw.Name
        };
       
        return dto;
    }

    public IEnumerable<DrawDTO> ToDtoArray(IEnumerable<Draw> list)
    {
        var dtos = new List<DrawDTO>();
        foreach (var item in list)
        {
            dtos.Add(ToDTO(item));
        }

        return dtos;
    }
}