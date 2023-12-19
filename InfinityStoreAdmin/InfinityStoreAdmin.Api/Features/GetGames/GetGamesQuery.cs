using System.ComponentModel;
using MediatR;

namespace InfinityStoreAdmin.Api.Features.GetGames
{
    public class GetGamesQuery : IRequest<GetGamesResponse>
    {
        [DefaultValue("")]
        public string SearchString { get; set; }

        [DefaultValue(1)]
        public int CurrentPage { get; set; }

        [DefaultValue(4)]
        public int ItemsPerPage { get; set; }
        public bool? IsTitleUp { get; set; }
        public bool? IsPriceUp { get; set; }
    }
}
