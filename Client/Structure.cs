using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Drawing;

namespace Client
{
    [Serializable()]
    public class Structure : ISerializable
    {
        private string _textChat;
        private Font myFont;
        private Color _myColor;
        public Structure()
        {
            this.TextChat = null;
            this.MyFont = new Font("Arial", 9, FontStyle.Regular);
            this.MyColor = Color.Red;

        }
        public Structure(string text, Font ft, Color cl)
        {
            this.TextChat = text;
            this.MyFont = ft;
            this.MyColor = cl;
        }
        public Structure(Structure str)
        {
            //this.TextChat = str.TextChat;
            this.MyFont = str.MyFont;
            this.MyColor = str.MyColor;
        }
        public Structure(SerializationInfo info, StreamingContext strcxt)
        {
            //this.TextChat = (string)info.GetValue("text", typeof(string));
            this.MyFont = (Font)info.GetValue("font", typeof(Font));
            this.MyColor = (Color)info.GetValue("color", typeof(Color));
        }
        public string TextChat
        {
            get
            {
                return _textChat;
            }

            set
            {
                _textChat = value;
            }
        }

        public Color MyColor
        {
            get
            {
                return _myColor;
            }

            set
            {
                _myColor = value;
            }
        }

        public Font MyFont
        {
            get
            {
                return myFont;
            }

            set
            {
                myFont = value;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("font", this.MyFont);
            info.AddValue("color", this.MyColor);

        }
    }
}
