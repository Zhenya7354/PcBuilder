using PcBuilder.Enums;

namespace PcBuilder.Services.Interfaces;

public interface ICurrencyExchangeService
{
    Task<decimal> ConvertAsync(decimal amount, Currency from, Currency to, CancellationToken cancellationToken);
}
