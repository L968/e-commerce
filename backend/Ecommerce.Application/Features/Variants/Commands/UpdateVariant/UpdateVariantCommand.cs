using Ecommerce.Domain.Repositories.VariantRepositories;

namespace Ecommerce.Application.Features.Variants.Commands.UpdateVariant;

[Authorize]
public record UpdateVariantCommand : IRequest<Result>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public IEnumerable<string> Options { get; set; }
}

public class UpdateVariantCommandHandler : IRequestHandler<UpdateVariantCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVariantRepository _variantRepository;

    public UpdateVariantCommandHandler(IUnitOfWork unitOfWork, IVariantRepository variantRepository)
    {
        _unitOfWork = unitOfWork;
        _variantRepository = variantRepository;
    }

    public async Task<Result> Handle(UpdateVariantCommand request, CancellationToken cancellationToken)
    {
        Variant? variant = await _variantRepository.GetByIdAsync(request.Id);
        if (variant is null) return Result.Fail(DomainErrors.NotFound(nameof(variant), request.Id));

        var result = variant.Update(request.Name, request.Options);
        if (result.IsFailed) return result;

        _variantRepository.Update(variant);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
