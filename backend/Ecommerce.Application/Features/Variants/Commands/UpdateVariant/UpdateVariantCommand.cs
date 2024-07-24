namespace Ecommerce.Application.Features.Variants.Commands.UpdateVariant;

[Authorize]
public record UpdateVariantCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public IEnumerable<string> Options { get; set; } = null!;
}

public class UpdateVariantCommandHandler(
    IUnitOfWork unitOfWork,
    IVariantRepository variantRepository
    ) : IRequestHandler<UpdateVariantCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IVariantRepository _variantRepository = variantRepository;

    public async Task Handle(UpdateVariantCommand request, CancellationToken cancellationToken)
    {
        Variant? variant = await _variantRepository.GetByIdAsync(request.Id);
        DomainException.ThrowIfNull(variant, request.Id);

        variant.Update(request.Name, request.Options);

        _variantRepository.Update(variant);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
