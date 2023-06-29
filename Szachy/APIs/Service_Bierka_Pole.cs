using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szachy.Factories;
using Szachy.Figury;
namespace Szachy.APIs
{
    internal class Service_Bierka_Pole
    {
        public void PrzypiszBierkeDoPola(IAFigury figura,BetterPB pb )
        {
            pb.aFigura = figura;
        }
    }
}
