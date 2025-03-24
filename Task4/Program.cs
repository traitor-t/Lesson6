public delegate void StockPriceUpdatedEventHandler(object sender, StockPriceUpdatedEventArgs e);
public class StockPriceUpdatedEventArgs : EventArgs
{
    public string StockName { get; }
    public decimal NewPrice { get; }

    public StockPriceUpdatedEventArgs(string stockName, decimal newPrice)
    {
        StockName = stockName;
        NewPrice = newPrice;
    }
}
public class StockMarket
{
    public event StockPriceUpdatedEventHandler StockPriceUpdated;

    public void UpdateStockPrice(string stockName, decimal newPrice)
    {
        OnStockPriceUpdated(stockName, newPrice);
    }

    protected virtual void OnStockPriceUpdated(string stockName, decimal newPrice)
    {
        StockPriceUpdated?.Invoke(this, new StockPriceUpdatedEventArgs(stockName, newPrice));
    }
}
public class MarketObserver
{
    private readonly Investor _investor;
    private readonly NewsPublisher _newsPublisher;

    public MarketObserver(Investor investor, NewsPublisher newsPublisher)
    {
        _investor = investor;
        _newsPublisher = newsPublisher;
    }

    public void Subscribe(StockMarket market)
    {
        market.StockPriceUpdated += _investor.OnStockPriceUpdated;
        market.StockPriceUpdated += _newsPublisher.OnStockPriceUpdated;
    }

    public void Unsubscribe(StockMarket market)
    {
        market.StockPriceUpdated -= _investor.OnStockPriceUpdated;
        market.StockPriceUpdated -= _newsPublisher.OnStockPriceUpdated;
    }
}
public class Investor
{
    public void OnStockPriceUpdated(object sender, StockPriceUpdatedEventArgs e)
    {
        Console.WriteLine($"Инвестор: Новая цена акций {e.StockName}: {e.NewPrice:C}");
    }
}

public class NewsPublisher
{
    public void OnStockPriceUpdated(object sender, StockPriceUpdatedEventArgs e)
    {
        Console.WriteLine($"Публикатор новостей: Обновление цены акций {e.StockName}: {e.NewPrice:C}");
    }
}
class Program
{
    static void Main(string[] args)
    {
        StockMarket stockMarket = new StockMarket();
        Investor investor = new Investor();
        NewsPublisher newsPublisher = new NewsPublisher();
        MarketObserver marketObserver = new MarketObserver(investor, newsPublisher);

        marketObserver.Subscribe(stockMarket);

        stockMarket.UpdateStockPrice("Apple", 150.75m);
        stockMarket.UpdateStockPrice("Microsoft", 280.50m);

        marketObserver.Unsubscribe(stockMarket);
    }
}