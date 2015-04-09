package mixter.domain.core.subscription;

import mixter.domain.identity.UserId;

import java.util.Set;

public interface FollowerRepository {
    Set<UserId> getFollowers(UserId authorId);
}
