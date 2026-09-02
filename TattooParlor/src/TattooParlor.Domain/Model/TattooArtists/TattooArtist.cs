using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.TattooArtists;

public class TattooArtist : Person
{
    public required DateOnly WorkStartDate { get; set; }
    public double Rating { get; set; }
    public TattooStyle? FavoriteStyle { get; set; }
}
