using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szachy.Figury
{
    public class Krol : AFigury, ICloneable
    {
        public bool czyZrobilRuch = false;
        public Wieza wiezaLewa = null;
        public Wieza wiezaPrawa = null;
        public Krol(int x, int y, Image image, bool isOnBlack, bool isBlack) : base(x, y, image, isOnBlack, isBlack)
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

            if (!isThereAnythingTheSameColour(grid, this.x-1, this.y-1))
            {
                listOfMovements.Add((this.x-1, this.y - 1));
            }
            if (!isThereAnythingTheSameColour(grid, this.x, this.y-1))
            {
                listOfMovements.Add((this.x, this.y - 1));
            }
            if (!isThereAnythingTheSameColour(grid, this.x +1, this.y-1))
            {
                listOfMovements.Add((this.x+1, this.y - 1));
            }
            if (!isThereAnythingTheSameColour(grid, this.x +1, this.y))
            {
                listOfMovements.Add((this.x+1, this.y));
            }
            if (!isThereAnythingTheSameColour(grid, this.x+1, this.y + 1))
            {
                listOfMovements.Add((this.x+1, this.y +1));
            }
            if (!isThereAnythingTheSameColour(grid, this.x, this.y + 1))
            {
                listOfMovements.Add((this.x, this.y + 1));
            }
            if (!isThereAnythingTheSameColour(grid, this.x - 1, this.y +1))
            {
                listOfMovements.Add((this.x-1, this.y + 1));
            }
            if (!isThereAnythingTheSameColour(grid, this.x - 1, this.y))
            {
                listOfMovements.Add((this.x-1, this.y));
            }



            if ((!isThereAnything(grid, this.x - 1, this.y) && !isThereAnything(grid, this.x - 2, this.y) && !isThereAnything(grid, this.x - 3, this.y))
              && wiezaLewa != null && !wiezaLewa.czyZrobilRuch && !czyZrobilRuch)
            {
                listOfMovements.Add((this.x - 2, this.y));
            }
            if ((!isThereAnything(grid, this.x + 1, this.y) && !isThereAnything(grid, this.x + 2, this.y))
                && wiezaPrawa != null && !wiezaPrawa.czyZrobilRuch && !czyZrobilRuch)
            {
                listOfMovements.Add((this.x + 2, this.y));
            }

            //Sprawdzenie czy po lewen stronie są odpowiednie warunki, czyli:
            return listOfMovements;
        }

        private bool isThereAnything(List<(int x, int y, BetterPB pb)> grid, int v1, int v2)
        {
            if (grid.Any(x => x.x == v1 && x.y == v2 && x.pb.aFigura != null))
            {
                return true;

            }
            return false;
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
            var clone = (Krol)this.MemberwiseClone();
            return clone;
        }


        public override void OnAfterMovement()
        {
        }

        internal void DodajWiezeLewa(Wieza wieza)
        {
            wiezaLewa = wieza;
        }

        internal void DodajWiezePrawa(Wieza wieza)
        {
            wiezaPrawa = wieza;
        }
        public bool SprawdzCzyIstniejeWiezaPrawa()
        {
            if (wiezaPrawa != null)
            {
                return true;
            }
            return false;
        }
        public bool SprawdzCzyIstniejeWiezaLewa()
        {
            if (wiezaLewa != null)
            {
                return true;
            }
            return false;
        }
    }
}
