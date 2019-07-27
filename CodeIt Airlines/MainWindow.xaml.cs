using CodeIT.Airlines.Business.Cenarios;
using CodeIT.Airlines.Business.Interfaces;
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
            var cenario = new CenarioExemplo();
            ObservarLogger(cenario);
            cenario.IniciarEmbarque();
        }

        private void ObservarLogger(IGerarLog logger)
        {
            logger.OnLog += Logger_OnLog;
        }

        private void Logger_OnLog(string msg)
        {
            rtbLog.AppendText(msg + Environment.NewLine);
            rtbLog.ScrollToEnd();
        }
    }
}
