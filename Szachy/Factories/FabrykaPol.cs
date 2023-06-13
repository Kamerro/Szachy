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
        public FabrykaPol()
        {
            if (piece == null)
            {
                piece = new PieceFactory();
            }
        }
        static PieceFactory piece;
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
            AFigury figura;
            //pierwsza pozycja (kolumna,wiersz);
            if (pictureBox.y == 1 || pictureBox.y == 6)
            {
                piece.MakePiece("Pionek", pictureBox);

            }
            if ((pictureBox.y == 0 || pictureBox.y == 7) && (pictureBox.x == 2 || pictureBox.x == 5))
            {
                piece.MakePiece("Biskup", pictureBox);
            }

            if ((pictureBox.y == 0 || pictureBox.y == 7) && (pictureBox.x == 0 || pictureBox.x == 7))
            {
                piece.MakePiece("Wieza", pictureBox);

            }

            if ((pictureBox.y == 0 || pictureBox.y == 7) && (pictureBox.x == 1 || pictureBox.x == 6))
            {
                piece.MakePiece("Kon", pictureBox);
            }

            if ((pictureBox.y == 0 && (pictureBox.x == 4)) || (pictureBox.y == 7 && (pictureBox.x == 4)))
            {
                piece.MakePiece("Krol", pictureBox);

            }
            if ((pictureBox.y == 0 && (pictureBox.x == 3)) || (pictureBox.y == 7 && (pictureBox.x == 3)))
            {
                piece.MakePiece("Krolowa", pictureBox);

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
