using Mixter.Domain.Core.Messages.Events;

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
            _timelineMessagesRepository.Save(new TimelineMessageProjection(evt.Author,evt));
        }

        public void Handle(ReplyMessagePublished evt)
        {
            _timelineMessagesRepository.Save(new TimelineMessageProjection(evt.Replier, evt));
        }
    }
}
