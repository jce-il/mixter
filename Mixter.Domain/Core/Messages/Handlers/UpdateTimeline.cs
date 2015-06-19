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
            TimelineMessageProjection projection = new TimelineMessageProjection(evt.Author, evt.Author, evt.Content, evt.Id);
            _timelineMessagesRepository.Save(projection);
        }

        public void Handle(ReplyMessagePublished evt)
        {
        }
    }
}
