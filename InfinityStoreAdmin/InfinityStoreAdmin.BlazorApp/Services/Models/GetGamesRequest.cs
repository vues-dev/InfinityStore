namespace InfinityStoreAdmin.BlazorApp.Services.Models;

public class GetGamesRequest
{
    // sorting

    public bool? IsTitleUp { get; set; }

    public bool? IsPriceUp { get; set; }

    // filtering

    public string SearchString { get; set; }

    //pagination

    public int CurrentPage { get; set; }

    public int ItemsPerPage { get; set; }
}