using SchopenSozlukDtoLayer.Dtos.EntryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukDtoLayer.Dtos.BaslikDtos
{
    public class BaslikDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UserId { get; set; }
        public List<EntryDto> Entries { get; set; }
    }
}
