using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Domen
{
    public class TipLeka : OpstiDomenskiObjekat
    {
        public int TipLekaID { get; set; }
        public string NazivTipa { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public List<OpstiDomenskiObjekat> GetReaderResult(SqlDataReader reader)
        {
            List<OpstiDomenskiObjekat> lista = new List<OpstiDomenskiObjekat>();

            while (reader.Read())
            {
                TipLeka tip = new TipLeka();
                tip.TipLekaID = Convert.ToInt32(reader[0]);
                tip.NazivTipa = reader[1].ToString();
                lista.Add(tip);
            }

            reader.Close();
            return lista;
        }

        public string Insert()
        {
            return $"{TipLekaID},'{NazivTipa}'";
        }

        public string Join()
        {
            return "";
        }

        public string Table1()
        {
            return "TipLeka";
        }

        public string Table2()
        {
            return "TipLeka tl";
        }

        public override string ToString()
        {
            return NazivTipa;
        }

        public string Update()
        {
            return $"nazivTipa = '{NazivTipa}'";

        }

        public string Where()
        {
            return $"where tipLekaID = {TipLekaID}";
        }
    }
}