using FSPBook.Data;
using FSPBook.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSPBook.Portal.Utilities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Security.Cryptography.Xml;
using RestSharp;
using System.Text.Json;

namespace FSPBook.Pages;
public class IndexModel : PageModel
{
   
    private readonly IConfiguration _configuration;
    private Context _dbContext { get; set; }
    [BindProperty]
    public PaginatedList<Post> Posts { get; set; }
    public string NameSort { get; set; }
    public string DateSort { get; set; }
    public string CurrentFilter { get; set; }
    public string CurrentSort { get; set; }

    public NewsFeed NewsFeed { get; set; }
    public IndexModel(IConfiguration configuration,Context dbContext)
    {      
        _configuration = configuration;
        _dbContext = dbContext;
    }

    public async void OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
    {
        CurrentSort = sortOrder;
        NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        DateSort = sortOrder == "Date" ? "date_desc" : "Date";
        if (searchString != null)
        {
            pageIndex = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        CurrentFilter = searchString;

        IQueryable<Post> postsIQ = from s in _dbContext.Post.Include(p => p.Author)
                                   orderby s.DateTimePosted descending
                                   select s;

        var pageSize = _configuration.GetValue("PageSize", 4);
        Posts = await PaginatedList<Post>.CreateAsync(
            postsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        LoadTopNews();
    }
    private void LoadTopNews()
    {

        var client = new RestClient(_configuration.GetValue<string>("News_Api"));
        client.Options.MaxTimeout = -1;
        var request = new RestRequest();
        request.AddQueryParameter("api_token", _configuration.GetValue<string>("Api_Token"));
        request.AddQueryParameter("categories", _configuration.GetValue<string>("Categories"));
        request.AddQueryParameter("language", _configuration.GetValue<string>("Language"));
        request.AddQueryParameter("locale", _configuration.GetValue<string>("Locale"));
        request.AddQueryParameter("limit", _configuration.GetValue<string>("Limit"));
        RestResponse response = client.Execute(request);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            NewsFeed = JsonSerializer.Deserialize<NewsFeed>(response.Content);
        }
    }
}