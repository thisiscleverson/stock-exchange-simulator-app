using System.Collections.ObjectModel;
using System.Text;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Stock_Exchange_Simulator;

public partial class MainPage : ContentPage
{
    
    ApiServive api = new ApiServive();
    public ObservableCollection<string> Ativos { get; set; }
    public ObservableCollection<string> Lado { get; set; }
    
    public MainPage()
    {
        InitializeComponent();

  
        Ativos = new ObservableCollection<string>
        {
            "PETR4",
            "VALE3"
        };
        
        Lado = new ObservableCollection<string>
        {
            "Venda",
            "Compra"
        };

        BindingContext = this;
    }
    
    private async void OnEnviarClicked(object sender, EventArgs e) {
       
        if (AtivosSelects.SelectedIndex == -1 || LadoSelects.SelectedIndex == -1 || string.IsNullOrWhiteSpace(ValorInput.Text) || string.IsNullOrWhiteSpace(QuantidadeInput.Text) || string.IsNullOrWhiteSpace(contaInput.Text)) {
            await DisplayAlert("Erro", "Por favor, preencha todos os campos.", "OK");
            return;
        }
        
        var dados = new {
            symbol = AtivosSelects.SelectedItem?.ToString(),
            side   = LadoSelects.SelectedItem?.ToString() == "Venda"? true: false,
            price  = decimal.TryParse(ValorInput.Text, out var price) ? price : 0,
            quantity = int.TryParse(QuantidadeInput.Text, out var quantity) ? quantity : 0,
            account = contaInput.Text
        };
        
        JObject data = await api.sendNewOrder(dados);
        
        
        var orderId = (string)data["new_order_response"]["order_id"];;
        var symbol  = (string)data["new_order_response"]["symbol"];;
        var side    = (string)data["new_order_response"]["side"];;
        var value   = (decimal)data["new_order_response"]["price"];;
        var qty     = (int)data["new_order_response"]["quantity"];;

        string message = $"Nova Ordem criada com sucesso:\n" +
                         $"ID da Ordem: {orderId}\n" +
                         $"Ativo: {symbol}\n" +
                         $"Tipo: {side}\n" +
                         $"Valor: {value}\n" +
                         $"Quantidade: {qty}";

        await DisplayAlert("Nova Ordem", message, "Ok");

    }
}


public class ApiServive
{
    string hostBase = "http://192.168.0.132:5000";
    
    private static readonly HttpClient client = new HttpClient();

    public async Task<JObject> sendNewOrder(object dados) {
        
        string jsonPayload = JsonSerializer.Serialize(dados);
        
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        
        try{
            var response = await client.PostAsync($"{hostBase}/api/v1/sendNewOrder", content);
            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);
            
            JObject newOrderResponse = JObject.Parse(result);
            
            return newOrderResponse;
        }
        catch (Exception e){
            Console.WriteLine(e);
            throw;
        }
 
    }
}

