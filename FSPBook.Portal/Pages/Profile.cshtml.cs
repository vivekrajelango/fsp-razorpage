using FSPBook.DAL.Repository;
using FSPBook.Data;
using FSPBook.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FSPBook.Portal.Pages
{
    public class ProfileModel : PageModel
    {
        
        private readonly IPostRepository _IPostRepository;
        private readonly IProfileRepository _IProfileRepository;

        public Post RecentPost { get; set; }
        public Profile Profile { get; set; }
        public ProfileModel(IPostRepository postRepository,IProfileRepository profileRepository)
        {
            _IPostRepository = postRepository;
            _IProfileRepository = profileRepository;
        }       
        public IActionResult OnGetAsync(int id)
        {
            if (id == 0)
            {
                return Page();
            }

            Profile = _IProfileRepository.GetProfileById(id);

            if (Profile == null)
            {
                return Page();
            }
            RecentPost = _IPostRepository.GetAllPosts().OrderByDescending(o => o.DateTimePosted).FirstOrDefault(p => p.AuthorId == id);
            return Page();
        }
    }
}
