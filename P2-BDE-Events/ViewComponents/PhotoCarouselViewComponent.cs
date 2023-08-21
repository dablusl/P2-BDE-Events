using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.ViewComponents
{
    public class PhotoCarouselViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // You can put logic to fetch photos and pass them to the view here
            return View();
        }
    }
}
