using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTI.Modules.ProductCenter.Business
{
    public class PositionSequence
    {
        /// <summary>
        /// Compares byte? treating null values as not equal.
        /// </summary>
        private class NullPositionInequalityComparer : IEqualityComparer<byte?>
        {
            private static readonly NullPositionInequalityComparer s_instance = new NullPositionInequalityComparer();
            public static NullPositionInequalityComparer Instance { get { return s_instance; } }

            public bool Equals(byte? x, byte? y)
            {
                if(x == null || y == null)
                    return false;
                else
                    return x.Value == y.Value;
            }

            public int GetHashCode(byte? obj)
            {
                if(obj == null)
                    return byte.MaxValue + 1;
                else
                    return obj.Value;
            }
        }

        byte?[] m_positionSequence;

        public PositionSequence()
        {
            m_positionSequence = null;
        }

        public PositionSequence(IEnumerable<byte?> positions)
        {
            var tPos = positions.Count();
            var dPos = positions.Distinct(NullPositionInequalityComparer.Instance).Count();
            if(tPos != dPos)
                throw new Exception("Positions may not be repeated.");

            var maxPos = positions.Max((p) => p ?? 0);
            if(maxPos > tPos)
                throw new Exception("Only positions >= 0 and < position space are permitted.");

            m_positionSequence = positions.ToArray();
        }

        public PositionSequence(IEnumerable<byte> positions)
        {
            var tPos = positions.Count();
            var dPos = positions.Distinct().Count();
            if(tPos != dPos)
                throw new Exception("Positions may not be repeated.");

            var maxPos = positions.Max();
            if(maxPos > tPos)
                throw new Exception("Only positions >= 0 and < position space are permitted.");

            m_positionSequence = new byte?[tPos];
            int i = 0;
            foreach(var p in positions)
            {
                m_positionSequence[i] = p;
                ++i;
            }
        }

        public byte PositionSpace { get { return (byte)m_positionSequence.Length; } }

        public byte? GetPosition(int sequenceIndex) { return m_positionSequence[sequenceIndex]; }
        public void SetPosition(int sequenceIndex, byte value) { m_positionSequence[sequenceIndex] = value; }
        public void ClearPosition(int sequenceIndex) { m_positionSequence[sequenceIndex] = null; }

        public PositionSequence GetShrunk(int newPositionSpace)
        {
            if(newPositionSpace > m_positionSequence.Length)
                throw new Exception("Cannot \"shrink\" a position sequence to a larger size.");
            else if(m_positionSequence.Count((p) => p.HasValue && p.Value < newPositionSpace) != newPositionSpace)
                throw new Exception("Cannot shrink a sequence that would be incompletely defined at the smaller position space.");
            else
            {
                var shrunk = new PositionSequence();
                shrunk.m_positionSequence = new byte?[newPositionSpace];

                for(int this_i = 0, shrunk_i = 0
                    ; this_i < this.m_positionSequence.Length && shrunk_i < shrunk.m_positionSequence.Length
                    ; ++this_i
                    )
                {
                    var currentPos = this.m_positionSequence[this_i];
                    if(currentPos.HasValue && currentPos < newPositionSpace)
                    {
                        shrunk.m_positionSequence[shrunk_i] = currentPos;
                        ++shrunk_i;
                    }
                }

                return shrunk;
            }
        }

        public void Shrink(int newPositionSpace)
        {
            if(newPositionSpace > m_positionSequence.Length)
                throw new Exception("Cannot \"shrink\" a position sequence to a larger size.");
            else if(m_positionSequence.Count((p) => p.HasValue && p.Value < newPositionSpace) != newPositionSpace)
                throw new Exception("Cannot shrink a sequence that would be incompletely defined at the smaller position space.");
            else
            {
                var new_sequence = new byte?[newPositionSpace];

                for(int this_i = 0, shrunk_i = 0
                    ; this_i < this.m_positionSequence.Length && shrunk_i < new_sequence.Length
                    ; ++this_i
                    )
                {
                    var currentPos = m_positionSequence[this_i];
                    if(currentPos.HasValue && currentPos < newPositionSpace)
                    {
                        new_sequence[shrunk_i] = currentPos;
                        ++shrunk_i;
                    }
                }

                m_positionSequence = new_sequence;
            }
        }

        public bool IsComplete()
        {
            return !m_positionSequence.Any((p) => p == null) // No undefined sequence positions
                && m_positionSequence.Distinct().Count() == m_positionSequence.Length // Proper number of positions
                && m_positionSequence.Min() == 0 // No "negative" positions
                && m_positionSequence.Max() < m_positionSequence.Length //No positions past end of position space
            ;
        }

        public byte[] GetNormalizedSequence()
        {
            if(IsComplete())
                return Array.ConvertAll(m_positionSequence, p => (byte)p);
            else
                return null;
        }
    }
}
