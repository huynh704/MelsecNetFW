using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelsecNetFW
{
    internal class CData
    {
        public CData(int iLength)
        {
            sq_Auto = new ushort[iLength];
            RegisterName = new string[iLength];
            RegisterData = new string[iLength];
            RegisterTriger = new string[iLength];
            TrigerData = new int[iLength];
            DataLength = new ushort[iLength];
        }
        ~CData()
        {
            RegisterName = null;
            RegisterTriger = null;
            RegisterData = null;
            DataLength = null;
        }
        public ushort[] sq_Auto;
        public string[] RegisterName;
        public string[] RegisterData;
        public string[] RegisterTriger;
        public int[] TrigerData;
        public ushort[] DataLength;
        public int Length
        {
            get { return sq_Auto.Length;}
        }
    }
}
