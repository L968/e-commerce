using Ecommerce.Application.DTOs.Variants;

namespace Ecommerce.Application.Features.Variants.Queries;

public record GetVariantQuery() : IRequest<IEnumerable<GetVariantDto>>;

public class GetVariantQueryHandler(
    IMapper mapper,
    IVariantRepository variantRepository
    ) : IRequestHandler<GetVariantQuery, IEnumerable<GetVariantDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IVariantRepository _variantRepository = variantRepository;

    public async Task<IEnumerable<GetVariantDto>> Handle(GetVariantQuery request, CancellationToken cancellationToken)
    {
        var variants = await _variantRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetVariantDto>>(variants);
    }
}
