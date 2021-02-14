using System;

namespace PhoneBook.Abstractions.Model
{
    public class RequestContext
    {
        public RequestContext(Guid requestId, string userId, Guid subscriptionId)
        {
            UserId = userId;
            RequestId = requestId;
            SubscriptionId = subscriptionId;
          
        }
        public Guid SubscriptionId { get; set; }
        public Guid RequestId { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string AccessToken { get; set; }

    }
}
