using System.Collections.ObjectModel;

namespace Stock_Exchange_Simulator;

public partial class MainPage : ContentPage
{
    public ObservableCollection<string> Ativos { get; set; }
    public string SelectedAtivo { get; set; }

    public MainPage()
    {
        InitializeComponent();

        // Inicializando a lista de ativos
        Ativos = new ObservableCollection<string>
        {
            "PETR4",
            "VALE3"
        };

        // Definindo o binding para a lista de ativos
        BindingContext = this;
    }

    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        // Aqui você pode capturar o item selecionado e fazer algo com ele
        var picker = (Picker)sender;
        string selectedAtivo = picker.SelectedItem as string;
        // Exemplo de ação: exibir o ativo selecionado
        DisplayAlert("Ativo Selecionado", selectedAtivo, "OK");
    }
}