using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szachy.Figury
{
    public class Biskup : AFigury, ICloneable
    {
        public Biskup(int x, int y, Image image, bool isOnBlack, bool isBlack) : base(x, y, image, isOnBlack, isBlack)
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
        

            for(int i = 1; i < 8; i++)
            {
                if (!isThereAnythingTheSameColour(grid, this.x + i, this.y + i))
                {
                    listOfMovements.Add((this.x + i, this.y + i));

                    if (IsThereAnything(grid, this.x + i, this.y + i))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }

            }
            for (int i = 1; i < 8; i++)
            {
                if (!isThereAnythingTheSameColour(grid, this.x - i, this.y - i))
                {
                    listOfMovements.Add((this.x - i, this.y - i));
                    if (IsThereAnything(grid, this.x - i, this.y - i))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
                
            }
            for (int i = 1; i < 8; i++)
            {
                if (!isThereAnythingTheSameColour(grid, this.x + i, this.y - i))
                {
                    listOfMovements.Add((this.x + i, this.y - i));
                    if (IsThereAnything(grid, this.x + i, this.y - i))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
                
            }
            for (int i = 1; i < 8; i++)
            {
                if (!isThereAnythingTheSameColour(grid, this.x - i, this.y + i))
                {
                    listOfMovements.Add((this.x - i, this.y + i));
                    if (IsThereAnything(grid, this.x - i, this.y + i))
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
                
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

        private bool IsThereAnything(List<(int x, int y, BetterPB pb)> grid, int v1, int v2)
        {
           if(grid.Any(x=> x.x == v1 && x.y == v2 && x.pb.aFigura!=null))
            {
                return true;

            }
           return false;
        }

        //Te funkcje są do implementacji czy usunięcia?
        public override void OnBeforeMovement()
        {


        }
        public override void MakeAMove()
        {

        }
        public override object Clone()
        {
            var clone = (Biskup)this.MemberwiseClone();
            return clone;
        }


        public override void OnAfterMovement()
        { 
        }
    }
}
