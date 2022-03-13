

namespace communicator.Models;

public class FriendViewModel{
    public string Id{set;get;}
    public string Sender_UserId{set;get;}
    public string Profile_Name{set;get;}
    public string Profile_Id{set;get;}
    public string? Profile_Pic{set;get;}
    public DateOnly? RequestedAt{set;get;}
}

public class IndexFriendViewModel{
    public IEnumerable<FriendViewModel>? Pending{set;get;}
    public IEnumerable<FriendViewModel>? Accepted{set;get;}
}