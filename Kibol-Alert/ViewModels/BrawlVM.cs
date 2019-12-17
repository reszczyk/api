﻿using Kibol_Alert.Models;
using System;

namespace Kibol_Alert.ViewModels
{
    public class BrawlVM
    {
        public int Id { get; set; }
        public string FirstClubName { get; set; }
        public string SecondClubName { get; set; }
        public DateTime Date { get; set; }
        public Location Location { get; set; }
    }
}
