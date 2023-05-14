using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinHotel.Model
{
    public class Hospedagem
    {
        /**
         * Quando eu crio um novo objeto do tipo Hospedagem, tenho
         * que informar qual quarto que é. Dentro do Quarto, lá no meu Model
         * CategoriaQuarto, eu tenho minhas propriedades
         */
        public CategoriaQuarto Quarto { get; set; }
        public int QuantidadeAdultos { get; set; }
        public int QuantidadeCriancas { get; set; }
        public int QuantidadeDias { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }
        public double ValorTotal { get; set; }

        /**
         * Calcula e retorna a diferença em dias da data de um CheckIn e um Checkout.
         */
        public static int CalcularTempoEstadia(DateTime checkin, DateTime checkout)
        {
            /**
             * Calculo a diferença dessa data
             */
            int total_dias = checkout.Subtract(checkin).Days;

            if (total_dias <= 0)
                throw new Exception("Saida não pode ser inferior a entrada");

            return total_dias;
        }

        /**
         * Calcula o valor da hospedagem de acordo com o quarto escolhido, tipo de hospede,
         * e quantidade de hospedes.
         */
        public double CalcularValorEstadia()
        {
            double valor_adultos = (QuantidadeAdultos * Quarto.ValorDiariaAdulto) * QuantidadeDias;
            double valor_criancas = (QuantidadeCriancas * Quarto.ValorDiariaCrianca) * QuantidadeDias;
            double valor_hospedagem = valor_adultos + valor_criancas;
            return valor_hospedagem;
        }



    }
}
