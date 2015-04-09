package mixter.domain.core.message.events;

import mixter.Event;
import mixter.domain.core.message.MessageId;
import mixter.domain.identity.UserId;

public class MessagePublished implements Event {
    private final MessageId messageId;
    private final String message;
    private final UserId authorId;

    public MessagePublished(MessageId messageId, String message, UserId authorId) {
        this.messageId = messageId;
        this.message = message;
        this.authorId = authorId;
    }

    public MessageId getMessageId() {
        return messageId;
    }

    public String getMessage() {
        return message;
    }

    public UserId getAuthorId() {
        return authorId;
    }
}
