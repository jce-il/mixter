using Mixter.Domain.Core.Messages.Events;
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
        }

        private void Save(UserId ownerId, UserId authorId, string content, MessageId messageId)
        {
            var projection = new TimelineMessageProjection(ownerId, authorId, content, messageId);
            _timelineMessagesRepository.Save(projection);
        }
    }
}
