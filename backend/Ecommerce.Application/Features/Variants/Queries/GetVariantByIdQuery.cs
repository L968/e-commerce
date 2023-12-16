using Ecommerce.Application.DTOs.Variants;
using Ecommerce.Domain.Repositories.VariantRepositories;

namespace Ecommerce.Application.Features.Variants.Queries;

public record GetVariantByIdQuery(int Id) : IRequest<GetVariantDto?>;

public class GetVariantByIdQueryHandler : IRequestHandler<GetVariantByIdQuery, GetVariantDto?>
{
    private readonly IMapper _mapper;
    private readonly IVariantRepository _variantRepository;

    public GetVariantByIdQueryHandler(IMapper mapper, IVariantRepository variantRepository)
    {
        _mapper = mapper;
        _variantRepository = variantRepository;
    }

    public async Task<GetVariantDto?> Handle(GetVariantByIdQuery request, CancellationToken cancellationToken)
    {
        Variant? variant = await _variantRepository.GetByIdAsync(request.Id);
        return _mapper.Map<GetVariantDto?>(variant);
    }
}
