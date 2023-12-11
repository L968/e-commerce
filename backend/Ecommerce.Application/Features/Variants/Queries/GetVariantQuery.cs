using Ecommerce.Application.DTOs.Variants;
using Ecommerce.Domain.Repositories.VariantRepositories;

namespace Ecommerce.Application.Features.Variants.Queries;

public record GetVariantQuery() : IRequest<IEnumerable<GetVariantDto>>;

public class GetVariantQueryHandler : IRequestHandler<GetVariantQuery, IEnumerable<GetVariantDto>>
{
    private readonly IMapper _mapper;
    private readonly IVariantRepository _variantRepository;

    public GetVariantQueryHandler(IMapper mapper, IVariantRepository variantRepository)
    {
        _mapper = mapper;
        _variantRepository = variantRepository;
    }

    public async Task<IEnumerable<GetVariantDto>> Handle(GetVariantQuery request, CancellationToken cancellationToken)
    {
        var variants = await _variantRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetVariantDto>>(variants);
    }
}
