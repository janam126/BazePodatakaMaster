using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulacija
{
    [Serializable]
    [Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native,
         IsByteOrdered = true, ValidationMethodName = "ValidatePoint")]

    public struct Procenti : INullable
    {

        private bool is_Null;
        private Double stopaRabata;
        private Double pdv;
        public bool IsNull => is_Null;

        public static Procenti Null
        {
            get
            {
                Procenti Procenti = new Procenti();
                Procenti.is_Null = true;
                return Procenti;
            }
        }

        public Double StopaRabata
        {
            get
            {
                return stopaRabata;
            }
            set
            {
                Double temp = stopaRabata;
                stopaRabata = value;
                if (!ValidatePoint())
                {
                    stopaRabata = temp;
                    throw new ArgumentException("Neispravan format stope rabata");
                }
            }
        }

        public Double PDV
        {
            get
            {
                return pdv;
            }
            set
            {
                Double temp = pdv;
                pdv = value;
                if (!ValidatePoint())
                {
                    pdv = temp;
                    throw new ArgumentException("Neispravan format PDV-a");
                }
            }
        }

        private bool ValidatePoint()
        {
            if ((stopaRabata >= 0 && stopaRabata <= 100) && (pdv >= 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [SqlMethod(OnNullCall = false)]
        public static Procenti Parse(SqlString s)
        {
            // With OnNullCall=false, this check is unnecessary if   
            // Point only called from SQL.  
            if (s.IsNull)
                return Null;

            // Parse input string to separate out points.  
            Procenti Procenti = new Procenti();
            string[] xy = s.Value.Split(",".ToCharArray());
            Procenti.stopaRabata = Convert.ToDouble(xy[0]);
            Procenti.pdv = Convert.ToDouble(xy[1]);

            // Call ValidatePoint to enforce validation  
            // for string conversions.  
            if (!Procenti.ValidatePoint())
                throw new ArgumentException("Nevalidne vrednosti");
            return Procenti;
        }

        [SqlMethod(OnNullCall = false)]
        public string Spojeno()
        {
            return $"Stopa rabata: {this.stopaRabata}\nPDV: {this.pdv}";
        }

        // Use StringBuilder to provide string representation of UDT.  
        public override string ToString()
        {
            // Since InvokeIfReceiverIsNull defaults to 'true'  
            // this test is unnecessary if Point is only being called  
            // from SQL.  
            if (this.IsNull)
                return "NULL";
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(this.stopaRabata);
                builder.Append(",");
                builder.Append(this.pdv);
                return builder.ToString();
            }
        }

    }

}
