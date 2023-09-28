using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylist.LinkedList
{
    public class Node
    {
        public Song Data { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }

        public Node(Song data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
    }
}
