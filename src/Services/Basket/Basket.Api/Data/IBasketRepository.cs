namespace Basket.Api.Data;

public interface IBasketRepository
{
    /// <summary>
    /// Retrieves the shopping cart for a specified user.
    /// </summary>
    /// <param name="userName">The username for which to retrieve the shopping cart.</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
    /// <returns>A Task representing the asynchronous operation, with a ShoppingCart as the result.</returns>
    Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stores or updates a shopping cart for a specified user.
    /// </summary>
    /// <param name="basket">The shopping cart to store.</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
    /// <returns>A Task representing the asynchronous operation, with a ShoppingCart as the result.</returns>
    Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the shopping cart for a specified user.
    /// </summary>
    /// <param name="userName">The username for which to delete the shopping cart.</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
    /// <returns>A Task representing the asynchronous operation, with a boolean indicating success or failure.</returns>
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
}