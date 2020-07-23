using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoItX3Lib;

namespace Bot
{
    class AutoIt
    {
        AutoItX3 auto = new AutoItX3();
        public void mClick(string ClickSite, int x, int y, int manyClick, int Speed)
        {
            auto.MouseClick(ClickSite, x, y, manyClick);

        }
    }
}
