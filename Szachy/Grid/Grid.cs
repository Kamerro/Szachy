using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Szachy.Figury;

namespace Szachy.Grid
{
    public class Siatka
    {
        private static readonly Siatka grid = new Siatka();
        public static Siatka Instance
        {
            get
            {
                return grid;
            }
        }
        private Siatka() {

        }
        //Ta metoda w zasadzie nic nie robi, a jest aż w 7 miejscach używana?
        public static Siatka initializeGrid()
        {
            return grid;
        }
        private readonly List<(int x, int y, BetterPB pb)> theGrid = new List<(int x, int y, BetterPB pb)>();
        public List<(int x, int y, BetterPB pb)> getTheGrid()
        {
            return theGrid;
        }
        public void pushGrid((int x, int y, BetterPB pb) obj)
        {
            theGrid.Add(obj);
        }

    }
}
