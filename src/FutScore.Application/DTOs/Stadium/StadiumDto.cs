using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutScore.Application.DTOs.Match;

namespace FutScore.Application.DTOs.Stadium
{
    public class StadiumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Capacity { get; set; }
        public string? ImageUrl { get; set; }

        public virtual ICollection<MatchDto> Matches { get; set; }
    }
}
