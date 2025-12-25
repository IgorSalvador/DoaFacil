using DoaFacil.Domain.Enums;

namespace DoaFacil.Domain.Entities;

public class DonationRequest : Entity
{
    public Guid ItemId { get; private set; }
    public string RequesterUserId { get; private set; } = default!;
    public int RequestedQuantity { get; private set; }
    public DonationStatus Status { get; private set; } = DonationStatus.Requested;

    private readonly List<DonationStatusHistory> _statusHistory = [];
    public IReadOnlyCollection<DonationStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

    private DonationRequest() : base() { }

    public DonationRequest(Guid itemId, string requesterUserId, int requestedQuantity)
    {
        if (itemId == Guid.Empty) throw new ArgumentException("ItemId inválido.");
        if (string.IsNullOrWhiteSpace(requesterUserId)) throw new ArgumentException("RequesterUserId é obrigatório.");
        if (requestedQuantity <= 0) throw new ArgumentException("RequestedQuantity deve ser > 0.");

        ItemId = itemId;
        RequesterUserId = requesterUserId.Trim();
        RequestedQuantity = requestedQuantity;

        _statusHistory.Add(DonationStatusHistory.Create(
            donationRequestId: Id,
            from: null,
            to: DonationStatus.Requested,
            changedByUserId: RequesterUserId,
            reason: null));
    }

    /// <summary>
    /// Approves the donation and records the status change with the specified user as the approver.
    /// </summary>
    /// <remarks>This method transitions the donation status to Approved. If the donation cannot be approved
    /// in its current state, an exception may be thrown. The status change is recorded in the donation's status history
    /// for auditing purposes.</remarks>
    /// <param name="changedByUserId">The identifier of the user who is approving the donation. Cannot be null or empty.</param>
    public void Approve(string changedByUserId)
    {
        EnsureCanTransitionTo(DonationStatus.Approved);
        var from = Status;
        Status = DonationStatus.Approved;

        _statusHistory.Add(DonationStatusHistory.Create(Id, from, Status, changedByUserId, null));
    }

    /// <summary>
    /// Transitions the donation status to InPickup to indicate that the pickup process has started.
    /// </summary>
    /// <remarks>Call this method to record the start of the pickup process for the donation. The status
    /// history is updated to reflect this transition.</remarks>
    /// <param name="changedByUserId">The identifier of the user who initiated the pickup status change. Cannot be null or empty.</param>
    public void StartPickup(string changedByUserId)
    {
        EnsureCanTransitionTo(DonationStatus.InPickup);
        var from = Status;
        Status = DonationStatus.InPickup;

        _statusHistory.Add(DonationStatusHistory.Create(Id, from, Status, changedByUserId, null));
    }

    /// <summary>
    /// Marks the donation as completed and records the status change.
    /// </summary>
    /// <remarks>This method updates the donation's status to Completed and adds an entry to the status
    /// history. The donation must be in a state that allows transitioning to Completed; otherwise, an exception may be
    /// thrown.</remarks>
    /// <param name="changedByUserId">The identifier of the user who is completing the donation. Cannot be null or empty.</param>
    public void Complete(string changedByUserId)
    {
        EnsureCanTransitionTo(DonationStatus.Completed);
        var from = Status;
        Status = DonationStatus.Completed;

        _statusHistory.Add(DonationStatusHistory.Create(Id, from, Status, changedByUserId, null));
    }

    /// <summary>
    /// Cancels the donation and updates its status to Cancelled.
    /// </summary>
    /// <remarks>Cancellation is only allowed when the donation status is Requested, Approved, or InPickup.
    /// The cancellation reason is recorded in the donation's status history.</remarks>
    /// <param name="changedByUserId">The identifier of the user who is performing the cancellation.</param>
    /// <param name="reason">The reason for cancelling the donation. Cannot be null, empty, or whitespace.</param>
    /// <exception cref="ArgumentException">Thrown if the reason is null, empty, or consists only of white-space characters.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the donation status is not Requested, Approved, or InPickup.</exception>
    public void Cancel(string changedByUserId, string reason)
    {
        if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("Motivo é obrigatório.");

        if (Status is not (DonationStatus.Requested or DonationStatus.Approved or DonationStatus.InPickup))
            throw new InvalidOperationException($"Não é possível cancelar quando status = {Status}.");

        var from = Status;
        Status = DonationStatus.Cancelled;

        _statusHistory.Add(DonationStatusHistory.Create(Id, from, Status, changedByUserId, reason.Trim()));
    }

    /// <summary>
    /// Validates that the donation can transition from its current status to the specified target status.
    /// </summary>
    /// <param name="target">The status to which the donation is intended to transition.</param>
    /// <exception cref="InvalidOperationException">Thrown if the transition from the current status to the specified target status is not allowed.</exception>
    private void EnsureCanTransitionTo(DonationStatus target)
    {
        var allowed = Status switch
        {
            DonationStatus.Requested => target is DonationStatus.Approved or DonationStatus.Cancelled,
            DonationStatus.Approved => target is DonationStatus.InPickup or DonationStatus.Cancelled,
            DonationStatus.InPickup => target is DonationStatus.Completed or DonationStatus.Cancelled,
            _ => false
        };

        if (!allowed)
            throw new InvalidOperationException($"Transição inválida: {Status} -> {target}.");
    }
}
