using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Szachy.Figury;
using Szachy.Grid;

namespace Szachy.Factories
{
    internal class FabrykaPol
    {
        static PieceFactory pieceFactory;

        // Nie wiem czy nie odwrotnie białe z czarnymi, ale coś na tej zasadzie
        const int blackPawnY = 6;
        const int whitePawnY = 1;
        public const string pionek = "Pionek";
        public FabrykaPol()
        {
            if (pieceFactory == null)
            {
                pieceFactory = new PieceFactory();
            }
        }
        internal void AddEvents(BetterPB pictureBox, Action<object, EventArgs> sprawdzZCzymSasiadujeICzyMoznaSieRuszyc)
        {
            
            pictureBox.Click += new EventHandler((sender, e) => sprawdzZCzymSasiadujeICzyMoznaSieRuszyc(sender, e));

        }

        internal void AddEventsLeave(BetterPB pictureBox, Action<object, EventArgs> pictureBoxHelperOut, Action<object, EventArgs> mouseLeave)
        {
            pictureBox.MouseLeave += new EventHandler((sender, e) => pictureBoxHelperOut(sender, e));
            pictureBox.MouseLeave += new EventHandler((sender, e) => mouseLeave(sender, e));
        }

        internal void AddEventsMouseMove(BetterPB pictureBox, Action<object, MouseEventArgs> mouseMove, Action<object, MouseEventArgs> ifThereIsNoPossibilityToDestroy)
        {
            pictureBox.MouseMove += new MouseEventHandler((sender, e) => mouseMove(sender, e));
            pictureBox.MouseMove += new MouseEventHandler((sender, e) => ifThereIsNoPossibilityToDestroy(sender, e));
        }

        internal void calculateCoordinateX(BetterPB pictureBox, int i, int sizeOfPB)
        {
            pictureBox.x = i / sizeOfPB;
        }

        internal void calculateCoordinateY(BetterPB pictureBox, int j, int sizeOfPB)
        {
            pictureBox.y = j / sizeOfPB;
        }

        internal void PrzypiszFigure(BetterPB pictureBox)
        {
            AFigury figura; // Nigdy nie używane? Można usunąć?

            //pierwsza pozycja (kolumna,wiersz);

            //Raczej unikaj "magic numberów" jeśli nie jest to ==1 lub ==0 warto zrobić jakieś stałe i porównać do stałej, poprawiam jeden przykład
            if (pictureBox.y == blackPawnY || pictureBox.y == whitePawnY)
            {
                // Warto też strigi usunąć z takiego bezpośredniego przekazywania do funkcji.
                // Może warto zrobić enum z możliwymi figurami i go tu przekazywać?
                pieceFactory.MakePiece(pionek, pictureBox);

            }
            if ((pictureBox.y == 0 || pictureBox.y == 7) && (pictureBox.x == 2 || pictureBox.x == 5))
            {
                pieceFactory.MakePiece("Biskup", pictureBox);
            }

            if ((pictureBox.y == 0 || pictureBox.y == 7) && (pictureBox.x == 0 || pictureBox.x == 7))
            {
                pieceFactory.MakePiece("Wieza", pictureBox);

            }

            if ((pictureBox.y == 0 || pictureBox.y == 7) && (pictureBox.x == 1 || pictureBox.x == 6))
            {
                pieceFactory.MakePiece("Kon", pictureBox);
            }

            if ((pictureBox.y == 0 && (pictureBox.x == 4)) || (pictureBox.y == 7 && (pictureBox.x == 4)))
            {
                pieceFactory.MakePiece("Krol", pictureBox);

            }
            if ((pictureBox.y == 0 && (pictureBox.x == 3)) || (pictureBox.y == 7 && (pictureBox.x == 3)))
            {
                pieceFactory.MakePiece("Krolowa", pictureBox);

            }

        }

        internal void setBackColor(BetterPB pictureBox, bool isTimeForWhite)
        {
            if (isTimeForWhite)
            {
                pictureBox.BackColor = Color.White;
            }
            else
            {
                pictureBox.BackColor = Color.Black;
            }
            pictureBox.background = pictureBox.BackColor;
        }

        internal void setBorderStyle(BetterPB pictureBox, BorderStyle fixedSingle)
        {
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
        }

        internal void SetSize(BetterPB pictureBox, int sizeOfPb)
        {
            pictureBox.Size = new Size(sizeOfPb, sizeOfPb);
            pictureBox.Image = new Bitmap(sizeOfPb, sizeOfPb);
        }

        internal void SetSizeMode(BetterPB pictureBox, PictureBoxSizeMode stretchImage)
        {
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }


         private void ZrobPole(int i, int j, BetterPB pb)
         {
            //Brak ciała, do implementacji czy do usunięcia?
         }

        internal void TotalInitializeOfHolder(BetterPB pictureBox, int sizeOfPB,bool IsTimeForWhite,int i,int j)
        {
            SetSize(pictureBox, sizeOfPB);
            SetSizeMode(pictureBox, PictureBoxSizeMode.StretchImage);
            setBackColor(pictureBox, IsTimeForWhite);
            setBorderStyle(pictureBox, BorderStyle.FixedSingle);
            calculateCoordinateX(pictureBox, i, sizeOfPB);
            calculateCoordinateY(pictureBox, j, sizeOfPB);
            if (pictureBox.y == 0 || pictureBox.y == 1 || pictureBox.y == 6 || pictureBox.y == 7)
            {
                PrzypiszFigure(pictureBox);
            }
        }
    }
}
