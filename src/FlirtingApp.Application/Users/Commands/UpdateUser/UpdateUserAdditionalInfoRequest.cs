namespace FlirtingApp.Application.Users.Commands.UpdateUser
{
	public class UpdateUserAdditionalInfoRequest
	{
		public string KnownAs { get; set; } = default!;
		public string Introduction { get; set; } = default!;
		public string LookingFor { get; set; } = default!;
		public string Interests { get; set; } = default!;
		public string City { get; set; } = default!;
		public string Country { get; set; } = default!;
	}
}
