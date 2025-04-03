using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutScore.Application.DTOs.Match;
using FutScore.Domain.Entities;
using FutScore.Application.DTOs.Team;

namespace FutScore.Application.DTOs.Stadium
{
    public class CreateStadiumDto
    {
        public required string Name { get; set; }
        public required string City { get; set; }
        public required int Capacity { get; set; }
        public required DateTime OpeningDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}
