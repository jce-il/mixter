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

            foreach (IDomainEvent evt in events)
            {
                _projection.Apply(evt);
            }
        }

        private void PublishEvent<TEvent>(IEventPublisher eventPublisher, TEvent evt) where TEvent : IDomainEvent
        {
            eventPublisher.Publish(evt);
            _projection.Apply(evt);
        }

        public void Unfollow(IEventPublisher eventPublisher)
        {
            PublishEvent(eventPublisher,new UserUnfollowed(_projection.Id));
        }

        public static void FollowUser(IEventPublisher eventPublisher, UserId follower, UserId followee)
        {
            var userFollowed = new UserFollowed(new SubscriptionId(follower, followee));
            eventPublisher.Publish(userFollowed);
        }

        private class DecisionProjection : DecisionProjectionBase
        {
            public DecisionProjection()
            {
                AddHandler<UserFollowed>(SetSubscribtionId);
            }

            public SubscriptionId Id { get; private set; }

            private void SetSubscribtionId(UserFollowed evt)
            {
                Id = evt.SubscriptionId;
            }
        }

    }
}