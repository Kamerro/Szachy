using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Szachy.Figury;
using Szachy.Grid;
using Szachy.Factories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static Szachy.Converted_Photos.ConvertedPhotos;
using System.Diagnostics;

namespace Szachy
{
    public partial class Chess : Form
    {
        //Wydaje się rozsądniej trzymać pogrupowane public osobno i private osobno
        //Globalne zmienne
        
        FabrykaPol fb = new FabrykaPol();
        Label label = new Label();

        public IAFigury ObjectInMovement = null;
        public int[,] SiatkaFigur = new int[8, 8];
        public char[] tablicaZnakow = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
        public Image PodrecznyImage { get; private set; }
        public Color PodrecznyBackground { get; private set; }
        public List<(int x, int y)> ListOfPossibleMovements { get; set; }

        FabrykaPol fb = new FabrykaPol();
        Label label = new Label();
        Siatka siatka;
        private int indexerX;
        private int indexerY;
        bool gracz_w_trakcie_ruchu = false;
        bool isThereWhiteMove = true;
        List<(int x, int y)> ListOfForbiddenMovementsForTheKing;
        //Warto albo trzymać się, że zawsze dajesz private, albo nigdy
        private bool flagMoveIsWrong = false;
        // istniejeRuch nigdy nie jest używane, warto trzymać?
        private bool istniejeRuch;

        public Chess()
        {
            InitializeComponent();
            siatka = Siatka.initializeGrid();
            PushPhotos();
            ZainicjujSiatke();
            LabelKtoMaRuch();
            this.Height += 50;
            this.Width += 50;
           
            this.Show();
        }
        //Dobrą praktyką jest unikanie zwracania typu void. Tego się praktycznie nie da przetestować czy działa tak jak powinno.
        //W przyszłości stworzę ErrorCode i każda funkcje przerobię na zwracanie błędu albo sukcesu.
        private void ZainicjujSiatke()
        {
            StworzSiatke();
            PrzypiszDoKroliWieze();
        }
        //Inicjalizacej siatki
        private void PrzypiszDoKroliWieze()
        {
            Krol krolCzarny = ZnajdzKrolaCzarnego();
            Krol krolBialy = ZnajdzKrolaBialego();
            foreach (var obj in siatka.getTheGrid())
            {
                if (obj.x == 0 && obj.y == 7)
                {
                    krolCzarny.DodajWiezeLewa(obj.pb.aFigura as Wieza);
                    //Przypisz lewą wieze do króla czarnego;
                }
                else if (obj.x == 7 && obj.y == 7)
                {
                    krolCzarny.DodajWiezePrawa(obj.pb.aFigura as Wieza);
                }
                else if (obj.x == 0 && obj.y == 0)
                {
                    krolBialy.DodajWiezeLewa(obj.pb.aFigura as Wieza);
                }
                else if (obj.x == 7 && obj.y == 0)
                {
                    krolBialy.DodajWiezePrawa(obj.pb.aFigura as Wieza);
                }
            }
        }
        private void StworzSiatke()
        {
            bool IsTimeForWhite = false;
            //Hm nie widzę gdzie ten rand jest wykorzystany?
            Random rand = new Random();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            int wiersz = 1;
            int kolumna = 0;
            this.Width = 8 * 80;
            this.Height = this.Width;
            int sizeOfPB = this.Width / 8;
            int margin = 5;
            for (int i = margin; i < this.Width; i += sizeOfPB + margin)
            {
                for (int j = margin; j < this.Height; j += sizeOfPB)
                {
                    try
                    {
                        //Inicjalizaca pb oraz bierki, dodanie eventów i przypisanie bierki do pb.
                        BetterPB pictureBox = new BetterPB();
                        fb.TotalInitializeOfHolder(pictureBox, sizeOfPB, IsTimeForWhite, i, j);
                        fb.AddEvents(pictureBox, SprawdzZCzymSasiadujeICzyMoznaSieRuszyc);
                        IsTimeForWhite = !IsTimeForWhite;
                        fb.AddEventsLeave(pictureBox, pictureBoxHelperOut, MouseLeave);
                        fb.AddEventsMouseMove(pictureBox, MouseMove, IfThereIsNoPossibilityToDestroy);

                        //Wciśnięcie do szachownicy pola z bierką
                        siatka.pushGrid((i / sizeOfPB, j / sizeOfPB, pictureBox));
                        this.Controls.Add(pictureBox);

                        pictureBox.Location = new Point(i, j);
                        if (pictureBox.x == 7)
                        {
                            PodpiszCyfry(i, j, wiersz, sizeOfPB);
                            wiersz++;
                        }
                        if (pictureBox.y == 7)
                        {
                            PodpiszLitery(i, j, kolumna, sizeOfPB);
                            kolumna++;
                        }
                    }
                    catch
                    {
                    }
                }
                IsTimeForWhite = !IsTimeForWhite;

            }
            this.Width += sizeOfPB + margin - 20;
            this.Height += sizeOfPB + margin - 15;
        }

