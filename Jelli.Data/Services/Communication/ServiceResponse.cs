namespace Jelli.Data.Services.Communication
{
	public class ServiceResponse<T>
	{
		public bool Success { get; protected set; }
		public string Message { get; protected set; }

		public T ServiceObject { get; protected set; }

		public ServiceResponse(T serviceObject, bool success = true, string message = null)
		{
			Success = success;
			Message = message;
			ServiceObject = serviceObject;
		}
	}
}