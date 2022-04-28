using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Domen
{
    public class Lek : OpstiDomenskiObjekat
    {
        public int SifraLeka { get; set; }
        public string NazivLeka { get; set; }
        public int JacinaLeka { get; set; }
        public string UputstvoKoriscenja { get; set; }
        public TipLeka TipLeka { get; set; }

        public string Table1() => "Lek";
        public string Table2() => "Lek l";
        public string Insert() => $"{SifraLeka},'{NazivLeka}','{JacinaLeka}',{TipLeka.TipLekaID}";
        public string Update() => $"nazivLeka ='{NazivLeka}', jacinaLeka = {JacinaLeka}, uputstvoKoriscenja='{UputstvoKoriscenja}', tipLekaID={TipLeka.TipLekaID}";
        public string Join() => "join TipLeka t on (l.sifraLeka = t.tipLekaID)";
        public string Where() => $"where sifraLeka = {SifraLeka}";

        public List<OpstiDomenskiObjekat> GetReaderResult(SqlDataReader reader)
        {
            List<OpstiDomenskiObjekat> lista = new List<OpstiDomenskiObjekat>();

            while (reader.Read())
            {
                Lek lek = new Lek();
                lek.SifraLeka = Convert.ToInt32(reader[0]);
                lek.NazivLeka = reader[1].ToString();
                lek.JacinaLeka = Convert.ToInt32(reader[2]);
                TipLeka tip = new TipLeka();
                tip.TipLekaID = Convert.ToInt32(reader[3]);
                tip.NazivTipa = reader[5].ToString();
                lek.TipLeka = tip;
                lista.Add(lek);
            }
            reader.Close();

            return lista;
        }

        public override string ToString()
        {
            return NazivLeka;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}