using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Final.Services;

namespace Final.Filters
{
    public class ValidateItemExistsFilter : IActionFilter
    {
        private readonly IItemRepository _itemRepository;

        public ValidateItemExistsFilter(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.TryGetValue("id", out var idObj) || idObj is not int id)
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (!_itemRepository.Exists(id))
            {
                context.Result = new NotFoundResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}