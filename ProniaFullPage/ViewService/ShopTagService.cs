using ProniaFullPage.Business.Abstract;
using ProniaFullPage.Core.Models;

namespace ProniaFullPage.ViewService;

public class ShopTagService
{
    private readonly ITagService _tagService;

    public ShopTagService(ITagService tagService)
    {
        _tagService = tagService;
    }
    public List<Tag> GetTags()
    {
        return _tagService.GetAllTags();
    }
}
