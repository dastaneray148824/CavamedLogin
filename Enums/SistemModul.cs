using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums
{
    public enum SistemModul
    {
        CRM = 1,
        BMD = 2,
        ITS = 4,
        EDU = 8,
        PROD = 0x10,
        UTS = 0x20,
        EYS = 0x40,
        FIN = 0x80,
        MUH = 0x100,
        QLTY = 0x200,
        FIX = 0x400,
        SCM = 0x800,
        HRC = 0x1000,
        CMP = 0x2000,
        WMS = 0x4000,
        CST = 0x8000,
        EDON = 0x10000,
        VHC = 0x20000,
        RTL = 0x80000,
        TRNS = 0x100000,
        SRV = 0x200000,
        COMM = 0x400000,
        MKYS = 0x800000,
        CMPT = 0x1000000,
        MYM = 0x2000000,
        KLB = 0x4000000
    }
}