        private void PodpiszLitery(int i, int j, int kolumna, int sizeOfPB)
        {
            // Może lepiej to wygląda w taki sposób?
            Label label = new Label
            {
                Font = new Font(FontFamily.Families[2], 9, FontStyle.Bold),
                AutoSize = true,
                Text = tablicaZnakow[kolumna].ToString(),
                Location = new Point(i + (sizeOfPB / 2), j + 100)
            };
            this.Controls.Add(label);
          
        }

        private void PodpiszCyfry(int i,int j,int wiersz,int sizeOfPB)
        {
            Label label = new Label
            {
                Font = new Font(FontFamily.Families[2], 9, FontStyle.Bold),
                Text = wiersz.ToString(),
                Location = new Point(i + 100, j + (sizeOfPB / 2) - 10)
            };
            this.Controls.Add(label);
            // Ten wiersz w jakimś konkretnym celu tu zwiększasz?
            wiersz++;
        }

        private Krol ZnajdzKrolaBialego()
        {
            foreach (var obj in siatka.getTheGrid())
            {
                if (obj.pb.aFigura != null && obj.pb.aFigura is Krol && !obj.pb.aFigura.is_black)
                {
                    return obj.pb.aFigura as Krol;
                }
            }
            return null;
        }

        private Krol ZnajdzKrolaCzarnego()
        {
            foreach (var obj in siatka.getTheGrid())
            {
                if (obj.pb.aFigura != null && obj.pb.aFigura is Krol && obj.pb.aFigura.is_black)
                {
                    return obj.pb.aFigura as Krol;
                }
            }
            return null;
        }

        private void MouseLeave(object sender, EventArgs e)
        {
            if ((sender as BetterPB).BackColor != Color.Yellow)
            {
                (sender as BetterPB).BackColor = (sender as BetterPB).background;
            }
        }
       
        private void MouseMove(object sender, MouseEventArgs e)
        {
            if ((sender as BetterPB).BackColor != Color.Yellow)
            {
                (sender as BetterPB).BackColor = Color.Green;
            }
        }
        private void IfThereIsNoPossibilityToDestroy(object sender, MouseEventArgs e)
        {
            if (gracz_w_trakcie_ruchu)
            {
                
                if (!ListOfPossibleMovements.Any(obj => obj.x == (sender as BetterPB).x && obj.y == (sender as BetterPB).y))
                {
                   if(!((sender as BetterPB).x == ObjectInMovement.x && (sender as BetterPB).y == ObjectInMovement.y))
                    (sender as BetterPB).BackColor = Color.Red;
                }
            }
        }
        private void SprawdzZCzymSasiadujeICzyMoznaSieRuszyc(object sender, EventArgs e)
        {
            //Metoda powinna być wywołana wewnątrz figury. 
            //Globalny stan: gracz_w_trakcie_ruchu;
            //Jeżeli gracz jest w ruchu, to:


            if (gracz_w_trakcie_ruchu)
                GraczWTrakcieRuchu(sender);
                 
           
            //Jeżeli gracz kliknął na bierkę:
                else
                GraczPrzedRuchem(sender);              
           
        }

