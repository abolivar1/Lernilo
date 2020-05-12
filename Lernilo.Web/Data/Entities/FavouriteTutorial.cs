﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lernilo.Web.Data.Entities
{
    public class FavouriteTutorial
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Tutorial Tutorial { get; set; }
    }
}
