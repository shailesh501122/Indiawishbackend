using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Payments.Domain;
using Subscriptions.Domain;
using Notifications.Domain;
using BuildingBlocks.Domain.Enums;

namespace Subscriptions.Application;

public sealed record ActivateSubscriptionCommand(Guid UserId, Guid PlanId, string PaymentProvider) : ICommand<Guid>;

public sealed class ActivateSubscriptionCommandValidator : AbstractValidator<ActivateSubscriptionCommand>
{
    public ActivateSubscriptionCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.PlanId).NotEmpty();
        RuleFor(x => x.PaymentProvider).NotEmpty();
    }
}

public sealed class ActivateSubscriptionCommandHandler(IApplicationDbContext dbContext, IPaymentGateway paymentGateway) : MediatR.IRequestHandler<ActivateSubscriptionCommand, Guid>
{
    public async Task<Guid> Handle(ActivateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var plan = await dbContext.SubscriptionPlans.FirstAsync(x => x.Id == request.PlanId && x.IsActive, cancellationToken);
        var activeSubscription = await dbContext.UserSubscriptions.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Status == SubscriptionStatus.Active, cancellationToken);
        if (activeSubscription is not null) activeSubscription.Expire();
        var subscription = new UserSubscription(request.UserId, plan.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(plan.DurationInDays));
        await dbContext.UserSubscriptions.AddAsync(subscription, cancellationToken);
        var payment = new Payment(request.UserId, subscription.Id, plan.Price, request.PaymentProvider);
        await dbContext.Payments.AddAsync(payment, cancellationToken);
        var result = await paymentGateway.ChargeAsync(payment, cancellationToken);
        if (!result.IsSuccessful)
        {
            payment.MarkFailed(result.ExternalReference);
            throw new InvalidOperationException(result.Message);
        }
        payment.MarkSuccessful(result.ExternalReference);
        await dbContext.Notifications.AddAsync(new NotificationMessage(request.UserId, NotificationChannel.Email, "Subscription activated", $"Your {plan.Name} plan is active until {subscription.EndsOnUtc:yyyy-MM-dd}."), cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return subscription.Id;
    }
}