        private void GraczPrzedRuchem(object sender)
        {
            if ((sender as BetterPB).aFigura != null)
            {
                if ((isThereWhiteMove != (sender as BetterPB).aFigura.is_black))
                {
                    //Z wybranego pictureboxa odepnij metodę
                    (sender as BetterPB).MouseLeave -= pictureBoxHelperOut;
                    //Castuj Figurę na ObjectInMovement
                    ObjectInMovement = (sender as BetterPB).aFigura;
                    indexerX = (sender as BetterPB).x;
                    indexerY = (sender as BetterPB).y;
                    //Sprawsz jaki był obraz na bierce, czytaj obraz z bierki
                    PodrecznyImage = (sender as BetterPB).Image;
                    //Sprawdz tło pola
                    PodrecznyBackground = (sender as BetterPB).background;
                    //Wyzeruj to jest na poprzednim polu
                    (sender as BetterPB).aFigura = null;
                    (sender as BetterPB).Image = null;
                    gracz_w_trakcie_ruchu = true;
                    ObliczMozliweRuchy();
                }
            }
        }

        private void GraczWTrakcieRuchu(object sender)
        {
            bool FlagaWykonanoRuch = false;
            foreach (var obj in ListOfPossibleMovements)
            {
                //Jeżeli gracz chce wykonać ruch we wskazane pole BetterPB to:
                if (obj.x == (sender as BetterPB).x && obj.y == (sender as BetterPB).y)
                {
                    FlagaWykonanoRuch = true;
                    FinalizujRuch(sender, obj.x, obj.y);
                    SprawdzCzyKtosWygral();
                }
            }
            if (!FlagaWykonanoRuch)
            {
                foreach (var obj in siatka.getTheGrid())
                {
                    if (obj.x == indexerX && obj.y == indexerY)
                    {
                        obj.pb.Image = PodrecznyImage;
                        obj.pb.aFigura = (IAFigury)ObjectInMovement.Clone();
                    }
                }
            }
            PoKliknieciuResetRuchu();
            (sender as BetterPB).MouseLeave += pictureBoxHelperOut;

        }
        private void PoKliknieciuResetRuchu()
        {
            gracz_w_trakcie_ruchu = false;
            RemovePossibleMovements(ListOfPossibleMovements);
        }

        private void SprawdzCzyKtosWygral()
        {
            bool czyBialiWygrali = true;
            bool czyCzarniWygrali = true;
            foreach (var obj in siatka.getTheGrid())
            {
                if (obj.pb.aFigura is Krol && obj.pb.aFigura.is_black)
                {
                    //Biali nie wygrali
                    czyBialiWygrali = false;
                }
                if (obj.pb.aFigura is Krol && !obj.pb.aFigura.is_black)
                {
                    //Czarni nie wygrali
                    czyCzarniWygrali = false;
                }
            }
            if (czyCzarniWygrali)
            {
                MessageBox.Show("Czarni wygrali");
                KoniecGry();
            }
            else if (czyBialiWygrali)
            {
                MessageBox.Show("Biali wygrali");
                KoniecGry();
            }
            
        }

        private void FinalizujRuch(object sender,int x,int y)
        {
            var obiekt = sender as BetterPB;
            int x_1 = ObjectInMovement.x;
            int y_1 = ObjectInMovement.y;
            ObjectInMovement.x = x;
            ObjectInMovement.y = y;
            IAFigury figura = null;
            flagMoveIsWrong = false;
            if (obiekt.aFigura != null)
            {
               figura = (IAFigury)obiekt.aFigura.Clone();
            }
            PrzedRuchemDlaKazdejFigury(ObjectInMovement,sender);
            bool czyMoznaWykonacRuch = SprawdzCzyMoznaWykonacRuch(obiekt, sender);
            if (!czyMoznaWykonacRuch)
            {
                WrocDoPoprzedniegoStanu(obiekt, figura, x_1, y_1);
                flagMoveIsWrong = false;
            }
            else
            {
                WykonajRuch(sender, figura, x_1, y_1);
            }
        }

