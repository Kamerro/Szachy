using System;
using System.Collections.Generic;
using System.Drawing;
using Szachy.Grid;

namespace Szachy.Figury
{
    
    public interface IAFigury:ICloneable
    {
        Image image { get; set; }
        Siatka siatka { get; set; }
        int x { get; set; }
        int y { get; set; }
        bool is_black { get; set; }
        List<(int x, int y)> calculatePossibleMovements();
        List<(int x, int y)> getList();
        (int x, int y) getPosition();
    }
}

