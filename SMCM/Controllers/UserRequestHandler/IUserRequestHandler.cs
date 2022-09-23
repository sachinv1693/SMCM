using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SmartMeterConsumerManagement.Models.DBContext;

namespace SmartMeterConsumerManagement.Controllers.UserRequestHandler
{
    public interface IUserRequestHandler
    {
        bool HandleRequest(UserRequest request, ViewDataDictionary viewData);
    }
}
