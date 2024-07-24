using Ecommerce.Application.DTOs.Variants;

namespace Ecommerce.Application.Features.Variants.Commands.CreateVariant;

[Authorize]
public record CreateVariantCommand : IRequest<GetVariantDto>
{
    public string Name { get; set; } = "";
    public List<string> Options { get; set; } = null!;
}

public class CreateVariantCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IVariantRepository variantRepository
    ) : IRequestHandler<CreateVariantCommand, GetVariantDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IVariantRepository _variantRepository = variantRepository;

    public async Task<GetVariantDto> Handle(CreateVariantCommand request, CancellationToken cancellationToken)
    {
        var variant = new Variant(request.Name, request.Options);

        _variantRepository.Create(variant);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GetVariantDto>(variant);
    }
}
