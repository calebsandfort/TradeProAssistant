using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TradeProAssistant.Models;

namespace TradeProAssistant.Controllers
{
    public class HuntAKillerController : Controller
    {
        // GET: HuntAKiller
        public ActionResult Decoder()
        {
            return View(new HakDecoderModel());
        }

        [HttpPost]
        public ActionResult Decoder(HakDecoderModel model)
        {
            String decrypted = model.Encrypted;
            Regex re = new Regex(@"#(\S+)\s?", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            MatchCollection mc = re.Matches(model.Encrypted);
            int mIdx = 0;
            foreach (Match m in mc)
            {
                decrypted = decrypted.Replace(m.Groups[0].Value, Decrypt(m.Groups[1].Value));
                //for (int gIdx = 0; gIdx < m.Groups.Count; gIdx++)
                //{
                //    Console.WriteLine("[{0}][{1}] = {2}", mIdx, re.GetGroupNames()[gIdx], m.Groups[gIdx].Value);
                //}
                //mIdx++;
            }

            model.Decrypted = decrypted;

            return View(model);
        }

        private string Decrypt(String s)
        {
            String ucfw = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            String ucbw = "ZYXWVUTSRQPONMLKJIHGFEDCBA";

            String lcfw = "abcdefghijklmnopqrstuvwxyz";
            String lcbw = "zyxwvutsrqponmlkjihgfedcba";

            String decrypted = "";

            foreach(char c in s)
            {
                if (ucbw.Contains(c))
                {
                    decrypted += ucfw[ucbw.IndexOf(c)];
                }
                else if (lcbw.Contains(c))
                {
                    decrypted += lcfw[lcbw.IndexOf(c)];
                }
                else
                {
                    decrypted += c;
                }
            }

            if(!decrypted.EndsWith(".") && !decrypted.EndsWith(","))
            {
                decrypted += " ";
            }

            return decrypted;
        }
    }
}