using CodeIT.Airlines.Business.Cenarios;
using System;
using System.Windows;

namespace CodeIt_Airlines
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IniciarTransferenciaTripulantes();
        }

        private void IniciarTransferenciaTripulantes()
        {
            //try
            //{
                var cenario = new CenarioExemplo();
                cenario.IniciarEmbarque();
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}

            Close();
        }
    }
}
