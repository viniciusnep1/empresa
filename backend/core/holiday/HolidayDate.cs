using System;

namespace core.holiday
{
    public class HolidayDate
    {
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public bool Movel { get; set; } = false;

        public HolidayDate(DateTime dataFeriado, string descricao)
        {
            (Data, Descricao) = (dataFeriado, descricao);
        }

        public HolidayDate(DateTime dataFeriado, string descricao, bool movel)
        {
           (Data, Descricao, Movel) = (dataFeriado, descricao, movel);
        }
    }
}