        // Tego potwora koniecznie trzeba podzielić na mniejsze części :D Ale to już temat na dłuższe posiedzenie
        private void WykonajRuch(object sender,IAFigury figura,int x_1, int y_1)
        {
            //Logika pionów:
            //Jeżeli współrzędne obiektu ruchu dla określonego koloru 
            //Iteracja po siatce, czytaj każdy obiekt z siatki,
            //dla każdego piona przeczytaj czy ruszył się ruch wcześniej o 2 pola
            //nie ważne czy czarny czy biały
            foreach (var obj in siatka.getTheGrid())
            {
                //przeczytanie piona, jeżeli ruch był pionem to:

                var czarny = obj.pb.aFigura as Pionek;
                //Jeżeli figura ma oznaczenie jako potencjalne en passant, figura w ruchu jest przeciwna od elPasso to:
                //Sprawdz czy ruch wczesniej pionek wykonał ruch o 2 pola
                if ((czarny != null && obj.pb.aFigura is Pionek && czarny.CzyRuchWczesniejWykonalRuchODwaPola())
                && (czarny.is_black && !ObjectInMovement.is_black))
                {
                    //Przeczytaj koordynaty obiektu w ruchu white oraz obiektu z elPasso black
                    //Jeżeli white jest centralnie za czarnym z elPasso to usuń black
                    if (ObjectInMovement.x == czarny.x && ObjectInMovement.y - 1 == czarny.y)
                    {
                        obj.pb.aFigura = null;
                        obj.pb.Image = null;
                    }
                    else
                    {
                        czarny.czyWlasniesnieWykonanoRuchODwa = false;
                    }
                }
                //Logika dla białych:
                else if ((czarny != null && obj.pb.aFigura is Pionek && czarny.CzyRuchWczesniejWykonalRuchODwaPola())
               && (!czarny.is_black && ObjectInMovement.is_black))
                {
                    //Przeczytaj koordynaty obiektu w ruchu white oraz obiektu z elPasso black
                    //Jeżeli white jest centralnie za czarnym z elPasso to usuń black
                    if (ObjectInMovement.x == czarny.x && ObjectInMovement.y + 1 == czarny.y)
                    {
                        obj.pb.aFigura = null;
                        obj.pb.Image = null;
                    }
                    else
                    {
                        czarny.czyWlasniesnieWykonanoRuchODwa = false;
                    }
                }
                if (ObjectInMovement is Pionek && ObjectInMovement.y - 1 == 0 && obj.x == ObjectInMovement.x && ObjectInMovement.is_black && ObjectInMovement.y == obj.y)
                {
                    obj.pb.Image = null;
                    obj.pb.aFigura = new Krolowa(czarny.x, czarny.y, KrolowaC, czarny.is_on_black, czarny.is_black);
                    obj.pb.Image = KrolowaC;
                    obj.pb.aFigura.siatka = siatka;
                }
                if (ObjectInMovement is Pionek && ObjectInMovement.y + 1 == 7 && obj.x == ObjectInMovement.x && !ObjectInMovement.is_black && ObjectInMovement.y == obj.y)
                {
                    obj.pb.Image = null;
                    obj.pb.aFigura = new Krolowa(czarny.x, czarny.y, KrolowaB, czarny.is_on_black, czarny.is_black);
                    obj.pb.Image = KrolowaB;
                    obj.pb.aFigura.siatka = siatka;
                }
            }


            //Jeżeli król !zrobił ruch
            //Podzielony kod dla każdej z ewentualności. 
            //
            
            if (ObjectInMovement is Krol && !(ObjectInMovement as Krol).czyZrobilRuch)
            {

                if (ObjectInMovement.x == 2 && ObjectInMovement.y == 7)
                {
                    foreach (var obj in siatka.getTheGrid())
                    {
                        if (obj.pb.aFigura != null && obj.pb.aFigura.is_black != (ObjectInMovement as Krol).is_black)
                        {
                            if (obj.pb.aFigura.calculatePossibleMovements().Any(x => (x.x == 2 && x.y == 7) || (x.x == 3 && x.y == 7) || (x.x == 4 && x.y == 7)))
                            {
                                WrocDoPoprzedniegoStanu(sender as BetterPB, figura, x_1, y_1);
                                return;
                            }
                        }
                    }

                    var krol = (ObjectInMovement as Krol);
                    foreach (var ob in siatka.getTheGrid())
                    {
                        if (ob.x == 3 && ob.y == 7)
                        {
                            ob.pb.aFigura = (Wieza)krol.wiezaLewa.Clone();
                            ob.pb.aFigura.x = 3;
                            ob.pb.aFigura.y = 7;
                            ob.pb.Image = WiezaC;
                            ob.pb.Refresh();
                           // krol.wiezaLewa = null;
                        }
                        if (ob.x == 0 && ob.y == 7)
                        {
                            ob.pb.Image = null;
                            ob.pb.aFigura = null;
                        }
                    }
                    foreach (var ob in siatka.getTheGrid())
                    {
                        if (ob.x == 2 && ob.y == 7)
                        {
                            ob.pb.aFigura = (Krol)krol.Clone();
                        }
                    }

                }
                else if (ObjectInMovement.x == 6 && ObjectInMovement.y == 7)
                {
                    foreach (var obj in siatka.getTheGrid())
                    {
                        if (obj.pb.aFigura != null && obj.pb.aFigura.is_black != (ObjectInMovement as Krol).is_black)
                        {
                            if (obj.pb.aFigura.calculatePossibleMovements().Any(x => (x.x == 5 && x.y == 7) || (x.x == 6 && x.y == 7) || (x.x == 4 && x.y == 7)))
                            {
                                WrocDoPoprzedniegoStanu(sender as BetterPB, figura, x_1, y_1);
                                return;
                            }
                        }
                    }
                    var krol = (ObjectInMovement as Krol);
                    foreach (var ob in siatka.getTheGrid())
                    {
                        if (ob.x == 5 && ob.y == 7)
                        {

                            ob.pb.aFigura = (Wieza)krol.wiezaPrawa.Clone();
                            ob.pb.aFigura.x = 5;
                            ob.pb.aFigura.y = 7;
                            ob.pb.Image = WiezaC;
                            ob.pb.Refresh();
                            //   krol.wiezaPrawa = null;
                        }
                        if (ob.x == 7 && ob.y == 7)
                        {
                            ob.pb.Image = null;
                            ob.pb.aFigura = null;
                        }
                    }
                    foreach (var ob in siatka.getTheGrid())
                    {
                        if (ob.x == 6 && ob.y == 7)
                        {
                            ob.pb.aFigura = (Krol)krol.Clone();
                        }
                    }
                }
                else if (ObjectInMovement.x == 2 && ObjectInMovement.y == 0)
                {
                    foreach (var obj in siatka.getTheGrid())
                    {
                        if (obj.pb.aFigura != null && obj.pb.aFigura.is_black != (ObjectInMovement as Krol).is_black)
                        {
                            if (obj.pb.aFigura.calculatePossibleMovements().Any(x => (x.x == 2 && x.y == 0) || (x.x == 3 && x.y == 0) || (x.x == 4 && x.y == 0)))
                            {
                                WrocDoPoprzedniegoStanu(sender as BetterPB, figura, x_1, y_1);
                                return;
                            }
                        }
                    }
                    var krol = (ObjectInMovement as Krol);
                    foreach (var ob in siatka.getTheGrid())
                    {
                        if (ob.x == 3 && ob.y == 0)
                        {

                            ob.pb.aFigura = (Wieza)krol.wiezaLewa.Clone();
                            ob.pb.aFigura.x = 3;
                            ob.pb.aFigura.y = 0;
                            ob.pb.Image = WiezaB;
                            ob.pb.Refresh();
                            //krol.wiezaLewa = null;
                        }
                        if (ob.x == 0 && ob.y == 0)
                        {
                           ob.pb.Image = null;
                           ob.pb.aFigura = null;
                        }
                    }
                    foreach (var ob in siatka.getTheGrid())
                    {
                        if (ob.x == 2 && ob.y == 0)
                        {
                            ob.pb.aFigura = (Krol)krol.Clone();
                        }
                    }
                }
                else if (ObjectInMovement.x == 6 && ObjectInMovement.y == 0)
                {
                    foreach (var obj in siatka.getTheGrid())
                    {
                        if (obj.pb.aFigura != null && obj.pb.aFigura.is_black != (ObjectInMovement as Krol).is_black)
                        {
                            if (obj.pb.aFigura.calculatePossibleMovements().Any(x => (x.x == 5 && x.y == 0) || (x.x == 6 && x.y == 0) || (x.x == 4 && x.y == 0)))
                            {
                                WrocDoPoprzedniegoStanu(sender as BetterPB, figura, x_1, y_1);
                                return;
                            }
                        }
                    }
                    var krol = (ObjectInMovement as Krol);
                    foreach (var ob in siatka.getTheGrid())
                    {
                        if (ob.x == 5 && ob.y == 0)
                        {
                            ob.pb.aFigura = (Wieza)krol.wiezaPrawa.Clone();
                            ob.pb.aFigura.x = 5;
                            ob.pb.aFigura.y = 0;
                            ob.pb.Image = WiezaB;
                            ob.pb.Refresh();
                            //   krol.wiezaPrawa = null;
                        }
                        if (ob.x == 7 && ob.y == 0)
                        {
                            ob.pb.Image = null;
                            ob.pb.aFigura = null;
                        }
                    }
                    foreach (var ob in siatka.getTheGrid())
                    {
                        if (ob.x == 6 && ob.y == 0)
                        {
                            ob.pb.aFigura = (Krol)krol.Clone();
                        }
                    }
                }
            }

            if (ObjectInMovement is Wieza)
            {
                (ObjectInMovement as Wieza).czyZrobilRuch = true;
            }
            if (ObjectInMovement is Krol)
            {
                (ObjectInMovement as Krol).czyZrobilRuch = true;
            }


            foreach (var obj in siatka.getTheGrid())
            {
                if (obj.x == ObjectInMovement.x && obj.y == ObjectInMovement.y)
                {
                    obj.pb.aFigura = (IAFigury)ObjectInMovement.Clone();
                }
            }
            ResetujObiektWRuchuIUstawNowyObazek(sender);
            ResetujKontenerPb();
            ZmienKolejke();
        }

