using System;

namespace Interfaces{
	public interface IDamageable
	{

		int HitPoints { get;}

		void Damage(int damage);

	}

}