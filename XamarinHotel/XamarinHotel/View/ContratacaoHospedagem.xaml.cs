using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinHotel.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContratacaoHospedagem : ContentPage
    {
        App PropriedadesApp;
        public ContratacaoHospedagem()
        {
            InitializeComponent();

            PropriedadesApp = (App)Application.Current;

            /**
             * Abastecendo o picker de quartos, com os valores definidos no array
             * de objetos, lá do App.xaml.cs
             */
            pck_quarto.ItemsSource = PropriedadesApp.tipos_quartos;

            /**
             * Definindo os valores máximos e minimos das datas. No minimo para o 
             * cliente não fazer Checkin no passado, e no máximo agendar o CheckIn
             * para daqui 6 meses;
             * 
             */
            dtpck_data_checkin.MinimumDate = DateTime.Now;
            dtpck_data_checkin.MaximumDate = new DateTime(DateTime.Now.Year,
                DateTime.Now.Month + 6, DateTime.Now.Day);

            /**
             * No checkout temos que definir que o cliente irá sair pelo menos
             * após uma diária, então o valor minimo será pelo menos o dia seguinte
             * e o tempo de estadia máxima será de 6 meses mais de uma semana.
             * 
             */
            dtpck_data_checkout.MinimumDate = new DateTime
                (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1);
            dtpck_data_checkout.MaximumDate = new DateTime
                (DateTime.Now.Year, DateTime.Now.Month +6, DateTime.Now.Day + 7);
        }

        private async void BtnCalcular_Clicked(object sender, EventArgs e)
        {
            try
            {
                int qnt_adultos = Convert.ToInt32(lbl_qnt_adultos.Text);
                int qnt_criancas = Convert.ToInt32(lbl_qnt_criancas.Text);

                if (qnt_adultos == 0 && qnt_criancas == 0)
                    throw new Exception("Desculpe, informe pelo menos um adulto ou uma criança.");

                /**
                 * Preciso fazer um castyng pq o c$ nao sabe o que tem dentro do pck_quarto,
                 * então eu falo pra ele que é um (Model.CategoriaQuarto)
                 */
                Model.CategoriaQuarto quarto_selecionado = (Model.CategoriaQuarto)pck_quarto.SelectedItem;

                if (quarto_selecionado == null)
                    throw new Exception("Desculpe, selecione um quarto.");
                /**
                 * Criando um objeto hospedagem que conterá todos os dados relativos a hospedagem
                 * e calculará o valor de acordo com o quarto.
                 */

                Model.Hospedagem dados_hospedagem = new Model.Hospedagem()
                {
                    Quarto = quarto_selecionado,

                    QuantidadeAdultos = qnt_adultos,
                    QuantidadeCriancas = qnt_criancas,

                    QuantidadeDias = Model.Hospedagem.CalcularTempoEstadia(dtpck_data_checkin.Date,
                    dtpck_data_checkout.Date),

                    DataCheckIn = dtpck_data_checkin.Date,
                    DataCheckOut = dtpck_data_checkout.Date

                };
                dados_hospedagem.ValorTotal = dados_hospedagem.CalcularValorEstadia();

                /**
                 * Cria uma instância da página que mostra os totais, adiciona o model
                 * Hospedaem ao bindContext da página para que as informações referentes
                 * a estadia estejam disponiveis na outra página: calculamos os dados aqui
                 * e enviamos para outra página.
                 */
                var segundaTela = new HospedagemCalculada();
                segundaTela.BindingContext = dados_hospedagem; //Objeto que contém os dados da hospedagem.

                await Navigation.PushAsync(segundaTela); //Navegando para ver o total.

            }catch(Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private void dtpck_data_checkin_DateSelected(object sender, DateChangedEventArgs e)
        {
            DatePicker elemento = (DatePicker)sender;

            DateTime data_checkin = elemento.Date;


            dtpck_data_checkout.MinimumDate = new DateTime
                (data_checkin.Year, data_checkin.Month, data_checkin.Day + 1);
             
        }
    }
}