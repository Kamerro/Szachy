using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szachy.Figury
{
    public class Kon : AFigury, ICloneable
    {
        public Kon(int x, int y, Image image, bool isOnBlack, bool isBlack) : base(x, y, image, isOnBlack, isBlack)
        {
        }
        public override List<(int x, int y)> calculatePossibleMovements()
        {
            return calcAllMovements();
        }

        private List<(int x, int y)> calcAllMovements()
        {
            List<(int x, int y, BetterPB pb)> grid = siatka.getTheGrid();
            List<(int x, int y)> listOfMovements = new List<(int x, int y)>();


         
                if (!isThereAnythingTheSameColour(grid, this.x + 1, this.y-2))
                {
                    listOfMovements.Add((this.x + 1, this.y-2));
                }
                if (!isThereAnythingTheSameColour(grid, this.x + 2, this.y - 1))
                {
                    listOfMovements.Add((this.x + 2, this.y -1));
                }
                if (!isThereAnythingTheSameColour(grid, this.x + 2, this.y + 1))
                {
                    listOfMovements.Add((this.x + 2, this.y + 1));
                }
                if (!isThereAnythingTheSameColour(grid, this.x + 1, this.y + 2))
                {
                    listOfMovements.Add((this.x + 1, this.y + 2));
                }

                if (!isThereAnythingTheSameColour(grid, this.x - 1, this.y - 2))
                {
                    listOfMovements.Add((this.x - 1, this.y - 2));
                }
                if (!isThereAnythingTheSameColour(grid, this.x - 2, this.y - 1))
                {
                    listOfMovements.Add((this.x - 2, this.y - 1));
                }
                if (!isThereAnythingTheSameColour(grid, this.x - 2, this.y + 1))
                {
                    listOfMovements.Add((this.x - 2, this.y + 1));
                }
                if (!isThereAnythingTheSameColour(grid, this.x - 1, this.y +2))
                {
                    listOfMovements.Add((this.x - 1, this.y+2));
                }


            return listOfMovements;
        }

        private bool isThereAnythingTheSameColour(List<(int x, int y, BetterPB pb)> grid, int v1, int v2)
        {
            if (grid.Any(x => x.x == v1 && x.y == v2 && x.pb.aFigura != null && x.pb.aFigura.is_black == this.is_black))
            {
                return true;

            }
            return false;
        }

        public override void OnBeforeMovement()
        {


        }
        public override void MakeAMove()
        {

        }
        public override object Clone()
        {
            var clone = (Kon)this.MemberwiseClone();
            return clone;
        }


        public override void OnAfterMovement()
        {
        }
    }
}
