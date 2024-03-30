using Ecommerce.Application.DTOs.Variants;

namespace Ecommerce.Application.Features.Variants.Queries;

public record GetVariantByIdQuery(Guid Id) : IRequest<GetVariantDto?>;

public class GetVariantByIdQueryHandler(
    IMapper mapper, 
    IVariantRepository variantRepository
    ) : IRequestHandler<GetVariantByIdQuery, GetVariantDto?>
{
    private readonly IMapper _mapper = mapper;
    private readonly IVariantRepository _variantRepository = variantRepository;

    public async Task<GetVariantDto?> Handle(GetVariantByIdQuery request, CancellationToken cancellationToken)
    {
        Variant? variant = await _variantRepository.GetByIdAsync(request.Id);
        return _mapper.Map<GetVariantDto?>(variant);
    }
}
