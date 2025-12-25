using DoaFacil.Domain.Enums;

namespace DoaFacil.Domain.Entities;

public class DonationStatusHistory : Entity
{
    public Guid DonationRequestId { get; private set; }
    public DonationStatus? FromStatus { get; private set; }
    public DonationStatus ToStatus { get; private set; }
    public string ChangedByUserId { get; private set; } = default!;
    public DateTime ChangedAtUtc { get; private set; } = DateTime.UtcNow;
    public string? Reason { get; private set; }

    private DonationStatusHistory() : base (){ }

    /// <summary>
    /// Creates a new instance of the DonationStatusHistory class representing a status change for a donation request.
    /// </summary>
    /// <param name="donationRequestId">The unique identifier of the donation request for which the status change is being recorded. Must not be empty.</param>
    /// <param name="from">The previous status of the donation request, or null if this is the initial status.</param>
    /// <param name="to">The new status to which the donation request is being changed.</param>
    /// <param name="changedByUserId">The identifier of the user who performed the status change. Cannot be null, empty, or whitespace.</param>
    /// <param name="reason">An optional reason describing why the status was changed. May be null or empty.</param>
    /// <returns>A DonationStatusHistory instance containing the details of the status change.</returns>
    /// <exception cref="ArgumentException">Thrown if donationRequestId is empty or if changedByUserId is null, empty, or consists only of whitespace.</exception>
    public static DonationStatusHistory Create(
        Guid donationRequestId,
        DonationStatus? from,
        DonationStatus to,
        string changedByUserId,
        string? reason)
    {
        if (donationRequestId == Guid.Empty) throw new ArgumentException("DonationRequestId inválido.");
        if (string.IsNullOrWhiteSpace(changedByUserId)) throw new ArgumentException("ChangedByUserId é obrigatório.");

        return new DonationStatusHistory
        {
            DonationRequestId = donationRequestId,
            FromStatus = from,
            ToStatus = to,
            ChangedByUserId = changedByUserId.Trim(),
            Reason = reason?.Trim()
        };
    }
}
