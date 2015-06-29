﻿using Mixter.Domain.Core.Messages.Events;
using Mixter.Domain.Identity;

namespace Mixter.Domain.Core.Messages.Handlers
{
    public class UpdateTimeline : IEventHandler<MessagePublished>, IEventHandler<ReplyMessagePublished>
    {
        private readonly ITimelineMessagesRepository _timelineMessagesRepository;

        public UpdateTimeline(ITimelineMessagesRepository timelineMessagesRepository)
        {
            _timelineMessagesRepository = timelineMessagesRepository;
        }

        public void Handle(MessagePublished evt)
        {
            Identity.UserId ownerId = evt.Author;
            Identity.UserId authorId = evt.Author;
            string content = evt.Content;
            MessageId messageId = evt.Id;

            _timelineMessagesRepository.Save(new TimelineMessageProjection(ownerId, authorId, content, messageId));
        }

        public void Handle(ReplyMessagePublished evt)
        {
            UserId ownerId = evt.Replier;
            UserId authorId = evt.Replier;
            string content = evt.ReplyContent;
            MessageId messageId = evt.ReplyId;

            _timelineMessagesRepository.Save(new TimelineMessageProjection(ownerId, authorId, content, messageId));
        }
    }
}
