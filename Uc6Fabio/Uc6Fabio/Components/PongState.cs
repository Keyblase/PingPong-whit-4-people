using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uc6Fabio.Components
{
    public class PongState
    {
        public enum PameState
        {
            IntroScreen,
            SinglePlayer,
            MultiPlayer,
            GameOver,
            GameWinP1,
            GameWinP2,
            GameWinP3,
            GameWinP4
        }
    }
}
