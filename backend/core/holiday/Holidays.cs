using System;
using System.Collections.Generic;

namespace core.holiday
{
    public class Holidays
    {
        public List<HolidayDate> Datas = new List<HolidayDate>();

        public Holidays()
        {
            var Ano = DateTime.Now.Year;
            GeraListaFeriados(Ano);
        }
        
        public Holidays(int Ano)
        {
            GeraListaFeriados(Ano);
        }

        private int ano;

        private void GeraListaFeriados(int ano)
        {
            this.ano = ano;

            var fm = new HolidayMovel(ano);
            
            Datas.Add(new HolidayDate(fm.DiaPascoa, "Domingo de Páscoa", true));
            Datas.Add(new HolidayDate(DateTime.Parse("01/01/" + ano), "Confraternização Universal"));
            Datas.Add(new HolidayDate(fm.SegundaCarnaval, "Segunda Carnaval", true));
            Datas.Add(new HolidayDate(fm.TercaCarnaval, "Terça Carnaval", true));
            Datas.Add(new HolidayDate(fm.SextaPaixao, "Sexta feira da paixão", true));
            Datas.Add(new HolidayDate(DateTime.Parse("21/04/" + ano), "Tiradentes"));
            Datas.Add(new HolidayDate(DateTime.Parse("01/05/" + ano), "Dia do trabalho")); 
            Datas.Add(new HolidayDate(fm.CorpusChristi, "Corpus Christi", true));
            Datas.Add(new HolidayDate(DateTime.Parse("07/09/" + ano), "Independência do Brasil"));
            Datas.Add(new HolidayDate(DateTime.Parse("12/10/" + ano), "Padroeira do Brasil"));
            Datas.Add(new HolidayDate(DateTime.Parse("02/11/" + ano), "Finados"));
            Datas.Add(new HolidayDate(DateTime.Parse("15/11/" + ano), "Proclamação da República"));
            Datas.Add(new HolidayDate(DateTime.Parse("25/12/" + ano), "Natal"));
        }

        public void AddFeriado(string data, string descricao)
        {
            Datas.Add(new HolidayDate(DateTime.Parse(data + ano), descricao));
        }

        public bool IsDiaUtil(DateTime data)
        {
            return IsFimDeSemana(data) || IsFeriado(data) ? false : true;
        }

        public bool IsFeriado(DateTime data)
        {
            return Datas.Find(f1=> f1.Data == data.Date) != null ? true : false;            
        }

        public static bool IsFimDeSemana(DateTime data)
        {
            return data.DayOfWeek == DayOfWeek.Sunday || data.DayOfWeek == DayOfWeek.Saturday ? true : false;
        }

        public DateTime ProximoDiaUtil(DateTime data)
        {
            var auxData = data;

            if (IsFeriado(auxData) || IsFimDeSemana(auxData))
            {
                auxData = data.AddDays(1);
                auxData = ProximoDiaUtil(auxData);
            }

            return auxData;
        }
    }
}
