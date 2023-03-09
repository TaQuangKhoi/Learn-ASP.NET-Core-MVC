using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.Models;

namespace ShoppingOnline.Component;

[ViewComponent(Name = "NavigationMenuViewComponent")]
public class NavigationMenuViewComponent : ViewComponent
{
    private IProductInterface _repository;

    public NavigationMenuViewComponent(IProductInterface repository)
    {
        _repository = repository;
    }

    public IViewComponentResult Invoke()
    {
        return View("~/Views/Shared/Component/NavigationMenuViewComponent/Default.cshtml", _repository.GetAllProducts()
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x));
    }
}