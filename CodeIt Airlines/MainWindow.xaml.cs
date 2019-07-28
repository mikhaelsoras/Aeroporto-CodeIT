using CodeIT.Airlines.Business.Cenarios;
using CodeIT.Airlines.Business.Interfaces;
using System;
using System.Windows;

namespace CodeIt_Airlines
{
    public partial class MainWindow : Window
    {
        IGerarLog loggerAtual;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void IniciarTransferenciaTripulantes(ICenario cenario)
        {
            rtbLog.Document.Blocks.Clear();
            ObservarLogger(cenario);
            cenario.IniciarEmbarque();
        }

        private void ObservarLogger(IGerarLog logger)
        {
            if (loggerAtual != null)
                loggerAtual.OnLog -= Logger_OnLog;

            loggerAtual = logger;
            logger.OnLog += Logger_OnLog;
        }

        private void Logger_OnLog(string msg)
        {
            rtbLog.AppendText($"{msg} {Environment.NewLine}");
            rtbLog.ScrollToEnd();
        }

        private void BtnCenarioTradicionalClick(object sender, RoutedEventArgs e)
        {
            var cenario = new CenarioTradicional();
            IniciarTransferenciaTripulantes(cenario);
        }

        private void BtnCenarioCompletoClick(object sender, RoutedEventArgs e)
        {
            var cenario = new CenarioTranferirTodos();
            IniciarTransferenciaTripulantes(cenario);
        }
    }
}