        private void WrocDoPoprzedniegoStanu(BetterPB obiekt,IAFigury figura, int x_1, int y_1)
        {
            obiekt.aFigura = figura;
            ObjectInMovement.x = x_1;
            ObjectInMovement.y = y_1;
            foreach(var obj in siatka.getTheGrid())
            {
                if(obj.pb.x == indexerX && obj.pb.y == indexerY)
                {
                    obj.pb.Image = PodrecznyImage;
                    obj.pb.aFigura = ObjectInMovement;
                    if(ObjectInMovement is Pionek)
                    {
                        (ObjectInMovement as Pionek).OnBack();
                    }
                }
            }
        }

        private bool SprawdzCzyMoznaWykonacRuch(BetterPB obiekt, object sender)
        {
            Krol k = null;
            istniejeRuch = false;
            foreach (var obj in siatka.getTheGrid())
            {

                if (obj.pb.aFigura is Krol && obj.pb.aFigura.is_black != isThereWhiteMove)
                {
                    k = (Krol)obj.pb.aFigura.Clone();
                }

            }
            //Policz czy figura może zbić króla
            foreach (var obj in siatka.getTheGrid())
            {
                //Król już się znajduje na odpowiednim polu,
                //Brak wyczyszczenia obiektu! tam jest tak jakby król na starym miejscu jeszcze!
                if (obj.pb.aFigura != null)
                {
                    //Kalkuluj prawdopodobne ruchy dla jednostki, jeżeli istnieją jakiekolwiek koordynaty pokrywające się
                    //to ustaw że nie można wykonać ruchu. Jeżeli Nie ma takich koordynatów które się pokrywają, to można wykonać ruch
                    if (obj.pb.aFigura.calculatePossibleMovements().Any(c => c.x == k.x && c.y == k.y))
                    {
                        flagMoveIsWrong = true;
                        return false;
                        //PrzywrocPoprzedniStan();
                    }
                    
                }
                //Raczej problem z polami które były ustawione na null
            }
            //Dla każdej aktualnej figury oblicz czy jest ruch
            if (!flagMoveIsWrong)
            {
                return true;
               
            }
            return false;
        }

