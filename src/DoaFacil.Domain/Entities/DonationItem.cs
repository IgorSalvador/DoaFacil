using DoaFacil.Domain.Enums;

namespace DoaFacil.Domain.Entities;

public class DonationItem : Entity
{
    public string OwnerUserId { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public string? Description { get; private set; }
    public ItemCategory Category { get; private set; }
    public ItemCondition Condition { get; private set; }
    public int QuantityAvailable { get; private set; }
    public string ApproxLocation { get; private set; } = default!;

    private DonationItem() : base() { }

    public DonationItem(string ownerUserId, string title, ItemCategory category, ItemCondition condition, 
        int quantityAvailable, string approxLocation, string? description = null)
    {
        if(string.IsNullOrWhiteSpace(ownerUserId))
            throw new ArgumentException("OwnerUserId é obrigatório.", nameof(ownerUserId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("title é obrigatório.", nameof(title));

        if (string.IsNullOrWhiteSpace(approxLocation))
            throw new ArgumentException("approxLocation é obrigatório.", nameof(approxLocation));

        if (quantityAvailable <= 0)
            throw new ArgumentException("QuantityAvailable deve ser maior que 0.", nameof(quantityAvailable));

        OwnerUserId = ownerUserId.Trim();
        Title = title.Trim();
        Category = category;
        Condition = condition;
        QuantityAvailable = quantityAvailable;
        ApproxLocation = approxLocation.Trim();
        Description = description?.Trim();
    }

    /// <summary>
    /// Determines whether the item has sufficient available stock to fulfill the specified quantity.
    /// </summary>
    /// <param name="requestedQuantity">The quantity of the item to check for availability. Must be greater than zero.</param>
    /// <returns><see langword="true"/> if the item is not deleted and the available quantity is greater than or equal to
    /// <paramref name="requestedQuantity"/>; otherwise, <see langword="false"/>.</returns>
    public bool HasStock(int requestedQuantity) => IsActive && QuantityAvailable >= requestedQuantity;

    /// <summary>
    /// Deactivates the current instance.
    /// </summary>
    public void Deactivate() => IsActive = false;

    /// <summary>
    /// Consumes the specified quantity from the available amount, reducing the current stock accordingly.
    /// </summary>
    /// <remarks>If consuming the specified quantity reduces the available amount to zero, the item is marked
    /// as inactive.</remarks>
    /// <param name="quantity">The number of units to consume. Must be greater than 0 and less than or equal to the available quantity.</param>
    /// <exception cref="ArgumentException">Thrown when the specified quantity is less than or equal to 0.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the specified quantity exceeds the available quantity.</exception>
    public void Consume(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantidade a consumir deve ser superior a 0.");
        if (quantity > QuantityAvailable) throw new InvalidOperationException("Quantidade insuficiente.");

        QuantityAvailable -= quantity;

        if (QuantityAvailable == 0)
            IsActive = false;
    }
}
