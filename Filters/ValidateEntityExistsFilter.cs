using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Final.Services;

namespace Final.Filters
{
    public class ValidateEntityExistsFilter : IActionFilter
    {
        private readonly IEventRepository? _eventRepository;
        private readonly IItemRepository? _itemRepository;
        private readonly string _entityType;

        // entityType: "event" veya "item"
        public ValidateEntityExistsFilter(IEventRepository? eventRepository, IItemRepository? itemRepository, string entityType)
        {
            _eventRepository = eventRepository;
            _itemRepository = itemRepository;
            _entityType = entityType;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.TryGetValue("id", out var idObj) || idObj is not int id)
            {
                context.Result = new BadRequestResult();
                return;
            }

            bool exists = _entityType switch
            {
                "event" => _eventRepository?.Exists(id) ?? false,
                "item" => _itemRepository?.Exists(id) ?? false,
                _ => false
            };

            if (!exists)
            {
                context.Result = new NotFoundResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No-op
        }
    }
}