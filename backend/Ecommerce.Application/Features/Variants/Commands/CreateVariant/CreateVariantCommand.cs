using Ecommerce.Application.DTOs.Variants;

namespace Ecommerce.Application.Features.Variants.Commands.CreateVariant;

[Authorize]
public record CreateVariantCommand : IRequest<Result<GetVariantDto>>
{
    public string Name { get; set; } = "";
    public List<string> Options { get; set; } = null!;
}

public class CreateVariantCommandHandler(
    IMapper mapper, 
    IUnitOfWork unitOfWork, 
    IVariantRepository variantRepository
    ) : IRequestHandler<CreateVariantCommand, Result<GetVariantDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IVariantRepository _variantRepository = variantRepository;

    public async Task<Result<GetVariantDto>> Handle(CreateVariantCommand request, CancellationToken cancellationToken)
    {
        Result<Variant> result = Variant.Create(request.Name, request.Options);
        if (result.IsFailed) return Result.Fail(result.Errors);

        _variantRepository.Create(result.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(_mapper.Map<GetVariantDto>(result.Value));
    }
}
