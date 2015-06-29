using System.Collections.Generic;
using Mixter.Domain.Core.Messages;
using Mixter.Domain.Core.Subscriptions.Events;
using Mixter.Domain.Identity;

namespace Mixter.Domain.Core.Subscriptions
{
    public class Subscription
    {
        private readonly DecisionProjection _projection;

        public Subscription(IDomainEvent[] events)
        {
            _projection = new DecisionProjection();

            foreach (var evt in events)
            {
                _projection.Apply(evt);
            }
        }

        public static void FollowUser(IEventPublisher eventPublisher, UserId follower, UserId followee)
        {
            SubscriptionId subId = new SubscriptionId(follower, followee);
            eventPublisher.Publish(new UserFollowed(subId));
        }

        public void Unfollow(IEventPublisher eventPublisher)
        {
            eventPublisher.Publish(new UserUnfollowed(_projection.Id));
        }

        private class DecisionProjection : DecisionProjectionBase
        {
            public DecisionProjection()
            {
                AddHandler<UserFollowed>(setSubId);
            }

            public SubscriptionId Id { get; private set; }

            private void setSubId(UserFollowed evt)
            {
                Id = evt.SubscriptionId;
            }
        }
    }
}