using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinHotel
{
    public partial class App : Application
    {
        /**
         * Instanciando a lista de quartos disponiveis no hotel
         * propriedade chamada tipos_quartos que é um list de categoriaQuarto
         */
        public List<Model.CategoriaQuarto> tipos_quartos = new List<Model.CategoriaQuarto>()
        {
            new Model.CategoriaQuarto()
            {
               Descricao = "Suíte Super Luxo",
               ValorDiariaAdulto = 110.0,
               ValorDiariaCrianca = 55.0
            },
            new Model.CategoriaQuarto()
            {
               Descricao = "Suíte Luxo",
               ValorDiariaAdulto = 80.0,
               ValorDiariaCrianca = 40.0
            },
            new Model.CategoriaQuarto()
            {
                Descricao = "Suite Single ", 
                ValorDiariaAdulto = 50.0, 
                ValorDiariaCrianca = 25.0
            },
            new Model.CategoriaQuarto()
            {
                Descricao = "Suite Crise ",
                ValorDiariaAdulto = 25.0,
                ValorDiariaCrianca = 12.5
            }
        };

        public App()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            /**
             * MainPage é uma propriedade da classe App
             * Instanciei que vai ser um NavigationPage pra poder trocar
             * de tela, e a ContratacaoHospedagem é a pagina inicial
             */
            MainPage = new NavigationPage(new View.ContratacaoHospedagem());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
