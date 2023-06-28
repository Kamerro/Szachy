using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Szachy.Grid;

namespace Szachy.Figury
{
    public abstract class AFigury : IAFigury
    {
        public Siatka siatka { get; set; }
        public bool is_on_black { get; set; }
        public bool is_black { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public Image image { get; set; }

        //Dziwnie wyglądają te konstrukcje, dlaczego są nazwy zmiennych (x i y) w deklaracji, nie wystarczy sam typ?
        //Ale to chyba bardziej osobiste preferencje
        public List<(int, int)> listOfPossibleMovements;
        public (int, int) getPosition()
        {
            return (this.x, this.y);
        }
        public List<(int, int)> getList()
        {
            return this.listOfPossibleMovements;
        }
        public AFigury(int x, int y, Image image,bool isOnBlack,bool isBlack)
        {
            this.x = x;
            this.y = y;
            this.image = image;
            this.is_black = isBlack;
            this.is_on_black = isOnBlack;
        }
        protected void UpdatePosition(int x, int y)
        {
            this.x = (int)x;
            this.y = (int)y;
        }

        public abstract void OnBeforeMovement();
        public abstract void OnAfterMovement();
        public abstract void MakeAMove();
        public abstract List<(int x, int y)> calculatePossibleMovements();

        public abstract object Clone();
     

    }
}