        private void PrzedRuchemDlaKazdejFigury(IAFigury objectInMovement,object sender)
        {
            switch (ObjectInMovement)
            {
                case Pionek pionek:
                    (ObjectInMovement as Pionek).OnAfterMovement();
                    (sender as BetterPB).aFigura = (Pionek)ObjectInMovement.Clone();
                    break;

                case Biskup biskup:
                    (ObjectInMovement as Biskup).OnAfterMovement();
                    (sender as BetterPB).aFigura = (Biskup)ObjectInMovement.Clone();
                    break;

                case Wieza wieza:
                    (ObjectInMovement as Wieza).OnAfterMovement();
                    (sender as BetterPB).aFigura = (Wieza)ObjectInMovement.Clone();
                    break;

                case Kon kon:
                    (ObjectInMovement as Kon).OnAfterMovement();
                    (sender as BetterPB).aFigura = (Kon)ObjectInMovement.Clone();
                    break;
                case Krol krol:
                    (sender as BetterPB).aFigura = (Krol)ObjectInMovement.Clone();
                    break;
                case Krolowa krolowa:
                    (ObjectInMovement as Krolowa).OnAfterMovement();
                    (sender as BetterPB).aFigura = (Krolowa)ObjectInMovement.Clone();
                    break;
            }
        }

