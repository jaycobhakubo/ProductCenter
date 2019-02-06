using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GTI.Modules.ProductCenter.UI
{
    class PaletteColor : IEquatable<PaletteColor>
    {
        public event EventHandler ColorChanged;
        public event EventHandler NameChanged;

        protected virtual void OnColorChanged()
        {
            var h = ColorChanged;
            if(h != null)
                h(this, null);
        }

        protected virtual void OnNameChanged()
        {
            var h = NameChanged;
            if(h != null)
                h(this, null);
        }

        private static System.Random r = new Random();
        public static PaletteColor Random(string name)
        {
            byte[] rgb = new byte[3];
            r.NextBytes(rgb);
            var c = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
            return new PaletteColor(name, c.ToArgb());
        }

        private string m_name;
        private System.Drawing.Color m_color;
        private int m_colorValue;

        public string Name
        {
            get { return m_name; }
            set
            {
                if(m_name != value)
                {
                    m_name = value;
                    OnNameChanged();
                }
            }
        }
        public int ColorValue
        {
            get { return m_colorValue; }
            set
            {
                if(m_colorValue != value)
                {
                    m_colorValue = value;
                    m_color = Color.FromArgb(m_colorValue);
                    OnColorChanged();
                }
            }
        }
        public Color Color
        {
            get { return m_color; }
            set
            {
                if(m_color != value)
                {
                    m_color = value;
                    m_colorValue = m_color.ToArgb();
                    OnColorChanged();
                }
            }
        }

        public PaletteColor(string name, int colorValue)
        {
            Name = name;
            ColorValue = colorValue;
        }

        public bool Equals(PaletteColor other) { return this.Name == other.Name && this.ColorValue == other.ColorValue; }
    }

    class PaletteColorComparer : IEqualityComparer<PaletteColor>
    {
        public static readonly PaletteColorComparer Default = new PaletteColorComparer();

        public bool Equals(PaletteColor x, PaletteColor y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(PaletteColor pc)
        {
            return (pc.Name + pc.ColorValue.ToString()).GetHashCode();
        }
    }
}
