namespace ProcessSystem
{
	public interface IWaitHandler
	{
		bool IsWaiting { get; }
		void Wait();
		void Close();
	}
}
