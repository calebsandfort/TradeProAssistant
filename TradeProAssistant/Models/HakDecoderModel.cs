using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeProAssistant.Models
{
    public class HakDecoderModel
    {
        private String encrypted;

        public String Encrypted
        {
            get { return encrypted; }
            set { encrypted = value; }
        }

        private String decrypted;

        public String Decrypted
        {
            get { return decrypted; }
            set { decrypted = value; }
        }

    }
}