using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Entities.NPC
{
    internal class Enemy_NPC
    {
        
        public int Health { get; set; }
        public float Position { get; set; }

        //public Enemy() {}

        public void Reset()
        {
            Health = 0;
            Position = 0;
        }
        
    }
}
