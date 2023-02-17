using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FSPBook.Data;
using FSPBook.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FSPBook.DAL.Repository;

namespace FSPBook.Pages;
public class CreateModel : PageModel
{
    private readonly IPostRepository _IPostRepository;
    private readonly IProfileRepository _IProfileRepository;

    [BindProperty]
    [Required(ErrorMessage = "Choose a person to post on behalf of")]
    [Range(1, 10000, ErrorMessage = "Choose a person to post on behalf of")]
    public int ProfileId { get; set; }
    [BindProperty]
    [Required(ErrorMessage = "Write a post")]
    [MinLength(1, ErrorMessage = "Post needs some content")]
    public string ContentInput { get; set; }
    public bool Success { get; set; }

    public List<Profile> Profiles { get; set; }

    public CreateModel(IPostRepository postRepository, IProfileRepository profileRepository)
    {
        _IPostRepository = postRepository;
        _IProfileRepository = profileRepository;
    }

    public void OnGetAsync()
    {
        LoadProfiles();
    }

    public void OnPostAsync()
    {
        if (ProfileId != -1)
        {
            _IPostRepository.SavePost(new Post
            {
                AuthorId = ProfileId,
                Content = ContentInput,
                DateTimePosted = DateTimeOffset.Now
            });
            _IPostRepository.Save();
            Success = true;
        }

        LoadProfiles();
    }

    private void LoadProfiles()
    {
        Profiles = _IProfileRepository.GetAllProfile().ToList();
    }
}
