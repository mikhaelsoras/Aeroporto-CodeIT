using CodeIT.Airlines.Models.Carros;
using CodeIT.Airlines.Models.Carros.Interfaces;
using CodeIT.Airlines.Models.Entidades;
using CodeIT.Airlines.Models.Entidades.Interfaces;
using CodeIT.Airlines.Models.Locais;
using CodeIT.Airlines.Models.Locais.Interfaces;
using System.Windows;

namespace CodeIt_Airlines
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                ILocal aeroporto = new Local("Aeroporto");
                ICarro carro = new Carro("Smart Fortwo", 2);
                ILocal terminal = new Local("Terminal");

                IPessoa[] pessoasAeroporto = { new Presidiario(), new Policial() };

                terminal.RegistrarEntrada(pessoasAeroporto);
                terminal.RegistrarEntrada(new Passageiro());
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
    }
}
