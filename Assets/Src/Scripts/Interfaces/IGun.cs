namespace Interfaces
{
	public interface IGun
	{
        float ShootCandece { get; }
		int Power { get; }

		void Shoot();

	}
}

