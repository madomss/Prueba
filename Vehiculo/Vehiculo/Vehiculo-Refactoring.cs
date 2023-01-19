namespace Vehiculo
{
	//CLASE ABSTRACTA VEHICULO
	public abstract class Vehiculo
	{
		protected int _numeroAsientos { get; set; }
		protected double _promedioVelocididad { get; set; }
		protected bool _poseeTurbo{ get; set; }

		public Vehiculo(int numeroAsientos, double promedioVelocididad, bool poseeTurbo)
		{
			_numeroAsientos = numeroAsientos;
			_promedioVelocididad = promedioVelocididad;
			_poseeTurbo = poseeTurbo;
		}

		protected const double VELOCIDAD_MINIMA = 5;

		public abstract double ObtenerVelocidad();

		public double ObtenerVelocidadBase()
		{
			return 12.0;
		}
	}

	//------------------------//
	//--VEHICULO PARTICULAR--//
	//----------------------//
	public class VehiculoParticular : Vehiculo
	{
		public VehiculoParticular(int numeroAsientos, double promedioVelocididad, bool poseeTurbo) : base(numeroAsientos, promedioVelocididad, poseeTurbo){}

		public override double ObtenerVelocidad()
		{
			return ObtenerVelocidadBase();
		}
	}

	//------------------------//
	//--VEHICULO CAMIONETA---//
	//----------------------//
	public class VehiculoCamioneta : Vehiculo
	{
		public VehiculoCamioneta(int numeroAsientos, double promedioVelocididad, bool poseeTurbo) : base(numeroAsientos, promedioVelocididad, poseeTurbo){}

		public override double ObtenerVelocidad()
		{
			return Math.Max(VELOCIDAD_MINIMA, ObtenerVelocidadCamioneta(_numeroAsientos));
		}

		private double ObtenerVelocidadCamioneta(int asientos)
		{
			return ObtenerVelocidadBase() - ObtenerVelocidadTurbo() * asientos;
		}

		private double ObtenerVelocidadTurbo()
		{
			return 9.0;
		}
	}

	//------------------------//
	//----VEHICULO CARGA-----//
	//----------------------//
	public class VehiculoCarga : Vehiculo
	{
		const double VELOCIDAD_BASE = 24.0;
		public VehiculoCarga(int numeroAsientos, double promedioVelocididad, bool poseeTurbo) : base(numeroAsientos, promedioVelocididad, poseeTurbo){}

		public override double ObtenerVelocidad()
		{
			return (!_poseeTurbo) ? VELOCIDAD_MINIMA : ObtenerVelocidadBase(_promedioVelocididad);
		}

		private double ObtenerVelocidadBase(double promedio)
		{
			return Math.Min(VELOCIDAD_BASE, promedio * ObtenerVelocidadBase());
		}
	}

	public class Program
	{
		static void Main(string[] args)
		{
			bool turbo = false;
			Console.Write("Ingrese el tipo de vehiculo: ");

			Console.Write("Ingrese número de asientos: ");
			int asientos = Convert.ToInt32(Console.ReadLine());
			Console.Write("Ingrese la velocidad: ");
			double velocidad = Convert.ToDouble(Console.ReadLine());
			Console.Write("Tiene turbo (y/n): ");
			string opcion = Console.ReadLine();

			switch (opcion)
			{
				case "y":
					turbo = true;
					break;
				case "n":
					turbo = false;
					break;
				default:
					Console.WriteLine("\nSe está tomando que el vehículo NO tiene turbo ya que no escribió ni 'y' ni 'n'\n");
					break;
			}

			VehiculoParticular particular = new VehiculoParticular(asientos, velocidad, turbo);
			Console.WriteLine("La velocidad de vehiculo particular es: " + particular.ObtenerVelocidad());

			VehiculoCamioneta camioneta = new VehiculoCamioneta(asientos, velocidad, turbo);
			Console.WriteLine("La velocidad del vehiculo camioneta es: " + camioneta.ObtenerVelocidad());

			VehiculoCarga carga = new VehiculoCarga(asientos, velocidad, turbo);
			Console.WriteLine("La velocidad del vehiculo carga es: " + carga.ObtenerVelocidad());
		}
	}
}