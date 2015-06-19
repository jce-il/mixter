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
            Save(evt.Author, evt.Author, evt.Content, evt.Id);
        }

        public void Handle(ReplyMessagePublished evt)
        {
            Save(evt.Replier, evt.Replier, evt.ReplyContent, evt.ReplyId);
        }

        private void Save(UserId ownerId, UserId authorId, string content, MessageId messageId)
        {
            var projection = new TimelineMessageProjection(ownerId, authorId, content, messageId);
            _timelineMessagesRepository.Save(projection);
        }
    }
}
