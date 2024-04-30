namespace Ecommerce.Application.Features.Variants.Commands.UpdateVariant;

[Authorize]
public record UpdateVariantCommand : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public IEnumerable<string> Options { get; set; } = null!;
}

public class UpdateVariantCommandHandler(
    IUnitOfWork unitOfWork,
    IVariantRepository variantRepository
    ) : IRequestHandler<UpdateVariantCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IVariantRepository _variantRepository = variantRepository;

    public async Task<Result> Handle(UpdateVariantCommand request, CancellationToken cancellationToken)
    {
        Variant? variant = await _variantRepository.GetByIdAsync(request.Id);
        if (variant is null) return DomainErrors.NotFound(nameof(variant), request.Id);

        var result = variant.Update(request.Name, request.Options);
        if (result.IsFailed) return result;

        _variantRepository.Update(variant);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
