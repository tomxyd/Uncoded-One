﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uncoded_One
{
    internal class Party
    {
        public List<Character> characters = new List<Character>();

        public void AddCharcter(Character character)
        {
            characters.Add(character);
        }
    }
}
