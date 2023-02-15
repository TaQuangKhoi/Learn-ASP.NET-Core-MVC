namespace PartyInvites.Models;

public static class Repository
{
    private static List<GuestResponse> responses = new List<GuestResponse>();
    
    // Phương thức trả về danh sách phản hồi
    public static IEnumerable<GuestResponse> Responses => responses;
    
    // Phương thức lưu lại một đối tượng GuestResponse
    public static void AddResponse(GuestResponse response) => responses.Add(response);
}