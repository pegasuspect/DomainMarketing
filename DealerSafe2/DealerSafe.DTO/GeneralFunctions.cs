using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Xml;
using DealerSafe.DTO.Enums.BCIT;
using System.Linq;
using Microsoft.SqlServer.Server;
using SimpleDnsPlus;

namespace DealerSafe.DTO
{
    /// <summary>
    /// Required startDate,finishDate keys in .config file
    /// </summary>
    /// 
    public static class GeneralFunctions
    {
        public static DateTime companyStartDate = string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["startDate"]) ? DateTime.Now : StrToDateTime(ConfigurationManager.AppSettings["startDate"].ToString());//new DateTime(2011, 11, 2, 0, 0, 0);
        public static DateTime companyFinishDate = string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["finishDate"]) ? DateTime.Now : StrToDateTime(ConfigurationManager.AppSettings["finishDate"].ToString());// new DateTime(2011, 11, 14, 0, 0, 0);

        public static string ToEmailAdres = "", FromEmailAdres = "";
        public static string adres1, adres2;

        public static ArrayList arliResult = new ArrayList();

        public static string GetXmlDocNodesValue(XmlDocument XMLDoc, string xPath)
        {
            string result = "";
            try
            {
                XmlNode Node = XMLDoc.SelectSingleNode(xPath);
                result = Node.InnerText;
            }
            catch (Exception)
            {
                result = "";
                return result;
            }
            return result;
        }

        public static string WriteLine(this string str)
        {
            Console.WriteLine(str);
            return str + ";";
        }

        public static string Write(this string str)
        {
            Console.Write(str);
            return str + ";";
        }

        public static string getIPAddress()
        {
            try
            {
                HttpContext context = HttpContext.Current;
                string ip = context.Request.ServerVariables["HTTP_C_IP"] ?? context.Request.ServerVariables["REMOTE_ADDR"];

                return ip;
            }
            catch (Exception)
            {
                return "";
            }
        }


        public static string productType(SepeteAtilanProductType enm)
        {
            switch (enm)
            {
                case SepeteAtilanProductType.Domain: return "domain";
                case SepeteAtilanProductType.Sunucu: return "Sunucu hizmetleri";
                case SepeteAtilanProductType.Vps: return "Vps hizmetleri";
                case SepeteAtilanProductType.SSL: return "ssl";
                case SepeteAtilanProductType.SSL_Uzatma: return "ssl uzatma";
                case SepeteAtilanProductType.Hosting_Uzatma: return "hosting uzatma";
                case SepeteAtilanProductType.Hosting: return "hosting";
                case SepeteAtilanProductType.Transfer: return "transfer";
                case SepeteAtilanProductType.Domain_Uzatma: return "alanadı uzatma";
                case SepeteAtilanProductType.Domain_Gerialma: return "alanadı gerialma";
                case SepeteAtilanProductType.Domain_Gerialma_Redemption: return "alanadı gerialma redemption";
                case SepeteAtilanProductType.Web_Klavuzu: return "Web Klavuzu";
                case SepeteAtilanProductType.Web_Klavuzu_Uzatma: return "Web Klavuzu Uzatma";
                case SepeteAtilanProductType.Borc_Odemesi: return "Borc Odemesi";
                case SepeteAtilanProductType.Hosting_Upgrade: return "hosting upgrade";
                case SepeteAtilanProductType.Hosting_Change: return "hosting change";
                case SepeteAtilanProductType.Hosting_Backup: return "hosting backup";
                case SepeteAtilanProductType.Hosting_Domain_Renewal: return "hosting domain renewal";
                case SepeteAtilanProductType.Vps_Uzatma: return "Vps uzatma";
                case SepeteAtilanProductType.Vps_Eklenti: return "Vps eklenti";
                case SepeteAtilanProductType.Sunucu_Uzatma: return "Sunucu uzatma";
                case SepeteAtilanProductType.Sunucu_Eklenti: return "Sunucu eklenti";
                case SepeteAtilanProductType.Co_location: return "Location";
                case SepeteAtilanProductType.Co_location_Uzatma: return "Location uzatma";
                case SepeteAtilanProductType.Server_Ekle: return "Server ekle";
                case SepeteAtilanProductType.Domain_Uzatma_Uyeliksiz: return "Uyeliksiz Domain Uzatma";
                case SepeteAtilanProductType.Domain_Gerialma_Uyeliksiz: return "Uyeliksiz Domain Geri Alma";
                case SepeteAtilanProductType.Domain_Gerialma_Uyeliksiz_Redemption: return "Uyeliksiz Domain Geri Alma Redemption";
                case SepeteAtilanProductType.Marka_Tescil: return "Marka Tescil";
                case SepeteAtilanProductType.Marka_EkstraSinif: return "Marka EkstraSinif";
                case SepeteAtilanProductType.Kargo_Bedeli: return "Kargo Bedeli";
                case SepeteAtilanProductType.Hizmet_Bedeli: return "Hizmet Bedeli";
                case SepeteAtilanProductType.Hediye_Ceki: return "Hediye Çeki";
                case SepeteAtilanProductType.Endustriyel_Tasarım: return "Endüstriyel Tasarım";
                case SepeteAtilanProductType.Customize_VDS_Server: return "Customize VDS Server";
                case SepeteAtilanProductType.PHosting: return "phosting";
                case SepeteAtilanProductType.PHosting_Uzatma: return "phosting uzatma";
                case SepeteAtilanProductType.PHosting_Upgrade: return "phosting upgrade";
                case SepeteAtilanProductType.PHosting_Backup: return "phosting backup";
                case SepeteAtilanProductType.Customize_VDS_Server_Upgrade: return "Customize VDS Server Upgrade";
                case SepeteAtilanProductType.Customize_VDS_Server_Uzat: return "Customize VDS Server Uzat";
                case SepeteAtilanProductType.StatikIp: return "Statik IP";
                case SepeteAtilanProductType.Sunucu_Yukseltme: return "Sunucu yukseltme";
                case SepeteAtilanProductType.Web_Klavuzu_Yukseltme: return "Web Klavuzu Yükseltme";
                case SepeteAtilanProductType.HyperV: return "HyperV";
                case SepeteAtilanProductType.HyperV_Uzat: return "HyperV Uzat";
                case SepeteAtilanProductType.HyperV_Component: return "HyperV Component";
                case SepeteAtilanProductType.HyperV_Extension: return "HyperV Extension";
                case SepeteAtilanProductType.PhysicalServer_Uzat: return "Physical Server Uzat";
                default: return "unknown";
            }
        }


        public static int ProductTypeEnum(string Type)
        {
            switch (Type)
            {
                case "Marka EkstraSinif": return (int)SepeteAtilanProductType.Marka_EkstraSinif;
                case "domain": return (int)SepeteAtilanProductType.Domain;
                case "alanadı uzatma": return (int)SepeteAtilanProductType.Domain_Uzatma;
                case "Uyeliksiz Domain Uzatma": return (int)SepeteAtilanProductType.Domain_Uzatma_Uyeliksiz;
                case "alanadı gerialma": return (int)SepeteAtilanProductType.Domain_Gerialma;
                case "alanadı gerialma redemption": return (int)SepeteAtilanProductType.Domain_Gerialma_Redemption;
                case "Uyeliksiz Domain Geri Alma": return (int)SepeteAtilanProductType.Domain_Gerialma_Uyeliksiz;
                case "Uyeliksiz Domain Geri Alma Redemption": return (int)SepeteAtilanProductType.Domain_Gerialma_Uyeliksiz_Redemption;
                case "transfer": return (int)SepeteAtilanProductType.Transfer;
                case "hosting": return (int)SepeteAtilanProductType.Hosting;
                case "hosting uzatma": return (int)SepeteAtilanProductType.Hosting_Uzatma;
                case "hosting upgrade": return (int)SepeteAtilanProductType.Hosting_Upgrade;
                case "hosting change": return (int)SepeteAtilanProductType.Hosting_Change;
                case "hosting backup": return (int)SepeteAtilanProductType.Hosting_Backup;
                case "hosting domain renewal": return (int)SepeteAtilanProductType.Hosting_Domain_Renewal;
                case "Sunucu hizmetleri": return (int)SepeteAtilanProductType.Sunucu;
                case "Sunucu uzatma": return (int)SepeteAtilanProductType.Sunucu_Uzatma;
                case "Sunucu eklenti": return (int)SepeteAtilanProductType.Sunucu_Eklenti;
                case "Vps hizmetleri": return (int)SepeteAtilanProductType.Vps;
                case "Vps uzatma": return (int)SepeteAtilanProductType.Vps_Uzatma;
                case "Vps eklenti": return (int)SepeteAtilanProductType.Vps_Eklenti;
                //srv_CoLocation = 21,
                case "Location uzatma": return (int)SepeteAtilanProductType.Co_location_Uzatma;
                case "ssl": return (int)SepeteAtilanProductType.SSL;
                case "ssl uzatma": return (int)SepeteAtilanProductType.SSL_Uzatma;
                case "Web Klavuzu": return (int)SepeteAtilanProductType.Web_Klavuzu;
                case "Web Klavuzu Uzatma": return (int)SepeteAtilanProductType.Web_Klavuzu_Uzatma;
                case "Marka Tescil": return (int)SepeteAtilanProductType.Marka_Tescil;
                case "Kargo Bedeli": return (int)SepeteAtilanProductType.Kargo_Bedeli;
                case "Hizmet Bedeli": return (int)SepeteAtilanProductType.Hizmet_Bedeli;
                case "Borc Odemesi": return (int)SepeteAtilanProductType.Borc_Odemesi;
                case "Server ekle": return (int)SepeteAtilanProductType.Server_Ekle;
                case "Hediye Çeki": return (int)SepeteAtilanProductType.Hediye_Ceki;
                case "Endüstriyel Tasarım": return (int)SepeteAtilanProductType.Endustriyel_Tasarım;
                case "Customize VDS Server": return (int)SepeteAtilanProductType.Customize_VDS_Server;
                case "phosting": return (int)SepeteAtilanProductType.PHosting;
                case "phosting uzatma": return (int)SepeteAtilanProductType.PHosting_Uzatma;
                case "phosting upgrade": return (int)SepeteAtilanProductType.PHosting_Upgrade;
                case "phosting backup": return (int)SepeteAtilanProductType.PHosting_Backup;
                case "Customize VDS Server Upgrade": return (int)SepeteAtilanProductType.Customize_VDS_Server_Upgrade;
                case "Customize VDS Server Uzat": return (int)SepeteAtilanProductType.Customize_VDS_Server_Uzat;
                case "Statik IP": return (int)SepeteAtilanProductType.StatikIp;
                case "Sunucu yukseltme": return (int)SepeteAtilanProductType.Sunucu_Yukseltme;
                case "Web Klavuzu Yükseltme": return (int)SepeteAtilanProductType.Web_Klavuzu_Yukseltme;
                case "HyperV": return (int)SepeteAtilanProductType.HyperV;
                case "HyperV Uzat": return (int)SepeteAtilanProductType.HyperV_Uzat;
                case "HyperV Component": return (int)SepeteAtilanProductType.HyperV_Component;
                case "HyperV Extension": return (int)SepeteAtilanProductType.HyperV_Extension;
                case "Physical Server Uzat": return (int)SepeteAtilanProductType.PhysicalServer_Uzat;
                default: return -1;
            }
        }

        public static string MarkaProductType(SepeteAtilanMarkaProductType enm)
        {
            switch (enm)
            {
                case SepeteAtilanMarkaProductType.Marka_Tescil: return "Marka Tescil";
                default: return "unknown";
            }
        }

        public static int MarkaProductTypeEnum(string Type)
        {
            switch (Type)
            {
                case "Marka Tescil": return (int)SepeteAtilanMarkaProductType.Marka_Tescil;
                default: return -1;
            }
        }

        /// <summary>
        /// Query String için kullanılır.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="QueryStringParameter"> Query String Parametresi : <br>.aspx?OrderID </br></param>
        /// <returns></returns>
        public static string getQSVAL(NameValueCollection qs, string QueryStringParameter)
        {
            string donen = "";
            try
            {
                if (qs[QueryStringParameter] != null)
                    donen = qs[QueryStringParameter].ToString();
            }
            catch
            {
                donen = "";
            }

            return donen;
        }

        /// <summary>
        /// Session değerinin alınmasında kullanılır.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="QueryStringParameter"> Session String Parametresi : <br> Session["MemberID"] </br></param>
        /// <returns></returns>
        public static string getSesVal(HttpSessionState ses, string SessionParameter)
        {
            string donen = "";
            try
            {
                if (ses[SessionParameter] != null)
                    donen = ses[SessionParameter].ToString();
            }
            catch
            {
                donen = "";
            }

            return donen;
        }

        /// <summary>
        /// Unix Timestamp'ı UTC formatına cevirir
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime ConvertUnixTimeStampttoUTCFormat(Int64 time)
        {
            // First make a System.DateTime equivalent to the UNIX Epoch.
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            if (time != 0)
                dateTime = dateTime.AddMilliseconds(time);

            return dateTime.ToLocalTime();
        }

        /// <summary>
        /// Datetime dan  Unix Timestamp'e cevirir
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ConvertDatetimeToUnixTimeStampt(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total milliseconds (which is a UNIX timestamp)
            string _return = span.TotalMilliseconds.ToString();
            if (_return.IndexOf(".") > -1)
                _return = _return.Split('.')[0];
            if (_return.IndexOf(",") > -1)
                _return = _return.Split(',')[0];
            return _return;
        }

        public static DateTime ConvertToDateTimeFromTimeStamp(int time)
        {
            if (time != 0)
            {
                //This is an example of a UNIX timestamp for the date/time 11-04-2005 09:25.
                double timestamp = Convert.ToDouble(time);

                // First make a System.DateTime equivalent to the UNIX Epoch.
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                // Add the number of seconds in UNIX timestamp to be converted.
                dateTime = dateTime.AddSeconds(timestamp);

                // The dateTime now contains the right date/time so to format the string,
                // use the standard formatting methods of the DateTime object.
                return dateTime;
            }
            else
                return DateTime.Now;
        }

        public static string getPaymentType(int paymentType)
        {
            enmPaymentType payment = (enmPaymentType)paymentType;
            string donen = "";
            switch (payment)
            {
                case enmPaymentType.BankaHavalesi:
                    {
                        donen = "Banka Havalesi";
                        break;
                    }
                case enmPaymentType.MailOrder:
                    {
                        donen = "Mail Order";
                        break;
                    }
                case enmPaymentType.KrediKartıPeşin:
                    {
                        donen = "Kredi Kartı Peşin";
                        break;
                    }
                case enmPaymentType.KrediKartıTaksit:
                    {
                        donen = "Kredi Kartı Taksit";
                        break;
                    }
                case enmPaymentType.MailOrderTaksit:
                    {
                        donen = "Mail Order Taksit";
                        break;
                    }
                case enmPaymentType.Kredili:
                    {
                        donen = "Kredili";
                        break;
                    }
                case enmPaymentType.PostaÇeki:
                    {
                        donen = "Posta Çeki";
                        break;
                    }
                case enmPaymentType.BonusPay:
                    {
                        donen = "CepBank";
                        break;
                    }
                case enmPaymentType.ÖdemeYapılmamış:
                    {
                        donen = "Ödeme Yapıl mamış";
                        break;
                    }
                case enmPaymentType.OdemeTamamlandi:
                    {
                        donen = "Ödeme Tamamlandı";
                        break;
                    }
                default:
                    {
                        donen = " ";
                        break;
                    }
            }

            return donen;
        }

        public static string getPaymentType(enmPaymentType payment)
        {
            string donen = "";
            switch (payment)
            {
                case enmPaymentType.BankaHavalesi:
                    {
                        donen = "Banka Havalesi";
                        break;
                    }
                case enmPaymentType.MailOrder:
                    {
                        donen = "Mail Order";
                        break;
                    }
                case enmPaymentType.KrediKartıPeşin:
                    {
                        donen = "Kredi Kartı Peşin";
                        break;
                    }
                case enmPaymentType.KrediKartıTaksit:
                    {
                        donen = "Kredi Kartı Taksit";
                        break;
                    }
                case enmPaymentType.MailOrderTaksit:
                    {
                        donen = "Mail Order Taksit";
                        break;
                    }
                case enmPaymentType.Kredili:
                    {
                        donen = "Kredili";
                        break;
                    }
                case enmPaymentType.PostaÇeki:
                    {
                        donen = "Posta Çeki";
                        break;
                    }
                case enmPaymentType.BonusPay:
                    {
                        donen = "CepBank";
                        break;
                    }
                case enmPaymentType.PayPal:
                    {
                        donen = "PayPal";
                        break;
                    }
                case enmPaymentType.ÖdemeYapılmamış:
                    {
                        donen = "Ödeme Yapıl mamış";
                        break;
                    }
                case enmPaymentType.OdemeTamamlandi:
                    {
                        donen = "Ödeme Tamamlandı";
                        break;
                    }
                default:
                    {
                        donen = " ";
                        break;
                    }
            }

            return donen;
        }

        #region TC Kimlik numarası kontrolü
        private static bool ValidateNumber(string parNumber)
        {
            for (int i = 0; i < parNumber.Length; i++)
            {
                if (!(Char.IsNumber(parNumber[i]))) return false;
            }
            return true;
        }
        /// <summary>
        /// Alınan Kimlik numarasının doğruluk kontrolü yapılıyor
        /// </summary>
        /// <param name="TCIdentityNumber"> Tc Numarasını string olarak vermek gerekir </param>
        /// <returns> True dönerse Numara doğru  False dönerse yanlış </returns>
        public static bool ValidateTCIdentityNumber(string TCIdentityNumber)
        {
            bool mesaj = true;
            try
            {
                if (ValidateNumber(TCIdentityNumber.Trim()))
                {
                    if (TCIdentityNumber.Trim().Length < 11)
                    {
                        mesaj = false; //"T.C. Kimlik Numarası 11 karakterden az olamaz. Lütfen kontrol ediniz.";
                    }
                    else
                    {
                        decimal identityNumberDecimal = Convert.ToDecimal(TCIdentityNumber.Trim());
                        decimal temp = Math.Floor(identityNumberDecimal / 100);
                        decimal temp1 = Math.Floor(identityNumberDecimal / 100);
                        int[] D = new int[10];
                        for (int n = 9; n >= 1; n--)
                        {
                            D[n] = (int)temp1 % 10;
                            temp1 = Math.Floor(temp1 / 10);
                        }
                        int oddSum = D[9] + D[7] + D[5] + D[3] + D[1];
                        int evenSum = D[8] + D[6] + D[4] + D[2];
                        int total = oddSum * 3 + evenSum;
                        int ChkDigit1 = (10 - (total % 10)) % 10;
                        oddSum = ChkDigit1 + D[8] + D[6] + D[4] + D[2];
                        evenSum = D[9] + D[7] + D[5] + D[3] + D[1];
                        total = oddSum * 3 + evenSum;
                        int ChkDigit2 = (10 - (total % 10)) % 10;
                        temp = (temp * 100) + (ChkDigit1 * 10) + ChkDigit2;
                        if (temp != identityNumberDecimal)
                        {
                            mesaj = false; //mesaj = "T.C. Kimlik Numaranızı kontrol ediniz.";
                        }
                        else
                            mesaj = true; // mesaj = "T.C. Kimlik Numarası başarılı ";
                    }
                }
                else
                {
                    mesaj = false; // mesaj = "T.C. Kimlik Numarası yalnızca rakamlardan oluşmalıdır. Lütfen kontrol ediniz.";
                }
            }
            catch
            {
                mesaj = false;
            }
            return mesaj;
        }
        #endregion

        /// <summary>
        /// hosting upgrade işleminden kalan sessionları temizler
        /// </summary>
        /// <param name="Session"></param>
        public static void SessionClearForHostUpgrade(HttpSessionState Session)
        {
            Session["UpgradeHost"] = null;
            Session["HostingUpgrade"] = null;
        }

        public static bool TCNOKOntrol(char[] text)
        {
            int say = 0;
            bool sonuc = true;
            for (int i = 0; i < text.Length - 1; i++)
            {
                say += Int32.Parse(text[i].ToString());
            }
            char[] son = say.ToString().ToCharArray();
            if (son[say.ToString().Length - 1].ToString() == text[10].ToString())
                sonuc = true;
            else
                sonuc = false;

            return sonuc;
        }

        public static bool StrToBool(string str)
        {
            bool _return = false;
            try { _return = Boolean.Parse(str); }
            catch { _return = false; }

            return _return;
        }

        public static int StrToInt(string str)
        {
            int _return = 0;
            try { _return = Int32.Parse(str); }
            catch { _return = 0; }

            return _return;
        }

        public static long StrToLong(string str)
        {
            long _return = 0;
            try { _return = Int64.Parse(str); }
            catch { _return = 0; }

            return _return;
        }

        public static short StrToShort(string str)
        {
            short _return = 0;
            try { _return = Int16.Parse(str); }
            catch { _return = 0; }

            return _return;
        }

        public static double StrToDouble(string str)
        {
            str = str.Replace(".", ",");

            double _return = 0;
            try
            {
                _return = Double.Parse(str);
            }
            catch { _return = 0; }

            return _return;
        }

        ///Sonradan eklenen
        public static decimal StrToDecimal(string str)
        {
            str = str.Replace(".", ",");
            decimal _return = 0;

            if (!(Decimal.TryParse(str, out _return)))
                _return = 0.0m;

            return _return;
        }


        ///Sonradan eklenen
        public static float StrToFloat(string str)
        {
            str = str.Replace(".", ",");
            float _return = 0;

            if (!(float.TryParse(str, out _return)))
                _return = 0;

            return _return;
        }



        public static int DoubleToInt32(double dbl)
        {
            try { return Convert.ToInt32(dbl); }
            catch { return 0; }
        }


        public static int DecimalToInt32(decimal dbl)
        {
            try { return Convert.ToInt32(dbl); }
            catch { return 0; }
        }


        public static DateTime StrToDateTime(string str)
        {
            DateTime _return = new DateTime(1970, 1, 1);
            try { _return = DateTime.Parse(str); }
            catch { _return = new DateTime(1970, 1, 1); }

            return _return;
        }

        public static string Controlstring(string Statement, string CanUsedChars)
        {

            char[] canuse = CanUsedChars.ToCharArray();
            bool control = false;
            char[] stra = Statement.ToCharArray();
            for (int i = 0; i < stra.Length; i++)
            {
                control = false;
                if (stra[i].ToString() != " ")
                {
                    for (int j = 0; j < CanUsedChars.Length; j++)
                    {
                        if (stra[i].ToString() == canuse[j].ToString())
                        {
                            control = true;
                        }
                    }
                }
                else
                {
                    control = true;
                }

                if (control == false)
                {
                    stra[i] = '!';
                }
            }
            int count = Statement.Length;
            Statement = "";
            for (int i = 0; i < count; i++)
            {
                if (stra[i].ToString() != "!")
                {
                    Statement += stra[i].ToString();
                }
            }

            return Statement;
        }

        public static bool ControlUserPassword(string Password)
        {
            bool _retVal = true;
            string CanNotUsedChar = ";-_'@ŞşÇçİıÜüÖöĞğ";
            char[] cannotuse = CanNotUsedChar.ToCharArray();
            char[] stra = Password.ToCharArray();
            for (int i = 0; i < stra.Length; i++)
            {
                for (int j = 0; j < cannotuse.Length; j++)
                {
                    if (stra[i].ToString() == cannotuse[j].ToString())
                    {
                        _retVal = false;
                        continue;
                    }
                }
            }
            return _retVal;
        }

        public static string ControlName(string Name)
        {
            string CanUsedChar = "QWERTYUIOPĞÜASDFGHJKLŞİZXCVBNMÖÇqwertyuıopğüasdfghjklşizxcvbnmöç1234567890 '-,./&() ";
            Name = Controlstring(Name, CanUsedChar);
            return Name;
        }

        public static string ControlCompanyName(string CompanyName)
        {
            string CanUsedChar = "QWERTYUIOPĞÜASDFGHJKLŞİZXCVBNMÖÇqwertyuıopğüasdfghjklşizxcvbnmöç1234567890 &#-.,()/:;@' ";
            CompanyName = Controlstring(CompanyName, CanUsedChar);
            return CompanyName;
        }

        public static string ControlAddress(string Address)
        {
            string CanUsedChar = "QWERTYUIOPĞÜASDFGHJKLŞİZXCVBNMÖÇqwertyuıopğüasdfghjklşizxcvbnmöç1234567890 &#-.,()/:;'@ ";
            char[] canuse = CanUsedChar.ToCharArray();
            bool control = false;
            char[] stra = Address.ToCharArray();
            for (int i = 0; i < stra.Length; i++)
            {
                control = false;
                if (stra[i].ToString() != " ")
                {
                    for (int j = 0; j < CanUsedChar.Length; j++)
                    {
                        if (stra[i].ToString() == canuse[j].ToString())
                        {
                            control = true;
                        }
                    }
                }
                else
                {
                    control = true;
                }

                if (control == false)
                {
                    stra[i] = '!';
                }
            }
            int count = Address.Length;
            Address = "";
            for (int i = 0; i < count; i++)
            {
                if (stra[i].ToString() != "!")
                {
                    Address += stra[i].ToString();
                }
            }

            return Address;
        }

        /// <summary>
        /// TR karakterli domaileri arama yapmak için hazırlayıcı fonksiyon
        /// ÖR: gülşen.tk = glen olarak döner veya gülş = gl 
        /// </summary>
        /// <param name="DomainName"></param>
        /// <returns></returns>
        public static string PrepareForSearchingInIDNDomains(string DomainName)
        {
            string[] cannotuse = { "I", "Ü", "ü", "Ö", "ö", "Ş", "ş", "Ç,", "ç", "Ğ", "ğ" };
            bool flag = false;
            for (int i = 0; i < cannotuse.Length; i++)
            {
                if (DomainName.Contains(cannotuse[i].ToString()))
                {
                    DomainName = DomainName.Replace(cannotuse[i].ToString(), "");
                    flag = true;
                }

            }
            if (DomainName.Contains(".") && flag == true)
            {
                DomainName = DomainName.Substring(0, DomainName.IndexOf("."));
            }

            return DomainName;
        }

        public static string kontrol(string parametre)
        {
            if (string.IsNullOrWhiteSpace(parametre)) return "";
            string[] blackList2 = { ";--", ";", "/*", "*/", "@@", "'", "£", "#,", "½", "{", "[", "]", "}", "|", "<", ">", "¨", "`", "´", "æ", "ß", "é", "^", "%", "&", "]", "~", "^", "+" };
            return blackList2.Aggregate(parametre, (current, t) => current.Replace(t, ""));
        }
        public static string kontrol(string parametre, string[] istisna)
        {
            string[] blackList2 = { ";--", ";", "/*", "*/", "@@", "'",
                                "£", "#,", "½", "{", "[", "]", "}",
                                "|", "<", ">", "¨", "`", "´",
                                "æ", "ß", "é", "^", "%", "&", "]", "~", "^", "+" };


            foreach (var t in istisna)
            {
                for (var i = 0; i < blackList2.Length; i++)
                {
                    if (t == blackList2[i])
                    {
                        blackList2[i] = blackList2[i - 1];
                    }
                }
            }

            return blackList2.Aggregate(parametre, (current, t) => current.Replace(t, ""));
        }

        public static bool kontrolBool(string parametre)
        {
            bool flag = true;
            string[] blackList2 = { ";--", ";", "/*", "*/", "@@", "'",
                                "£", "#,", "½", "{", "[", "]", "}",
                                "|", "<", ">", "¨", "`", "´",
                                "ß", "é", "^", "%", "&", "]","!",
                                "+", "$", "/", "(", ")", "=", "?", "*",
                                "-", "_", ".", ","};

            for (int i = 0; i < blackList2.Length; i++)
            {
                if (parametre.Contains(blackList2[i]))
                {
                    flag = false;
                }
            }
            return flag;
        }

        public static string GetWhois(string DomainName, string WhoisServer)
        {
            string WhoisResult = string.Empty;
            TcpClient tcpc = new TcpClient();
            tcpc.Connect(WhoisServer, 43); // whois.internic.com
            String strDomain = DomainName + System.Environment.NewLine;
            Byte[] arrDomain = Encoding.ASCII.GetBytes(strDomain.ToCharArray());
            Stream s = tcpc.GetStream();
            s.Write(arrDomain, 0, strDomain.Length);
            StreamReader sr = new StreamReader(tcpc.GetStream(), Encoding.ASCII);
            WhoisResult = sr.ReadToEnd();
            return WhoisResult;
        }


        public static bool kontrolBoolDomainName(string parametre)
        {
            bool flag = true;
            string[] blackList2 = { ";--", ";", "/*", "*/", "@@", "'",
                                "£", "#,", "½", "{", "[", "]", "}",
                                "|", "<", ">", "¨", "`", "´",
                                 "ß", "é", "^", "%", "&", "]","!",
                                "+", "$", "/", "(", ")", "=", "?", "*",
                                 "_", ","};

            for (int i = 0; i < blackList2.Length; i++)
            {
                if (parametre.IndexOf(blackList2[i]) > -1)
                {
                    flag = false;
                }
            }
            return flag;
        }

        public static bool kontrolBool(string parametre, string[] istisna)
        {
            bool flag = true;
            string[] blackList2 = { ";--", ";", "/*", "*/", "@@", "'",
                                "£", "#,", "½", "{", "[", "]", "}",
                                "|", "<", ">", "¨", "`", "´",
                                "ß", "é", "^", "%", "&", "]","!",
                                "+", "$", "/", "(", ")", "=", "?", "*",
                                "-", "_", ".", ",", "#"};

            //for (int j = 0; j < istisna.Length; j++)
            //{
            //    for (int i = 0; i < blackList2.Length; i++)
            //    {
            //        if (istisna[j] == blackList2[i])
            //        {
            //            blackList2[i] = blackList2[i - 1];
            //        }
            //    }
            //}

            //for (int i = 0; i < blackList2.Length; i++)
            //{
            //    if (parametre.IndexOf(blackList2[i]) > -1)
            //    {
            //        flag = false;
            //    }
            //}
            //return flag;



            foreach (var s in blackList2)
            {
                if (!istisna.Contains(s) && parametre.Contains(s))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool kontrolDomain(string onayliKarekterler, char[] parametre)
        {
            bool flag = false;

            for (int j = 0; j < parametre.Length; j++)
            {
                if (onayliKarekterler.Contains(parametre[j].ToString())) flag = true;
                else
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        /// <summary>
        /// Verilen Parametrenin karekter sayısına göre anlamlı değeri ve referans verilen geriye kalan değeri döner.
        /// </summary>
        /// <param name="parametre"><b>Örnek :</b> Adres : (dsads asdasd asdasdsad)<br /> İşlem sonucunda  parametre = (dsads asdasd) </param>
        /// <param name="fazlalik"> gereye kalan karakterler = (asdasdsad) </param>
        /// <param name="sayi"> örnek = 50 </param>
        /// <returns></returns>
        public static string KarakterSayisiSinirlama(string parametre, ref string fazlalik, int sayi)
        {
            string content = parametre.Trim();
            if (content.Length < sayi)
                return parametre;
            string content50 = content.Substring(0, sayi);
            int index = content50.LastIndexOf(" ");
            int toplamUzunluk = content.Length;
            if (index > 0)
            {
                parametre = content.Substring(0, index);
                fazlalik = content.Substring(index, (toplamUzunluk - index));
            }
            else
            {
                parametre = content50;
                fazlalik = content.Substring(content50.Length, (toplamUzunluk - content50.Length));
            }
            return parametre;
        }



        public static string returnMemberView(string Approved, string GSMApproved)//Üyelerin Onaylı onaysız ve GSM onaylı onaysız görünüm şeklini döndürür
        {
            string sonuc = "";
            try
            {
                sonuc = Approved == "1" ? "<font color=\"#00CC00\">Onaylı</font>" : "<font color=\"#FF0000\">Onaysız</font>";
                sonuc = GSMApproved == "1" ? sonuc + " GSM <img src=\"images/OK.gif\">" : sonuc + " GSM <img src=\"images/Cancel.gif\">";
            }
            catch
            {
                sonuc = "hatalı işlem!";
            }

            return sonuc;
        }

        public static string getToEmailAdres()
        {
            ToEmailAdres = ConfigurationManager.AppSettings["ToEmailAdres"].ToString();
            return ToEmailAdres;
        }

        public static string getToEmailAdres2()
        {
            ToEmailAdres = ConfigurationManager.AppSettings["ToEmailAdres2"].ToString();
            return ToEmailAdres;
        }

        public static string getFromEmailAdres()
        {
            FromEmailAdres = ConfigurationManager.AppSettings["FromEmailAdres"].ToString();
            return FromEmailAdres;
        }

        public static string getFromEmailAdresCamping()
        {
            FromEmailAdres = ConfigurationManager.AppSettings["FromEmailAdresCamping"].ToString();
            return FromEmailAdres;
        }

        public static string KarakterTemizle(string kelime)
        {
            try
            {
                kelime = kelime.Replace('ç', 'c');
                kelime = kelime.Replace('Ç', 'C');
                kelime = kelime.Replace('ö', 'o');
                kelime = kelime.Replace('Ö', 'O');
                kelime = kelime.Replace('ü', 'u');
                kelime = kelime.Replace('Ü', 'U');
                kelime = kelime.Replace('ı', 'i');
                kelime = kelime.Replace('İ', 'I');
                kelime = kelime.Replace('ş', 's');
                kelime = kelime.Replace('Ş', 'S');
                kelime = kelime.Replace('ğ', 'g');
                kelime = kelime.Replace('Ğ', 'G');
                kelime = kelime.Replace('=', ':');
                kelime = kelime.Replace(',', ' ');
            }
            catch
            {
                return kelime;
            }
            return kelime;
        }

        public static string AdresIcinKarakterTemizle(string kelime)
        {
            try
            {
                kelime = kelime.Replace('ç', 'c');
                kelime = kelime.Replace('Ç', 'C');
                kelime = kelime.Replace('ö', 'o');
                kelime = kelime.Replace('Ö', 'O');
                kelime = kelime.Replace('ü', 'u');
                kelime = kelime.Replace('Ü', 'U');
                kelime = kelime.Replace('ı', 'i');
                kelime = kelime.Replace('İ', 'I');
                kelime = kelime.Replace('ş', 's');
                kelime = kelime.Replace('Ş', 'S');
                kelime = kelime.Replace('ğ', 'g');
                kelime = kelime.Replace('Ğ', 'G');
                kelime = kelime.Replace('=', ':');
                kelime = kelime.Replace(',', ' ');
                kelime = kelime.Replace('.', ' ');
                kelime = kelime.Replace(':', ' ');
            }
            catch
            {
                return kelime;
            }
            return kelime;
        }

        public static string CleanBadCharactersPlesk(string InputString, int InputLength)
        {
            string ReturnString;

            ReturnString = InputString.Trim();
            ReturnString = ReturnString.Replace(",", " ");
            ReturnString = ReturnString.Replace("ç", "c");
            ReturnString = ReturnString.Replace("Ç", "C");
            ReturnString = ReturnString.Replace("ğ", "g");
            ReturnString = ReturnString.Replace("Ğ", "G");
            ReturnString = ReturnString.Replace("ı", "i");
            ReturnString = ReturnString.Replace("İ", "I");
            ReturnString = ReturnString.Replace("ö", "o");
            ReturnString = ReturnString.Replace("Ö", "O");
            ReturnString = ReturnString.Replace("ş", "s");
            ReturnString = ReturnString.Replace("Ş", "S");
            ReturnString = ReturnString.Replace("ü", "u");
            ReturnString = ReturnString.Replace("Ü", "U");
            ReturnString = ReturnString.Replace("'", "`");
            ReturnString = ReturnString.Replace("\"", "´");
            ReturnString = ReturnString.Replace("'*", "");
            ReturnString = ReturnString.Replace("+", "");
            ReturnString = ReturnString.Replace("\n", " ");
            ReturnString = ReturnString.Replace("\r", " ");
            ReturnString = ReturnString.Replace("\t", " ");
            if (InputString.Length > InputLength)
                ReturnString = ReturnString.Substring(0, InputLength);

            return ReturnString;
        }

        public static string KarakterTemizleUTF8(string kelime)
        {
            kelime = kelime.Replace("ç", "Ã§");
            kelime = kelime.Replace("Ç", "Ã‡");
            kelime = kelime.Replace("ö", "Ã¶");
            kelime = kelime.Replace("Ö", "Ã–");
            kelime = kelime.Replace("ü", "Ã¼");
            kelime = kelime.Replace("Ü", "Ãœ");
            kelime = kelime.Replace("ı", "Ä±");
            kelime = kelime.Replace("İ", "Ä°");
            kelime = kelime.Replace("ş", "ÅŸ");
            kelime = kelime.Replace("Ş", "ÅŸ");
            kelime = kelime.Replace("ğ", "ÄŸ");
            kelime = kelime.Replace("Ğ", "ÄŸ");
            return kelime;
        }

        public static string KarakterTemizleUNICODE(string kelime)
        {

            kelime = kelime.Replace("ç", "&#231;");
            kelime = kelime.Replace("Ç", "&#199;");
            kelime = kelime.Replace("ö", "&#246;");
            kelime = kelime.Replace("Ö", "&#214");
            kelime = kelime.Replace("ü", "&#252");
            kelime = kelime.Replace("Ü", "&#220");
            kelime = kelime.Replace("ı", "&#305");
            kelime = kelime.Replace("İ", "&#304");
            kelime = kelime.Replace("ş", "&#351");
            kelime = kelime.Replace("Ş", "&#350");
            kelime = kelime.Replace("ğ", "&#287");
            kelime = kelime.Replace("Ğ", "&#286");
            kelime = kelime.Replace("<", "&lt;");
            kelime = kelime.Replace(">", "&gt;");
            return kelime;
        }

        public static string KarakterTemizleToEnglishChar(string kelime)
        {

            kelime = kelime.Replace("ç", "c");
            kelime = kelime.Replace("Ç", "C");
            kelime = kelime.Replace("ö", "o");
            kelime = kelime.Replace("Ö", "O");
            kelime = kelime.Replace("ü", "u");
            kelime = kelime.Replace("Ü", "U");
            kelime = kelime.Replace("ı", "i");
            kelime = kelime.Replace("İ", "I");
            kelime = kelime.Replace("ş", "s");
            kelime = kelime.Replace("Ş", "S");
            kelime = kelime.Replace("ğ", "g");
            kelime = kelime.Replace("Ğ", "G");
            kelime = kelime.Replace("<", "&lt;");
            kelime = kelime.Replace(">", "&gt;");
            return kelime;
        }

        public static bool isTurkishLetteExist(string kelime)
        {
            if (kelime.Contains("ç")) return true;
            if (kelime.Contains("ö")) return true;
            if (kelime.Contains("ü")) return true;
            if (kelime.Contains("ı")) return true;
            if (kelime.Contains("ş")) return true;
            if (kelime.Contains("ğ")) return true;
            return false;
        }

        public static string KarakterDuzenleUNICODEAndUTF8(string kelime)
        {

            kelime = kelime.Replace("Ä", "Ğ");
            kelime = kelime.Replace("Ã", "Ü");
            kelime = kelime.Replace("Å", "Ş");
            kelime = kelime.Replace("Ä°", "İ");
            kelime = kelime.Replace("Ã", "Ö");
            kelime = kelime.Replace("Ã", "Ç");
            kelime = kelime.Replace("Ä±", "ı");
            kelime = kelime.Replace("Ä", "ğ");
            kelime = kelime.Replace("Ã¼", "ü");
            kelime = kelime.Replace("Å", "ş");
            kelime = kelime.Replace("Ã¶", "ö");
            kelime = kelime.Replace("Ã§", "ç");
            kelime = kelime.Replace("ÄŸ", "ğ");
            kelime = kelime.Replace("ÅŸ", "ş");

            kelime = kelime.Replace("&#231;", "ç");
            kelime = kelime.Replace("&#199;", "Ç");
            kelime = kelime.Replace("&#246;", "ö");
            kelime = kelime.Replace("&#214", "Ö");
            kelime = kelime.Replace("&#252", "ü");
            kelime = kelime.Replace("&#220", "Ü");
            kelime = kelime.Replace("&#305", "ı");
            kelime = kelime.Replace("&#304", "İ");
            kelime = kelime.Replace("&#351", "ş");
            kelime = kelime.Replace("&#350", "Ş");
            kelime = kelime.Replace("&#287", "ğ");
            kelime = kelime.Replace("&#286", "Ğ");

            return kelime;
        }

        public static string KarakterTemizleSQL(string Kelime)
        {
            string[] blackList3 = {"'--",";--",";","/*","*/","@@","@", "&#39", "ascii(", "ASCII(", "}", "¨", 
                                         "`", "´", "æ", "ß", "é", "^", "&", "char","nchar","varchar","nvarchar","alter","begin","cast",
                                         "create","cursor","declare","delete","drop","exec","select", "sys","sysobjects",
                                         "syscolumns","fetch","insert","kill","open","table","update"};// Cookies için
            for (int i = 0; i < blackList3.Length; i++)
            {
                if (Kelime.Contains(blackList3[i].ToString()))
                {
                    Kelime = Kelime.Replace(blackList3[i].ToString(), "");
                }
            }
            return Kelime;
        }

        public static string KarakterTemizleUNICODEAndUTF8(string kelime)
        {
            kelime = kelime.Replace("Äž", "Ğ");
            kelime = kelime.Replace("Ãœ", "Ü");
            kelime = kelime.Replace("Åž", "Ş");
            kelime = kelime.Replace("Ä°", "İ");
            kelime = kelime.Replace("Ã–", "Ö");
            kelime = kelime.Replace("Ã‡", "Ç");
            kelime = kelime.Replace("Ã¼", "ü");
            kelime = kelime.Replace("Ã¶", "ö");
            kelime = kelime.Replace("Ã§", "ç");
            kelime = kelime.Replace("ÄŸ", "ğ");
            kelime = kelime.Replace("ÅŸ", "ş");
            kelime = kelime.Replace("Ä±", "ı");


            kelime = kelime.Replace("&#231;", "ç");
            kelime = kelime.Replace("&#199;", "Ç");
            kelime = kelime.Replace("&#246;", "ö");
            kelime = kelime.Replace("&#214", "Ö");
            kelime = kelime.Replace("&#252", "ü");
            kelime = kelime.Replace("&#220", "Ü");
            kelime = kelime.Replace("&#305", "ı");
            kelime = kelime.Replace("&#304", "İ");
            kelime = kelime.Replace("&#351", "ş");
            kelime = kelime.Replace("&#350", "Ş");
            kelime = kelime.Replace("&#287", "ğ");
            kelime = kelime.Replace("&#286", "Ğ");
            kelime = kelime.Replace("&lt;", "<");
            kelime = kelime.Replace("&gt;", ">");


            return kelime;
        }

        /// <summary>
        /// Clean turkish chars and return value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ClearTurkishChar(string value)
        {
            string val = value;

            val = val.Replace("ç", "c")
                .Replace("ğ", "g")
                .Replace("ı", "i")
                .Replace("ö", "o")
                .Replace("ş", "s")
                .Replace("ü", "u");

            return val;
        }

        public static bool CheckRegularity(string _rawString, string sPattern)
        {
            //char[] SerahChars = { '\'','_','-','.','0','1','2','3','4','5','6','7','8','9','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z' };            
            //Regex rx = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b",RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //string x = ">£#$½{[]}\\|,<.:>;@€¨~`´æß<é*-!'^+%&/()=?_";
            bool _return = false;
            _return = Regex.IsMatch(_rawString, sPattern);
            return _return;
        }

        public static string ReturRegularString(string _rawString, string sPattern)
        {
            string _return = "";
            _return = Regex.Replace(_rawString, sPattern, "");
            return _return;
        }

        public static void divideAddress(string adres)
        {
            if (adres.Length <= 64)
            {
                adres1 = adres;
                adres2 = "";
            }
            else if (adres.Length > 64)
            {
                adres1 = adres.Substring(0, 64);
                adres2 = adres.Substring(64);
            }
        }

        public static void divideAddress(ref string AddressI, ref string AddressII, int length)
        {
            string adres = AddressI + " " + AddressII;
            if (adres.Length <= length)
            {
                AddressI = adres;
                AddressII = "";
            }
            else if (adres.Length > length)
            {
                AddressI = adres.Substring(0, length);
                AddressII = adres.Substring(length);
            }
        }

        public static void divideName(string nameSurname, ref string name, ref string surname)
        {
            if (nameSurname.IndexOf(" ") > -1 && nameSurname.Length > 0)
            {
                name = nameSurname.Substring(0, nameSurname.LastIndexOf(" "));
                surname = nameSurname.Substring(nameSurname.LastIndexOf(" ") + 1);
            }
            else
            {
                name = nameSurname;
                surname = nameSurname;
            }
        }


        public static string returnRandomString(int characterLength)//SAYI VE KARAKTERDEN OLUŞAN RANDOM STRING
        {
            int randomSayi, secim, i;
            string randomString = "";
            Random rastGele = new Random();
            string[] letterArray = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "y", "x", "z" };

            for (i = 0; i < characterLength; i++)
            {
                secim = rastGele.Next(0, 2);//Sayi mi karakter mi seçilsin
                if (secim == 0)//sayi
                {
                    randomSayi = rastGele.Next(0, 9);
                    randomString += randomSayi.ToString();
                }
                else
                {
                    randomSayi = rastGele.Next(0, 25);
                    randomString += letterArray[randomSayi];
                }
            }
            return randomString;
        }

        public static string returnRandomSayiString(int characterLength)//SADECE SAYIDAN OLUŞAN RANDOM STRING
        {
            int randomSayi, i;
            string randomString = "";
            Random rastGele = new Random();

            for (i = 0; i < characterLength; i++)
            {
                randomSayi = rastGele.Next(0, 9);
                randomString += randomSayi.ToString();
            }
            return randomString;
        }

        public static string returnRandomCharacter(int characterLength)// KARAKTERDEN OLUŞAN RANDOM STRING
        {
            int randomSayi, i;
            string randomString = "";
            Random rastGele = new Random();
            string[] letterArray = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "y", "x", "z" };

            for (i = 0; i < characterLength; i++)
            {
                randomSayi = rastGele.Next(0, 25);
                randomString += letterArray[randomSayi];
            }
            return randomString;
        }

        public static string ReadFile(string Filename)
        {
            string Satir = "";
            string HTML = "";

            try
            {
                using (StreamReader Sr = new StreamReader(Filename, Encoding.Default, true))
                {
                    while ((Satir = Sr.ReadLine()) != null)
                    {
                        HTML += Satir;
                    }
                }

                return HTML;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// AuthCode en az 8 karakterden oluşmalıdır.
        /// AuthCode en fazla 16 karakterden oluşmalıdır.        
        /// en az bir harf içermelidir.
        /// en az bir rakam içermelidir.
        /// en az bir alphanumeric(özel) karakter içermelidir.        
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CreateAuthCode(int length)
        {
            string secretCode = "";
            int randomValue = 0;
            Random rnd = new Random();
            string[] letterArray = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "y", "x", "z" };
            string[] specialCharArray = { ";", "*", "!" };

            #region generate activationCode
            secretCode = "";
            for (int i = 0; i < 11; i++)
            {
                int secim = rnd.Next(0, 3);
                if (i == 10)
                {
                    randomValue = rnd.Next(0, 3);
                    secretCode += specialCharArray[randomValue];
                }
                if (secim == 0 && i > 0)
                {
                    randomValue = rnd.Next(0, 9);
                    secretCode += randomValue.ToString();
                }
                else if (secim == 1)
                {
                    randomValue = rnd.Next(0, 25);
                    secretCode += letterArray[randomValue];
                }
                else
                {
                    randomValue = rnd.Next(0, 3);
                    secretCode += specialCharArray[randomValue];
                }
            }
            #endregion
            return secretCode;
        }



        public static string ConvertToLongDateTimeFromTimestamp(string p)
        {
            throw new NotImplementedException();
        }



        public static string GetCheckSum()
        {
            return Security.staticEncrypt("&.FBS-Int.&");
        }

        public static string CreateCouponCode(int length)
        {
            string couponCode = "";

            string[] library = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };//62

            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int check = rnd.Next(0, 61);
                couponCode += library[check];
            }

            return Security.EncryptCouponCode(couponCode);
        }

        public static string RemoveMultiSpace(string value)
        {
            var ret = value;
            if (value.Contains("  "))
            {
                ret = ret.Replace("\n", " ").Replace("\r", "").Replace("\t", " ").Trim().Replace("  ", " ");
                ret = RemoveMultiSpace(ret);
            }
            return ret;
        }

        public static string DomainTLD(string domainName)
        {
            var indexOf = domainName.IndexOf(".");
            if (indexOf == -1) return "";
            return domainName.Substring(0, indexOf);
        }


        public static string GetTLD(string domainName)
        {
            var indexOf = domainName.IndexOf(".");
            if (indexOf == -1) return "";
            return domainName.Substring(indexOf, domainName.Length - indexOf);
        }

        /// <summary>
        /// Regex pattern ile domaini kontrol eder uyumsuz ise boş string dizisi döner, 
        /// uyumlu ise domain ve uzantısını parçalayarak döndürür.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static string[] DomainSplitter(string domain)
        {
            const string patternd = "^((?!-)[A-Za-z0-9-]{1,63}(?<!-)\\.)+[A-Za-z]{2,50}$";
            const string patternk = "^(?!-)[A-Za-z0-9-]{1,63}(?<!-)$";

            if (!Regex.Match(domain, domain.IndexOf('.') > -1 ? patternd : patternk).Success) return new string[0];

            var series = domain.IndexOf('.') == -1
                ? new[] { domain }
                : domain.Split(new[] { "." }, 2, StringSplitOptions.None);

            if (series.Length > 1)
                series[1] = "." + series[1];

            return series;

        }

        public static string CleanTransferCode(string code)
        {
            for (int i = 0; i < code.Length; i++)
            {
                if (code.Contains("[kucuk]")) code = code.Replace("[kucuk]", "<");
                if (code.Contains("[buyuk]")) code = code.Replace("[buyuk]", ">");
                if (code.Contains("[amp]")) code = code.Replace("[amp]", "&");
                if (code.Contains("[arti]")) code = code.Replace("[arti]", "+");
                if (code.Contains("[diyez]")) code = code.Replace("[diyez]", "#");
                if (code.Contains("[yuzde]")) code = code.Replace("[yuzde]", "%");
            }
            return code;
        }

        public static string EditTransferCode(string code)
        {
            for (int i = 0; i < code.Length; i++)
            {
                if (code.Contains("<")) code = code.Replace("<", "[kucuk]");
                if (code.Contains(">")) code = code.Replace(">", "[buyuk]");
                if (code.Contains("&")) code = code.Replace("&", "[amp]");
                if (code.Contains("+")) code = code.Replace("+", "[arti]");
                if (code.Contains("#")) code = code.Replace("#", "[diyez]");
                if (code.Contains("%")) code = code.Replace("%", "[yuzde]");
            }
            return code;
        }


        public static string TransferCodeEncoder(string code, string array)
        {
            var dic = new Dictionary<char, string>();

            for (var i = 0; i < array.ToCharArray().Length; i++)
            {
                var arrayItem = array.ToCharArray()[i];
                for (var index = 0; index < code.ToCharArray().Length; index++)
                {
                    var codeItem = code.ToCharArray()[index];
                    if (codeItem == arrayItem)
                    {
                        try
                        {
                            dic.Add(arrayItem, HttpUtility.UrlEncode(codeItem.ToString()));
                        }
                        catch
                        {
                        }
                    }
                }
            }

            for (var i = 0; i < array.ToCharArray().Length; i++)
            {
                var arrayItem = array.ToCharArray()[i];
                foreach (var dicItem in dic)
                {
                    if (dicItem.Key == arrayItem)
                    {
                        code = code.Replace(dicItem.Key.ToString(), dicItem.Value);
                    }
                }
            }

            return code;
        }

        public static string TLDUppercaseFirst(string s)
        {
            try
            {
                // Check for empty string.
                if (String.IsNullOrEmpty(s))
                {
                    return String.Empty;
                }
                // Return char and concat substring.
                return Char.ToUpper(s[0]).ToString() + Char.ToUpper(s[1]).ToString() + s.Substring(2);
            }
            catch
            {
                return s;
            }
        }

        public static string BlackListControl(string parametre)
        {
            if (parametre != null || parametre != string.Empty || parametre != "")
            {
                string[] blackList2 = { ";--", ";", "/*", "*/", "@@", "'",
                                "£", "#,", "½", "{", "[", "]", "}",
                                "|", "<", ">", "¨", "`", "´",
                                "æ", "ß", "é", "^", "%", "&", "]", "~", "^", "+" };

                for (int i = 0; i < blackList2.Length; i++)
                {
                    parametre = parametre.Replace(blackList2[i], "");
                }
            }
            else
                parametre = "";

            return parametre;
        }


        public static string StringRemoveTurkishCharacters(string value)
        {
            return value.Trim().Replace("ı", "i").Replace("ü", "u").Replace("Ü", "U").Replace("İ", "i").Replace("ş", "s").Replace("Ş", "S")
                        .Replace("ö", "o").Replace("Ö", "O").Replace("ğ", "g").Replace("Ğ", "G").Replace("ç", "c").Replace("Ç", "C");
        }

        public static bool StringContainsAlphaNumeric(string data)
        {
            return data == "" || data.Contains("+") || data.Contains("\"") || data.Contains("/")
                   || data.Contains("*") || data.Contains("#") || data.Contains("$") || data.Contains("&") || data.Contains("%")
                   || data.Contains("(") || data.Contains(")") || data.Contains("") || data.Contains("'") || data.Contains("\"")
                   || data.Contains("[") || data.Contains("]") || data.Contains("=") || data.Contains("?");
        }

        /// <summary>
        /// Türkçe karakterleri çevirir, "="-":",","-' ' şeklinde çevirir
        /// </summary>
        /// <param name="kelime"></param>
        /// <returns></returns>
        public static string CleanCharacters(string kelime)
        {
            return kelime.Replace('ç', 'c').Replace('Ç', 'C').Replace('ö', 'o').Replace('Ö', 'O').Replace('ü', 'u').Replace('Ü', 'U').Replace('ı', 'i').Replace('İ', 'I').Replace('ş', 's').Replace('Ş', 'S')
                .Replace('ğ', 'g').Replace('Ğ', 'G').Replace('=', ':').Replace(',', ' ');
        }

        public static string RandomString(int characterLength)//SAYI VE KARAKTERDEN OLUŞAN RANDOM STRING
        {
            int randomSayi, secim, i;
            string randomString = "";
            Random rastGele = new Random();
            string[] letterArray = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "y", "x", "z" };

            for (i = 0; i < characterLength; i++)
            {
                //if (i == 0 && characterLength>1) // tek karakter olusturmak isterse..
                //{
                //    randomString += "!";
                //    continue;
                //}

                //if (i == 1 && characterLength > 1)// tek karakter olusturmak isterse..
                if (i == 0 && characterLength > 1)// tek karakter olusturmak isterse..
                {
                    randomSayi = rastGele.Next(0, 25);
                    randomString += letterArray[randomSayi].ToUpper();
                    continue;
                }

                secim = rastGele.Next(0, 2);//Sayi mi karakter mi seçilsin
                if (secim == 0)//sayi
                {
                    randomSayi = rastGele.Next(0, 9);
                    randomString += randomSayi.ToString();
                }
                else
                {
                    randomSayi = rastGele.Next(0, 25);
                    randomString += letterArray[randomSayi];
                }
            }

            System.Threading.Thread.Sleep(100); // çok az bekle, for-foreach içinde çalıştırırlarsa mükerrer çıkmasın.
            return randomString;

        }

        //public static int ToInt32(int defaultValue, this object converReqString)
        //{
        //    try { return Convert.ToInt32(converReqString); }
        //    catch { return 0; }
        //}

        public static string ToStringOrBlank(this object stringValue)
        {
            if (stringValue == null) return "";
            return stringValue.ToString();
        }

        /// <summary>
        /// Convert a string to Integer, or return defaultValue
        /// </summary>
        /// <param name="converReqString"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt32(this object converReqString, int defaultValue)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(converReqString.ToString()))
                    return Convert.ToInt32(converReqString);
                else
                    return defaultValue;
            }
            catch { return defaultValue; }
        }

        /// <summary>
        /// Convert a string to Integer, or return defaultValue
        /// </summary>
        /// <param name="converReqString"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt32(this object converReqString)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(converReqString.ToString()))
                    return Convert.ToInt32(converReqString);
                else
                    return 0;
            }
            catch { return 0; }
        }
        /// <summary>
        /// Convert a string to Integer, or return defaultValue
        /// </summary>
        /// <param name="converReqString"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt32(this string converReqString)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(converReqString))
                    return Convert.ToInt32(converReqString);
                else
                    return 0;
            }
            catch { return 0; }
        }

        public static decimal ToDecimal(this string converReqString)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(converReqString))
                    return Convert.ToDecimal(converReqString);
                else
                    return 0;
            }
            catch { return 0; }
        }

        /// <summary>
        /// Convert a string to Short, or thrown error
        /// </summary>
        /// <param name="converReqString"></param>
        /// <returns></returns>
        public static short ToShort(this object converReqString)
        {
            try { return Convert.ToInt16(converReqString); }
            catch { throw new Exception("int ToShort(this object ConverReqString)"); }
        }

        /// <summary>
        /// Convert a string to Short, or return defaultValue
        /// </summary>
        /// <param name="converReqString"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ToShort(this object converReqString, short defaultValue)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(converReqString.ToString()))
                    return Convert.ToInt16(converReqString);
                else
                    return defaultValue;
            }
            catch { return defaultValue; }
        }

        /// <summary>
        /// Convert a long to Long, or thrown error
        /// </summary>
        /// <param name="converReqString"></param>
        /// <returns></returns>
        public static long ToLong(this object converReqString)
        {
            try { return long.Parse(converReqString.ToString()); }
            catch { throw new Exception("long ToLong(this object ConverReqString)"); }
        }

        /// <summary>
        /// Convert a string to Long, or return defaultValue
        /// </summary>
        /// <param name="converReqString"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToLong(this object converReqString, long defaultValue)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(converReqString.ToString()))
                    return long.Parse(converReqString.ToString());
                else
                    return defaultValue;
            }
            catch { return defaultValue; }
        }

        /// <summary>
        /// Convert a double to double or return defaltValue
        /// </summary>
        /// <param name="converReqString"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this object converReqString, double defaultValue = 0.0)
        {
            try { return Convert.ToDouble(converReqString); }
            catch { return defaultValue; }
        }


        /// <summary>
        /// Convert a double to double or return defaltValue
        /// </summary>
        /// <param name="converReqString"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float Tofloat(this object converReqString, float defaultValue = 0)
        {
            try { return Convert.ToSingle(converReqString); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// Convert a string to datetime or return 1970,1,1
        /// </summary>
        /// <param name="converReqString"></param>
        /// <returns></returns>
        public static DateTime ToDatetime(this object converReqString)
        {
            DateTime _return;

            try
            {
                _return = DateTime.Parse(converReqString.ToString());
            }
            catch
            {
                _return = new DateTime(1970, 1, 1);
            }

            return _return;
        }

        public static bool ToBool(this object converReqString)
        {
            bool _return = false;
            try
            {
                _return = bool.Parse(converReqString.ToString());
            }
            catch
            {
                _return = false;
            }

            return _return;
        }

        public static Decimal ToDecimal(this object input)
        {
            Decimal _return;

            try
            {
                _return = Decimal.Parse(input.ToString());
            }
            catch
            {
                throw new Exception();
            }

            return _return;
        }

        public static string GetStartTransaction()
        {
            return "<br/>" + DateTime.Now + " = ";
        }

        public static string RemoveSpecialCharactersForContact(this string str)
        {
            var sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ':' || c == '.' || c == ' '
                    || c == 'ı' || c == 'ğ' || c == 'ü' || c == 'ş' || c == 'i' || c == 'ç' || c == 'ö'
                    || c == 'I' || c == 'Ğ' || c == 'Ü' || c == 'Ş' || c == 'İ' || c == 'Ç' || c == 'Ö')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string DomainNameToIdn(this string domainName)
        {
            try
            {
                return IDNLib.Encode(domainName);
            }
            catch
            {
                return domainName;
            }
        }

        public static string IdnToDomainName(this string idn)
        {
            try
            {
                return IDNLib.Decode(idn);
            }
            catch
            {
                return idn;
            }
        }

        public static bool In<T>(T t, params T[] values)
        {
            bool D = false;

            foreach (var a in values)
            {
                if (t.ToString().Contains(a.ToString()))
                    D = true;
            }
            return D;
        }

        public static bool Where<T>(T t, params T[] values)
        {
            bool D = false;

            foreach (var a in values)
            {
                if (t.ToString() == a.ToString())
                    D = true;
            }
            return D;
        }

        public static string GetM(string str)
        {
            try
            {
                string ret = !string.IsNullOrWhiteSpace(str) ? str : "";
                return ret.Replace("\r","").Replace("\n","").Trim();
            }
            catch
            {
                return "";
            }
        }
    }

    /// <summary>
    /// Summary description for Security.
    /// </summary>
    public class Security
    {
        private const string strEncrKey = "&%#@?,:*.";

        public string unMemberSessionID
        {
            get
            {
                if (HttpContext.Current.Session["unMemberSessionID"] == null)
                {
                    HttpContext.Current.Session["unMemberSessionID"] = "";
                }

                return HttpContext.Current.Session["unMemberSessionID"].ToString();
            }

            set
            {
                HttpContext.Current.Session["unMemberSessionID"] = value;

            }
        }

        public Security()
        {
        }

        public string Encrypt(string strText)
        {
            byte[] byKey;
            byte[] inputByteArray;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Decrypt(string strText)
        {
            byte[] byKey;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            //byte inputByteArray(strText.Length);
            byte[] inputByteArray = new byte[strText.Length];
            try
            {

                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string RemoveInvalidChars(string strSource)
        {
            return Regex.Replace(strSource, @"[^0-9a-zA-Z=+\/ :]", "");
        }
        public static string Sha512CardNumberEncryption(string CardNumber)
        {
            string _hashCardNo;
            string _mixedKey = "fbs123CREDITfbs126CARDfbs54SECUREKEY";

            SHA512Managed HashTool = new SHA512Managed();
            Byte[] PasswordAsByte = Encoding.UTF8.GetBytes(string.Concat(CardNumber, _mixedKey));
            Byte[] EncryptedBytes = HashTool.ComputeHash(PasswordAsByte);
            HashTool.Clear();
            return _hashCardNo = Convert.ToBase64String(EncryptedBytes);
        }

        //For Coupon Codes

        public static string EncryptCouponCode(string couponCode)
        {
            byte[] byKey;
            byte[] inputByteArray;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                string strEncrKeyPair = strEncrKey + staticDecrypt(GeneralFunctions.GetCheckSum());
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKeyPair.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(couponCode);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch { return ""; }
        }

        public static string DecryptCouponCode(string EncryptedCouponCode)
        {
            byte[] byKey;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] inputByteArray = new byte[EncryptedCouponCode.Length];
            try
            {
                string strEncrKeyPair = strEncrKey + staticDecrypt(GeneralFunctions.GetCheckSum());
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKeyPair.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(EncryptedCouponCode);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch { return ""; }
        }

        public static string staticEncrypt(string strText)
        {
            byte[] byKey;
            byte[] inputByteArray;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

        public static string staticDecrypt(string strText)
        {
            byte[] byKey;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            //byte inputByteArray(strText.Length);
            byte[] inputByteArray = new byte[strText.Length];
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

        // Create an md5 sum string of this string
        static public string GetMd5Sum(string str)
        {
            // First we need to convert the string into bytes, which
            // means using a text encoder.
            Encoder enc = System.Text.Encoding.Unicode.GetEncoder();

            // Create a buffer large enough to hold the string
            byte[] unicodeText = new byte[str.Length * 2];
            enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);

            // Now that we have a byte array we can ask the CSP to hash it
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(unicodeText);

            // Build the final string by converting each byte
            // into hex and appending it to a StringBuilder
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }

            // And return it
            return sb.ToString();
        }

        /// <summary>
        /// hybrid panele giriş için 10 dk geçerli olacak bir şifre üretilir.
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="hostId"></param>
        /// <returns></returns>
        public static string LinkEncrypt(int memberId, int hostId)
        {
            var s = (memberId + 500) + "-" + new Security().Encrypt(DateTime.Now.AddMinutes(10) + "ikey") + "-" + (hostId + 300) + "-" + DateTime.Now.Ticks;
            var s1 = s.ToArray().Reverse();
            string r = "";
            foreach (var t in s1)
            {
                r += t;
            }
            return r;
        }

        public static class StringCipher
        {
            // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
            // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
            // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
            private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

            // This constant is used to determine the keysize of the encryption algorithm.
            private const int keysize = 256;

            public static string Encrypt(string plainText, string passPhrase)
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
                {
                    byte[] keyBytes = password.GetBytes(keysize / 8);
                    using (RijndaelManaged symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.Mode = CipherMode.CBC;
                        using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                                {
                                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                    cryptoStream.FlushFinalBlock();
                                    byte[] cipherTextBytes = memoryStream.ToArray();
                                    return Convert.ToBase64String(cipherTextBytes);
                                }
                            }
                        }
                    }
                }
            }

            public static string Decrypt(string cipherText, string passPhrase)
            {
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
                {
                    byte[] keyBytes = password.GetBytes(keysize / 8);
                    using (RijndaelManaged symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.Mode = CipherMode.CBC;
                        using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                        {
                            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}