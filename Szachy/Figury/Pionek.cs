using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Szachy.Figury
{
    [Serializable]
    public class Pionek : AFigury,ICloneable
    {
        private bool earlyState;
        bool isFirstMovement = true;
        bool flagaCosStoiPrzed = false;
        bool flagaCosStoi2Przed = false;
        bool cosStoiPoLewej = false;
        bool cosStoiPoPrawej = false;
        private bool isSomethingOnDiagonalLeft;
        private bool isSomethingOnDiagonalRight;
        public bool czyWlasniesnieWykonanoRuchODwa = false;

        public Pionek(int x, int y, Image image, bool isOnBlack, bool isBlack) : base(x, y, image, isOnBlack, isBlack)
        {
        }
        public override List<(int x, int y)> calculatePossibleMovements()
        {
            if (is_black)
            {
             return calcAllBlackMovements();
            }
            else
            {
              return calcAllWhiteMovements();
            }
        }

        private List<(int x, int y)> calcAllWhiteMovements()
        {
            List<(int x, int y,BetterPB pb)> grid = siatka.getTheGrid();
            List<(int x, int y)> listOfMovements = new List<(int x, int y)>();
            flagaCosStoiPrzed = false;
            flagaCosStoi2Przed = false;
            isSomethingOnDiagonalLeft= false;
            isSomethingOnDiagonalRight= false;
            foreach (var obj in grid)
            {
                if (obj.x == this.x && obj.y == this.y + 1 && obj.pb.aFigura != null)
                {
                    flagaCosStoiPrzed = true;
                }
                else
                {
                    
                    if (isFirstMovement)
                    {
                        if (obj.x == this.x && obj.y == this.y + 2 && obj.pb.aFigura != null)
                        {
                            flagaCosStoi2Przed = true;
                        }
                    }
                }
                //BicieDiagonalnie, Można dodatkowo sprawdzić czy po skosie jest pionek, który ruch wcześniej wykonał 
                //ruch o 2 pola do przodu
                if(obj.x == this.x+1 && obj.y == this.y + 1 && obj.pb.aFigura != null)
                {
                    if (!isThereAnythingTheSameColour(grid, this.x + 1, this.y + 1))
                    {
                        isSomethingOnDiagonalRight = true;
                    }
                }
                if (obj.x == this.x - 1 && obj.y == this.y + 1 && obj.pb.aFigura != null)
                {
                    if (!isThereAnythingTheSameColour(grid, this.x - 1, this.y + 1))
                    {
                        isSomethingOnDiagonalLeft = true;
                    }
                }

                
                if((obj.x == this.x + 1 && obj.y == this.y && obj.pb.aFigura is Pionek)
                && (obj.pb.aFigura.is_black)
                && ((obj.pb.aFigura as Pionek).CzyRuchWczesniejWykonalRuchODwaPola()))
                {
                    if (!isThereAnythingTheSameColour(grid, this.x + 1, this.y + 1))
                    {
                        isSomethingOnDiagonalRight = true;
                    }
                }

                if((obj.x == this.x - 1 && obj.y == this.y && obj.pb.aFigura is Pionek)
                 && (obj.pb.aFigura.is_black)
                 && ((obj.pb.aFigura as Pionek).CzyRuchWczesniejWykonalRuchODwaPola()))
                {
                    if (!isThereAnythingTheSameColour(grid, this.x - 1, this.y + 1))
                    {
                        isSomethingOnDiagonalLeft = true;
                    }
                }

            }
            if (!flagaCosStoiPrzed)
            {
                listOfMovements.Add((x, y + 1));
                if (isFirstMovement) {
                    if (!flagaCosStoi2Przed)
                    {
                        listOfMovements.Add((x, y + 2));
                    }
                }
            }
            if (isSomethingOnDiagonalLeft)
            {
                listOfMovements.Add((x-1, y + 1));
            }
            if (isSomethingOnDiagonalRight)
            {
                listOfMovements.Add((x + 1, y + 1));
            }
                return listOfMovements;
        }

        private List<(int x, int y)> calcAllBlackMovements()
        {
            List<(int x, int y, BetterPB pb)> grid = siatka.getTheGrid();
            List<(int x, int y)> listOfMovements = new List<(int x, int y)>();
            flagaCosStoiPrzed = false;
            flagaCosStoi2Przed = false;
            isSomethingOnDiagonalLeft = false;
            isSomethingOnDiagonalRight = false;
            foreach (var obj in grid)
            {
                if (obj.x == this.x && obj.y == this.y - 1 && obj.pb.aFigura != null)
                {
                    flagaCosStoiPrzed = true;
                }
                else
                {
                    if (isFirstMovement)
                    {
                        if (obj.x == this.x && obj.y == this.y - 2 && obj.pb.aFigura != null)
                        {
                            flagaCosStoi2Przed = true;
                        }
                    }
                }
                if (obj.x == this.x + 1 && obj.y == this.y - 1 && obj.pb.aFigura != null)
                {
                    if (!isThereAnythingTheSameColour(grid, this.x + 1, this.y - 1))
                    {
                        isSomethingOnDiagonalRight = true;
                    }
                }
                if (obj.x == this.x - 1 && obj.y == this.y - 1 && obj.pb.aFigura != null) {
                    if (!isThereAnythingTheSameColour(grid, this.x - 1, this.y - 1))
                    {
                        isSomethingOnDiagonalLeft = true;
                    }
                }

                if ((obj.x == this.x + 1 && obj.y == this.y && obj.pb.aFigura is Pionek)
                && (!obj.pb.aFigura.is_black)
                && ((obj.pb.aFigura as Pionek).CzyRuchWczesniejWykonalRuchODwaPola()))
                {
                    if (!isThereAnythingTheSameColour(grid, this.x - 1, this.y - 1))
                    {
                        isSomethingOnDiagonalRight = true;
                    }
                }

                if ((obj.x == this.x - 1 && obj.y == this.y && obj.pb.aFigura is Pionek)
                && (!obj.pb.aFigura.is_black)
                && ((obj.pb.aFigura as Pionek).CzyRuchWczesniejWykonalRuchODwaPola()))
                {
                    if (!isThereAnythingTheSameColour(grid, this.x + 1, this.y - 1))
                    {
                        isSomethingOnDiagonalLeft = true;
                    }
                }
            }
            if (!flagaCosStoiPrzed)
            {
                listOfMovements.Add((x, y - 1));
                if (isFirstMovement)
                {
                    if (!flagaCosStoi2Przed)
                    {
                        listOfMovements.Add((x, y - 2));
                    }
                }
            }

            if (isSomethingOnDiagonalLeft)
            {
                    listOfMovements.Add((x - 1, y - 1));
            }
            if (isSomethingOnDiagonalRight)
            {
                    listOfMovements.Add((x + 1, y - 1));
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
        public override void  OnBeforeMovement()
        {


        }
        public override void MakeAMove()
        {

        }
        public override void OnAfterMovement()
        {
            if (isFirstMovement == true)
            {
                czyWlasniesnieWykonanoRuchODwa = true;
            }
            else
            {
                czyWlasniesnieWykonanoRuchODwa = false;
            }
            earlyState = isFirstMovement;
            isFirstMovement= false;
        }

        public override object Clone()
        {
            return this.MemberwiseClone();


        }
        public bool CzyRuchWczesniejWykonalRuchODwaPola()
        {
            return czyWlasniesnieWykonanoRuchODwa;
        }
        public bool czyPotencjalneElPasso = false;
        public bool czyObiektDoUsuniecia = false;
        public void ZatwierdzElPasso()
        {
            czyObiektDoUsuniecia = true;

        }

        internal void OnBack()
        {
            isFirstMovement = earlyState;
            if (earlyState == true)
            {
                czyWlasniesnieWykonanoRuchODwa = false;
            }
        }
    }
}
