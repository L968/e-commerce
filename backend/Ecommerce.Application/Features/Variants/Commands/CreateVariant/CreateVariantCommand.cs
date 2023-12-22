using Ecommerce.Application.DTOs.Variants;

namespace Ecommerce.Application.Features.Variants.Commands.CreateVariant;

[Authorize]
public record CreateVariantCommand : IRequest<Result<GetVariantDto>>
{
    public string Name { get; set; } = "";
    public List<string> Options { get; set; } = null!;
}

public class CreateVariantCommandHandler : IRequestHandler<CreateVariantCommand, Result<GetVariantDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVariantRepository _variantRepository;

    public CreateVariantCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IVariantRepository variantRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _variantRepository = variantRepository;
    }

    public async Task<Result<GetVariantDto>> Handle(CreateVariantCommand request, CancellationToken cancellationToken)
    {
        Result<Variant> result = Variant.Create(request.Name, request.Options);
        if (result.IsFailed) return Result.Fail(result.Errors);

        _variantRepository.Create(result.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(_mapper.Map<GetVariantDto>(result.Value));
    }
}
