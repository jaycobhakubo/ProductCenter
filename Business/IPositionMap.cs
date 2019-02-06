using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTI.Modules.ProductCenter.Business
{
    public interface IPositionMap
    {
        byte MapSpace { get; }
        int NumSequences { get; }

        PositionSequence GetSequence(int setNumber);
        List<PositionSequence> GetSequences(int firstSetNumber, int setCount);
    }
}
