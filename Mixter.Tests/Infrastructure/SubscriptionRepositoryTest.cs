﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mixter.Domain;
using Mixter.Domain.Subscriptions;
using Mixter.Domain.Subscriptions.Events;
using Mixter.Infrastructure;
using NFluent;

namespace Mixter.Tests.Infrastructure
{
    [TestClass]
    public class SubscriptionRepositoryTest
    {
        private static readonly UserId Followee1 = new UserId("followee1@mixit.fr");
        private static readonly UserId Followee2 = new UserId("followee2@mixit.fr");

        private static readonly UserId Follower1 = new UserId("follower1@mixit.fr");
        private static readonly UserId Follower2 = new UserId("follower2@mixit.fr");

        private EventsDatabase _database;
        private SubscriptionRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _database = new EventsDatabase();
            _repository = new SubscriptionRepository(_database);
        }

        [TestMethod]
        public void GivenSubscriptionEventsWhenGetFollowersThenReturnSubscription()
        {
            For(Followee1)
                .Add(Follower1)
                .Add(Follower2);

            var subscriptions = _repository.GetFollowers(Followee1);

            Check.That(subscriptions).HasSize(2);
        }

        [TestMethod]
        public void WhenGetFollowersOfUserThenReturnSubscriptionOfOnlyThisUser()
        {
            For(Followee1).Add(Follower1);
            For(Followee2).Add(Follower2);

            var subscriptions = _repository.GetFollowers(Followee1);

            Check.That(subscriptions.Select(o => o.GetId().Follower)).HasSize(1).And.Contains(Follower1);
        }

        private FolloweeFor For(UserId followee)
        {
            return new FolloweeFor(_database, followee);
        }

        private class FolloweeFor
        {
            private readonly EventsDatabase _database;
            private readonly UserId _followee;

            public FolloweeFor(EventsDatabase database, UserId followee)
            {
                _database = database;
                _followee = followee;
            }

            public FolloweeFor Add(UserId follower)
            {
                _database.Store(new UserFollowed(new SubscriptionId(follower, _followee)));

                return this;
            }
        }
    }
}
