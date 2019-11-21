using System;

namespace core.holiday
{
    public class HolidayMovel
    {
        #region Properties

        public DateTime DiaPascoa { get; }
        public DateTime DomingoCarnaval { get; }
        public DateTime SegundaCarnaval { get; }
        public DateTime TercaCarnaval { get; }
        public DateTime SextaPaixao { get; }
        public DateTime CorpusChristi { get; }
        #endregion

        public HolidayMovel(int ano)
        {
            DiaPascoa = CalculaDiaPascoa(ano);
            DomingoCarnaval = CalculaDomingoCarnaval(DiaPascoa);
            SegundaCarnaval = CalculaSegundaCarnaval(DiaPascoa);
            TercaCarnaval = CalculaTercaCarnaval(DiaPascoa);
            SextaPaixao = CalculaSextaPaixao(DiaPascoa);
            CorpusChristi = CalculaCorpusChristi(DiaPascoa);
        }

        private static DateTime CalculaDiaPascoa(int anoCalcular)
        {
            var x = 24;
            var y = 5;

            var a = anoCalcular % 19;
            var b = anoCalcular % 4;
            var c = anoCalcular % 7;

            var d = (19 * a + x) % 30;
            var e = (2 * b + 4 * c + 6 * d + y) % 7;

            var dia = 0;
            var mes = 0;

            if (d + e > 9)
            {
                dia = (d + e - 9);
                mes = 4;
            }
            else
            {
                dia = (d + e + 22);
                mes = 3;
            }
            return DateTime.Parse(string.Format("{0},{1},{2}", anoCalcular.ToString(), mes.ToString(), dia.ToString()));
        }


        private static DateTime CalculaDomingoCarnaval(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(-49);
        }

        private static DateTime CalculaSegundaCarnaval(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(-48);
        }

        private static DateTime CalculaTercaCarnaval(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(-47);
        }

        private static DateTime CalculaSextaPaixao(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(-2);
        }

        private static DateTime CalculaCorpusChristi(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(+60);
        }
    }
}