        private void ResetujKontenerPb()
        {
            //pbContainer.Image = null;
            //pbContainer.aFigura = null;
        }

        private void ZmienKolejke()
        {

            isThereWhiteMove = !isThereWhiteMove;
            if (isThereWhiteMove)
            {
                label.Text = "Ruch białych";
            }
            else
            {
                label.Text = "Ruch czarnych";
            }
        }
        private void ResetujObiektWRuchuIUstawNowyObazek(object sender)
        {
            (sender as BetterPB).Image = ObjectInMovement.image;
            ObjectInMovement.image = null;
            ObjectInMovement.siatka = null;

        }

        //Dodać interfejs który wywołuje metodę obliczanaia ruchów i wywołuje metode show possible movements
        //
        // do klasy OperacjeGraczy -> list = operacje.ObliczMozliweRuchy(ObjectInMovement)
        // OperacjeNaPlanszy ->  ShowPossibleMovements(ListOfPossibleMovements);
        private void ObliczMozliweRuchy()
        {
            //Można zrobić fabrykę ruchów ale nie jest to zbyt czytelne.
            //Dla obiektu w ruchu oblicza się możliwe ruchy i wyświetla je.
            ListOfPossibleMovements = ObjectInMovement.calculatePossibleMovements();
            ShowPossibleMovements(ListOfPossibleMovements);

        }

        private void KoniecGry()
        {
            Application.Exit();
        }


        private void ShowPossibleMovements(List<(int x, int y)> listOfPossibleMovements)
        {
            foreach(var pb in siatka.getTheGrid())
            {
              foreach(var obj in listOfPossibleMovements)
              {
                    if(pb.Item1 == obj.x && pb.Item2 == obj.y)
                    {
                        pb.Item3.BackColor = Color.Yellow;
                    }
              }
            }
        }
        private void RemovePossibleMovements(List<(int x, int y)> listOfPossibleMovements)
        {
            foreach (var pb in siatka.getTheGrid())
            {
                foreach (var obj in listOfPossibleMovements)
                {
                    if (pb.Item1 == obj.x && pb.Item2 == obj.y)
                    {
                        pb.Item3.BackColor = pb.Item3.background;
                    }
                }

            }
        }
        private void LabelKtoMaRuch()
        {
            label.Location = new Point((this.Width / 2) - 50, this.Height - 20);
            label.AutoSize = false;
            label.Width = 200;
            label.Font = new Font(FontFamily.Families[2], 14, FontStyle.Bold);
            label.Text = "Ruch białych";
            this.Controls.Add(label);
        }
        private void pictureBoxHelperOut(object sender, EventArgs e)
        {
            var obj = sender as BetterPB;
            if (obj != null && obj.BackColor != Color.Yellow)
            {
                obj.BackColor = obj.background;
            }
        }

    }
}

