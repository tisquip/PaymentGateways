namespace SASA.PayNow.SampleClient.RazorP.Services
{
    public interface IShoppingCart
    {
        string AdditionalInformation { get; }
        System.Collections.Generic.Dictionary<string, (decimal price, int quantity)> Cart { get; }
        string Reference { get; }
        decimal Total { get; }
    }
}