﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukEntityLayer.Concrete
{
    public class Dislike
    {
        public int Id { get; set; }
        public int EntryId { get; set; }
        public Entry Entry { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}
